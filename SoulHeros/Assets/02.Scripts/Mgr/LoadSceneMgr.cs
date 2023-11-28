using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneMgr : MonoBehaviour
{
    public static string m_NextScene; //불러올 씬 저장할 변수
    public Image m_ProgressBar;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneProgress());
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public static void LoadScene(string a_NextScene)
    {
        m_NextScene = a_NextScene;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadSceneProgress()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(m_NextScene);
        op.allowSceneActivation = false;  //로딩속도가 빨라서 false로 설정

        float a_Timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f) // 진행도 90퍼 이하
            {
                m_ProgressBar.fillAmount = op.progress;
            }
            else
            {
                a_Timer += Time.unscaledDeltaTime;
                m_ProgressBar.fillAmount = Mathf.Lerp(0.9f, 1.0f, a_Timer);
                if (m_ProgressBar.fillAmount >= 1.0f)
                {
                    op.allowSceneActivation = true; //화면전환
                    yield break;
                }
            }

        }
    }
}
