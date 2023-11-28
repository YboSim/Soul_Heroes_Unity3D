using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCam : MonoBehaviour
{
    public GameObject m_Target;
    Vector3 m_TargetPos = Vector3.zero;
    float m_TargetCamDist = 2.0f; //플레이어와 카메라간 거리
    Vector3 m_CamForward = Vector3.zero; //카메라가 바라보는 방향
    Vector3 m_CamPos = Vector3.zero; //카메라 위치
    float m_CamSpeed = 30.0f; //카메라 이동 보간 속도

    // Start is called before the first frame update
    void Start()
    {
        m_TargetPos = m_Target.transform.position;
        m_CamForward = m_Target.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        CamPosUpdate();
    }

    void CamPosUpdate()
    {
        m_TargetPos = m_Target.transform.position;
        m_CamForward = m_Target.transform.forward;
        //카메라 바라보는 방향과 타겟의 바라보는 방향 일치시키기

        //카메라 위치 보간하여 이동 및 회전
        m_CamPos = m_TargetPos + m_CamForward * m_TargetCamDist;
        //transform.position = m_CamPos;
        transform.position = Vector3.Lerp(transform.position,
                                m_CamPos, m_CamSpeed * Time.deltaTime);

        transform.LookAt(m_TargetPos);
        //카메라 위치 보간하여 이동 및 회전
    }

}
