using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mage_MeteorShower : MonoBehaviour
{
    public GameObject m_MeteorShowerObj;
    public GameObject m_MsSkillRange;
    public static float m_CoolTime;
    public static float m_Timer;
    public float m_ReqMp = 5.0f;
    public float m_RefTimer;
    public Image m_CoolTimeImg;
    Skill m_Skill;
    HeroCtrl m_Player;
    public static bool m_MeteorShowerOn = false;
    public static bool m_CanUseSkill = false;
    Ray m_MsPickPos;
    RaycastHit hitInfo;
    public Vector3 m_SkillPos;

    // Start is called before the first frame update
    void Start()
    {
        m_MsSkillRange = GameObject.Find("SkillRangeRoot").transform.GetChild(0).gameObject;

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

        if(m_MeteorShowerOn == true)
        {
            m_MsPickPos = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(m_MsPickPos, out hitInfo))
            {
                if(hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Terrain"))
                {
                    if (m_MsSkillRange.activeSelf == false)
                        m_MsSkillRange.SetActive(true);
                    m_MsSkillRange.transform.position = hitInfo.point;
                }
                else if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Object"))
                {
                    if (m_MsSkillRange.activeSelf == false)
                        m_MsSkillRange.SetActive(true);
                    m_MsSkillRange.transform.position = hitInfo.collider.gameObject.transform.position;
                }
            }

            if(Input.GetMouseButtonDown(0))
            {
                m_Player.m_MoveMethod = MoveMethod.SkillMove;
                m_Player.m_SkillPos = hitInfo.point;
            }


            if(Input.GetKeyDown(KeyCode.Escape))
            {
                m_MsSkillRange.SetActive(false);
                m_MeteorShowerOn = false;
            }
        }

        if (m_CanUseSkill == true)
        {
            SkillActivation();
            m_CanUseSkill = false;
        }
    }

    public void MeteorShower()
    {
        if (SkillSlotMgr.Inst.m_Editing == true)
            return;

        if (m_RefTimer > 0.0f)
            return;

        m_MeteorShowerOn = true;
    }

    public void SkillActivation()
    {
        Instantiate(m_MeteorShowerObj, m_Player.m_SkillPos, Quaternion.identity);

        Mage_MeteorShower.m_CoolTime = 14.0f;
        //스킬 슬롯에 있는 분노스킬을 전부찾아 쿨타임 돌려주기
        for (int ii = 0; ii < m_Player.m_Skills.Length; ii++)
        {
            Mage_MeteorShower a_MeteorShower = m_Player.m_Skills[ii].GetComponentInChildren<Mage_MeteorShower>();
            if (a_MeteorShower != null)
            {
                a_MeteorShower.m_RefTimer = 14.0f;
            }
        }
        //스킬 슬롯에 있는 분노스킬을 전부찾아 쿨타임 돌려주기

        if (m_Player.m_CurMp >= m_ReqMp)
        {
            m_Player.m_CurMp -= m_ReqMp;
            m_Player.m_MpBar.fillAmount = m_Player.m_CurMp / m_Player.m_MaxMp;
        }

        m_Player.m_MoveMethod = MoveMethod.AttackMove;

        m_MsSkillRange.SetActive(false);
        m_MeteorShowerOn = false;
    }
}
