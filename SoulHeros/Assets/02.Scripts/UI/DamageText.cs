using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    [HideInInspector] public Text m_RefText = null;
    [HideInInspector] public int m_DamageVal = 0;
    [HideInInspector] public Vector3 m_BaseWdPos = Vector3.zero;

    Animator m_RefAnimator = null;

    RectTransform CanvasRect;
    Vector2 ScreenPos = Vector2.zero;
    Vector2 WdScPos = Vector2.zero;
    Vector3 m_CacVec = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        m_RefText = this.gameObject.GetComponentInChildren<Text>();
        if(m_RefText != null)
        {
            if (m_DamageVal > 0)
                m_RefText.text = "_" + m_DamageVal.ToString() + " Dmg";
            else if (m_DamageVal == 0)
                m_RefText.text = "0";
            else
                m_RefText.text = "+" + (-m_DamageVal).ToString() + " Heal";
        }

        m_RefAnimator = GetComponentInChildren<Animator>();
        if(m_RefAnimator != null)
        {
            AnimatorStateInfo a_AnimStateInfo = m_RefAnimator.GetCurrentAnimatorStateInfo(0); //애니메이션 재생 시간

            float a_LifeTime = a_AnimStateInfo.length;
            Destroy(gameObject, a_LifeTime); //재생완료 후 파괴
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
            if (m_RefText.gameObject.activeSelf == true)
                m_RefText.gameObject.SetActive(false);
        }
        else if (0.0f < Vector3.Dot(Camera.main.transform.forward, m_CacVec.normalized))
        {  //카메라 앞쪽에 있다는 뜻
            if (m_RefText.gameObject.activeSelf == false)
                m_RefText.gameObject.SetActive(true);
        }
        else  //if (Vector3.Dot(Camera.main.transform.forward, m_CacVec.normalized) <= 0.0f)
        { //카메라 뒤쪽에 있다는 뜻
            if (m_RefText.gameObject.activeSelf == true)
                m_RefText.gameObject.SetActive(false);
        }
        //카메라 컬링...
    }
}
