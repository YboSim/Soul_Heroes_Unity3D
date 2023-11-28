using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using UnityEngine.UI;
using Photon.Pun;

public enum MoveMethod
{
    Stop,
    MouseMove,
    KeyBoardMove,
    AttackMove,
    SkillMove
}

public class HeroCtrl : MonoBehaviourPunCallbacks, IPunObservable
{
    [HideInInspector]public ObjectInfo m_ObjectInfo;
    Animator m_Animator;
    Rigidbody m_RigidBody;
    NavMeshAgent m_NavMeshAgent;
    SkillSlotMgr m_SkillSlotMgr;
    [HideInInspector]public PhotonView m_PhotonView;
    public MoveMethod m_MoveMethod = MoveMethod.Stop;
    public GameObject m_EnemyBtnPrefab;
    public GameObject m_EnemeyPanel;
    public GameObject m_Scanner;
    public Image m_HpBar;
    public Image m_MpBar;
    public Text m_HpText;
    public GameObject[] m_Skills = new GameObject[8];
    public Text m_NickNameText;

    //포톤을 위한 캐릭터 위치 회전값등 정보 변수
    Vector3 currPos = Vector3.zero;
    Quaternion currRot = Quaternion.identity;
    string animState;
    string lastAnim;
    float maxHp;
    float curHp;

    //KeyBDMove 관련 변수
    Vector3 m_MoveStep = Vector3.zero;
    float h, v;
    float m_KBMoveSpeed = 0.0f;   //이동 속도
    //float m_DashSpeed = 50.0f; //대쉬 속도

    //RotateUpdate 관련 변수
    float m_MouseX;          //마우스좌우 드래그 이동 값
    float m_RotSpeed = 5.0f; //회전속도

    //AnimationChange 관련 변수
    string m_LastAnimState; //마지막 재생애니메이션

    //AttackMoveUpdate 관련 변수
    Vector3 m_LookDirVec = Vector3.zero;
    Quaternion m_TargetRot = Quaternion.identity;
    float m_TargetRotSpeed = 10.0f;
    float m_AttDist = 0.0f;

    //SkillMoveUpdate 관련 변수
    [HideInInspector] public Vector3 m_SkillPos = Vector3.zero;

    //MouseMoveUpdate 관련 변수
    Ray m_MsPickPos;
    RaycastHit hitInfo;
    LayerMask m_LayerMask = -1;

    //TakeDamage
    float m_ReSpawnTimer;

    //BasicAttack 관련 변수
    public GameObject m_AttTarget = null;
    public GameObject m_BasicHitEffect = null;

    //EnemyScensor 관련 변수
    List<GameObject> m_EnemyList = new List<GameObject>();
    Vector3 m_EnemyPos = Vector3.zero;
    Vector3 m_MyPos = Vector3.zero;
    GameObject m_ShortEnemy = null;
    float m_ShortDist = 0.0f;

    //Scanner 관련 변수
    public float m_ScannerTimer = 0.0f;

    //UseSkill 관련 변수
    public GameObject m_KnightBuff;
    public GameObject m_KnightStrokeHitObj;
    public GameObject m_MagicClawObj;
    public GameObject m_MagicClawObj2;
    public GameObject m_CharmObj;
    public GameObject m_CharmObj2;
    [HideInInspector] public float m_KnightBuffTimer;  //분노스킬 지속시간 타이머
    [HideInInspector] public bool m_KnightStrokeHit = false;
    [HideInInspector] public bool m_MagicClaw = false;
    [HideInInspector] public bool m_Charm = false;
    float m_AddDmg = 0.0f;

    //MpHp관련 변수
    public float m_CurHp;
    public float m_MaxHp;
    public float m_CurMp;
    public float m_MaxMp;

    float m_MpTimer;
    bool m_ReqMpUp = false;

    //Soul 관련
    public GameObject[] m_Soul;
    public GameObject m_SoulPrefab;
    public Transform m_SoulStartPos;

