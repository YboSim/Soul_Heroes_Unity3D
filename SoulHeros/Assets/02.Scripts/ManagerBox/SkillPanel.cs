using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    GameObject[] m_Heroes;

    [Header("--- SkillBtn ---")]
    public Button m_StrokeHitBtn;
    public Button m_AngerBtn;
    public Button m_ClawBtn;
    public Button m_MeteorShowerBtn;
    public Button m_HealBtn;
    public Button m_CharmBtn;
    [Header("--- UseBtn ---")]
    public Button m_StrokeHitUseBtn;
    public Button m_AngerUseBtn;
    public Button m_ClawnUseBtn;
    public Button m_MeteorShowerUseBtn;
    public Button m_HealUseBtn;
    public Button m_CharmUseBtn;
    [Header("--- Panel ---")]
    public GameObject m_StorkeHitPanel;
    public GameObject m_AngerPanel;
    public GameObject m_ClawPanel;
    public GameObject m_MeteorShowerPanel;
    public GameObject m_HealPanel;
    public GameObject m_CharmPanel;
    [Header("--- SkillPrefab ---")]
    public GameObject m_StrokeHitPrefab;
    public GameObject m_AngerPrefab;
    public GameObject m_ClawPrefab;
    public GameObject m_MeteorShowerPrefab;
    public GameObject m_HealPrefab;
    public GameObject m_CharmPrefab;
    [Header("--- SkillSlot ---")]
    public Transform[] m_Skill = new Transform[8];

    // Start is called before the first frame update
    void Start()
    {
        //SKillBtn
        if (m_AngerBtn != null)
            m_AngerBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

                AllPanelActivefalse();
                m_AngerPanel.SetActive(true);
            });

        if (m_StrokeHitBtn != null)
            m_StrokeHitBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

                AllPanelActivefalse();
                m_StorkeHitPanel.SetActive(true);
            });

        if (m_ClawBtn != null)
            m_ClawBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

                AllPanelActivefalse();
                m_ClawPanel.SetActive(true);
            });

        if (m_MeteorShowerBtn != null)
            m_MeteorShowerBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

                AllPanelActivefalse();
                m_MeteorShowerPanel.SetActive(true);
            });

        if (m_HealBtn != null)
            m_HealBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

                AllPanelActivefalse();
                m_HealPanel.SetActive(true);
            });

        if (m_CharmBtn != null)
            m_CharmBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

                AllPanelActivefalse();
                m_CharmPanel.SetActive(true);
            });
        //SKillBtn

        //UseBtn
        if (m_AngerUseBtn != null)
            m_AngerUseBtn.onClick.AddListener(() =>
            {
                UseSkillBtnClick(m_AngerPrefab);
            });


        if (m_StrokeHitUseBtn != null)
            m_StrokeHitUseBtn.onClick.AddListener(() =>
            {
                UseSkillBtnClick(m_StrokeHitPrefab);
            });

        if (m_ClawnUseBtn != null)
            m_ClawnUseBtn.onClick.AddListener(() =>
            {
                UseSkillBtnClick(m_ClawPrefab);
            });

        if (m_MeteorShowerUseBtn != null)
            m_MeteorShowerUseBtn.onClick.AddListener(() =>
            {
                UseSkillBtnClick(m_MeteorShowerPrefab);
            });

        if (m_HealUseBtn != null)
            m_HealUseBtn.onClick.AddListener(() =>
            {
                UseSkillBtnClick(m_HealPrefab);
            });

        if (m_CharmUseBtn != null)
            m_CharmUseBtn.onClick.AddListener(() =>
            {
                UseSkillBtnClick(m_CharmPrefab);
            });
        //UseBtn

        m_Heroes = GameObject.FindGameObjectsWithTag("Hero");
        for(int ii =0; ii< m_Heroes.Length; ii++)
        {
            HeroCtrl a_Hero = m_Heroes[ii].GetComponent<HeroCtrl>();
            if (a_Hero.m_PhotonView.IsMine == true)
            {
                if(a_Hero.gameObject.name.Contains("Knight"))
                {
                    m_StrokeHitBtn.transform.parent.gameObject.SetActive(true);
                    m_AngerBtn.transform.parent.gameObject.SetActive(true);
                }
                else if(a_Hero.gameObject.name.Contains("Mage"))
                {
                    m_MeteorShowerBtn.transform.parent.gameObject.SetActive(true);
                    m_ClawBtn.transform.parent.gameObject.SetActive(true);
                }
                else if(a_Hero.gameObject.name.Contains("Healer"))
                {
                    m_HealBtn.transform.parent.gameObject.SetActive(true);
                    m_CharmBtn.transform.parent.gameObject.SetActive(true);
                }
                
            }
        }

    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    void AllPanelActivefalse()
    {
        if (m_StorkeHitPanel.activeSelf == true)
            m_StorkeHitPanel.SetActive(false);
        if (m_AngerPanel.activeSelf == true)
            m_AngerPanel.SetActive(false);
        if (m_ClawPanel.activeSelf == true)
            m_ClawPanel.SetActive(false);
        if (m_MeteorShowerPanel.activeSelf == true)
            m_MeteorShowerPanel.SetActive(false);
        if (m_HealPanel.activeSelf == true)
            m_HealPanel.SetActive(false);
        if (m_CharmPanel.activeSelf == true)
            m_CharmPanel.SetActive(false);
    }

    void UseSkillBtnClick(GameObject a_Skill)
    {
        Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

        for (int ii = 0;  ii < m_Skill.Length; ii++)
        {//비어있는 스킬 창을 찾아서 넣어주기
            Button a_SkillBtn = m_Skill[ii].GetComponentInChildren<Button>();

            if(a_SkillBtn == null)
            {//스킬창이 비어있으면
                GameObject a_SkillObj = Instantiate(a_Skill) as GameObject;
                a_SkillObj.transform.SetParent(m_Skill[ii], false);

                return;
            }
        }
    }
}
