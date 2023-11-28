using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Knight_Anger : MonoBehaviour
{
    public static float m_CoolTime;
    public static float m_Timer;
    public float m_ReqMp = 5.0f;
    public float m_RefTimer;
    public Image m_CoolTimeImg;
    Skill m_Skill;
    HeroCtrl m_Player;

    // Start is called before the first frame update
    void Start()
    {
        m_Skill = GetComponent<Skill>();
        m_Player = GameMgr.Inst.m_Player;
        m_Skill.m_SkillIconImg = GetComponent<Image>();

        m_RefTimer = m_Timer;
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer = m_RefTimer;

        if (m_RefTimer > 0.0f)
        {
            m_CoolTimeImg.gameObject.SetActive(true);

            m_CoolTimeImg.fillAmount = m_Timer / m_CoolTime;

            m_RefTimer -= Time.deltaTime;

            if (m_RefTimer < 0.0f)
            {
                m_CoolTimeImg.gameObject.SetActive(false);

                m_RefTimer = 0.0f;
            }
        }
    }

    public void Anger()
    {
        if (SkillSlotMgr.Inst.m_Editing == true)
            return;

        if (m_RefTimer > 0.0f)
            return;

        Knight_Anger.m_CoolTime = 20.0f;
        //스킬 슬롯에 있는 분노스킬을 전부찾아 쿨타임 돌려주기
        for (int ii = 0; ii < m_Player.m_Skills.Length; ii++)
        {
            Knight_Anger a_Anger = m_Player.m_Skills[ii].GetComponentInChildren<Knight_Anger>();
            if (a_Anger != null)
            {
                a_Anger.m_RefTimer = 20.0f;
            }
        }
        //스킬 슬롯에 있는 분노스킬을 전부찾아 쿨타임 돌려주기

        //스킬효과
        if (m_Player != null)
            m_Player.m_KnightBuff.SetActive(true);

        GlobalValue.m_Att += 10;
        GlobalValue.m_Def += 20;

        GameMgr.Inst.UIRefresh();
        //스킬효과

        if (m_Player.m_CurMp >= m_ReqMp)
        {
            m_Player.m_CurMp -= m_ReqMp;
            m_Player.m_MpBar.fillAmount = m_Player.m_CurMp / m_Player.m_MaxMp;
        }

        m_Player.m_KnightBuffTimer = 10.0f; //지속시간

        Sound_Mgr.Instance.PlayEffSound("Anger", 1.0f);
    }
}
