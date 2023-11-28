using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginMgr : MonoBehaviour
{
    public InputField m_CA_IDInputfield;
    public InputField m_CA_PWInputfield;
    public InputField m_IDInputfield;
    public InputField m_PWInputfield;

    public Text m_InfoText;

    float m_ShowMsTimer;

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        if(m_ShowMsTimer > 0.0f)
        {
            m_ShowMsTimer -= Time.deltaTime;
            if(m_ShowMsTimer < 0.0f)
            {
                m_ShowMsTimer = 0.0f;
                m_InfoText.text = "계정과 암호를 입력해주세요";
            }
        }
    }

    public void LogInBtnClick()
    {
        string a_Id = m_IDInputfield.text;
        string a_Pw = m_PWInputfield.text;

        PlayfabMgr.inst.Login(a_Id, a_Pw);

        Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);
    }

    public void CreateAccountBtnClick()
    {
        string a_Id = m_CA_IDInputfield.text;
        string a_Pw = m_CA_PWInputfield.text;

        PlayfabMgr.inst.CreateAccount(a_Id, a_Pw);

        Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);
    }

    public void Message(string a_Msg)
    {
        m_InfoText.text = a_Msg;
        m_ShowMsTimer = 7.0f;
    }
}
