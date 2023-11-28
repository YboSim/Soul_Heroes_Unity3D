using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    public GameObject m_CursorMark = null;
    public HeroCtrl m_Player;
    public Transform[] m_Skill = new Transform[8];
    public ManagerBox m_ManagerBox;
    public ExpMgr m_ExpMgr;
    public Transform m_SpawnPos;
    PhotonView pv;

    [Header("--- UI ---")]
    public Image m_HpBar;
    public Image m_MpBar;
    public Text m_HpText;
    public Text m_GoldText;
    public Text m_DiaText;
    public Text m_AttText;
    public Text m_DefText;
    public Text m_AccText;
    public Button m_StoreBtn;
    public Button m_SoulBtn;
    public Button m_ItemBtn;
    public Button m_SkillBtn;
    public Button m_ConfigBtn;
    public Button m_HelpBtn;
    public Button m_AccountBtn;

    [Header("--- EnemyPanel ---")]
    public GameObject m_EnemyPanel;
    public Button m_EnemyPanelUpBtn;
    public Button m_ScanBtn;
    public Image m_ScanCoolImg;
    public float m_EnemyPanelTimer = 2.0f;
    bool m_IsDown = true;

    [Header("--- SapwnText ---")]
    public Transform m_Canvas = null;
    public GameObject m_DamagePrefab = null;
    public GameObject m_GoldExpPrefab = null;
    GameObject m_TextObj = null; //프리팹가져와 담을 변수
    RectTransform CanvasRect;
    Vector2 ScreenPos = Vector2.zero;
    Vector2 WdScPos = Vector2.zero;

    [Header("--- Chat ---")]
    public GameObject m_ChatRoot;
    public InputField m_ChatIF;
    public Text m_ChatMsg;
    List<string> m_ChatList = new List<string>();
    [HideInInspector] public bool m_Enter = false;

    [Header("--- Message ---")]
    public GameObject m_MsgRoot;
    public Text m_MsgText;
    List<string> m_MsgList = new List<string>();
    float m_MsgTimer;
    string m_LastMsg;

    public static GameMgr Inst = null;
    void Awake()
    {
        Inst = this;

        Application.targetFrameRate = 60; //실행 프레임 속도 60프레임으로 고정 시키기.. 코드
        QualitySettings.vSyncCount = 0;
        //모니터 주사율(플레임율)이 다른 컴퓨터일 경우 캐릭터 조작시 빠르게 움직일 수 있다.

        DataSetting();
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonInit.inst.JoinInGameField(); // 방 참여 및 Player instantiate

        if (m_ScanBtn != null)
            m_ScanBtn.onClick.AddListener(()=>
            {
                m_Player.EnemySensor();
            });

        if (m_EnemyPanelUpBtn != null)
            m_EnemyPanelUpBtn.onClick.AddListener(() =>
            {
                Animator a_Anim = m_EnemyPanel.GetComponent<Animator>();
                if (a_Anim != null)
                {
                    a_Anim.SetTrigger("Up");
                }

                m_IsDown = false;
            });

        if (m_StoreBtn != null)
            m_StoreBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

                PhotonInit.inst.m_SceneIdx = 1;

                PhotonInit.inst.LeaveRoom();
            });

        if (m_SoulBtn != null)
            m_SoulBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

                m_ManagerBox.AllPanelActivefalse();
                m_ManagerBox.m_SoulPanel.SetActive(true);
                m_ManagerBox.transform.localPosition = m_ManagerBox.m_OnPos;
            });

        if (m_ItemBtn != null)
            m_ItemBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

                m_ManagerBox.AllPanelActivefalse();
                m_ManagerBox.m_ItemPanel.SetActive(true);
                m_ManagerBox.transform.localPosition = m_ManagerBox.m_OnPos;
            });

        if (m_SkillBtn != null)
            m_SkillBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

                m_ManagerBox.AllPanelActivefalse();
                m_ManagerBox.m_SkillPanel.SetActive(true);
                m_ManagerBox.transform.localPosition = m_ManagerBox.m_OnPos;
            });

        if (m_ConfigBtn != null)
            m_ConfigBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

                m_ManagerBox.AllPanelActivefalse();
                m_ManagerBox.m_ConfigPanel.SetActive(true);
                m_ManagerBox.transform.localPosition = m_ManagerBox.m_OnPos;
            });

        if (m_HelpBtn != null)
            m_HelpBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

                m_ManagerBox.AllPanelActivefalse();
                m_ManagerBox.m_HelpPanel.SetActive(true);
                m_ManagerBox.transform.localPosition = m_ManagerBox.m_OnPos;
            });

        if (m_AccountBtn != null)
            m_AccountBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

                m_ManagerBox.AllPanelActivefalse();
                m_ManagerBox.m_AccountPanel.SetActive(true);
                m_ManagerBox.transform.localPosition = m_ManagerBox.m_OnPos;
            });

        pv = GetComponent<PhotonView>();
        m_ExpMgr = GetComponent<ExpMgr>();

        StartCoroutine(MsgController());

        Sound_Mgr.Instance.PlayBGM("GameBGM", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        UseSkill();
        ManagerBoxOnOff();

        MsgTimer();
        if (m_EnemyPanelTimer > 0.0f)
        {
            m_EnemyPanelTimer -= Time.deltaTime;
            if(m_EnemyPanelTimer < 0.0f)
            {
                m_EnemyPanelTimer = 0.0f;

                if (m_IsDown == true)
                {
                    Animator a_Anim = m_EnemyPanel.GetComponent<Animator>();
                    a_Anim.SetTrigger("Up");
                }

                m_IsDown = false;
            }
        }

        //채팅 구현 텍스트 입력
        if (Input.GetKeyDown(KeyCode.Return))
        { //엔터키를 누르면 인풋 필드 활성화

            if (m_ChatRoot.activeSelf == true)
            {
                if (string.IsNullOrEmpty(m_ChatIF.text) == true)
                {
                    m_Enter = false;
                    m_ChatRoot.SetActive(false);
                }
                else
                {
                    BroadcastingChat();
                    m_ChatIF.ActivateInputField();
                }
            }
            else
            {
                m_Enter = true;
                m_ChatRoot.SetActive(true);
                m_ChatIF.ActivateInputField();
            }
        }
    }

    public void SpawnText(Vector3 a_SpPos, int dmg = 0, int gold = 0, int exp = 0, int a_ColorIdx = 0, int a_DmgOrGold = 0)
    {
        if (m_DamagePrefab == null || m_Canvas == null)
            return;

        //스폰 및 위치 잡아주기
        if (a_DmgOrGold == 0) //데미지 텍스트 스폰이면
        {
            m_TextObj = Instantiate(m_DamagePrefab) as GameObject;
            a_SpPos.y += 1.65f;
            float a_RandomX = Random.Range(-0.5f, 0.5f);
            a_SpPos.x += a_RandomX;
            m_TextObj.transform.SetParent(m_Canvas, false);
            DamageText a_DamageText = m_TextObj.GetComponent<DamageText>();
            a_DamageText.m_BaseWdPos = a_SpPos;
            a_DamageText.m_DamageVal = (int)dmg;
        }
        else if(a_DmgOrGold == 1) //골드,경험치 텍스트 스폰이면
        {
            m_TextObj = Instantiate(m_GoldExpPrefab) as GameObject;
            a_SpPos.y += 1.2f;
            //float a_RandomX = Random.Range(-0.1f, 0.1f);
            a_SpPos.x -= 1.4f;
            m_TextObj.transform.SetParent(m_Canvas, false);
            GoldExpText a_GoldExpText = m_TextObj.GetComponent<GoldExpText>();
            a_GoldExpText.m_BaseWdPos = a_SpPos;
            a_GoldExpText.m_GoldVal = (int)gold;
            a_GoldExpText.m_ExpVal = (int)exp;
        }
        //스폰 및 위치 잡아주기

        //World좌표를 UGUI 좌표로 환산
        CanvasRect = m_Canvas.GetComponent<RectTransform>();
        ScreenPos = Camera.main.WorldToViewportPoint(a_SpPos);
        WdScPos.x = ((ScreenPos.x * CanvasRect.sizeDelta.x) -
                            (CanvasRect.sizeDelta.x * 0.5f));
        WdScPos.y = ((ScreenPos.y * CanvasRect.sizeDelta.y) -
                                    (CanvasRect.sizeDelta.y * 0.5f));
        m_TextObj.GetComponent<RectTransform>().anchoredPosition = WdScPos;

        if (a_ColorIdx == 1) //주인공 일 때 데미지 텍스트 색 바꾸기...
        {
            Outline a_Outline = m_TextObj.GetComponentInChildren<Outline>();
            a_Outline.effectColor = new Color32(255, 255, 255, 0);
            a_Outline.enabled = false;

            Text a_RefText = m_TextObj.GetComponentInChildren<Text>();
            a_RefText.color = new Color32(255, 255, 230, 255);
        }
    }

    void BroadcastingChat()
    {
        string msg = "\n<color=#ffffff>[" +
                PhotonNetwork.LocalPlayer.NickName + "] " +
                m_ChatIF.text + "</color>";
        pv.RPC("LogMsg", RpcTarget.AllBuffered, msg);

        m_ChatIF.text = "";
    }

    [PunRPC]
    void LogMsg(string msg)
    {
        m_ChatList.Add(msg);
        if (20 < m_ChatList.Count)
            m_ChatList.RemoveAt(0);

        m_ChatMsg.text = "";
        for (int ii = 0; ii < m_ChatList.Count; ii++)
        {
            m_ChatMsg.text += m_ChatList[ii];
        }

        ////로그 메시지 Text UI에 텍스트를 누적시켜 표시
        //txtLogMsg.text = txtLogMsg.text + msg;
    }

    public void UIRefresh()
    {
        if (m_Player.m_CurHp > m_Player.m_MaxHp)
            m_Player.m_CurHp = m_Player.m_MaxHp;
        m_Player.m_HpBar.fillAmount = m_Player.m_CurHp / m_Player.m_MaxHp;
        m_Player.m_HpText.text = ((int)m_Player.m_CurHp).ToString() + " / " + ((int)m_Player.m_MaxHp).ToString();
        m_GoldText.text = GlobalValue.m_Gold.ToString("N0");
        m_DiaText.text = GlobalValue.m_Diamond.ToString("N0");
        m_AttText.text = GlobalValue.m_Att.ToString();
        m_DefText.text = GlobalValue.m_Def.ToString();
        m_AccText.text = GlobalValue.m_Acc.ToString();
    }

    public void EnemyPanelMove()
    {
        m_EnemyPanelTimer = 10.0f;

        if (m_IsDown == true)
            return;

        Sound_Mgr.Instance.PlayEffSound("EnemyPanel", 1.0f);

        Animator a_Anim = m_EnemyPanel.GetComponent<Animator>();
        if(a_Anim != null)
        {
            a_Anim.SetTrigger("Down");
        }

        m_IsDown = true;
    }

    public void CursorMarkOn(Vector3 a_PickVec)
    {
        if (m_CursorMark == null)
            return;

        m_CursorMark.transform.position = 
            new Vector3(a_PickVec.x ,a_PickVec.y + 0.1f, a_PickVec.z);

        m_CursorMark.SetActive(true);
    }

    public void CursorMarkOff()
    {
        if (m_CursorMark == null)
            return;

        m_CursorMark.SetActive(false);
    }

    IEnumerator MsgController()
    {
        while (true)
        {
            if (m_MsgList.Count == 0)
            {
                
            }
            else
            {
                if (m_MsgRoot.activeSelf == false)
                {//메시지가 진행중이지 않으면

                    for (int ii = 0; ii < m_MsgList.Count; ii++)
                    {//메세지 리스트에 쌓여있는 가장 첫번째 메시지를 찾아 띄워준다.
                        if (m_MsgList[ii] != null)
                        {
                            m_MsgText.text = m_MsgList[ii];
                            m_LastMsg = m_MsgList[ii]; //MsgList에서 지울 메시지 저장
                            break;
                        }
                    }

                    m_MsgRoot.SetActive(true); //메시지On
                    m_MsgTimer = 5.0f; //5초동안 메시지 보이게 하기
                }
            }
            yield return new WaitForSeconds(0.25f); // 0.25초 마다 List에 쌓인 메시지가 있는지 확인 한다.
        }
    }

    public void SetMsg(string a_Msg)
    {
        m_MsgList.Add(a_Msg);
    }

    void MsgTimer()
    {
        if(m_MsgTimer > 0.0f)
        {
            m_MsgTimer -= Time.deltaTime;
            if(m_MsgTimer < 0.0f)
            {
                m_MsgRoot.SetActive(false); // MessageOff

                if (m_LastMsg != null) //진행완료한 메시지는 리스트에서 삭제
                    m_MsgList.Remove(m_LastMsg);

                m_MsgTimer = 0.0f;
            }
        }
    }

    void UseSkill()
    {
        if (m_Player != null)
        {
            if (m_Player.m_ObjectInfo.m_IsDie == true)
                return;

            if (m_Player.m_ObjectInfo.m_IsStun == true)
                return;
        }

        if (m_Enter == true)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Button a_Btn = m_Skill[0].GetComponentInChildren<Button>();
            if (a_Btn != null)
                a_Btn.onClick.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Button a_Btn = m_Skill[1].GetComponentInChildren<Button>();
            if (a_Btn != null)
                a_Btn.onClick.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Button a_Btn = m_Skill[2].GetComponentInChildren<Button>();
            if (a_Btn != null)
                a_Btn.onClick.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Button a_Btn = m_Skill[3].GetComponentInChildren<Button>();
            if (a_Btn != null)
                a_Btn.onClick.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Button a_Btn = m_Skill[4].GetComponentInChildren<Button>();
            if (a_Btn != null)
                a_Btn.onClick.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Button a_Btn = m_Skill[5].GetComponentInChildren<Button>();
            if (a_Btn != null)
                a_Btn.onClick.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Button a_Btn = m_Skill[6].GetComponentInChildren<Button>();
            if (a_Btn != null)
                a_Btn.onClick.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Button a_Btn = m_Skill[7].GetComponentInChildren<Button>();
            if (a_Btn != null)
                a_Btn.onClick.Invoke();
        }
    }

    void ManagerBoxOnOff()
    {
        if (m_Enter == true)
            return;

        if(Input.GetKeyDown(KeyCode.I))
        {
            m_ManagerBox.AllPanelActivefalse();
            m_ManagerBox.m_ItemPanel.SetActive(true);
            m_ManagerBox.transform.localPosition = m_ManagerBox.m_OnPos;
        }
        else if(Input.GetKeyDown(KeyCode.L))
        {
            m_ManagerBox.AllPanelActivefalse();
            m_ManagerBox.m_SoulPanel.SetActive(true);
            m_ManagerBox.transform.localPosition = m_ManagerBox.m_OnPos;
        }
        else if(Input.GetKeyDown(KeyCode.K))
        {
            m_ManagerBox.AllPanelActivefalse();
            m_ManagerBox.m_SkillPanel.SetActive(true);
            m_ManagerBox.transform.localPosition = m_ManagerBox.m_OnPos;
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            m_ManagerBox.AllPanelActivefalse();
            m_ManagerBox.m_ConfigPanel.SetActive(true);
            m_ManagerBox.transform.localPosition = m_ManagerBox.m_OnPos;
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            m_ManagerBox.transform.localPosition = m_ManagerBox.m_OffPos;
        }
    }

    void DataSetting()
    {
        if (PlayfabMgr.inst.m_Data.TryGetValue("Dia", out string a_Dia))
        {
            GlobalValue.m_Diamond = int.Parse(a_Dia);
        }
        if (PlayfabMgr.inst.m_Data.TryGetValue("Gold", out string a_Gold))
        {
            GlobalValue.m_Gold = int.Parse(a_Gold);
        }
        if (PlayfabMgr.inst.m_Data.TryGetValue("Point", out string a_Point))
        {
            GlobalValue.m_Point = int.Parse(a_Point);
        }
        if(PlayfabMgr.inst.m_Data.TryGetValue("Soul_Dark", out string a_SD))
        {
            GlobalValue.m_DarkSoul = int.Parse(a_SD);
        }
        if (PlayfabMgr.inst.m_Data.TryGetValue("Soul_Fire", out string a_SF))
        {
            GlobalValue.m_FireSoul = int.Parse(a_SF);
        }
        if (PlayfabMgr.inst.m_Data.TryGetValue("Soul_Ice", out string a_SI))
        {
            GlobalValue.m_IceSoul = int.Parse(a_SI);
        }
        if (PlayfabMgr.inst.m_Data.TryGetValue("Soul_Nature", out string a_SN))
        {
            GlobalValue.m_NatureSoul = int.Parse(a_SN);
        }
        if (PlayfabMgr.inst.m_Data.TryGetValue("Soul_Water", out string a_SW))
        {
            GlobalValue.m_WaterSoul = int.Parse(a_SW);
        }
        if(PlayfabMgr.inst.m_Data.TryGetValue("Soul", out string a_S))
        {
            GlobalValue.m_Soul = int.Parse(a_S);
        }
    }

    public static bool IsPointerOverUIObject() //UGUI의 UI들이 먼저 피킹되는지 확인하는 함수
    {
        PointerEventData a_EDCurPos = new PointerEventData(EventSystem.current);

#if !UNITY_EDITOR && (UNITY_IPHONE || UNITY_ANDROID)

			List<RaycastResult> results = new List<RaycastResult>();
			for (int i = 0; i < Input.touchCount; ++i)
			{
				a_EDCurPos.position = Input.GetTouch(i).position;  
				results.Clear();
				EventSystem.current.RaycastAll(a_EDCurPos, results);
                if (0 < results.Count)
                    return true;
			}

			return false;
#else
        a_EDCurPos.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(a_EDCurPos, results);
        return (0 < results.Count);
#endif
    }//public bool IsPointerOverUIObject() 
}
