using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMgr : MonoBehaviour
{
    public Text m_InfoText;
    public Button m_QuitBtn;

    [Header("--- Login ---")]
    public InputField m_IDInputField;
    public InputField m_PWInputField;
    public Button m_StartGameBtn;
    public Button m_CreateAccountBtn;

    [Header("--- CreateAcc ---")]
    public GameObject m_CA_CreateAccountBox;
    public InputField m_CA_IDInputField;
    public InputField m_CA_PWInputField;
    public Button m_CA_CreateAccountBtn;
    public Button m_CA_QuitBtn;
    bool m_CA_CABon = false;
    bool m_CA_CABoff = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayfabMgr.inst.m_Data.Clear();
        GlobalValue.ClearData();

        if (m_CreateAccountBtn != null)
            m_CreateAccountBtn.onClick.AddListener(() =>
            {
                m_CA_CreateAccountBox.SetActive(true);
                m_CA_CABon = true;
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);
            });

        if (m_CA_QuitBtn != null)
            m_CA_QuitBtn.onClick.AddListener(() =>
            {
                m_CA_CABoff = true;
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);
            });

        if (m_QuitBtn != null)
            m_QuitBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

                Application.Quit();
            });

        //Sound_Mgr.Instance.PlayBGM("TitleBGM", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_CA_CABon == true)
        {
            m_CA_CreateAccountBox.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            if(m_CA_CreateAccountBox.transform.localScale.x >= 1.0f)
            {
                m_CA_CABon = false;
            }
        }

        if(m_CA_CABoff == true)
        {
            m_CA_CreateAccountBox.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            if(m_CA_CreateAccountBox.transform.localScale.x <= 0.0f)
            {
                m_CA_CreateAccountBox.SetActive(false);
                m_CA_CABoff = false;
            }
        }
    }
}
