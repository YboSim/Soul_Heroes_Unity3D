using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldExpText : MonoBehaviour
{
    public Text m_GoldText = null;
    public Text m_ExpText = null;
    [HideInInspector] public float m_GoldVal = 0.0f;
    [HideInInspector] public float m_ExpVal = 0.0f;
    [HideInInspector] public Vector3 m_BaseWdPos = Vector3.zero;

    Animator m_RefAnimator = null;

    RectTransform CanvasRect;
    Vector2 ScreenPos = Vector2.zero;
    Vector2 WdScPos = Vector2.zero;
    Vector3 m_CacVec = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        if (m_GoldText != null && m_ExpText != null)
        {
            m_GoldText.text = "+ " + m_GoldVal.ToString("N0");
            m_ExpText.text = "+ " + m_ExpVal.ToString("N0");
        }

        m_RefAnimator = GetComponent<Animator>();
        if (m_RefAnimator != null)
        {
            AnimatorStateInfo a_AnimStateInfo = m_RefAnimator.GetCurrentAnimatorStateInfo(0);

            float a_LifeTime = a_AnimStateInfo.length;
            Destroy(gameObject, a_LifeTime);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //--World 좌표를 UGUI좌표로 환산해 주는 코드
        CanvasRect = GameMgr.Inst.m_Canvas.GetComponent<RectTransform>();
        ScreenPos = Camera.main.WorldToViewportPoint(m_BaseWdPos);
        WdScPos.x = ((ScreenPos.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f));
        WdScPos.y = ((ScreenPos.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f));
        transform.GetComponent<RectTransform>().anchoredPosition = WdScPos;
        //--World 좌표를 UGUI좌표로 환산해 주는 코드

        //카메라 컬링...
        m_CacVec = m_BaseWdPos - Camera.main.transform.position;
        if (m_CacVec.magnitude <= 0.0f)
        {  //데미지텍스트와 카메라가 같은 위치에 있어도 보일 필요 없음
            if (m_GoldText.gameObject.activeSelf == true)
                m_GoldText.gameObject.SetActive(false);
            if (m_ExpText.gameObject.activeSelf == true)
                m_ExpText.gameObject.SetActive(false);
        }
        else if (0.0f < Vector3.Dot(Camera.main.transform.forward, m_CacVec.normalized))
        {  //카메라 앞쪽에 있다는 뜻
            if (m_GoldText.gameObject.activeSelf == false)
                m_GoldText.gameObject.SetActive(true);
            if (m_ExpText.gameObject.activeSelf == false)
                m_ExpText.gameObject.SetActive(true);
        }
        else  //if (Vector3.Dot(Camera.main.transform.forward, m_CacVec.normalized) <= 0.0f)
        { //카메라 뒤쪽에 있다는 뜻
            if (m_GoldText.gameObject.activeSelf == true)
                m_GoldText.gameObject.SetActive(false);
            if (m_ExpText.gameObject.activeSelf == true)
                m_ExpText.gameObject.SetActive(false);
        }
        //카메라 컬링...
    }
}
