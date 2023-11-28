using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulPanel : MonoBehaviour
{
    HeroCtrl m_Hero;

    public Text m_DarkSoulText;
    public Text m_FireSoulText;
    public Text m_IceSoulText;
    public Text m_NatureSoulText;
    public Text m_WaterSoulText;

    public GameObject[] m_Soul;

    // Start is called before the first frame update
    void Start()
    {
        HeroCtrl[] a_Heroes = FindObjectsOfType<HeroCtrl>();
        for (int ii = 0; ii < a_Heroes.Length; ii++)
        {
            if (a_Heroes[ii].m_PhotonView.IsMine == true)
                m_Hero = a_Heroes[ii];
        }

        GetSoulData();
    }

    void OnEnable()
    {
        GetSoulData();
    }


    // Update is called once per frame
    //void Update()
    //{
        
    //}

    void GetSoulData()
    {
        m_DarkSoulText.text = "X " + GlobalValue.m_DarkSoul.ToString();
        m_FireSoulText.text = "X " + GlobalValue.m_FireSoul.ToString();
        m_IceSoulText.text = "X " + GlobalValue.m_IceSoul.ToString();
        m_NatureSoulText.text = "X " + GlobalValue.m_NatureSoul.ToString();
        m_WaterSoulText.text = "X " + GlobalValue.m_WaterSoul.ToString();
    }

    public void BtnClick(int a_Idx)
    {
        m_Hero.m_SoulPrefab = m_Soul[a_Idx];

        PlayfabMgr.inst.SetPlayerData("Soul", a_Idx.ToString());
    }
}
