using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mage_Claw : MonoBehaviour
{
    public static float m_CoolTime;
    public static float m_Timer;
    public float m_ReqMp = 3.0f;
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

    public void Claw()
    {
        if (SkillSlotMgr.Inst.m_Editing == true)
            return;

        if (m_RefTimer > 0.0f)
            return; //스킬쿨이면 리턴

        if (m_Player.m_CurMp >= m_ReqMp && m_Player.m_KnightStrokeHit == false)
        {
            m_Player.m_CurMp -= m_ReqMp;
            m_Player.m_MpBar.fillAmount = m_Player.m_CurMp / m_Player.m_MaxMp;
        }

        m_Player.m_MagicClaw = true;

        if (m_Player.m_MagicClawObj2 != null)
            m_Player.m_MagicClawObj2.SetActive(true);
    }
}
