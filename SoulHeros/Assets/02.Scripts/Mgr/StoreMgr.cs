using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreMgr : MonoBehaviour
{
    public GameObject[] m_Panel;
    public GameObject m_PurchaseCheckBox;
    public Button m_ReturnBtn;

    [Header("--- MyGoods ---")]
    public Text m_DiaText;
    public Text m_GoldText;
    public Text m_PointText;

    [Header("--- PurchaseCheckBox ---")]
    public Button m_OkBtn;
    public Button m_CancelBtn;

    int m_PanelIdx = 0; //구매하려고하는 재화 판넬이 무엇인지 저장해놓을 변수
    int m_ReqGoods = 0; //필요재화
    int m_AcqGoods = 0; //획득재화

    // Start is called before the first frame update
    void Start()
    {
        MyGoodsRefresh();

        if (m_ReturnBtn != null)
            m_ReturnBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

                //상점씬 을 나갈 때 데이터를 다시 세팅해주기
                var request = new GetUserDataRequest()
                {
                    //PlayFabId = GlobalValue.m_ID
                };

                PlayfabMgr.inst.m_Data = new Dictionary<string, string>();

                PlayFabClientAPI.GetUserData(request,
                    (result) =>
                    {
                        foreach (var eachData in result.Data)
                        {
                            PlayfabMgr.inst.m_Data.Add(eachData.Key, eachData.Value.Value);
                        }
                    },
                    (error) =>
                    {
                        Debug.Log("Error");
                    });
                //상점씬 을 나갈 때 데이터를 다시 세팅해주기

                LoadSceneMgr.LoadScene("InGame");
            });

        if (m_OkBtn != null)
            m_OkBtn.onClick.AddListener(OkBtnClick);

        if (m_CancelBtn != null)
            m_CancelBtn.onClick.AddListener(() =>
            {
                m_ReqGoods = 0;
                m_AcqGoods = 0;
                m_PurchaseCheckBox.SetActive(false);

                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);
            });

        GlobalValue.m_Att = 10;
        GlobalValue.m_Def = 10;
        GlobalValue.m_Acc = 10;
        GameMgr.Inst.m_Player.m_MaxHp = 100;
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public void BtnClick(int a_Idx)
    {
        for(int ii = 0; ii< m_Panel.Length; ii++)
        {
            if(ii == a_Idx)
            {
                m_Panel[ii].gameObject.SetActive(true);
            }
            else
            {
                m_Panel[ii].gameObject.SetActive(false);
            }
        }

        m_PanelIdx = a_Idx;

        Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);
    }

    void MyGoodsRefresh()
    {
        m_DiaText.text = GlobalValue.m_Diamond.ToString("N0");
        m_GoldText.text = GlobalValue.m_Gold.ToString("N0");
        m_PointText.text = GlobalValue.m_Point.ToString("N0");
    }

    public void PurchaseBtnClick(int a_ReqGoods) //Req : 필요재화. Acq : 획득재화
    {
        m_ReqGoods = a_ReqGoods; //필요재화 저장

        //획득재화 저장
        if (m_PanelIdx == 0) //소울뽑기
            m_AcqGoods = 0;
        else if (m_PanelIdx == 1) //패키지
            m_AcqGoods = 0;
        else if (m_PanelIdx == 2) //신석
            m_AcqGoods = (a_ReqGoods / 3300) * 120;
        else if (m_PanelIdx == 3) //금화
            m_AcqGoods = a_ReqGoods * 100;
        else // 포인트
            m_AcqGoods = 0;
        //획득재화 저장

        if (m_PurchaseCheckBox != null)
            m_PurchaseCheckBox.SetActive(true);

        Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);
    }

    void OkBtnClick()
    {
        Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

        if (m_PanelIdx == 0) //소울뽑기
        {
            if (GlobalValue.m_Diamond - m_ReqGoods >= 0)
                GlobalValue.m_Diamond -= m_ReqGoods;

            if (m_ReqGoods >= 1000)
                SoulSceneMgr.m_Idx = 11;
            else
                SoulSceneMgr.m_Idx = 1;
        }
        else if(m_PanelIdx == 1) // 패키지
        {

        }
        else if(m_PanelIdx ==2) // 신석
        {
            GlobalValue.m_Diamond += m_AcqGoods;
            GlobalValue.m_Point += m_ReqGoods / 10;
        }
        else if(m_PanelIdx ==3) // 골드
        {
            if(GlobalValue.m_Diamond - m_ReqGoods >= 0)
            {
                GlobalValue.m_Diamond -= m_ReqGoods;
                GlobalValue.m_Gold += m_AcqGoods;
            }
        }
        else //포인트
        {
            if (GlobalValue.m_Point - m_ReqGoods >= 0)
                GlobalValue.m_Point -= m_ReqGoods;
        }

        MyGoodsRefresh();

        m_ReqGoods = 0;
        m_AcqGoods = 0;
        m_PurchaseCheckBox.SetActive(false);

        PlayfabMgr.inst.SetPlayerData("Dia", GlobalValue.m_Diamond.ToString());
        PlayfabMgr.inst.SetPlayerData("Gold", GlobalValue.m_Gold.ToString());
        PlayfabMgr.inst.SetPlayerData("Point", GlobalValue.m_Point.ToString());

        if (m_PanelIdx == 0)
        {
            SceneManager.LoadScene("SoulScene");
        }
    }
}
