using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterCtrl : MonoBehaviour, IPunObservable
{
    Rigidbody m_RigidBody;
    Collider[] m_Enemies = null;
    HeroCtrl m_Target = null;
    Animator m_Animator;
    ObjectInfo m_ObjectInfo;
    MonSpawnMgr m_MonSpawnMgr;
    public PhotonView m_PhotonView;

    //MonsterStateUpdate 관련 변수
    Vector3 m_CacLenVec = Vector3.zero;
    Vector3 m_MoveDir = Vector3.zero;
    Quaternion m_TargetRot = Quaternion.identity;
    float m_WalkSpeed = 2.0f;
    float m_RotateSpeed = 10.0f;
    [HideInInspector]public float m_AttackDist;
    float m_GoBackDist = 10.0f;
    bool m_IsAttacking = false;

    //포톤을 위한 캐릭터 위치 회전값 변수
    Vector3 currPos = Vector3.zero;
    Quaternion currRot = Quaternion.identity;

    //AnimationChange 관련 변수
    string m_LastAnimState;

    //ReturnSpawnPos 관련 변수
    public Vector3 m_SpawnPos = Vector3.zero; //초기스폰 위치를 저장할 변수
    Vector3 m_CacLenVec_RS = Vector3.zero;
    Vector3 m_MoveDir_RS = Vector3.zero;
    Quaternion m_TargetRot_RS = Quaternion.identity;
    float m_WalkSpeed_RS = 3.0f;
    float m_RotateSpeed_RS = 50.0f;
    [HideInInspector]public float m_ReturnDist;

    //TakeDamage 관련 변수
    [HideInInspector] public float m_MaxHp;
    [HideInInspector] public float m_CurHp;
    public Image m_HpBar;
    [HideInInspector] public int m_Gold;
    [HideInInspector] public int m_Exp;
    [HideInInspector] public string m_SpawnArea;
    [HideInInspector] public int m_Att;
    [HideInInspector] public int m_Def;

    //힐러 매혹스킬 관련
    [HideInInspector] public bool m_IsCharmed = false;
    float m_CharmTimer = 5.0f;

    //PhotonDestroyTimer 관련 변수
    float m_DestroyTimer = 0.0f;

    void Awake()
    {
        m_MonSpawnMgr = GameObject.FindObjectOfType<MonSpawnMgr>();

        // 비어있는 자리를 찾아 위치시켜 주기
        if (gameObject.name.Contains("Slime") || gameObject.name.Contains("Turtle"))
            for (int ii = 0; ii < m_MonSpawnMgr.m_SATSpawnPos.Length; ii++)
            {
                if (m_MonSpawnMgr.m_SATSpawnPos[ii].childCount < 1)
                {
                    transform.SetParent(m_MonSpawnMgr.m_SATSpawnPos[ii], false);
                    m_SpawnPos = transform.position;
                    break;
                }
            }
        else if(gameObject.name.Contains("Orc") || gameObject.name.Contains("Golem"))
            for (int ii = 0; ii < m_MonSpawnMgr.m_GAOSpawnPos.Length; ii++)
            {
                if (m_MonSpawnMgr.m_GAOSpawnPos[ii].childCount < 1)
                {
                    transform.SetParent(m_MonSpawnMgr.m_GAOSpawnPos[ii], false);
                    m_SpawnPos = transform.position;
                    break;
                }
            }
        // 비어있는 자리를 찾아 위치시켜 주기
    }

    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
        m_ObjectInfo = GetComponent<ObjectInfo>();
        m_PhotonView = GetComponent<PhotonView>();

        currPos = transform.position; //송신 해줄 위치값 정보
        currRot = transform.rotation; //송신 해줄 회전값 정보
    }

    // Update is called once per frame
    void Update()
    {
        EnemySensor();

        MonsterStateUpdate();

        if(m_IsCharmed == true)
        {
            m_CharmTimer -= Time.deltaTime;
            if(m_CharmTimer < 0.0f)
            {
                m_Def = m_Def * 2;
                m_CharmTimer = 5.0f;
                m_IsCharmed = false;
            }
        }

        PhotonDestroyTimer();
    }

    bool HasTarget()
    {
        if (m_Target != null && m_CurHp > 0.0f)
            return true;
        else
            return false;
    }

    void EnemySensor()
    {
        if (HasTarget() == false)
        {
            m_Enemies = Physics.OverlapSphere(transform.position, 6.0f);

            foreach (Collider coll in m_Enemies)
            {
                m_Target = coll.GetComponent<HeroCtrl>();

                if (m_Target != null)
                    return;
            }
        }
    }

    void MonsterStateUpdate()
    {
        if (m_CurHp <= 0.0f)
            return;

        if (m_ObjectInfo.m_IsStun == true)
            return;

        m_CacLenVec_RS = m_SpawnPos - transform.position;

        m_MoveDir_RS = m_CacLenVec_RS.normalized;
        if (m_Target == null && m_CacLenVec_RS.magnitude > m_ReturnDist)
        {
            m_TargetRot_RS = Quaternion.LookRotation(m_MoveDir_RS);
            transform.rotation = Quaternion.Slerp(transform.rotation, m_TargetRot_RS, Time.deltaTime * m_RotateSpeed_RS);

            transform.position += m_MoveDir_RS * Time.deltaTime * m_WalkSpeed_RS;
            AnimationChange("Run");
        }
        else if(m_Target == null && m_CacLenVec_RS.magnitude <= m_ReturnDist)
        {
            AnimationChange("Idle");
        }
        else if(m_Target != null)
        {
            m_CacLenVec = m_Target.transform.position - transform.position;

            m_MoveDir = m_CacLenVec.normalized;

            if (m_CacLenVec.magnitude > m_AttackDist && m_CacLenVec.magnitude <= m_GoBackDist) //몬스터와 주인공 간 거리가 공격거리보다 길면(&& m_Target != null)
            {
                if (m_IsAttacking == false) //공격중에는 이동 및 회전 불가()
                {
                    m_TargetRot = Quaternion.LookRotation(m_MoveDir);
                    transform.rotation = Quaternion.Slerp(transform.rotation, m_TargetRot, Time.deltaTime * m_RotateSpeed);

                    transform.position += m_MoveDir * Time.deltaTime * m_WalkSpeed;

                    AnimationChange("Walk");
                }
            }
            else if (m_CacLenVec.magnitude <= m_AttackDist) //주인공이 몬스터 공격거리 안에 들어오면
            {
                AnimationChange("Attack");
                if (m_IsAttacking == false)
                    m_IsAttacking = true;
            }
            else if(m_CacLenVec.magnitude > m_GoBackDist) //10m 이상멀어지면 스폰지로 돌아감
            {
                m_Target = null;
            }
        }
    }

    [PunRPC]
    public void TakeDamage(float a_Damage)
    {
        if (m_HpBar == null)
            return;

        m_CurHp -= a_Damage;
        if (m_CurHp <= 0.0f)
            a_Damage = 0.0f;

        GameMgr.Inst.SpawnText(transform.position,(int)a_Damage,0,0,0,0);

        if (m_CurHp <= 0.0f) //사망 시
        {
            m_CurHp = 0.0f;

            GetComponent<ObjectInfo>().m_IsDie = true;

            //물리엔진 끄기
            BoxCollider a_Coll = GetComponent<BoxCollider>();
            Rigidbody a_RigidBody = GetComponent<Rigidbody>();
            if (a_Coll != null && a_RigidBody != null)
            {
                a_Coll.isTrigger = true;
                a_RigidBody.useGravity = false;
            }
            //물리엔진 끄기

            AnimationChange("Die");

            if (m_Target != null)
            {//플레이어 상태 전환
                m_Target.AnimationChange("Idle"); 
                m_Target.m_MoveMethod = MoveMethod.Stop;
            }

            //Enemy버튼 삭제
            EnemyBtn[] a_Btns = m_Target.m_EnemeyPanel.GetComponentsInChildren<EnemyBtn>();
            for(int ii = 0; ii < a_Btns.Length; ii++)
            {
                if (a_Btns[ii].m_Enemy == this.gameObject)
                    Destroy(a_Btns[ii].gameObject);
            }
            //Enemy버튼 삭제

            //보상
            if (GameMgr.Inst.m_Player.m_PhotonView.IsMine)
            {
                GameMgr.Inst.SpawnText(transform.position, 0, m_Gold, m_Exp, 0, 1);
                GlobalValue.m_Gold += m_Gold;
                GlobalValue.m_CurExp += m_Exp;
                GameMgr.Inst.UIRefresh();
                ItemPanel.Inst.GainItem(this);
                GameMgr.Inst.m_ExpMgr.LevelUp(m_Exp);
            }
            //보상

            PlayfabMgr.inst.SetPlayerData("Gold", GlobalValue.m_Gold.ToString());

            //재스폰 준비
            if (PhotonNetwork.IsMasterClient) //방장이 몬스터 스폰해주기
            {
                transform.SetParent(m_MonSpawnMgr.m_MonsterDieZone, true); //사망즉시 재스폰 자리 비켜주기

                if (gameObject.name.Contains("Slime") || gameObject.name.Contains("Turtle"))
                {
                    StartCoroutine(m_MonSpawnMgr.SATSpawn_Cor(5.0f));
                }
                else if (gameObject.name.Contains("Golem") || gameObject.name.Contains("Orc"))
                {
                    StartCoroutine(m_MonSpawnMgr.GAOSpawn_Cor(5.0f));
                }
            }
            //재스폰 준비
        }
        else
        {
            
        }

        m_HpBar.fillAmount = m_CurHp / m_MaxHp;
    }

    void Die() //Die애니메이션 재생 시 호출 되는 이벤트 함수
    {
        if (m_CurHp > 0.0f)
            return;

        transform.position = m_MonSpawnMgr.m_MonsterDieZone.position;

        m_DestroyTimer = 5.0f;
    }

    void PhotonDestroyTimer()
    {
        if (m_DestroyTimer > 0.0f)
        {
            m_DestroyTimer -= Time.deltaTime;
            if (m_DestroyTimer < 0.0f)
            {
                m_PhotonView.RPC("PhotonDestroy", RpcTarget.MasterClient);
                m_DestroyTimer = 0.0f;
            }
        }
    }

    [PunRPC]
    void PhotonDestroy()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.Destroy(this.gameObject);
    }

    public void AnimationChange(string a_Anim = "Idle")
    {//마지막 재생된 애니메이션이 이번프레임에 진행할 애니메이션과 다르면
        if (m_ObjectInfo.m_IsStun == true && a_Anim != "Die") //스턴상태에서는 Die를 제외한 다른 애니메이션 재생 불가능
            return;

        if (m_LastAnimState != a_Anim)
        {
            if (m_LastAnimState != null)
                m_Animator.ResetTrigger(m_LastAnimState);
            m_Animator.SetTrigger(a_Anim);
        }

        m_LastAnimState = a_Anim; //마지막 재생애니메이션 저장
    }

    void Attack()
    {
        if (m_Target == null)
            return;

        int a_Dmg = (m_Att - GlobalValue.m_Def) /2;
        if (a_Dmg < 0)
            a_Dmg = 0;

        m_Target.TakeDamage(a_Dmg);
    }

    void AttackEnd()
    {
        m_IsAttacking = false;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            currPos = (Vector3)stream.ReceiveNext();
            currRot = (Quaternion)stream.ReceiveNext();
        }
    }
    //private void OnCollisionEnter(Collision coll)
    //{
    //    if(coll.gameObject.tag == "Monster")
    //    {
    //        m_RigidBody.constraints = RigidbodyConstraints.FreezePositionY |
    //                                  RigidbodyConstraints.FreezeRotationX |
    //                                  RigidbodyConstraints.FreezeRotationZ;
    //    }
    //}

    //private void OnCollisionExit(Collision coll)
    //{
    //    if (coll.gameObject.tag == "Monster")
    //    {
    //        m_RigidBody.constraints = RigidbodyConstraints.FreezeRotationX |
    //                                  RigidbodyConstraints.FreezeRotationZ;
    //    }
    //}
}
