using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValue : MonoBehaviour
{
    public static string m_ID = "";
    public static string m_NickName = "";
    public static string m_Class = "";

    public static int m_Level = 1;   //레벨
    public static float m_CurExp;  //현재 경험치
    //public static float m_MaxHp = 100;   //최대체력
    //public static float m_CurHp = 100;   //현재체력
    //public static float m_MaxMp = 100;
    //public static float m_CurMp = 100;
    public static int m_Att = 10;    //공격력
    public static int m_Def = 10;    //방어력 
    public static int m_Acc = 10;    //명중
    public static int m_Gold = 0;    //골드
    public static int m_Diamond = 0; //다이아
    public static int m_Point = 0;   //포인트

    public static int m_Soul = 0; //장착 중인 소울 인덱스(0 : Dark, 1 : Fire, 2 : Ice, 3 : Nature, 4 : Water)
    public static int m_DarkSoul = 0;   //소지중인 소울 개수
    public static int m_FireSoul = 0;   //소지중인 소울 개수
    public static int m_IceSoul = 0;    //소지중인 소울 개수
    public static int m_NatureSoul = 0; //소지중인 소울 개수
    public static int m_WaterSoul = 0;  //소지중인 소울 개수

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
