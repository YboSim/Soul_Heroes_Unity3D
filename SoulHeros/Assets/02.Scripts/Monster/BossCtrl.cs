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

    //Wandering���� ����
    Vector3 m_SpawnPos;
    Vector3 m_TargetPos;
    RaycastHit m_HitInfo;
    float m_TargetDist;
    float m_MoveTime = 0.0f;
    bool m_WanderingStart = true;

    //AnimationChange ���� ����
    string m_LastAnimState;

    //TakeDamage ���� ����
    float m_MaxHp = 10000.0f;
    float m_CurHp = 10000.0f;
    [HideInInspector]public float m_Def = 11.0f;

    //TargetPlayerUpdate ���� ����
    Vector3 m_TargetPPos = Vector3.zero; //Ÿ���ø��� ��ġ
    Vector3 m_BossPos = Vector3.zero;    //������ġ
    Vector3 m_TargetCacVec = Vector3.zero; //�������� �Ÿ�,���� ���� ����
    Vector3 m_TargetDir = Vector3.zero; //Ÿ���� �ٶ󺸴� ���⺤��(����)
    Vector3 m_SpawnCacVec = Vector3.zero; //�������� �Ÿ�,���� ���� ����
    Vector3 m_SpawnDir = Vector3.zero;    //������ġ�� �ٶ󺸴� ���⺤��(����)
    Quaternion m_TargetRot = Quaternion.identity;
    float m_TargetPDist; //Ÿ���÷��̾���� �Ÿ�
    float m_AttDist = 3.5f; //������ ���� ��Ÿ�
    float m_AgroDist = 12.0f; //������ ��׷� ��Ÿ�
    float m_RotSpeed = 10.0f; //���� ȸ���ӵ�
    float m_MoveSpeed = 2.5f; //���� �̵��ӵ�
    float m_SpawnPosDist; //������ġ���� �Ÿ�
    float m_MaxMoveDist = 60.0f; //������ �ʱ⽺����ġ�κ��� ������ �� �ִ� �Ÿ�
    bool m_IsReturning = false;
    float m_ReturnSpeed = 7.0f;

    //AttackEndEvent ���� ����
    float m_AttDelayTimer;
    int m_AttCount = 0;
    int m_RndPatternIdx = 0;
    int rndPatterIdx = 0;
    bool m_IsAttacking = false;

    //Patter1 ���� ����
    public GameObject m_MagicBoardPrefab;

    //Pattern2 ���� ����
    public GameObject m_MagicBoardPrefab2;
    public GameObject m_ElectronicBall;

    //PhotonView ���� ����
    Vector3 currPos;
    Quaternion currRot;
    string animState;
    string lastAnim;

    void Awake()
    {
        m_ObjectInfo = GetComponent<ObjectInfo>();
        m_ObjectInfo.InitObject(ObjectType.Monster, "�����̾�");
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
        if (m_Target != null) //����,�߰�,Returning
        {
            BossStateUpdate();

            if (m_NavMeshAgent.destination != null)//Wandering���̿����� ����
                m_NavMeshAgent.ResetPath();

            //���� �⺻ ���� ������ Ÿ�̸�
            if(m_AttDelayTimer > 0.0f)
            {
                m_AttDelayTimer -= Time.deltaTime;
                if(m_AttDelayTimer < 0.0f)
                {
                    AnimationChange("Attack");
                    m_AttDelayTimer = 4.0f;
                }
            }
            //���� �⺻ ���� ������ Ÿ�̸�
        }
        else //Wandering
        {
            if (m_PhotonView.IsMine == true) //PhotonNetwork.InstantiateRoomObject�� �����߱� ������ Room���� ����
            {
                WanderingAnimationUpdate();

                StartCoroutine(Wandering());
            }
            else //Room�ȿ� ������ ���忡��
            {
                if (10.0f < (transform.position - currPos).magnitude)
                {   //�߰� ���� ��ǥ�� ���� ��ǥ�� �Ÿ����� 10m �̻��̸� ��� ����
                    transform.position = currPos;
                }
                else
                {
                    //���� �÷��̾��� ��ġ�� ���Ź��� ��ġ���� �ε巴�� �̵���Ŵ
                    transform.position = Vector3.Lerp(transform.position,
                                                    currPos, Time.deltaTime * 10.0f);
                }

                //���� �÷��̾��� ��ġ�� ���Ź��� ������ŭ �ε巴�� ȸ����Ŵ
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
                    m_NavMeshAgent.SetDestination(m_HitInfo.point);                  //������ ����
                    m_TargetDist = (transform.position - m_HitInfo.point).magnitude; //�Ÿ�
                    m_MoveTime = m_TargetDist / m_NavMeshAgent.speed;                //�ð�
                }
            }

            m_WanderingStart = false;
            yield return new WaitForSeconds(m_MoveTime + 10.0f); //������ 10���Ŀ� �ٽ� Wandering
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
            {//�÷��̾� ���� ��ȯ
                m_Target.AnimationChange("Idle");
                m_Target.m_MoveMethod = MoveMethod.Stop;
            }

            //Enemy��ư ����
            EnemyBtn[] a_Btns = m_Target.m_EnemeyPanel.GetComponentsInChildren<EnemyBtn>();
            for (int ii = 0; ii < a_Btns.Length; ii++)
            {
                if (a_Btns[ii].m_Enemy == this.gameObject)
                    Destroy(a_Btns[ii].gameObject);
            }
            //Enemy��ư ����

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

        //�̵� �� ȸ�� ����
        m_TargetPPos = m_Target.transform.position;
        m_BossPos = transform.position;
        m_TargetCacVec = new Vector3(m_TargetPPos.x, 0, m_TargetPPos.z) - new Vector3(m_BossPos.x, 0, m_BossPos.z);
        m_TargetDir = m_TargetCacVec.normalized;  //Ÿ�� ����
        m_TargetPDist = m_TargetCacVec.magnitude; //Ÿ�� �Ÿ�
        //�̵� �� ȸ�� ����

        //return ����
        m_SpawnCacVec = new Vector3(m_SpawnPos.x, 0, m_SpawnPos.z) - new Vector3(m_BossPos.x, 0, m_BossPos.z);
        m_SpawnDir = m_SpawnCacVec.normalized;    //���� ����
        m_SpawnPosDist = m_SpawnCacVec.magnitude; //������ġ �Ÿ�

        if (m_SpawnPosDist > m_MaxMoveDist) //�ʱ⽺����ġ�κ��� 60m�̻� ������ ������
        {
            m_IsReturning = true;
        }
        else if(m_SpawnPosDist < 1.0f) //�ʱ⽺����ġ �����ϸ�
        {
            m_IsReturning = false;
        }
        //return ����

        if (m_IsReturning == true)
        {//�ʱ⽺����ġ�� �Ÿ��� �־��� �ǵ��ư��� ���� �϶�
            m_TargetRot = Quaternion.LookRotation(m_SpawnDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, m_TargetRot, Time.deltaTime * m_RotSpeed);

            transform.position += m_SpawnDir * Time.deltaTime * m_ReturnSpeed;
            AnimationChange("Move");

            if (m_SpawnPosDist < 1.1f)
                AnimationChange("Idle");
        }
        else
        {
            if (m_TargetPDist > m_AgroDist) // Ÿ�ٰ��ǰŸ��� ��׷� ��Ÿ����� �־�����(Wandering���·� ������)
            {
                AnimationChange("Idle");
                m_WanderingStart = true;
                m_Target = null;
            }
            else if (m_AttDist < m_TargetPDist && m_TargetPDist <= m_AgroDist) //Ÿ�ٰ��� �Ÿ��� ��׷� ��Ÿ� �ȿ��ְ� ���� ��Ÿ����� �ָ�(Ÿ���� �Ѿư��� ����)
            {
                if (m_IsAttacking == false)
                {
                    m_TargetRot = Quaternion.LookRotation(m_TargetDir);
                    transform.rotation = Quaternion.Slerp(transform.rotation, m_TargetRot, Time.deltaTime * m_RotSpeed);

                    transform.position += m_TargetDir * Time.deltaTime * m_MoveSpeed;
                    AnimationChange("Move");
                }
            }
            else if (m_TargetPDist <= m_AttDist) //Ÿ�ٰ��� �Ÿ��� ���ݻ�Ÿ� ���� ������(Ÿ���� �������� ����)
            {
                if (m_TargetPDist > m_AttDist - 1.0f) // ���ݻ�Ÿ����� 1m�� �����̿ͼ� �������� ����
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
        if (m_Target != null) //Ÿ���� ������ WanderingX
            return;

        if (m_NavMeshAgent.remainingDistance <= 0.1f) //������ ���� ��
        {
            AnimationChange("Idle");
        }
        else //�̵���
        {
            AnimationChange("Move");
        }
    }

    void AnimationChange(string a_Anim = "Idle")
    {//������ ����� �ִϸ��̼��� �̹������ӿ� ������ �ִϸ��̼ǰ� �ٸ���
        if (m_LastAnimState != a_Anim)
        {
            if (m_LastAnimState != null)
                m_Animator.ResetTrigger(m_LastAnimState);
            m_Animator.SetTrigger(a_Anim);
        }

        m_LastAnimState = a_Anim; //������ ����ִϸ��̼� ����
    }

    public void NotMineAnimUpdate(string a_Anim)
    {
        if (lastAnim != a_Anim)
            GetComponent<Animator>().SetTrigger(a_Anim);

        lastAnim = a_Anim; //������ ����ִϸ��̼� ����
    }

    public void AttackEndEvent() //�⺻������ �����̸� �ְ� 3��° �⺻���ݸ��� ���������� �߻���Ű�� ���� �̺�Ʈ �Լ�
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
            {// 3��° �������Ͽ� ����ϳ����� �̸�����
                m_RndPatternIdx = Random.Range(1, 3);
            }


            if (m_AttCount == 3)
            {
                if (m_RndPatternIdx == 1)  //����1
                {
                    Pattern1();
                }
                else if (m_RndPatternIdx == 2) //����2
                {
                    Pattern2();
                }

                m_AttCount = 0;
            }
        }
        else
        {//OnPhotonSrealizeView�� ���� ������� ������ �Ѱܹ޴´�
            if (m_AttCount == 3)
            {
                if (rndPatterIdx == 1)  //����1
                {
                    Pattern1();
                }
                else if (rndPatterIdx == 2) //����2
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
