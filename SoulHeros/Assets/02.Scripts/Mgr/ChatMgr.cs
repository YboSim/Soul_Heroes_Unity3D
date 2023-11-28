using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatMgr : MonoBehaviour
{
    public GameObject m_ChatRoot;
    public InputField m_ChatIF;
    public Text m_ChatMsg;

    PhotonView pv;

    List<string> m_ChatList = new List<string>();
    //bool m_Enter = false;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        //ä�� ���� �ؽ�Ʈ �Է�
        if (Input.GetKeyDown(KeyCode.Return))
        { //����Ű�� ������ ��ǲ �ʵ� Ȱ��ȭ

            if (m_ChatRoot.activeSelf == true)
            {
                if (string.IsNullOrEmpty(m_ChatIF.text) == true)
                {
                    //m_Enter = false;
                    m_ChatRoot.SetActive(false);
                }
                else
                {
                    BroadcastingChat();
                    m_ChatIF.ActivateInputField();
                }
            }
            else
            {
                //m_Enter = true;
                m_ChatRoot.SetActive(true);
                m_ChatIF.ActivateInputField();
            }
        }
    }

    void BroadcastingChat()
    {
        string msg = "\n<color=#ffffff>[" +
                PhotonNetwork.LocalPlayer.NickName + "] " +
                m_ChatIF.text + "</color>";
        pv.RPC("LogMsg", RpcTarget.AllBuffered, msg);

        m_ChatIF.text = "";
    }

    [PunRPC]
    void LogMsg(string msg)
    {
        m_ChatList.Add(msg);
        if (20 < m_ChatList.Count)
            m_ChatList.RemoveAt(0);

        m_ChatMsg.text = "";
        for (int ii = 0; ii < m_ChatList.Count; ii++)
        {
            m_ChatMsg.text += m_ChatList[ii];
        }

        ////�α� �޽��� Text UI�� �ؽ�Ʈ�� �������� ǥ��
        //txtLogMsg.text = txtLogMsg.text + msg;
    }
}
