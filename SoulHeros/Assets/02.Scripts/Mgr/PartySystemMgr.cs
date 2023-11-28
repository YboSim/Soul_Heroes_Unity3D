using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class PartySystemMgr : MonoBehaviour
{
    public GameObject[] m_AroundPlayerInviteBtn;
    public GameObject[] m_PartyMember;
    public GameObject m_InviteMsgBox;
    public Button m_InviteMemberBtn;
    public Image m_TimerBar;

    [HideInInspector] public PhotonView m_PhotonView;
    [HideInInspector] public HeroCtrl m_Player;
    [HideInInspector] public List<Player> m_PartyMemberList = new List<Player>();

    bool m_AlreadyHasParty = false; //��Ƽ �Ҽ� ����
    bool m_Inviting = false;        //��Ƽ ����,�ź� ���� ��
    float m_Timer = 0.0f;
    float m_InviteMsgBoxOnPosX = 0.0f;
    float m_InviteMsgBoxOffPosX = -375.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_PhotonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        //m_InviteMsgBox ��ġ ����
        if (m_Inviting == true)
        {
            if (m_InviteMsgBox.transform.localPosition.x < m_InviteMsgBoxOnPosX)
            {
                m_InviteMsgBox.transform.localPosition += new Vector3(30.0f, 0.0f, 0.0f);
            }
        }
        else
        {
            if (m_InviteMsgBox.transform.localPosition.x > m_InviteMsgBoxOffPosX)
            {
                m_InviteMsgBox.transform.localPosition -= new Vector3(30.0f, 0.0f, 0.0f);
            }
        }
        //m_InviteMsgBox ��ġ ����

        if (m_Timer > 0.0f)
        {
            m_Timer -= Time.deltaTime;
            m_TimerBar.fillAmount = m_Timer / 5.0f;

            if (m_Timer < 0.0f)
            {
                m_Inviting = false;

                m_Timer = 0.0f;
            }
        }
    }

    public void InviteBtnClick()
    {
        Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

        if (m_AlreadyHasParty == true) //��Ƽ�� �ҼӵǾ� �ִ� �����̸�
            return;

        Player[] a_Players = PhotonNetwork.PlayerListOthers; //���� ������ ����濡 ������ �÷��̾� ����Ʈ

        for (int ii = 0; ii < a_Players.Length; ii++)
        {
            if (m_AroundPlayerInviteBtn[ii].activeSelf == false)
                m_AroundPlayerInviteBtn[ii].SetActive(true);

            m_AroundPlayerInviteBtn[ii].GetComponent<AroundPlayerInviteBtn>().m_Player = a_Players[ii];

            m_AroundPlayerInviteBtn[ii].GetComponentInChildren<Text>().text
                = a_Players[ii].NickName; //�г��� ǥ��
        }
    }


    [PunRPC]
    void RecieveInviteMsg(Player a_Inviter) //Local�̾ƴ� �ʴ븦 �޴�Pc���� ȣ�� �� RPC �Լ�
    {
        if (m_AlreadyHasParty == true) //�̹� ��Ƽ�� ���ԵǾ� ������
            return;

        m_Inviting = true; //m_InviteMsgBox ���̰� �ϱ� 
        m_Timer = 5.0f;

        m_InviteMsgBox.GetComponentInChildren<Text>().text =
              a_Inviter.NickName + "�� ��Ƽ�ʴ뿡 �����Ͻðڽ��ϱ�?";

        m_PartyMemberList = new List<Player>(); //�ʱ�ȭ

        //��Ƽ ��� ����
        m_PartyMemberList.Add(PhotonNetwork.LocalPlayer);
        m_PartyMemberList.Add(a_Inviter);
        //��Ƽ ��� ����
    }

    public void InviteAcceptRejectBtnClick(bool a_Accept)
    {
        if (a_Accept == true) //��Ƽ ����
        {
            //InviteAccept(); //�ʴ븦 �� Pc���� ȣ�� �Ǿ�� �� �Լ�

            //�ʴ븦 ���� Pc���� ȣ�� �Ǿ�� �� �Լ�
            for (int ii = 0; ii < m_PartyMemberList.Count; ii++)
            {
                //if (m_PartyMemberList[ii] != PhotonNetwork.LocalPlayer) // ���� ������
                    m_PhotonView.RPC("InviteAccept", m_PartyMemberList[ii]);
            }
            //�ʴ븦 ���� Pc���� ȣ�� �Ǿ�� �� �Լ�
        }
        else //��Ƽ ����
        {
            InviteReject();
        }
    }

    [PunRPC]
    void InviteAccept()//�ʴ븦 �� Pc���� ȣ�� �Ǿ�� �� �Լ�
    {
        Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

        m_AlreadyHasParty = true; //��Ƽ �Ҽ� ���� Ȱ��ȭ

        PartyMemberRefresh();

        m_Timer = 0.1f; // InviteMsgBox ����
    }

    void InviteReject()
    {
        Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

        m_Timer = 0.1f; // InviteMsgBox ����
    }

    void PartyMemberRefresh()
    {
        // ��Ƽ������ �г��� ǥ���� �ֱ�, ���� ���
        for (int ii = 0; ii < m_PartyMemberList.Count; ii++)
        {
            m_PartyMember[ii].GetComponent<PartyMember>().m_Player = m_PartyMemberList[ii];

            if (m_PartyMember[ii].activeSelf == false)
                m_PartyMember[ii].SetActive(true);

            m_PartyMember[ii].GetComponentInChildren<Text>().text = m_PartyMemberList[ii].NickName;
        }
        // ��Ƽ������ �г��� ǥ���� �ֱ�, ���� ���
    }

    [PunRPC]
    void PartyBreakUpBtnClick()
    {
        //���Ϳ��� ����� Ÿ�� ��ȯ
        HeroCtrl[] a_Heroes = GameObject.FindObjectsOfType<HeroCtrl>();

        foreach (HeroCtrl a_Hero in a_Heroes)
        {
            if (a_Hero.m_ObjectInfo.m_ObjType == ObjectType.Hero_ally)
            {
                a_Hero.m_ObjectInfo.m_ObjType = ObjectType.Hero;
                break;
            }
        }
        //���Ϳ��� ����� Ÿ�� ��ȯ

        //��Ƽ��� ����
        for (int ii = 0; ii < m_PartyMember.Length; ii++)
        {
            if (m_PartyMember[ii].activeSelf == true)
            {
                m_PartyMember[ii].SetActive(false);
            }
            else
            {
                break;
            }
        }
        //��Ƽ��� ����

        m_AlreadyHasParty = false;
    }

}
