using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyMember : MonoBehaviour
{
    PartySystemMgr m_PartySystemMgr;
    public Player m_Player;
    public Button m_PartyBreakUpBtn;
    public Image m_HpBar;
    public HeroCtrl m_Hero;

    // Start is called before the first frame update
    void Start()
    {
        m_PartySystemMgr = GameObject.FindObjectOfType<PartySystemMgr>();

        HeroCtrl[] a_Heroes = GameObject.FindObjectsOfType<HeroCtrl>();

        foreach(HeroCtrl a_Hero in a_Heroes)
        {
            if (a_Hero.m_PhotonView.Owner.NickName == m_Player.NickName)
            {
                m_Hero = a_Hero;
                m_Hero.m_ObjectInfo.m_ObjType = ObjectType.Hero_ally;
                break;
            }
        }

        if (m_PartyBreakUpBtn != null)
            m_PartyBreakUpBtn.onClick.AddListener(PartyBreakUpBtnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Hero != null)
            m_HpBar.fillAmount = m_Hero.m_CurHp / m_Hero.m_MaxHp;
    }

    void PartyBreakUpBtnClick()
    {
        Debug.Log(m_PartySystemMgr.m_PartyMemberList.Count);

        for (int ii = 0; ii < m_PartySystemMgr.m_PartyMemberList.Count; ii++)
        {
            Debug.Log(m_PartySystemMgr.m_PartyMemberList[ii].NickName);
            m_PartySystemMgr.m_PhotonView.RPC("PartyBreakUpBtnClick", m_PartySystemMgr.m_PartyMemberList[ii]);
        }
    }
}
