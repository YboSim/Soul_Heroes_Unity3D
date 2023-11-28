using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSoul : MonoBehaviour
{
    public GameObject[] m_SoulPrefab;
    public GameObject m_OnOffEffect;
    public Button m_PickBtn;

    bool m_PickBtnDown = false;
    float m_PickBtnTimer = 0.0f;

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        if(m_PickBtnDown == true)
        {
            m_PickBtnTimer += Time.deltaTime;
            if (m_OnOffEffect.activeSelf == false)
                m_OnOffEffect.SetActive(true);
            if(m_PickBtnTimer > 2.0f)
            {
                OpenRandomSoul();
            }
        }
    }

    public void PickBtn()
    {
        
    }

    public void PickBtnDown()
    {
        m_PickBtnDown = true;
    }

    public void PickBtnUp()
    {
        m_PickBtnDown = false;

        if(m_OnOffEffect.activeSelf == true && m_PickBtnTimer <= 2.0f)
        {
            m_OnOffEffect.SetActive(false);
            m_PickBtnTimer = 0.0f;
        }
    }

    public void OpenRandomSoul()
    {

        
        int a_Rnd = Random.Range(0, 5);
        GameObject a_Soul = Instantiate(m_SoulPrefab[a_Rnd]) as GameObject;
        a_Soul.transform.position = transform.position;

        if (a_Rnd == 0)
        {
            GlobalValue.m_DarkSoul += 1;
            PlayfabMgr.inst.SetPlayerData("Soul_Dark", GlobalValue.m_DarkSoul.ToString());
        }
        else if (a_Rnd == 1)
        {
            GlobalValue.m_FireSoul += 1;
            PlayfabMgr.inst.SetPlayerData("Soul_Fire", GlobalValue.m_FireSoul.ToString());
        }
        else if (a_Rnd == 2)
        {
            GlobalValue.m_IceSoul += 1;
            PlayfabMgr.inst.SetPlayerData("Soul_Ice", GlobalValue.m_IceSoul.ToString());
        }
        else if (a_Rnd == 3)
        {
            GlobalValue.m_NatureSoul += 1;
            PlayfabMgr.inst.SetPlayerData("Soul_Nature", GlobalValue.m_NatureSoul.ToString());
        }
        else if (a_Rnd == 4)
        {
            GlobalValue.m_WaterSoul += 1;
            PlayfabMgr.inst.SetPlayerData("Soul_Water", GlobalValue.m_WaterSoul.ToString());
        }

        Destroy(gameObject);
    }
}
