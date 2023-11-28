using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneMgr : MonoBehaviour
{
    public static string m_NextScene; //�ҷ��� �� ������ ����
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
        op.allowSceneActivation = false;  //�ε��ӵ��� ���� false�� ����

        float a_Timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f) // ���൵ 90�� ����
            {
                m_ProgressBar.fillAmount = op.progress;
            }
            else
            {
                a_Timer += Time.unscaledDeltaTime;
                m_ProgressBar.fillAmount = Mathf.Lerp(0.9f, 1.0f, a_Timer);
                if (m_ProgressBar.fillAmount >= 1.0f)
                {
                    op.allowSceneActivation = true; //ȭ����ȯ
                    yield break;
                }
            }

        }
    }
}