    void Awake()
    { 
        m_Animator = GetComponent<Animator>();
        m_RigidBody = GetComponent<Rigidbody>();
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_PhotonView = GetComponent<PhotonView>();
        m_ObjectInfo = GetComponent<ObjectInfo>();
        m_SkillSlotMgr = GameObject.FindObjectOfType<SkillSlotMgr>();

        //주인공을 따라다니는 카메라의 타겟 설정
        if (m_PhotonView.IsMine)
        {
            CameraCtrl a_CameraCtrl = Camera.main.GetComponent<CameraCtrl>();
            if (a_CameraCtrl != null)
                a_CameraCtrl.InitCamera(this.gameObject);
        }
        //주인공을 따라다니는 카메라의 타겟 설정

        m_ObjectInfo.InitObject(ObjectType.Hero, m_PhotonView.Owner.NickName);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (m_PhotonView.IsMine)
        {
            //스킬 슬롯 연결
            for (int ii = 0; ii < m_Skills.Length; ii++)
            {
                m_Skills[ii] = GameMgr.Inst.m_Skill[ii].gameObject;
            }

            Camera.main.transform.position = gameObject.transform.position; //카메라 위치 조정

            GameMgr.Inst.m_Player = this; //GameMgr m_Player Init
            GameMgr.Inst.GetComponent<PartySystemMgr>().m_Player = this;

            m_SoulPrefab = m_Soul[GlobalValue.m_Soul]; // 장착 중이였던 소울 가져오기

            m_CurMp = 100.0f;
            m_MaxMp = 100.0f;
        }

        m_CurHp = 100.0f;
        m_MaxHp = 100.0f;

        //머리 위 닉네임 띄워주기
        m_NickNameText = GetComponentInChildren<Text>();
        if (m_NickNameText != null)
            m_NickNameText.text = m_PhotonView.Owner.NickName;

        m_EnemeyPanel = GameObject.Find("EnemyPanel");
        m_HpBar = GameMgr.Inst.m_HpBar;
        m_MpBar = GameMgr.Inst.m_MpBar;
        m_HpText = GameMgr.Inst.m_HpText;

        //원거리 근거리 사거리 조절
        if (gameObject.name.Contains("Knight"))
            m_AttDist = 2.0f;
        else
            m_AttDist = 6.0f;

        currPos = transform.position; //송신 해줄 위치값 정보
        currRot = transform.rotation; //송신 해줄 회전값 정보
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PhotonView.IsMine)
        {
            if (m_ObjectInfo.m_IsDie == false)
            {
                if (m_ObjectInfo.m_IsStun == false)
                {
                    MoveMethodUpdate();

                    RotateUpdate();

                    if (Input.GetKeyDown(KeyCode.R) == true)
                    {
                        if (GameMgr.Inst.m_Enter == true)
                            return;

                        EnemySensor();

                        GameMgr.Inst.EnemyPanelMove();
                    }
                }
            }
        }
        else
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

