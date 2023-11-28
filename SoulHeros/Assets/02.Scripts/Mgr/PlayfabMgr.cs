using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayfabMgr : MonoBehaviour
{
    public static PlayfabMgr inst = null;
    public Dictionary<string, string> m_Data = new Dictionary<string, string>();

    public static PlayfabMgr Inst
    {
        get
        {
            if(inst == null)
            {
                return null;
            }
            return inst;
        }
    }

    private void Awake()
    {
        if(inst == null)
        {
            inst = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    PhotonInit m_PhotonInit;
    LoginMgr m_LoginMgr;
    CharacterSceneMgr m_CharacterSceneMgr;

    // Start is called before the first frame update
    void Start()
    {
        m_LoginMgr = GameObject.FindObjectOfType<LoginMgr>();
        m_PhotonInit = GameObject.FindObjectOfType<PhotonInit>();
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public void SetPlayerData(string a_Key, string a_Value)
    {
        var request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {a_Key,a_Value}
            }
        };

        PlayFabClientAPI.UpdateUserData(request,
            (result)=> SetDataSuccess(),
            (error)=> SetDataFailure());
    }

    void SetDataSuccess()
    {

    }

    void SetDataFailure()
    {
        Debug.Log("�������");
    }

    public void GetPlayerData()
    {
        var request = new GetUserDataRequest()
        {
            PlayFabId = GlobalValue.m_ID
        };

        PlayFabClientAPI.GetUserData(request,
            (result) =>
            {
                foreach (var eachData in result.Data)
                {
                    m_Data.Add(eachData.Key, eachData.Value.Value);
                }
            },
            (error) => GetDataFailure());
    }

    void GetDataFailure()
    {
        Debug.Log("�ҷ��������");
    }

    public void Login(string a_Id, string a_Pw)
    {
        a_Id = a_Id.Trim();
        a_Pw = a_Pw.Trim();

        if (string.IsNullOrEmpty(a_Id) == true ||
           string.IsNullOrEmpty(a_Pw) == true)
        {
            m_LoginMgr.Message("ID, PW ��ĭ ���� �Է��� �ּ���.");
            return;
        }

        if (!(6 <= a_Id.Length && a_Id.Length < 20)) //6 ~ 20
        {
            m_LoginMgr.Message("ID�� 6���� �̻� 20���� ���Ϸ� �ۼ��� �ּ���.");
            return;
        }

        if (!(6 <= a_Pw.Length && a_Pw.Length < 20)) //6 ~ 20
        {
            m_LoginMgr.Message("��й�ȣ�� 6���� �̻� 20���� ���Ϸ� �ۼ��� �ּ���.");
            return;
        }

        //--- �α��� ������ � ���� ������ ���������� �����ϴ� �ɼ� ��ü ����
        var option = new GetPlayerCombinedInfoRequestParams()
        {
            //--- DisplayName(�г���)�� �������� ���� �ɼ�
            GetPlayerProfile = true,
            ProfileConstraints = new PlayerProfileViewConstraints()
            {
                ShowDisplayName = true,  //DisplayName(�г���) �������� ���� ��û �ɼ�
                //ShowAvatarUrl = true     //AvatarUrl �� �������� �ɼ�
            },
            //--- DisplayName(�г���)�� �������� ���� �ɼ�

            //--- BestScore ��谪(����ǥ�� �����ϴ�)�� �ҷ��� �� �ִ� �ɼ�
            GetPlayerStatistics = true,

            //--- < �÷��̾� ������(Ÿ��Ʋ) > ���� �ҷ��� �� �ְ� �ϴ� �ɼ�
            GetUserData = true
        };

        var request = new LoginWithEmailAddressRequest
        {
            Email = a_Id,
            Password = a_Pw,
            InfoRequestParameters = option
        };

        PlayFabClientAPI.LoginWithEmailAddress(request,
                                        OnLoginSuccess, OnLoginFailure);

        //SceneManager.LoadScene("scLobby");
    }
    void OnLoginSuccess(LoginResult result)
    {
        m_LoginMgr.Message("�α��� ����");

        GlobalValue.m_ID = result.PlayFabId;

        if (result.InfoResultPayload != null)
        {
            GlobalValue.m_NickName = result.InfoResultPayload.PlayerProfile.DisplayName;
        }

        if (result.InfoResultPayload.PlayerProfile.DisplayName == null) //���� ó�������̸�
            LoadSceneMgr.LoadScene("CharacterScene");//ĳ���� ���� ������
        else //������ �÷����ߴ� ������
        {
            GetPlayerData(); //���� ������ �ҷ�����
            GlobalValue.m_NickName = result.InfoResultPayload.PlayerProfile.DisplayName;

            LoadSceneMgr.LoadScene("InGame"); //�ΰ��� ������
        }
    }

    void OnLoginFailure(PlayFabError error)
    {
        m_LoginMgr.Message("�α��� ����");
    }

    public void CreateAccount(string a_Id, string a_Pw)
    {
        a_Id = a_Id.Trim();
        a_Pw = a_Pw.Trim();

        if (string.IsNullOrEmpty(a_Id) == true ||
           string.IsNullOrEmpty(a_Pw) == true)
        {
            m_LoginMgr.Message("ID, PW, ������ ��ĭ ���� �Է��� �ּ���.");
            return;
        }

        if (!(6 <= a_Id.Length && a_Id.Length < 20)) //6 ~ 20
        {
            m_LoginMgr.Message("ID�� 6���� �̻� 20���� ���Ϸ� �ۼ��� �ּ���.");
            return;
        }

        if (!(6 <= a_Pw.Length && a_Pw.Length < 20)) //6 ~ 20
        {
            m_LoginMgr.Message("��й�ȣ�� 6���� �̻� 20���� ���Ϸ� �ۼ��� �ּ���.");
            return;
        }

        var request = new RegisterPlayFabUserRequest
        {
            Email = a_Id,
            Password = a_Pw,
            RequireBothUsernameAndEmail = false  //Email�� �⺻ ID�� ����ϰڴٴ� �ɼ�
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, RegisterSuccess, RegisterFailure);

    }//void CreateAccountBtn()

    void RegisterSuccess(RegisterPlayFabUserResult result)
    {
        m_LoginMgr.Message("���� ����");
    }

    void RegisterFailure(PlayFabError error)
    {
        m_LoginMgr.Message("���� ���� : " + error.GenerateErrorReport());
    }

    public void NickNameCheck(string a_NickName)
    {
        if (a_NickName == null || a_NickName == "")
            return;

        m_CharacterSceneMgr = GameObject.FindObjectOfType<CharacterSceneMgr>();

        a_NickName = a_NickName.Trim();

        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = a_NickName
        };

        GlobalValue.m_NickName = a_NickName;

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, 
            (result)=>DisplayNameUpdateSuccess(),
            (error) =>DisplayNameUpdateFailure());

    }

    void DisplayNameUpdateSuccess()
    {
        if (m_CharacterSceneMgr != null)
        {
            m_CharacterSceneMgr.m_NickNameCheckOk = true;
            m_CharacterSceneMgr.m_InfoText.text = "�ߺ�Ȯ�� �Ϸ�";
        }
    }

    void DisplayNameUpdateFailure()
    {
        if (m_CharacterSceneMgr != null)
            m_CharacterSceneMgr.m_InfoText.text = "�߸��� �г��� �̰ų� �̹� �����ϴ� �г��� �Դϴ�.";
    }
}
