using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    GameObject m_Player = null;
    Vector3 m_TargetPos = Vector3.zero;

    //CamPosUpdate/ZoomInOut 관련 변수
    float m_PlayerCamDist = 5.0f; //플레이어와 카메라간 거리
    float m_MaxDist = 10.0f; //플레이어와 카메라간 최대거리
    float m_MinDist = 2.0f; //플레이어와 카메라간 최소거리
    Vector3 m_CamForward = Vector3.zero; //카메라가 바라보는 방향
    Vector3 m_CamPos = Vector3.zero; //카메라 위치
    float m_CamHeight = 1.8f; //위로 카메라를 상승시켜줄 높이
    float m_CamSpeed = 5.0f; //카메라 이동 보간 속도
    float m_ZoomSpeed = 5.0f;
    //CamPosUpdate/ZoomInOut 관련 변수

    public void InitCamera(GameObject a_Player)
    {
        m_Player = a_Player;
    }

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void LateUpdate()
    {
        CamPosUpdate();
    }

    void CamPosUpdate()
    {
        if (m_Player == null)
            return;

        m_TargetPos = m_Player.transform.position;
        m_CamForward = m_Player.transform.forward;
        //카메라 바라보는 방향과 플레이어의 바라보는 방향 일치시키기

        CamZoomInOut();

        //카메라 위치 보간하여 이동 및 회전
        m_CamPos = m_TargetPos - m_CamForward * m_PlayerCamDist;
        m_CamPos.y = m_CamPos.y + m_CamHeight;
        transform.position = Vector3.Lerp(transform.position,
                                m_CamPos, m_CamSpeed *Time.deltaTime);
        
        transform.LookAt(m_TargetPos);
        //카메라 위치 보간하여 이동 및 회전
    }

    void CamZoomInOut()
    {
        float a_MouseSW = Input.GetAxis("Mouse ScrollWheel");
        m_PlayerCamDist -= a_MouseSW * m_ZoomSpeed;

        //카메라 와 플레이어간 최대,최소 거리 설정
        if (m_PlayerCamDist > m_MaxDist)
            m_PlayerCamDist = m_MaxDist;
        else if (m_PlayerCamDist < m_MinDist)
            m_PlayerCamDist = m_MinDist;
        //카메라 와 플레이어간 최대,최소 거리 설정
    }
}
