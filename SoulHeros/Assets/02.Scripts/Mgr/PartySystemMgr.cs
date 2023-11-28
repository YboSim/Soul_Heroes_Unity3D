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

    bool m_AlreadyHasParty = false; //파티 소속 여부
    bool m_Inviting = false;        //파티 수락,거부 결정 중
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
        //m_InviteMsgBox 위치 조절
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
        //m_InviteMsgBox 위치 조절

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

        if (m_AlreadyHasParty == true) //파티에 소속되어 있는 상태이면
            return;

        Player[] a_Players = PhotonNetwork.PlayerListOthers; //나를 제외한 현재방에 참가한 플레이어 리스트

        for (int ii = 0; ii < a_Players.Length; ii++)
        {
            if (m_AroundPlayerInviteBtn[ii].activeSelf == false)
                m_AroundPlayerInviteBtn[ii].SetActive(true);

            m_AroundPlayerInviteBtn[ii].GetComponent<AroundPlayerInviteBtn>().m_Player = a_Players[ii];

            m_AroundPlayerInviteBtn[ii].GetComponentInChildren<Text>().text
                = a_Players[ii].NickName; //닉네임 표시
        }
    }


    [PunRPC]
    void RecieveInviteMsg(Player a_Inviter) //Local이아닌 초대를 받는Pc에서 호출 될 RPC 함수
    {
        if (m_AlreadyHasParty == true) //이미 파티에 가입되어 있으면
            return;

        m_Inviting = true; //m_InviteMsgBox 보이게 하기 
        m_Timer = 5.0f;

        m_InviteMsgBox.GetComponentInChildren<Text>().text =
              a_Inviter.NickName + "님 파티초대에 수락하시겠습니까?";

        m_PartyMemberList = new List<Player>(); //초기화

        //파티 멤버 구성
        m_PartyMemberList.Add(PhotonNetwork.LocalPlayer);
        m_PartyMemberList.Add(a_Inviter);
        //파티 멤버 구성
    }

    public void InviteAcceptRejectBtnClick(bool a_Accept)
    {
        if (a_Accept == true) //파티 수락
        {
            //InviteAccept(); //초대를 한 Pc에서 호출 되어야 할 함수

            //초대를 받은 Pc에서 호출 되어야 할 함수
            for (int ii = 0; ii < m_PartyMemberList.Count; ii++)
            {
                //if (m_PartyMemberList[ii] != PhotonNetwork.LocalPlayer) // 나를 제외한
                    m_PhotonView.RPC("InviteAccept", m_PartyMemberList[ii]);
            }
            //초대를 받은 Pc에서 호출 되어야 할 함수
        }
        else //파티 거절
        {
            InviteReject();
        }
    }

    [PunRPC]
    void InviteAccept()//초대를 한 Pc에서 호출 되어야 할 함수
    {
        Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

        m_AlreadyHasParty = true; //파티 소속 여부 활성화

        PartyMemberRefresh();

        m_Timer = 0.1f; // InviteMsgBox 끄기
    }

    void InviteReject()
    {
        Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

        m_Timer = 0.1f; // InviteMsgBox 끄기
    }

    void PartyMemberRefresh()
    {
        // 파티원들의 닉네임 표시해 주기, 정보 담기
        for (int ii = 0; ii < m_PartyMemberList.Count; ii++)
        {
            m_PartyMember[ii].GetComponent<PartyMember>().m_Player = m_PartyMemberList[ii];

            if (m_PartyMember[ii].activeSelf == false)
                m_PartyMember[ii].SetActive(true);

            m_PartyMember[ii].GetComponentInChildren<Text>().text = m_PartyMemberList[ii].NickName;
        }
        // 파티원들의 닉네임 표시해 주기, 정보 담기
    }

    [PunRPC]
    void PartyBreakUpBtnClick()
    {
        //동맹에서 적대로 타입 변환
        HeroCtrl[] a_Heroes = GameObject.FindObjectsOfType<HeroCtrl>();

        foreach (HeroCtrl a_Hero in a_Heroes)
        {
            if (a_Hero.m_ObjectInfo.m_ObjType == ObjectType.Hero_ally)
            {
                a_Hero.m_ObjectInfo.m_ObjType = ObjectType.Hero;
                break;
            }
        }
        //동맹에서 적대로 타입 변환

        //파티멤버 끄기
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
        //파티멤버 끄기

        m_AlreadyHasParty = false;
    }

}
