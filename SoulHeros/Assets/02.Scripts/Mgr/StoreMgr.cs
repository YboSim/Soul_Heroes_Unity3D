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

    int m_PanelIdx = 0; //�����Ϸ����ϴ� ��ȭ �ǳ��� �������� �����س��� ����
    int m_ReqGoods = 0; //�ʿ���ȭ
    int m_AcqGoods = 0; //ȹ����ȭ

    // Start is called before the first frame update
    void Start()
    {
        MyGoodsRefresh();

        if (m_ReturnBtn != null)
            m_ReturnBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

                //������ �� ���� �� �����͸� �ٽ� �������ֱ�
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
                //������ �� ���� �� �����͸� �ٽ� �������ֱ�

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

    public void PurchaseBtnClick(int a_ReqGoods) //Req : �ʿ���ȭ. Acq : ȹ����ȭ
    {
        m_ReqGoods = a_ReqGoods; //�ʿ���ȭ ����

        //ȹ����ȭ ����
        if (m_PanelIdx == 0) //�ҿ�̱�
            m_AcqGoods = 0;
        else if (m_PanelIdx == 1) //��Ű��
            m_AcqGoods = 0;
        else if (m_PanelIdx == 2) //�ż�
            m_AcqGoods = (a_ReqGoods / 3300) * 120;
        else if (m_PanelIdx == 3) //��ȭ
            m_AcqGoods = a_ReqGoods * 100;
        else // ����Ʈ
            m_AcqGoods = 0;
        //ȹ����ȭ ����

        if (m_PurchaseCheckBox != null)
            m_PurchaseCheckBox.SetActive(true);

        Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);
    }

    void OkBtnClick()
    {
        Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

        if (m_PanelIdx == 0) //�ҿ�̱�
        {
            if (GlobalValue.m_Diamond - m_ReqGoods >= 0)
                GlobalValue.m_Diamond -= m_ReqGoods;

            if (m_ReqGoods >= 1000)
                SoulSceneMgr.m_Idx = 11;
            else
                SoulSceneMgr.m_Idx = 1;
        }
        else if(m_PanelIdx == 1) // ��Ű��
        {

        }
        else if(m_PanelIdx ==2) // �ż�
        {
            GlobalValue.m_Diamond += m_AcqGoods;
            GlobalValue.m_Point += m_ReqGoods / 10;
        }
        else if(m_PanelIdx ==3) // ���
        {
            if(GlobalValue.m_Diamond - m_ReqGoods >= 0)
            {
                GlobalValue.m_Diamond -= m_ReqGoods;
                GlobalValue.m_Gold += m_AcqGoods;
            }
        }
        else //����Ʈ
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
