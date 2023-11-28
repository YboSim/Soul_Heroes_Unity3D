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
        {//���� Ŭ���� ���� ����
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

    public override void OnConnectedToMaster() //ConnectUsingSettings ���� �ݹ� �Լ�
    {
        //Debug.Log("���� ���� �Ϸ�");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() //JoinLobby ���� �ݹ� �Լ�
    {
        //Debug.Log("�κ� ���� �Ϸ�");
    }

    public void JoinInGameField() //�÷����� �α��� ���� �� ȣ�� �Ǵ� �Լ�
    {
        PhotonNetwork.LocalPlayer.NickName = GlobalValue.m_NickName; //�г��� ����

        PhotonNetwork.JoinRandomRoom();
        //Debug.Log("���� �ΰ��� �ʵ�");
    }

    public override void OnJoinRandomFailed(short returnCode, string message) // JoinRandomRoom ���� �� ȣ��Ǵ� �ݹ� �Լ�
    {
        //Debug.Log("�� ����");

        //���� �� ���� ���� ����
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 16;

        PhotonNetwork.CreateRoom("InGameField", roomOptions); //�� ���� �� ����(���� ���� ��)
    }

    public override void OnJoinedRoom() //CreateRoom, JoinRoom, JoinRandomRoom ���� �� ȣ��Ǵ� �ݹ� �Լ�
    {
        //Debug.Log("�� ����");

        foreach(var player in PhotonNetwork.CurrentRoom.Players)
        {
            //Debug.Log(player.Value.NickName);
        }

        //ĳ����,���� ����
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
        //ĳ����,���� ����
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
        //������ ����� ���� ���� �� ���� CustomProperties�� �ʱ�ȭ �� �־�� �Ѵ�.
        if (PhotonNetwork.PlayerList != null && PhotonNetwork.PlayerList.Length <= 1)
        {
            if (PhotonNetwork.CurrentRoom != null)
                PhotonNetwork.CurrentRoom.CustomProperties.Clear();
        }

        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom() //LeaveRoom ���� �ݹ� �Լ�
    {
        if (m_SceneIdx == 1)
            LoadSceneMgr.LoadScene("StoreScene");
        else if (m_SceneIdx == 2)
            LoadSceneMgr.LoadScene("TitleScene");
    }

}
