using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossCtrl : MonoBehaviour, IPunObservable
{
    [HideInInspector] public PhotonView m_PhotonView;
    NavMeshAgent m_NavMeshAgent;
    Animator m_Animator;
    public HeroCtrl m_Target = null;
    ObjectInfo m_ObjectInfo;
    public Image m_Hpbar;

    //Wandering관련 변수
    Vector3 m_SpawnPos;
    Vector3 m_TargetPos;
    RaycastHit m_HitInfo;
    float m_TargetDist;
    float m_MoveTime = 0.0f;
    bool m_WanderingStart = true;

    //AnimationChange 관련 변수
    string m_LastAnimState;

    //TakeDamage 관련 변수
    float m_MaxHp = 10000.0f;
    float m_CurHp = 10000.0f;
    [HideInInspector]public float m_Def = 11.0f;

    //TargetPlayerUpdate 관련 변수
    Vector3 m_TargetPPos = Vector3.zero; //타겟플리어 위치
    Vector3 m_BossPos = Vector3.zero;    //보스위치
    Vector3 m_TargetCacVec = Vector3.zero; //보스와의 거리,방향 계산용 변수
    Vector3 m_TargetDir = Vector3.zero; //타겟을 바라보는 방향벡터(단위)
    Vector3 m_SpawnCacVec = Vector3.zero; //스폰과의 거리,방향 계산용 변수
    Vector3 m_SpawnDir = Vector3.zero;    //스폰위치를 바라보는 방향벡터(단위)
    Quaternion m_TargetRot = Quaternion.identity;
    float m_TargetPDist; //타겟플레이어와의 거리
    float m_AttDist = 3.5f; //보스의 공격 사거리
    float m_AgroDist = 12.0f; //보스의 어그로 사거리
    float m_RotSpeed = 10.0f; //보스 회전속도
    float m_MoveSpeed = 2.5f; //보스 이동속도
    float m_SpawnPosDist; //스폰위치와의 거리
    float m_MaxMoveDist = 60.0f; //보스가 초기스폰위치로부터 움직일 수 있는 거리
    bool m_IsReturning = false;
    float m_ReturnSpeed = 7.0f;

    //AttackEndEvent 관련 변수
    float m_AttDelayTimer;
    int m_AttCount = 0;
    int m_RndPatternIdx = 0;
    int rndPatterIdx = 0;
    bool m_IsAttacking = false;

    //Patter1 관련 변수
    public GameObject m_MagicBoardPrefab;

    //Pattern2 관련 변수
    public GameObject m_MagicBoardPrefab2;
    public GameObject m_ElectronicBall;

    //PhotonView 관련 변수
    Vector3 currPos;
    Quaternion currRot;
    string animState;
    string lastAnim;

    void Awake()
    {
        m_ObjectInfo = GetComponent<ObjectInfo>();
        m_ObjectInfo.InitObject(ObjectType.Monster, "슬레이어");
    }

    // Start is called before the first frame update
    void Start()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        m_PhotonView = GetComponent<PhotonView>();

        m_SpawnPos = transform.position;

        if (m_PhotonView.IsMine == true)
        {
            StartCoroutine(Wandering());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Target != null) //전투,추격,Returning
        {
            BossStateUpdate();

            if (m_NavMeshAgent.destination != null)//Wandering중이였으면 중지
                m_NavMeshAgent.ResetPath();

            //보스 기본 공격 딜레이 타이머
            if(m_AttDelayTimer > 0.0f)
            {
                m_AttDelayTimer -= Time.deltaTime;
                if(m_AttDelayTimer < 0.0f)
                {
                    AnimationChange("Attack");
                    m_AttDelayTimer = 4.0f;
                }
            }
            //보스 기본 공격 딜레이 타이머
        }
        else //Wandering
        {
            if (m_PhotonView.IsMine == true) //PhotonNetwork.InstantiateRoomObject로 생성했기 때문에 Room에서 제어
            {
                WanderingAnimationUpdate();

                StartCoroutine(Wandering());
            }
            else //Room안에 유저들 입장에서
            {
                if (10.0f < (transform.position - currPos).magnitude)
                {   //중계 받은 좌표와 현재 좌표의 거리차가 10m 이상이면 즉시 보정
                    transform.position = currPos;
                }
                else
                {
                    //원격 플레이어의 위치를 수신받은 위치까지 부드럽게 이동시킴
                    transform.position = Vector3.Lerp(transform.position,
                                                    currPos, Time.deltaTime * 10.0f);
                }

                //원격 플레이어의 위치를 수신받은 각도만큼 부드럽게 회전시킴
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                currRot, Time.deltaTime * 10.0f);

                NotMineAnimUpdate(animState);
            }
        }
    }

    public IEnumerator Wandering()
    {
        while (m_WanderingStart == true)
        {
            float a_RanX = m_SpawnPos.x + Random.Range(-20, 20);
            float a_RanZ = m_SpawnPos.z + Random.Range(-20, 20);

            m_TargetPos = new Vector3(a_RanX, 100, a_RanZ);

            if (Physics.Raycast(m_TargetPos, Vector3.down, out m_HitInfo, Mathf.Infinity))
            {
                if (m_HitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Terrain"))
                {
                    m_NavMeshAgent.SetDestination(m_HitInfo.point);                  //목적지 설정
                    m_TargetDist = (transform.position - m_HitInfo.point).magnitude; //거리
                    m_MoveTime = m_TargetDist / m_NavMeshAgent.speed;                //시간
                }
            }

            m_WanderingStart = false;
            yield return new WaitForSeconds(m_MoveTime + 10.0f); //도착후 10초후에 다시 Wandering
            m_WanderingStart = true;
        }
    }

    [PunRPC]
    public void TakeDamage(float a_Dmg, string a_PlayerName)
    {
        if (m_CurHp < 0.0f)
            return;

        HeroCtrl[] a_Heroes = GameObject.FindObjectsOfType<HeroCtrl>();
        foreach (HeroCtrl a_Hero in a_Heroes)
        {
            if (a_Hero.m_PhotonView.Owner.NickName == a_PlayerName)
            {
                m_Target = a_Hero;
                break;
            }
        }

        m_CurHp -= a_Dmg;

        GameMgr.Inst.SpawnText(transform.position, (int)a_Dmg, 0, 0, 0, 0);

        if (m_CurHp <= 0.0f)
        {
            m_CurHp = 0.0f;

            if (m_Target != null)
            {//플레이어 상태 전환
                m_Target.AnimationChange("Idle");
                m_Target.m_MoveMethod = MoveMethod.Stop;
            }

            //Enemy버튼 삭제
            EnemyBtn[] a_Btns = m_Target.m_EnemeyPanel.GetComponentsInChildren<EnemyBtn>();
            for (int ii = 0; ii < a_Btns.Length; ii++)
            {
                if (a_Btns[ii].m_Enemy == this.gameObject)
                    Destroy(a_Btns[ii].gameObject);
            }
            //Enemy버튼 삭제

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

            Destroy(gameObject, 1.5f);
        }
        else
        {

        }

        m_Hpbar.fillAmount = m_CurHp / m_MaxHp;
    }

    void BossStateUpdate()
    {
        if (m_Target == null)
            return;

        //이동 및 회전 관련
        m_TargetPPos = m_Target.transform.position;
        m_BossPos = transform.position;
        m_TargetCacVec = new Vector3(m_TargetPPos.x, 0, m_TargetPPos.z) - new Vector3(m_BossPos.x, 0, m_BossPos.z);
        m_TargetDir = m_TargetCacVec.normalized;  //타겟 방향
        m_TargetPDist = m_TargetCacVec.magnitude; //타겟 거리
        //이동 및 회전 관련

        //return 관련
        m_SpawnCacVec = new Vector3(m_SpawnPos.x, 0, m_SpawnPos.z) - new Vector3(m_BossPos.x, 0, m_BossPos.z);
        m_SpawnDir = m_SpawnCacVec.normalized;    //스폰 방향
        m_SpawnPosDist = m_SpawnCacVec.magnitude; //스폰위치 거리

        if (m_SpawnPosDist > m_MaxMoveDist) //초기스폰위치로부터 60m이상 밖으로 나가면
        {
            m_IsReturning = true;
        }
        else if(m_SpawnPosDist < 1.0f) //초기스폰위치 도착하면
        {
            m_IsReturning = false;
        }
        //return 관련

        if (m_IsReturning == true)
        {//초기스폰위치와 거리가 멀어져 되돌아가는 상태 일때
            m_TargetRot = Quaternion.LookRotation(m_SpawnDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, m_TargetRot, Time.deltaTime * m_RotSpeed);

            transform.position += m_SpawnDir * Time.deltaTime * m_ReturnSpeed;
            AnimationChange("Move");

            if (m_SpawnPosDist < 1.1f)
                AnimationChange("Idle");
        }
        else
        {
            if (m_TargetPDist > m_AgroDist) // 타겟과의거리가 어그로 사거리보다 멀어지면(Wandering상태로 돌려줌)
            {
                AnimationChange("Idle");
                m_WanderingStart = true;
                m_Target = null;
            }
            else if (m_AttDist < m_TargetPDist && m_TargetPDist <= m_AgroDist) //타겟과의 거리가 어그로 사거리 안에있고 공격 사거리보다 멀면(타겟을 쫓아가는 상태)
            {
                if (m_IsAttacking == false)
                {
                    m_TargetRot = Quaternion.LookRotation(m_TargetDir);
                    transform.rotation = Quaternion.Slerp(transform.rotation, m_TargetRot, Time.deltaTime * m_RotSpeed);

                    transform.position += m_TargetDir * Time.deltaTime * m_MoveSpeed;
                    AnimationChange("Move");
                }
            }
            else if (m_TargetPDist <= m_AttDist) //타겟과의 거리가 공격사거리 내에 있으면(타겟을 공격중인 상태)
            {
                if (m_TargetPDist > m_AttDist - 1.0f) // 공격사거리보다 1m더 가까이와서 때리도록 설정
                {
                    if (m_IsAttacking == false)
                    {
                        m_TargetRot = Quaternion.LookRotation(m_TargetDir);
                        transform.rotation = Quaternion.Slerp(transform.rotation, m_TargetRot, Time.deltaTime * m_RotSpeed);

                        transform.position += m_TargetDir * Time.deltaTime * m_MoveSpeed;
                    }

                    if (m_IsAttacking == false && m_AttDelayTimer <= 0.0f)
                        m_IsAttacking = true;
                }

                m_TargetRot = Quaternion.LookRotation(m_TargetDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, m_TargetRot, Time.deltaTime * m_RotSpeed);

                if (m_AttDelayTimer <= 0.0f)
                    AnimationChange("Attack");
            }
        }
    }

    void WanderingAnimationUpdate()
    {
        if (m_Target != null) //타겟이 있으면 WanderingX
            return;

        if (m_NavMeshAgent.remainingDistance <= 0.1f) //목적지 도착 시
        {
            AnimationChange("Idle");
        }
        else //이동중
        {
            AnimationChange("Move");
        }
    }

    void AnimationChange(string a_Anim = "Idle")
    {//마지막 재생된 애니메이션이 이번프레임에 진행할 애니메이션과 다르면
        if (m_LastAnimState != a_Anim)
        {
            if (m_LastAnimState != null)
                m_Animator.ResetTrigger(m_LastAnimState);
            m_Animator.SetTrigger(a_Anim);
        }

        m_LastAnimState = a_Anim; //마지막 재생애니메이션 저장
    }

    public void NotMineAnimUpdate(string a_Anim)
    {
        if (lastAnim != a_Anim)
            GetComponent<Animator>().SetTrigger(a_Anim);

        lastAnim = a_Anim; //마지막 재생애니메이션 저장
    }

    public void AttackEndEvent() //기본공격후 딜레이를 주고 3번째 기본공격마다 랜덤패턴을 발생시키기 위한 이벤트 함수
    {
        if (m_LastAnimState == "Attack" && m_AttDelayTimer > 0.0f)
        {
            AnimationChange("Idle");
        }

        float a_Dmg = (40 - GlobalValue.m_Def / 2) / 2;
        if (a_Dmg < 0)
            a_Dmg = 0;

        if (m_Target != null)
            m_Target.m_PhotonView.RPC("TakeDamage", Photon.Pun.RpcTarget.AllBuffered, a_Dmg);

        m_AttDelayTimer = 4.0f;
        m_AttCount += 1;

        if(m_PhotonView.IsMine)
        {
            if (m_AttCount == 2)
            {// 3번째 랜덤패턴에 어떤패턴나올지 미리결정
                m_RndPatternIdx = Random.Range(1, 3);
            }


            if (m_AttCount == 3)
            {
                if (m_RndPatternIdx == 1)  //패턴1
                {
                    Pattern1();
                }
                else if (m_RndPatternIdx == 2) //패턴2
                {
                    Pattern2();
                }

                m_AttCount = 0;
            }
        }
        else
        {//OnPhotonSrealizeView를 통해 어떤패턴이 나올지 넘겨받는다
            if (m_AttCount == 3)
            {
                if (rndPatterIdx == 1)  //패턴1
                {
                    Pattern1();
                }
                else if (rndPatterIdx == 2) //패턴2
                {
                    Pattern2();
                }

                m_AttCount = 0;
            }
        }

        m_IsAttacking = false;
    }

    void Pattern1()
    {
        GameObject[] a_Heroes = GameObject.FindGameObjectsWithTag("Hero");

        for (int ii = 0; ii < a_Heroes.Length; ii++)
        {
            Vector3 a_HeroPos = a_Heroes[ii].transform.position;
            GameObject a_MagicBoard = Instantiate(m_MagicBoardPrefab) as GameObject;
            a_MagicBoard.transform.position = new Vector3(a_HeroPos.x, a_HeroPos.y + 0.1f, a_HeroPos.z);
        }
    }

    void Pattern2()
    {
        if (m_Target == null)
            return;

        AnimationChange("Skill2");
    }

    void Pattern2_Event()
    {
        GameObject a_MagicBoardObj = Instantiate(m_MagicBoardPrefab2);
        a_MagicBoardObj.transform.position = new Vector3(m_TargetPPos.x, m_TargetPPos.y + 0.1f, m_TargetPPos.z);
        GameObject a_ElecBall = Instantiate(m_ElectronicBall);
        a_ElecBall.transform.position = new Vector3(m_TargetPPos.x, m_TargetPPos.y + 1.4f, m_TargetPPos.z);

        Collider[] colls = Physics.OverlapSphere(m_TargetPPos, 10.0f);

        foreach(Collider coll in colls)
        {
            HeroCtrl a_Hero = coll.GetComponent<HeroCtrl>();
            if (a_Hero == null)
                continue;

            Vector3 a_HeroPos = a_Hero.transform.position;
            Vector3 a_BossPos = transform.position;
            Vector3 a_CacVec = new Vector3(a_HeroPos.x ,0,a_HeroPos.z) - new Vector3(a_BossPos.x , 0, a_BossPos.z);
            float a_Dist = a_CacVec.magnitude;

            float a_Dmg;
            if (a_Dist < 1.0f)
                a_Dmg = (100 - GlobalValue.m_Def / 2) / 2;
            else if (a_Dist < 5.0f && a_Dist >= 1.0f)
                a_Dmg = (80 - GlobalValue.m_Def / 2) / 2;
            else
                a_Dmg = (60 - GlobalValue.m_Def / 2) / 2;

            if (a_Dmg < 0)
                a_Dmg = 0;

            a_Hero.m_PhotonView.RPC("TakeDamage",Photon.Pun.RpcTarget.AllBuffered,a_Dmg);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(m_LastAnimState);
            stream.SendNext(m_RndPatternIdx);
        }
        else
        {
            currPos = (Vector3)stream.ReceiveNext();
            currRot = (Quaternion)stream.ReceiveNext();
            animState = (string)stream.ReceiveNext();
            rndPatterIdx = (int)stream.ReceiveNext();
        }
    }
}
