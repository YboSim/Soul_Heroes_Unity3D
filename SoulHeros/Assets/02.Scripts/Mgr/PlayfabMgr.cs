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
        Debug.Log("저장실패");
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
        Debug.Log("불러오기실패");
    }

    public void Login(string a_Id, string a_Pw)
    {
        a_Id = a_Id.Trim();
        a_Pw = a_Pw.Trim();

        if (string.IsNullOrEmpty(a_Id) == true ||
           string.IsNullOrEmpty(a_Pw) == true)
        {
            m_LoginMgr.Message("ID, PW 빈칸 없이 입력해 주세요.");
            return;
        }

        if (!(6 <= a_Id.Length && a_Id.Length < 20)) //6 ~ 20
        {
            m_LoginMgr.Message("ID는 6글자 이상 20글자 이하로 작성해 주세요.");
            return;
        }

        if (!(6 <= a_Pw.Length && a_Pw.Length < 20)) //6 ~ 20
        {
            m_LoginMgr.Message("비밀번호는 6글자 이상 20글자 이하로 작성해 주세요.");
            return;
        }

        //--- 로그인 성공시 어떤 유저 정보를 가져올지를 설정하는 옵션 객체 생성
        var option = new GetPlayerCombinedInfoRequestParams()
        {
            //--- DisplayName(닉네임)을 가져오기 위한 옵션
            GetPlayerProfile = true,
            ProfileConstraints = new PlayerProfileViewConstraints()
            {
                ShowDisplayName = true,  //DisplayName(닉네임) 가져오기 위한 요청 옵션
                //ShowAvatarUrl = true     //AvatarUrl 을 가져오는 옵션
            },
            //--- DisplayName(닉네임)을 가져오기 위한 옵션

            //--- BestScore 통계값(순위표에 관여하는)을 불러올 수 있는 옵션
            GetPlayerStatistics = true,

            //--- < 플레이어 데이터(타이틀) > 값을 불러올 수 있게 하는 옵션
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
        m_LoginMgr.Message("로그인 성공");

        GlobalValue.m_ID = result.PlayFabId;

        if (result.InfoResultPayload != null)
        {
            GlobalValue.m_NickName = result.InfoResultPayload.PlayerProfile.DisplayName;
        }

        if (result.InfoResultPayload.PlayerProfile.DisplayName == null) //게임 처음시작이면
            LoadSceneMgr.LoadScene("CharacterScene");//캐릭터 선택 씬으로
        else //게임을 플레이했던 유저면
        {
            GetPlayerData(); //유저 데이터 불러오기
            GlobalValue.m_NickName = result.InfoResultPayload.PlayerProfile.DisplayName;

            LoadSceneMgr.LoadScene("InGame"); //인게임 씬으로
        }
    }

    void OnLoginFailure(PlayFabError error)
    {
        m_LoginMgr.Message("로그인 실패");
    }

    public void CreateAccount(string a_Id, string a_Pw)
    {
        a_Id = a_Id.Trim();
        a_Pw = a_Pw.Trim();

        if (string.IsNullOrEmpty(a_Id) == true ||
           string.IsNullOrEmpty(a_Pw) == true)
        {
            m_LoginMgr.Message("ID, PW, 별명은 빈칸 없이 입력해 주세요.");
            return;
        }

        if (!(6 <= a_Id.Length && a_Id.Length < 20)) //6 ~ 20
        {
            m_LoginMgr.Message("ID는 6글자 이상 20글자 이하로 작성해 주세요.");
            return;
        }

        if (!(6 <= a_Pw.Length && a_Pw.Length < 20)) //6 ~ 20
        {
            m_LoginMgr.Message("비밀번호는 6글자 이상 20글자 이하로 작성해 주세요.");
            return;
        }

        var request = new RegisterPlayFabUserRequest
        {
            Email = a_Id,
            Password = a_Pw,
            RequireBothUsernameAndEmail = false  //Email을 기본 ID로 사용하겠다는 옵션
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, RegisterSuccess, RegisterFailure);

    }//void CreateAccountBtn()

    void RegisterSuccess(RegisterPlayFabUserResult result)
    {
        m_LoginMgr.Message("가입 성공");
    }

    void RegisterFailure(PlayFabError error)
    {
        m_LoginMgr.Message("가입 실패 : " + error.GenerateErrorReport());
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
            m_CharacterSceneMgr.m_InfoText.text = "중복확인 완료";
        }
    }

    void DisplayNameUpdateFailure()
    {
        if (m_CharacterSceneMgr != null)
            m_CharacterSceneMgr.m_InfoText.text = "잘못된 닉네임 이거나 이미 존재하는 닉네임 입니다.";
    }
}
