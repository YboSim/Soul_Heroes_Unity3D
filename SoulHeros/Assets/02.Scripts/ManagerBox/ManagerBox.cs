using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerBox : MonoBehaviour
{
    public Button m_ExitBtn;
    public Button m_ItemBtn;
    public Button m_SoulBtn;
    public Button m_SkillBtn;
    public Button m_ConfigBtn;
    public Button m_HelpBtn;
    public Button m_AccountBtn;
    public GameObject m_ItemPanel;
    public GameObject m_SoulPanel;
    public GameObject m_SkillPanel;
    public GameObject m_ConfigPanel;
    public GameObject m_HelpPanel;
    public GameObject m_AccountPanel;

    public Vector3 m_OnPos = new Vector3(0, 0, 0);
    public Vector3 m_OffPos = new Vector3(-2000, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        if (m_ExitBtn != null)
            m_ExitBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

                gameObject.transform.localPosition = m_OffPos;
                AllPanelActivefalse();
                m_ItemPanel.SetActive(true);
            });

        if (m_ItemBtn != null)
            m_ItemBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

                AllPanelActivefalse();
                m_ItemPanel.SetActive(true);
            });

        if (m_SoulBtn != null)
            m_SoulBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

                AllPanelActivefalse();
                m_SoulPanel.SetActive(true);
            });

        if (m_SkillBtn != null)
            m_SkillBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

                AllPanelActivefalse();
                m_SkillPanel.SetActive(true);
            });

        if (m_ConfigBtn != null)
            m_ConfigBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

                AllPanelActivefalse();
                m_ConfigPanel.SetActive(true);
            });

        if (m_HelpBtn != null)
            m_HelpBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

                AllPanelActivefalse();
                m_HelpPanel.SetActive(true);
            });

        if (m_AccountBtn != null)
            m_AccountBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

                AllPanelActivefalse();
                m_AccountPanel.SetActive(true);
            });
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public void AllPanelActivefalse()
    {
        if (m_ItemPanel.activeSelf == true)
            m_ItemPanel.SetActive(false);
        if (m_SoulPanel.activeSelf == true)
            m_SoulPanel.SetActive(false);
        if (m_SkillPanel.activeSelf == true)
            m_SkillPanel.SetActive(false);
        if (m_ConfigPanel.activeSelf == true)
            m_ConfigPanel.SetActive(false);
        if (m_HelpPanel.activeSelf == true)
            m_HelpPanel.SetActive(false);
        if (m_AccountPanel.activeSelf == true)
            m_AccountPanel.SetActive(false);
    }
}
