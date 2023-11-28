using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonInit : MonoBehaviourPunCallbacks
{
    public static PhotonInit inst = null;

    public int m_SceneIdx = 0;

    public static PhotonInit Inst
    {
        get
        {
            if (inst == null)
            {
                return null;
            }
            return inst;
        }
    }

    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (!PhotonNetwork.IsConnected)
        {//포톤 클라우드 서버 접속
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public override void OnConnectedToMaster() //ConnectUsingSettings 성공 콜백 함수
    {
        //Debug.Log("서버 접속 완료");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() //JoinLobby 성공 콜백 함수
    {
        //Debug.Log("로비 접속 완료");
    }

    public void JoinInGameField() //플래이팹 로그인 성공 시 호출 되는 함수
    {
        PhotonNetwork.LocalPlayer.NickName = GlobalValue.m_NickName; //닉네임 저장

        PhotonNetwork.JoinRandomRoom();
        //Debug.Log("조인 인게임 필드");
    }

    public override void OnJoinRandomFailed(short returnCode, string message) // JoinRandomRoom 실패 시 호출되는 콜백 함수
    {
        //Debug.Log("방 없음");

        //생성 할 룸의 조건 설정
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 16;

        PhotonNetwork.CreateRoom("InGameField", roomOptions); //방 생성 및 입장(방이 없을 때)
    }

    public override void OnJoinedRoom() //CreateRoom, JoinRoom, JoinRandomRoom 성공 시 호출되는 콜백 함수
    {
        //Debug.Log("방 참가");

        foreach(var player in PhotonNetwork.CurrentRoom.Players)
        {
            //Debug.Log(player.Value.NickName);
        }

        //캐릭터,몬스터 생성
        if (PlayfabMgr.inst.m_Data.TryGetValue("Class", out string a_Class))
        {
            float a_RndX = Random.Range(-5.0f, 5.0f);
            float a_RndZ = Random.Range(-7.0f, 7.0f);

            Vector3 a_SpawnPos = GameMgr.Inst.m_SpawnPos.position;
            Vector3 a_RndSpawnPos = new Vector3(a_SpawnPos.x + a_RndX, a_SpawnPos.y, a_SpawnPos.z + a_RndZ);

            if (a_Class == "Knight")
                PhotonNetwork.Instantiate("Knight", a_RndSpawnPos, Quaternion.identity);
            else if (a_Class == "Mage")
                PhotonNetwork.Instantiate("Mage", a_RndSpawnPos, Quaternion.identity);
            else if (a_Class == "Healer")
                PhotonNetwork.Instantiate("Healer", a_RndSpawnPos, Quaternion.identity);
        }
        //캐릭터,몬스터 생성
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log(returnCode.ToString());
        Debug.Log(message);
    }

    public override void OnCreatedRoom()
    {
        MonSpawnMgr a_MS = FindObjectOfType<MonSpawnMgr>();
        for (int ii = 0; ii < a_MS.m_SATSpawnPos.Length; ii++)
        { //SlimeAndTurtle Spawn
            a_MS.SATSpawn();
        }
        for (int ii = 0; ii < a_MS.m_GAOSpawnPos.Length; ii++)
        { //GolemAndOrc Spawn
            a_MS.GAOSpawn();
        }

        //Slayer Spawn
        PhotonNetwork.InstantiateRoomObject("Slayer_Boss", a_MS.m_SlayerSpawnPos.position, Quaternion.identity);
    }

    public void LeaveRoom()
    {
        //마지막 사람이 방을 떠날 때 룸의 CustomProperties를 초기화 해 주어야 한다.
        if (PhotonNetwork.PlayerList != null && PhotonNetwork.PlayerList.Length <= 1)
        {
            if (PhotonNetwork.CurrentRoom != null)
                PhotonNetwork.CurrentRoom.CustomProperties.Clear();
        }

        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom() //LeaveRoom 성공 콜백 함수
    {
        if (m_SceneIdx == 1)
            LoadSceneMgr.LoadScene("StoreScene");
        else if (m_SceneIdx == 2)
            LoadSceneMgr.LoadScene("TitleScene");
    }

}
