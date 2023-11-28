using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValue : MonoBehaviour
{
    public static string m_ID = "";
    public static string m_NickName = "";
    public static string m_Class = "";

    public static int m_Level = 1;   //����
    public static float m_CurExp;  //���� ����ġ
    //public static float m_MaxHp = 100;   //�ִ�ü��
    //public static float m_CurHp = 100;   //����ü��
    //public static float m_MaxMp = 100;
    //public static float m_CurMp = 100;
    public static int m_Att = 10;    //���ݷ�
    public static int m_Def = 10;    //���� 
    public static int m_Acc = 10;    //����
    public static int m_Gold = 0;    //���
    public static int m_Diamond = 0; //���̾�
    public static int m_Point = 0;   //����Ʈ

    public static int m_Soul = 0; //���� ���� �ҿ� �ε���(0 : Dark, 1 : Fire, 2 : Ice, 3 : Nature, 4 : Water)
    public static int m_DarkSoul = 0;   //�������� �ҿ� ����
    public static int m_FireSoul = 0;   //�������� �ҿ� ����
    public static int m_IceSoul = 0;    //�������� �ҿ� ����
    public static int m_NatureSoul = 0; //�������� �ҿ� ����
    public static int m_WaterSoul = 0;  //�������� �ҿ� ����

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public static void ClearData()
    {
        m_ID = "";
        m_NickName = "";
        m_Class = "";

        m_Level = 1;
        m_CurExp = 0.0f;

        m_Att = 10;
        m_Def = 10;
        m_Acc = 10;
        m_Gold = 0;
        m_Diamond = 0;
        m_Point = 0;

        m_Soul = 0;
        m_DarkSoul = 0;
        m_FireSoul = 0;
        m_IceSoul = 0;
        m_NatureSoul = 0;
        m_WaterSoul = 0;
    }
}
