using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSceneMgr : MonoBehaviour
{
    public GameObject[] m_Character;
    public GameObject[] m_ClassInfoPanel;

    public InputField m_NickNameInputField;
    public Button m_LogOutBtn;
    public Button m_NickNameCheckBtn;
    public Button m_CreateCharacterBtn;
    public Text m_InfoText;

    public bool m_NickNameCheckOk = false;

    int m_ClassIdx = 0; //선택클래스를 저장해놓을 변수

    // Start is called before the first frame update
    void Start()
    {
        if (m_NickNameCheckBtn != null)
            m_NickNameCheckBtn.onClick.AddListener(NickNameCheckBtnClick) ;

        if (m_CreateCharacterBtn != null)
            m_CreateCharacterBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

                if (m_NickNameCheckOk == false)
                    m_InfoText.text = "닉네임 중복확인 후 생성해야 합니다.";
                else
                {
                    if (m_ClassIdx == 0)
                    {
                        PlayfabMgr.inst.SetPlayerData("Class", "Knigt");
                        PlayfabMgr.inst.m_Data.Add("Class", "Knigt");
                    }
                    else if (m_ClassIdx == 1)
                    {
                        PlayfabMgr.inst.SetPlayerData("Class", "Mage");
                        PlayfabMgr.inst.m_Data.Add("Class", "Mage");
                    }
                    else if (m_ClassIdx == 2)
                    {
                        PlayfabMgr.inst.SetPlayerData("Class", "Healer");
                        PlayfabMgr.inst.m_Data.Add("Class", "Healer");
                    }

                    LoadSceneMgr.LoadScene("InGame");
                }
            });

        if (m_LogOutBtn != null)
            m_LogOutBtn.onClick.AddListener(() =>
            {
                Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);
                LoadSceneMgr.LoadScene("TitleScene");
            });
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
    
    public void ClassBtnClick(int a_Idx)
    {
        for(int ii = 0; ii < m_Character.Length; ii++)
        {
            if(ii == a_Idx)
            {
                m_Character[ii].SetActive(true);
                m_ClassInfoPanel[ii].SetActive(true);
            }
            else
            {
                m_Character[ii].SetActive(false);
                m_ClassInfoPanel[ii].SetActive(false);
            }
        }

        m_ClassIdx = a_Idx;

        Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);
    }

    void NickNameCheckBtnClick()
    {
        PlayfabMgr.inst.NickNameCheck(m_NickNameInputField.text);
        PhotonNetwork.LocalPlayer.NickName = GlobalValue.m_NickName; //닉네임 저장

        Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);
    }
}
