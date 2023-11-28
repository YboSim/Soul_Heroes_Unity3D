using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotMgr : MonoBehaviour
{
    public Button m_EditBtn;
    public Image m_EditOnBtnImg;
    public Image m_EditOffBtnImg;
    public Image m_IconImg;
    public Image m_EditOnIconImg;
    public Image m_EditOffIconImg;
    public SkillSlot[] m_SkillSlot;
    public Image m_MouseObj;

    public bool m_Editing = false;

    int m_SaveIdx = -1; //Ŭ���ѽ����� ���° ���� ��Ƴ��� ����



    public static SkillSlotMgr Inst = null;

    void Awake()
    {
        Inst = this;
    }

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        if (m_Editing == true)
        {
            if (Input.GetMouseButtonDown(0) == true)
            {
                MouseClickDown();
            }

            if (Input.GetMouseButton(0) == true)
            {
                MouseClick();
            }

            if (Input.GetMouseButtonUp(0) == true)
            {
                MouseClickUp();
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            EditOnOff();
        }
    }

    void MouseClickDown() //���콺 Ŭ��������
    {
        for (int ii = 0; ii < m_SkillSlot.Length; ii++)
        {
            if (IsSkillSlot(m_SkillSlot[ii]) == true) //Ŭ���� ���� ��ų���� �̸�
            {
                m_SaveIdx = ii;

                Skill a_Skill = m_SkillSlot[ii].GetComponentInChildren<Skill>();
                if (a_Skill != null) //Ŭ���ѽ��Կ� ��ų�� �����������
                {
                    Transform a_MouseImg = m_MouseObj.transform.Find("MouseImg");
                    if (a_MouseImg != null)
                        a_MouseImg.GetComponent<Image>().sprite =  a_Skill.m_SkillIconImg.sprite;

                    m_MouseObj.gameObject.SetActive(true);

                    break;
                }
                else //Ŭ���ѽ����� ��ĭ�̸�
                {
                    m_SaveIdx = -1;
                }
            }
        }
    }

    void MouseClick() //���콺 Ŭ����
    {
        if (m_SaveIdx >= 0)
            m_MouseObj.transform.position = Input.mousePosition;
    }

    void MouseClickUp() //���콺 Ŭ���� ������
    {
        if (m_SaveIdx < 0)
            return;

        int a_InstallSlot = -1;

        for (int ii = 0; ii < m_SkillSlot.Length; ii++)
        {
            if (IsSkillSlot(m_SkillSlot[ii]) == true) //���콺�� �� ���� ��ų�����̸�(�ű��)
            {
                Skill a_Skill = m_SkillSlot[m_SaveIdx].GetComponentInChildren<Skill>();
                a_Skill.transform.SetParent(m_SkillSlot[ii].transform, false);

                a_InstallSlot = ii;
                break;
            }
        }

        if(a_InstallSlot == -1) //���콺�� �� ���� ��ų������ �ƴϸ�(���ֱ�)
        {
            Skill a_Skill = m_SkillSlot[m_SaveIdx].GetComponentInChildren<Skill>();
            Destroy(a_Skill.gameObject);
        }

        m_SaveIdx = -1;
        m_MouseObj.gameObject.SetActive(false);
    }

    bool IsSkillSlot(SkillSlot a_SkillSlot) //��ų�������� �ִ��� Ȯ���ϴ� �Լ�
    {
        Vector3[] v = new Vector3[4];
        a_SkillSlot.GetComponent<RectTransform>().GetWorldCorners(v);

        if (v[0].x <= Input.mousePosition.x && Input.mousePosition.x <= v[2].x &&
            v[0].y <= Input.mousePosition.y && Input.mousePosition.y <= v[2].y)
        {
            return true;
        }

        return false;
    }

    public void EditOnOff()
    {
        m_Editing = !m_Editing;

        if (m_Editing == true)
        {
            m_EditBtn.GetComponent<Image>().sprite = m_EditOnBtnImg.sprite;
            m_IconImg.sprite = m_EditOnIconImg.sprite;
        }
        else
        {
            m_EditBtn.GetComponent<Image>().sprite = m_EditOffBtnImg.sprite;
            m_IconImg.sprite = m_EditOffIconImg.sprite;
        }
    }
}