        TimerUpdate();
    }

    void KeyBDMove()
    {
        if (m_NavMeshAgent.isStopped == false) //마우스 이동중이였다면
        {
            m_NavMeshAgent.ResetPath();

            if (GameMgr.Inst.m_CursorMark.activeSelf == true)
                GameMgr.Inst.CursorMarkOff();
        }

        if(h != 0.0f || v != 0.0f)
        {
            m_MoveStep = new Vector3(h, 0, v);

            if (v < 0.0f) // 뒤로 걷기
            {
                m_KBMoveSpeed = 2.0f;
                AnimationChange("Walk");
            }
            else if (v > 0.0f && Input.GetButton("Run") == false &&
                        h == 0.0f) //앞으로 걷기 
            {
                m_KBMoveSpeed = 3.0f;
                AnimationChange("Walk");
            }
            else if(v >= 0.0f && Input.GetButton("Run") == false &&
                        h > 0.0f) //오른쪽으로 걷기
            {
                m_KBMoveSpeed = 2.5f;
                AnimationChange("Walk_R");
            }
            else if(v >= 0.0f && Input.GetButton("Run") == false &&
                        h < 0.0f) //왼쪽으로 걷기
            {
                m_KBMoveSpeed = 2.5f;
                AnimationChange("Walk_L");
            }
            else if (v > 0.0f && Input.GetButton("Run") == true &&
                        h == 0.0f) //앞으로 달리기
            {
                m_KBMoveSpeed = 6.0f;
                AnimationChange("Run");
            }
            else if (v > 0.0f && Input.GetButton("Run") == true &&
                        h > 0.0f) //우측대각 달리기
            {
                m_KBMoveSpeed = 5.5f;
                AnimationChange("Run_R");
            }
            else if (v > 0.0f && Input.GetButton("Run") == true &&
                        h < 0.0f) //좌측대각 달리기
            {
                m_KBMoveSpeed = 5.5f;
                AnimationChange("Run_L");
            }

            transform.Translate(m_MoveStep * m_KBMoveSpeed * Time.deltaTime);
        }
        else if (h == 0.0f && v == 0.0f)
        {
            AnimationChange("Idle");

            m_MoveMethod = MoveMethod.Stop;
        }
    }

    void RotateUpdate()
    {
        if (m_SkillSlotMgr.m_Editing == true)
            return;

        if (m_MoveMethod == MoveMethod.MouseMove ||
            m_MoveMethod == MoveMethod.AttackMove)
            return;

        if (Input.GetMouseButton(0) == true)
        {
            m_MouseX = Input.GetAxis("Mouse X");

            transform.Rotate(0, m_MouseX * m_RotSpeed, 0, Space.World);
        }
    }

    public void AnimationChange(string a_Anim = "Idle")
    {
        if (m_ObjectInfo.m_IsDie == true && m_LastAnimState == "Die")
            return; //플레이어가 사망 상태 이면 다른애니메이션 재생X

        if (m_LastAnimState != a_Anim) //마지막 재생된 애니메이션이 이번프레임에 진행할 애니메이션과 다르면
            m_Animator.SetTrigger(a_Anim);

        m_LastAnimState = a_Anim; //마지막 재생애니메이션 저장
    }

    public void NotMineAnimUpdate(string a_Anim)
    {
        if (lastAnim != a_Anim)
            GetComponent<Animator>().SetTrigger(a_Anim);

        lastAnim = a_Anim; //마지막 재생애니메이션 저장
    }

    void MouseMoveUpdate()
    {
        if(Input.GetMouseButtonDown(1) == true)
        {
            m_MoveMethod = MoveMethod.MouseMove;

            m_MsPickPos = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(m_MsPickPos, out hitInfo))
            {
                if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Terrain"))
                {//MsPickPos가 땅이면
                    m_NavMeshAgent.SetDestination(hitInfo.point);

                    GameMgr.Inst.CursorMarkOn(hitInfo.point);
                }
                else if(hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Object"))
                {//MsPickPos가 오브젝트이면
                    m_NavMeshAgent.SetDestination(hitInfo.collider.gameObject.transform.position);

                    m_AttTarget = hitInfo.collider.gameObject;

                    TargetCheck();

                    m_MoveMethod = MoveMethod.AttackMove;
                }
            }
        }

        if (m_NavMeshAgent.remainingDistance < 0.1f && m_NavMeshAgent.velocity.sqrMagnitude >= 0.2f * 0.2f) //목적지 도착 시
        { //처음 출발시 remainingDistance가 0을반환해서 sqrMagnitude추가
            AnimationChange("Idle");

            m_NavMeshAgent.ResetPath(); //목적지 제거(이동 중단)

            m_MoveMethod = MoveMethod.Stop; 

            GameMgr.Inst.CursorMarkOff();
        }
        else //이동중
        {
            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Terrain") ||
                    hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Object"))
                {
                    if (Input.GetButton("Run") == true) //달리기
                    {
                        AnimationChange("Run");

                        if (m_NavMeshAgent != null)
                            m_NavMeshAgent.speed = 6.0f;
                    }
                    else
                    {
                        AnimationChange("Walk");

                        if (m_NavMeshAgent != null)
                            m_NavMeshAgent.speed = 3.5f;
                    }
                }
            }
        }

    }

    void AttackMoveUpdate()
    {
        if (m_AttTarget == null)
        {
            AnimationChange("Idle");
            m_MoveMethod = MoveMethod.Stop;
        }

        if (m_AttTarget != null)
        {
            m_LookDirVec = m_AttTarget.transform.position - transform.position;
            m_TargetRot = Quaternion.LookRotation(m_LookDirVec);
            transform.rotation = Quaternion.Slerp(transform.rotation, m_TargetRot, Time.deltaTime * m_TargetRotSpeed);
        }

        //if (m_NavMeshAgent.remainingDistance < 3.5f && m_NavMeshAgent.velocity.sqrMagnitude >= 0.2f * 0.2f &&
        if(m_AttTarget != null && (m_AttTarget.transform.position - transform.position).magnitude < m_AttDist)
        {
            m_NavMeshAgent.ResetPath();

            if (m_AttTarget.GetComponent<ObjectInfo>().m_IsDie == false)
                AnimationChange("Attack_B");
        }
    }

    void SkillMoveUpdate()
    {
        if((m_SkillPos - transform.position).magnitude < m_AttDist)
        {
            m_NavMeshAgent.ResetPath();

            m_LookDirVec = m_SkillPos - transform.position;
            m_TargetRot = Quaternion.LookRotation(m_LookDirVec);
            transform.rotation = Quaternion.Slerp(transform.rotation, m_TargetRot, Time.deltaTime * m_TargetRotSpeed);

            if (Mage_MeteorShower.m_MeteorShowerOn == true)
            {
                AnimationChange("Attack_B");
                Mage_MeteorShower.m_CanUseSkill = true;
            }
        }
        else
        {
            m_NavMeshAgent.SetDestination(m_SkillPos);
            AnimationChange("Walk");
        }
    }

    void MoveMethodUpdate()
    {
        if (GameMgr.Inst.m_Enter == true)
            return;

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (h != 0.0f || v != 0.0f)
            m_MoveMethod = MoveMethod.KeyBoardMove;
        else if (Input.GetMouseButtonDown(1))
            m_MoveMethod = MoveMethod.MouseMove;

        if (m_MoveMethod == MoveMethod.KeyBoardMove)
            KeyBDMove();
        else if (m_MoveMethod == MoveMethod.MouseMove)
            MouseMoveUpdate();
        else if (m_MoveMethod == MoveMethod.AttackMove)
            AttackMoveUpdate();
        else if (m_MoveMethod == MoveMethod.SkillMove)
            SkillMoveUpdate();
    }

    public void EnemySensor()
    {
        if (m_ScannerTimer > 0.0f)
            return;
        else
            m_ScannerTimer = 3.0f;

        Sound_Mgr.Instance.PlayEffSound("Scan", 0.5f);

        //초기화
        if (m_EnemyList.Count > 0)
            m_EnemyList.Clear();

        EnemyBtn[] a_EnemyBtns = m_EnemeyPanel.GetComponentsInChildren<EnemyBtn>();
        for(int ii = 0; ii < a_EnemyBtns.Length; ii++)
        {
            Destroy(a_EnemyBtns[ii].gameObject);
        }
        //초기화

        //히어로 주변 적들을 List로 가져오기
        Collider[] a_Enemies = Physics.OverlapSphere(transform.position, 30.0f);

        if (a_Enemies.Length < 1)
            return;

        for (int ii = 0; ii < a_Enemies.Length; ii++)
        {
            if (a_Enemies[ii].gameObject.tag == "Monster")
            {
                m_EnemyList.Add(a_Enemies[ii].gameObject);
            }
            else if(a_Enemies[ii].gameObject.tag == "Hero")
            {
                if(a_Enemies[ii].gameObject.GetComponent<HeroCtrl>().m_PhotonView.IsMine == false)
                {
                    m_EnemyList.Add(a_Enemies[ii].gameObject);
                }
            }
        }

        m_ShortDist = 100;
        float a_Dist;
        int a_EnemyCount = m_EnemyList.Count;

        if (a_EnemyCount < 1)
            return;

        for (int ii = 0; ii < a_EnemyCount; ii++)
        {
            //거리가 가장 짧은 Enemy 구하기
            foreach (GameObject a_Enemy in m_EnemyList)
            {
                m_EnemyPos = a_Enemy.transform.position;
                m_EnemyPos.y = 0.0f;
                m_MyPos = transform.position;
                m_MyPos.y = 0.0f;
                a_Dist = (m_EnemyPos - m_MyPos).magnitude;
                if (a_Dist < m_ShortDist)
                {
                    m_ShortDist = a_Dist;
                    m_ShortEnemy = a_Enemy;
                }
            }
            //거리가 가장 짧은 Enemy 구하기

            //가장짧은 Enemy 버튼 생성후 정보 담기
            GameObject a_EnemyBtnObj = Instantiate(m_EnemyBtnPrefab) as GameObject;
            a_EnemyBtnObj.transform.SetParent(m_EnemeyPanel.transform, false);

            EnemyBtn a_EnemyBtn = a_EnemyBtnObj.GetComponent<EnemyBtn>();
            a_EnemyBtn.m_Enemy = m_ShortEnemy; //버튼클릭으로 공격할때 타겟을 저장함
            ObjectInfo a_ObjInfo = m_ShortEnemy.GetComponent<ObjectInfo>();
            a_EnemyBtn.m_NameText.text = a_ObjInfo.m_ObjName;
            a_EnemyBtn.m_DistText.text = m_ShortDist.ToString("N0") + "m"; //버튼에 Enemy 이름 써주기
            //가장짧은 Enemy 버튼 생성후 정보 담기

            m_EnemyList.Remove(m_ShortEnemy);
            m_ShortDist = 100; //m_ShortDist는 리스트에서 삭제된 m_ShortEnemy의 거리기 때문에 'if (a_Dist < m_ShortDist)' 구문을 위해 충분히 큰 수로 바꿔준다

            //각 오브젝트 타입에 따라 글씨색 변경
            if (a_ObjInfo.m_ObjType == ObjectType.Monster)
            {
                a_EnemyBtn.m_NameText.color = Color.black;
            }
            else if (a_ObjInfo.m_ObjType == ObjectType.Hero)
            {
                a_EnemyBtn.m_NameText.color = Color.red;
            }
            else if (a_ObjInfo.m_ObjType == ObjectType.Hero_ally)
            {
                a_EnemyBtn.m_NameText.color = Color.blue;
            }
            //각 오브젝트 타입에 따라 글씨색 변경
        }
    }

    void Scanner()
    {
        if (m_Scanner == null)
            return;

        if (m_Scanner.activeSelf == false)
        {
            m_Scanner.transform.position = gameObject.transform.position;
            m_Scanner.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);

            m_Scanner.SetActive(true);
        }

        m_Scanner.transform.localScale += new Vector3(2.0f, 2.0f, 2.0f);
    }

    public void Attack_Event() //애니메이션 이벤트 함수
    {
        if (m_AttTarget == null)
            return;

        if(m_AttTarget.GetComponent<ObjectInfo>().m_IsDie == true)
        {
            m_AttTarget = null;
            AnimationChange("Idle");
            return;
        }

        if (m_SoulPrefab != null)
        {   //소울 소환
            if (m_AttTarget != null)
            {
                GameObject a_SoulPrefab = Instantiate(m_SoulPrefab, m_SoulStartPos.position, Quaternion.identity);
                a_SoulPrefab.GetComponent<Rigidbody>().velocity = Vector3.up;
                Destroy(a_SoulPrefab, 3.0f);
            }
        }

        GameObject a_BasicHitEff = Instantiate(m_BasicHitEffect);
        if (gameObject.name.Contains("Knight"))
            a_BasicHitEff.transform.position = transform.position + transform.forward;
        else
            a_BasicHitEff.transform.position = m_AttTarget.transform.position + transform.forward;

        Destroy(a_BasicHitEff, 1.0f);

        // 사운드
        if (m_MagicClaw == true)
            Sound_Mgr.Instance.PlayEffSound("MagicClaw", 1.0f);
        else if (m_Charm == true)
            Sound_Mgr.Instance.PlayEffSound("Charm", 7.0f);
        else
            Sound_Mgr.Instance.PlayEffSound("BasicAttack", 1.0f);
        // 사운드

        #region//스킬관련
        //강타 스킬 관련
        if (m_KnightStrokeHit == true)
        {
            ObjectInfo a_ObjectInfo = m_AttTarget.GetComponent<ObjectInfo>();
            a_ObjectInfo.m_PhotonView.RPC("Stun", RpcTarget.AllBuffered);

            m_KnightStrokeHit = false;

            if (m_KnightStrokeHitObj != null)
                m_KnightStrokeHitObj.SetActive(false);

            //스킬 슬롯에 있는 강타스킬을 전부찾아 쿨타임 돌려주기
            for (int ii = 0; ii < m_Skills.Length; ii++)
            {
                Knight_StrokeHit a_StrokeHit = m_Skills[ii].GetComponentInChildren<Knight_StrokeHit>();
                if (a_StrokeHit != null)
                {
                    Knight_StrokeHit.m_CoolTime = 10.0f;
                    a_StrokeHit.m_RefTimer = 10.0f;
                }
            }
            //스킬 슬롯에 있는 강타스킬을 전부찾아 쿨타임 돌려주기

            m_AddDmg = GlobalValue.m_Att * 0.3f;
        }
        //강타 스킬 관련

        //매직클로 스킬 관련
        if (m_MagicClaw == true)
        {
            m_MagicClaw = false;

            GameObject a_MagicClawObj = Instantiate(m_MagicClawObj) as GameObject;
            a_MagicClawObj.transform.position = m_AttTarget.transform.position + transform.forward;

            if (m_MagicClawObj2 != null)
                m_MagicClawObj2.SetActive(false);

            //스킬 슬롯에 있는 매직클로스킬을 전부찾아 쿨타임 돌려주기
            for (int ii = 0; ii < m_Skills.Length; ii++)
            {
                Mage_Claw a_Mage_Claw = m_Skills[ii].GetComponentInChildren<Mage_Claw>();
                if (a_Mage_Claw != null)
                {
                    Mage_Claw.m_CoolTime = 10.0f;
                    a_Mage_Claw.m_RefTimer = 10.0f;
                }
            }
            //스킬 슬롯에 있는 매직클로스킬을 전부찾아 쿨타임 돌려주기

            m_AddDmg = GlobalValue.m_Att * 0.6f;
        }
        //매직클로 스킬 관련

        //매혹 스킬 관련
        if (m_Charm == true)
        {
            m_Charm = false;

            GameObject a_CharmObj = Instantiate(m_CharmObj2) as GameObject;
            a_CharmObj.transform.position = m_AttTarget.transform.position;
            a_CharmObj.transform.SetParent(m_AttTarget.transform);
            a_CharmObj.transform.gameObject.SetActive(true);
            Destroy(a_CharmObj, 5.0f);

            if (m_CharmObj != null)
                m_CharmObj.SetActive(false);

            //스킬 슬롯에 있는 매직클로스킬을 전부찾아 쿨타임 돌려주기
            for (int ii = 0; ii < m_Skills.Length; ii++)
            {
                Healer_Charm a_Healer_Charm = m_Skills[ii].GetComponentInChildren<Healer_Charm>();
                if (a_Healer_Charm != null)
                {
                    Healer_Charm.m_CoolTime = 10.0f;
                    a_Healer_Charm.m_RefTimer = 10.0f;
                }
            }
            //스킬 슬롯에 있는 매직클로스킬을 전부찾아 쿨타임 돌려주기

            if (m_AttTarget.gameObject.tag == "Monster")
            {
                if (!m_AttTarget.gameObject.name.Contains("Boss"))
                {
                    MonsterCtrl a_Monster = m_AttTarget.GetComponent<MonsterCtrl>();
                    a_Monster.m_Def = a_Monster.m_Def / 2;
                    a_Monster.m_IsCharmed = true;
                }
            }
        }
        //매혹 스킬 관련
        #endregion

        if (m_AttTarget.gameObject.tag == "Monster")
        {
            if (m_AttTarget.gameObject.name.Contains("Boss"))
            {
                BossCtrl a_BossCtrl = m_AttTarget.GetComponent<BossCtrl>();
                float a_Dmg = GlobalValue.m_Att + m_AddDmg - a_BossCtrl.m_Def;
                if (a_Dmg <= 0.0f)
                    a_Dmg = 0.0f;
                if (a_BossCtrl != null)
                {
                    a_BossCtrl.m_PhotonView.RPC("TakeDamage",RpcTarget.AllBuffered,a_Dmg, m_PhotonView.Owner.NickName);
                    a_BossCtrl.StopAllCoroutines();
                }
            }
            else
            {
                MonsterCtrl a_Target = m_AttTarget.GetComponent<MonsterCtrl>();
                float a_Dmg = (GlobalValue.m_Att + m_AddDmg - a_Target.m_Def);
                if (a_Dmg <= 0.0f)
                    a_Dmg = 0.0f;
                if (a_Target != null)
                    a_Target.m_PhotonView.RPC("TakeDamage", RpcTarget.AllBuffered, a_Dmg);
            }
        }
        else if (m_AttTarget.gameObject.tag == "Hero")
        {
            HeroCtrl a_Hero = m_AttTarget.GetComponent<HeroCtrl>();
            float a_Dmg = GlobalValue.m_Att + m_AddDmg;
            if (a_Dmg <= 0.0f)
                a_Dmg = 0.0f;
            if (a_Hero != null)
                a_Hero.m_PhotonView.RPC("TakeDamage", RpcTarget.AllBuffered, a_Dmg);
        }

        m_AddDmg = 0.0f; //초기화
    }


    public void Attack(GameObject a_Target) //Enemy버튼을 눌러 공격한 메서드
    {
        m_AttTarget = a_Target;

        TargetCheck();

        if (gameObject.name.Contains("Knight"))
        {
            if (m_AttTarget.name.Contains("Boss"))
                m_AttDist = 3.0f;
            else
                m_AttDist = 2.0f;
        }
        else
            m_AttDist = 6.0f;

        if ((transform.position - m_AttTarget.transform.position).magnitude > m_AttDist)
        {//타겟과의 거리가 공격 사거리 보다 멀 경우
            m_NavMeshAgent.ResetPath();

            AnimationChange("Walk");

            m_NavMeshAgent.SetDestination(a_Target.transform.position);

            m_MoveMethod = MoveMethod.AttackMove;
        }
        else
        {//타겟과의 거리가 공격 사거리내에 도달했을 경우
            m_NavMeshAgent.ResetPath();

            if (m_AttTarget.GetComponent<ObjectInfo>().m_IsDie == false)
                AnimationChange("Attack_B"); //공격 애니메이션 재생

            m_MoveMethod = MoveMethod.AttackMove;
        }
    }

    void TargetCheck()
    {
        ObjectInfo[] a_ObjectInfo = FindObjectsOfType<ObjectInfo>();

        for(int ii = 0; ii < a_ObjectInfo.Length; ii++)
        {
            if(a_ObjectInfo[ii].gameObject == m_AttTarget) //타겟이면
            {
                a_ObjectInfo[ii].TargetMarkOn();
            }
            else if(a_ObjectInfo[ii].gameObject != m_AttTarget) //타겟이 아니면
            {
                a_ObjectInfo[ii].TargetMarkOff();
            }
        }
    }

    [PunRPC]
    public void TakeDamage(float a_Damage)
    {
        if(m_PhotonView.IsMine ==false)
        {
            m_CurHp = curHp;
            m_MaxHp = maxHp;
        }

        if (m_HpBar == null)
            return;

        if (m_CurHp <= 0.0f)
            return;

        m_CurHp -= a_Damage;

        if (m_CurHp <= 0.0f)
        {
            a_Damage = 0.0f;

            m_ObjectInfo.m_IsDie = true;

            m_CurHp = 0.0f;

            AnimationChange("Die");

            m_ReSpawnTimer = 5.0f;
        }

        if (m_PhotonView.IsMine == true) // 맞을 때
        {
            m_HpText.text = ((int)m_CurHp).ToString() + " / " + ((int)m_MaxHp).ToString();

            m_HpBar.fillAmount = m_CurHp / m_MaxHp;

            GameMgr.Inst.SpawnText(this.transform.position, (int)a_Damage, 0, 0, 1, 0);

            Sound_Mgr.Instance.PlayEffSound("TakeDamage", 1.0f);
        }
        else
        {
            GameMgr.Inst.SpawnText(this.transform.position, (int)a_Damage, 0, 0, 0, 0);
        }
    }

    void TimerUpdate()
    {
        //적 스캔
        if (m_ScannerTimer > 0.0f)
        {
            m_ScannerTimer -= Time.deltaTime;

            Scanner();

            GameMgr.Inst.m_ScanCoolImg.fillAmount = m_ScannerTimer / 3.0f;

            if (m_ScannerTimer < 0.0f)
            {
                m_Scanner.SetActive(false);

                m_ScannerTimer = 0.0f;
            }
        }
        //적 스캔

        //분노 스킬지속시간 타이머
        if (m_KnightBuffTimer > 0.0f)
        {
            m_KnightBuffTimer -= Time.deltaTime;

            if (m_KnightBuffTimer < 0.0f)
            {
                m_KnightBuff.SetActive(false);
                GlobalValue.m_Att -= 10;
                GlobalValue.m_Def -= 20;
                GameMgr.Inst.UIRefresh();

                m_KnightBuffTimer = 0.0f;
            }
        }
        //분노 스킬지속시간 타이머

        //Mp자연회복 타이머
        if (m_CurMp < 100.0f)
        {
            m_ReqMpUp = true;
        }
        else
        {
            m_ReqMpUp = false;
        }

        if (m_CurMp < 100.0f && m_ReqMpUp == true)
        {
            m_MpTimer -= Time.deltaTime;
            if (m_MpTimer < 0.0f)
            {
                m_CurMp += 0.5f;
                m_MpBar.fillAmount = m_CurMp / m_MaxMp;
                m_MpTimer = 3.0f;
            }
        }
        //Mp자연회복 타이머

        //사망시 리스폰 타이머
        if (m_ReSpawnTimer > 0.0f)
        {
            m_ReSpawnTimer -= Time.deltaTime;
            if (m_ReSpawnTimer < 0.0f)
            {
                transform.position = GameMgr.Inst.m_SpawnPos.position;
                m_ObjectInfo.m_IsDie = false;
                AnimationChange("Idle");

                m_CurHp = m_MaxHp;
                GameMgr.Inst.UIRefresh();

                m_ReSpawnTimer = 0.0f;
            }
        }
        //사망시 리스폰 타이머
    }

    public void WalkAndRun_Event()
    {
        if (m_MoveMethod == MoveMethod.Stop)
            return;

        Sound_Mgr.Instance.PlayEffSound("FootStep", 1.0f);
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Monster")
            m_RigidBody.constraints = RigidbodyConstraints.FreezeRotationY |
                                      RigidbodyConstraints.FreezeRotationX |
                                      RigidbodyConstraints.FreezeRotationZ;
    }

    private void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.tag == "Monster")
            m_RigidBody.constraints = RigidbodyConstraints.FreezeRotationX |
                                      RigidbodyConstraints.FreezeRotationZ;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting) //송신
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(m_LastAnimState);
            stream.SendNext(m_MaxHp);
            stream.SendNext(m_CurHp);
        }
        else
        {
            currPos = (Vector3)stream.ReceiveNext();
            currRot = (Quaternion)stream.ReceiveNext();
            animState = (string)stream.ReceiveNext();
            maxHp = (float)stream.ReceiveNext();
            curHp = (float)stream.ReceiveNext();
        }
    }
}
