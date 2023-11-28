using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRotMgr : MonoBehaviour
{
    float m_MouseX;
    float m_RotSpeed = 10.0f;

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        RoatetUpdate();
    }

    void RoatetUpdate()
    {
        if (Input.GetMouseButton(0) == true)
        {
            m_MouseX = Input.GetAxis("Mouse X") / 2;

            transform.Rotate(0, m_MouseX * m_RotSpeed, 0, Space.World);
        }
    }
}
