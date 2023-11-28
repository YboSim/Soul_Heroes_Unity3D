using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healer_Heal : MonoBehaviour
{
    public GameObject m_HealEffect;
    public static float m_CoolTime;
    public static float m_Timer;
    public float m_ReqMp = 10.0f;
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

    public void Heal()
    {
        if (SkillSlotMgr.Inst.m_Editing == true)
            return;

        if (m_RefTimer > 0.0f)
            return;

        Healer_Heal.m_CoolTime = 10.0f;
        //스킬 슬롯에 있는 분노스킬을 전부찾아 쿨타임 돌려주기
        for (int ii = 0; ii < m_Player.m_Skills.Length; ii++)
        {
            Healer_Heal a_Anger = m_Player.m_Skills[ii].GetComponentInChildren<Healer_Heal>();
            if (a_Anger != null)
            {
                a_Anger.m_RefTimer = 10.0f;
            }
        }
        //스킬 슬롯에 있는 분노스킬을 전부찾아 쿨타임 돌려주기

        //스킬효과
        GameObject[] a_Heroes = GameObject.FindGameObjectsWithTag("Hero");
        foreach(GameObject a_HeroObj in a_Heroes)
        {
            HeroCtrl a_Hero = a_HeroObj.GetComponent<HeroCtrl>();
            if (a_Hero == null)
                continue;

            ObjectInfo a_ObjectInfo = a_Hero.GetComponent<ObjectInfo>();
            if (a_ObjectInfo.m_ObjType == ObjectType.Hero_ally || a_Hero.m_PhotonView.IsMine == true)
            {
                GameObject a_HealEffect = Instantiate(m_HealEffect) as GameObject;
                a_HealEffect.transform.SetParent(a_Hero.transform);
                a_HealEffect.transform.position = a_Hero.transform.position;

                float a_HealHp;
                if (a_Hero.m_CurHp + 30.0f >= a_Hero.m_MaxHp)
                    a_HealHp = a_Hero.m_MaxHp - a_Hero.m_CurHp;
                else
                    a_HealHp = 30.0f;

                a_Hero.m_PhotonView.RPC("TakeDamage", Photon.Pun.RpcTarget.AllBuffered, -a_HealHp);
            }
        }
        GameMgr.Inst.UIRefresh();
        //스킬효과

        if (m_Player.m_CurMp >= m_ReqMp)
        {
            m_Player.m_CurMp -= m_ReqMp;
            m_Player.m_MpBar.fillAmount = m_Player.m_CurMp / m_Player.m_MaxMp;
        }

        Sound_Mgr.Instance.PlayEffSound("Heal", 1.0f);
    }
}
