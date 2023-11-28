using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour
{
    MonsterCtrl m_MonsterCtrl;
    ObjectInfo m_ObjInfo;

    float m_AttackDist = 2.0f;
    float m_ReturnDist = 1.3f;
    float m_MaxHp = 200f;
    float m_CurHp = 200f;
    int m_Gold;
    int m_Exp;
    string SpawnArea = "숙련자 사냥터";
    int m_Att = 33;
    int m_Def = 8;

    private void Awake()
    {
        m_MonsterCtrl = GetComponent<MonsterCtrl>();
        m_ObjInfo = GetComponent<ObjectInfo>();

        if (m_ObjInfo != null)
            m_ObjInfo.InitObject(ObjectType.Monster, "오크");

        m_Gold = Random.Range(300, 400);
        m_Exp = Random.Range(200, 240);

        if (m_MonsterCtrl != null)
            InItMonsterInfo();
    }

    void InItMonsterInfo()
    {
        m_MonsterCtrl.m_AttackDist = m_AttackDist;
        m_MonsterCtrl.m_ReturnDist = m_ReturnDist;
        m_MonsterCtrl.m_MaxHp = m_MaxHp;
        m_MonsterCtrl.m_CurHp = m_CurHp;
        m_MonsterCtrl.m_Gold = m_Gold;
        m_MonsterCtrl.m_Exp = m_Exp;
        m_MonsterCtrl.m_SpawnArea = SpawnArea;
        m_MonsterCtrl.m_Att = m_Att;
        m_MonsterCtrl.m_Def = m_Def;
    }

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
