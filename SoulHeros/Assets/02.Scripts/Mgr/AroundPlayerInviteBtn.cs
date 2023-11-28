using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AroundPlayerInviteBtn : MonoBehaviour
{
    public Player m_Player;
    public Button m_InviteBtn;

    PartySystemMgr m_PartySystemMgr;

    // Start is called before the first frame update
    void Start()
    {
        m_PartySystemMgr = GameObject.FindObjectOfType<PartySystemMgr>();

        if (m_InviteBtn != null)
            m_InviteBtn.onClick.AddListener(InviteBtnClick);
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    void InviteBtnClick()
    {
        m_PartySystemMgr.m_PhotonView.RPC("RecieveInviteMsg", m_Player, PhotonNetwork.LocalPlayer);

        //�ʴ� �� �ʴ� ��ư�� ���ֱ�
        for(int ii = 0; ii < m_PartySystemMgr.m_AroundPlayerInviteBtn.Length; ii ++)
        {
            if (m_PartySystemMgr.m_AroundPlayerInviteBtn[ii].activeSelf == true)
                m_PartySystemMgr.m_AroundPlayerInviteBtn[ii].SetActive(false);
        }
        //�ʴ� �� �ʴ� ��ư�� ���ֱ�

        m_PartySystemMgr.m_PartyMemberList = new List<Player>(); // �ʱ�ȭ

        //��Ƽ ��� ����
        m_PartySystemMgr.m_PartyMemberList.Add(PhotonNetwork.LocalPlayer); 
        m_PartySystemMgr.m_PartyMemberList.Add(m_Player);
        //��Ƽ ��� ����
    }
}
