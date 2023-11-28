using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpMgr : MonoBehaviour
{
    List<int> m_LvTable = new List<int>() {0, 1000, 2000, 3000, 4000, 5000,
                                              6000, 7000, 8000, 9000, 10000};

    float m_ExpPer = 0.0f;
    public Image m_ExpBar;
    public Text m_ExpPerText;
    public Text m_LevelText;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayfabMgr.inst.m_Data.TryGetValue("Exp", out string v))
        {
            GlobalValue.m_CurExp = int.Parse(v);
        }

        LevelUp(0);
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public void LevelUp(float a_MonExp)
    {
        float a_Exp;
        GlobalValue.m_CurExp = GlobalValue.m_CurExp + a_MonExp;
        a_Exp = GlobalValue.m_CurExp;

        if(a_Exp > 55000.0f) //최대 경험치
        {
            m_LevelText.text = "Lv.10";
            m_ExpBar.fillAmount = 1.0f;
            GlobalValue.m_CurExp = 55000.0f;
        }

        for(int ii = 0; ii < m_LvTable.Count; ii++)
        {
            if(a_Exp >= m_LvTable[ii])
            {
                a_Exp = a_Exp - m_LvTable[ii];
            }
            else
            {
                GlobalValue.m_Level = ii;
                m_ExpPer = ((a_Exp / m_LvTable[ii])) * 100.0f;
                break;
            }
        }
        m_ExpBar.fillAmount = m_ExpPer / 100.0f;
        m_ExpPerText.text = m_ExpPer.ToString("F1") + "%";
        m_LevelText.text = "Lv." + GlobalValue.m_Level;

        PlayfabMgr.inst.SetPlayerData("Exp", GlobalValue.m_CurExp.ToString());
    }
}
