using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    GameObject m_Player = null;
    Vector3 m_TargetPos = Vector3.zero;

    //CamPosUpdate/ZoomInOut ���� ����
    float m_PlayerCamDist = 5.0f; //�÷��̾�� ī�޶� �Ÿ�
    float m_MaxDist = 10.0f; //�÷��̾�� ī�޶� �ִ�Ÿ�
    float m_MinDist = 2.0f; //�÷��̾�� ī�޶� �ּҰŸ�
    Vector3 m_CamForward = Vector3.zero; //ī�޶� �ٶ󺸴� ����
    Vector3 m_CamPos = Vector3.zero; //ī�޶� ��ġ
    float m_CamHeight = 1.8f; //���� ī�޶� ��½����� ����
    float m_CamSpeed = 5.0f; //ī�޶� �̵� ���� �ӵ�
    float m_ZoomSpeed = 5.0f;
    //CamPosUpdate/ZoomInOut ���� ����

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
        //ī�޶� �ٶ󺸴� ����� �÷��̾��� �ٶ󺸴� ���� ��ġ��Ű��

        CamZoomInOut();

        //ī�޶� ��ġ �����Ͽ� �̵� �� ȸ��
        m_CamPos = m_TargetPos - m_CamForward * m_PlayerCamDist;
        m_CamPos.y = m_CamPos.y + m_CamHeight;
        transform.position = Vector3.Lerp(transform.position,
                                m_CamPos, m_CamSpeed *Time.deltaTime);
        
        transform.LookAt(m_TargetPos);
        //ī�޶� ��ġ �����Ͽ� �̵� �� ȸ��
    }

    void CamZoomInOut()
    {
        float a_MouseSW = Input.GetAxis("Mouse ScrollWheel");
        m_PlayerCamDist -= a_MouseSW * m_ZoomSpeed;

        //ī�޶� �� �÷��̾ �ִ�,�ּ� �Ÿ� ����
        if (m_PlayerCamDist > m_MaxDist)
            m_PlayerCamDist = m_MaxDist;
        else if (m_PlayerCamDist < m_MinDist)
            m_PlayerCamDist = m_MinDist;
        //ī�޶� �� �÷��̾ �ִ�,�ּ� �Ÿ� ����
    }
}
