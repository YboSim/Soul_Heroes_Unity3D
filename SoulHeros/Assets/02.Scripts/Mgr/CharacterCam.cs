using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCam : MonoBehaviour
{
    public GameObject m_Target;
    Vector3 m_TargetPos = Vector3.zero;
    float m_TargetCamDist = 2.0f; //�÷��̾�� ī�޶� �Ÿ�
    Vector3 m_CamForward = Vector3.zero; //ī�޶� �ٶ󺸴� ����
    Vector3 m_CamPos = Vector3.zero; //ī�޶� ��ġ
    float m_CamSpeed = 30.0f; //ī�޶� �̵� ���� �ӵ�

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
        //ī�޶� �ٶ󺸴� ����� Ÿ���� �ٶ󺸴� ���� ��ġ��Ű��

        //ī�޶� ��ġ �����Ͽ� �̵� �� ȸ��
        m_CamPos = m_TargetPos + m_CamForward * m_TargetCamDist;
        //transform.position = m_CamPos;
        transform.position = Vector3.Lerp(transform.position,
                                m_CamPos, m_CamSpeed * Time.deltaTime);

        transform.LookAt(m_TargetPos);
        //ī�޶� ��ġ �����Ͽ� �̵� �� ȸ��
    }

}
