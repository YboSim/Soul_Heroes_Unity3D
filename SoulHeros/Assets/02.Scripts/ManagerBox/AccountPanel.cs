using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountPanel : MonoBehaviour
{
    public Text m_IdText;
    public Button m_LogOutBtn;

    // Start is called before the first frame update
    void Start()
    {
        m_IdText.text = "User ID : " + GlobalValue.m_NickName;

        if (m_LogOutBtn != null)
            m_LogOutBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

                PhotonInit.inst.m_SceneIdx = 2;

                PhotonInit.inst.LeaveRoom();
            });
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
