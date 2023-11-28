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

    //MonsterStateUpdate ���� ����
    Vector3 m_CacLenVec = Vector3.zero;
    Vector3 m_MoveDir = Vector3.zero;
    Quaternion m_TargetRot = Quaternion.identity;
    float m_WalkSpeed = 2.0f;
    float m_RotateSpeed = 10.0f;
    [HideInInspector]public float m_AttackDist;
    float m_GoBackDist = 10.0f;
    bool m_IsAttacking = false;

    //������ ���� ĳ���� ��ġ ȸ���� ����
    Vector3 currPos = Vector3.zero;
    Quaternion currRot = Quaternion.identity;

    //AnimationChange ���� ����
    string m_LastAnimState;

    //ReturnSpawnPos ���� ����
    public Vector3 m_SpawnPos = Vector3.zero; //�ʱ⽺�� ��ġ�� ������ ����
    Vector3 m_CacLenVec_RS = Vector3.zero;
    Vector3 m_MoveDir_RS = Vector3.zero;
    Quaternion m_TargetRot_RS = Quaternion.identity;
    float m_WalkSpeed_RS = 3.0f;
    float m_RotateSpeed_RS = 50.0f;
    [HideInInspector]public float m_ReturnDist;

    //TakeDamage ���� ����
    [HideInInspector] public float m_MaxHp;
    [HideInInspector] public float m_CurHp;
    public Image m_HpBar;
    [HideInInspector] public int m_Gold;
    [HideInInspector] public int m_Exp;
    [HideInInspector] public string m_SpawnArea;
    [HideInInspector] public int m_Att;
    [HideInInspector] public int m_Def;

    //���� ��Ȥ��ų ����
    [HideInInspector] public bool m_IsCharmed = false;
    float m_CharmTimer = 5.0f;

    //PhotonDestroyTimer ���� ����
    float m_DestroyTimer = 0.0f;

    void Awake()
    {
        m_MonSpawnMgr = GameObject.FindObjectOfType<MonSpawnMgr>();

        // ����ִ� �ڸ��� ã�� ��ġ���� �ֱ�
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
        // ����ִ� �ڸ��� ã�� ��ġ���� �ֱ�
    }

    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
        m_ObjectInfo = GetComponent<ObjectInfo>();
        m_PhotonView = GetComponent<PhotonView>();

        currPos = transform.position; //�۽� ���� ��ġ�� ����
        currRot = transform.rotation; //�۽� ���� ȸ���� ����
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

            if (m_CacLenVec.magnitude > m_AttackDist && m_CacLenVec.magnitude <= m_GoBackDist) //���Ϳ� ���ΰ� �� �Ÿ��� ���ݰŸ����� ���(&& m_Target != null)
            {
                if (m_IsAttacking == false) //�����߿��� �̵� �� ȸ�� �Ұ�()
                {
                    m_TargetRot = Quaternion.LookRotation(m_MoveDir);
                    transform.rotation = Quaternion.Slerp(transform.rotation, m_TargetRot, Time.deltaTime * m_RotateSpeed);

                    transform.position += m_MoveDir * Time.deltaTime * m_WalkSpeed;

                    AnimationChange("Walk");
                }
            }
            else if (m_CacLenVec.magnitude <= m_AttackDist) //���ΰ��� ���� ���ݰŸ� �ȿ� ������
            {
                AnimationChange("Attack");
                if (m_IsAttacking == false)
                    m_IsAttacking = true;
            }
            else if(m_CacLenVec.magnitude > m_GoBackDist) //10m �̻�־����� �������� ���ư�
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

        if (m_CurHp <= 0.0f) //��� ��
        {
            m_CurHp = 0.0f;

            GetComponent<ObjectInfo>().m_IsDie = true;

            //�������� ����
            BoxCollider a_Coll = GetComponent<BoxCollider>();
            Rigidbody a_RigidBody = GetComponent<Rigidbody>();
            if (a_Coll != null && a_RigidBody != null)
            {
                a_Coll.isTrigger = true;
                a_RigidBody.useGravity = false;
            }
            //�������� ����

            AnimationChange("Die");

            if (m_Target != null)
            {//�÷��̾� ���� ��ȯ
                m_Target.AnimationChange("Idle"); 
                m_Target.m_MoveMethod = MoveMethod.Stop;
            }

            //Enemy��ư ����
            EnemyBtn[] a_Btns = m_Target.m_EnemeyPanel.GetComponentsInChildren<EnemyBtn>();
            for(int ii = 0; ii < a_Btns.Length; ii++)
            {
                if (a_Btns[ii].m_Enemy == this.gameObject)
                    Destroy(a_Btns[ii].gameObject);
            }
            //Enemy��ư ����

            //����
            if (GameMgr.Inst.m_Player.m_PhotonView.IsMine)
            {
                GameMgr.Inst.SpawnText(transform.position, 0, m_Gold, m_Exp, 0, 1);
                GlobalValue.m_Gold += m_Gold;
                GlobalValue.m_CurExp += m_Exp;
                GameMgr.Inst.UIRefresh();
                ItemPanel.Inst.GainItem(this);
                GameMgr.Inst.m_ExpMgr.LevelUp(m_Exp);
            }
            //����

            PlayfabMgr.inst.SetPlayerData("Gold", GlobalValue.m_Gold.ToString());

            //�罺�� �غ�
            if (PhotonNetwork.IsMasterClient) //������ ���� �������ֱ�
            {
                transform.SetParent(m_MonSpawnMgr.m_MonsterDieZone, true); //������ �罺�� �ڸ� �����ֱ�

                if (gameObject.name.Contains("Slime") || gameObject.name.Contains("Turtle"))
                {
                    StartCoroutine(m_MonSpawnMgr.SATSpawn_Cor(5.0f));
                }
                else if (gameObject.name.Contains("Golem") || gameObject.name.Contains("Orc"))
                {
                    StartCoroutine(m_MonSpawnMgr.GAOSpawn_Cor(5.0f));
                }
            }
            //�罺�� �غ�
        }
        else
        {
            
        }

        m_HpBar.fillAmount = m_CurHp / m_MaxHp;
    }

    void Die() //Die�ִϸ��̼� ��� �� ȣ�� �Ǵ� �̺�Ʈ �Լ�
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
    {//������ ����� �ִϸ��̼��� �̹������ӿ� ������ �ִϸ��̼ǰ� �ٸ���
        if (m_ObjectInfo.m_IsStun == true && a_Anim != "Die") //���ϻ��¿����� Die�� ������ �ٸ� �ִϸ��̼� ��� �Ұ���
            return;

        if (m_LastAnimState != a_Anim)
        {
            if (m_LastAnimState != null)
                m_Animator.ResetTrigger(m_LastAnimState);
            m_Animator.SetTrigger(a_Anim);
        }

        m_LastAnimState = a_Anim; //������ ����ִϸ��̼� ����
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
