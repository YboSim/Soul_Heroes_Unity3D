using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoulSceneMgr : MonoBehaviour
{
    public Transform[] m_RndSoulPos;
    public GameObject m_RndSoulPrefab;
    [SerializeField] GameObject[] m_RndSoul = new GameObject[11];
    public Button m_AllOpenBtn;
    public Button m_ReturnStoreBtn;

    public static int m_Idx;

    // Start is called before the first frame update
    void Start()
    {
        if (m_ReturnStoreBtn != null)
            m_ReturnStoreBtn.onClick.AddListener(ResturnStoreBtnClick);

        SoulSetting(m_Idx);
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    void SoulSetting(int a_Idx)
    {
        if (a_Idx == 11)
        {
            for (int ii = 0; ii < m_RndSoulPos.Length; ii++)
            {
                m_RndSoul[ii] = Instantiate(m_RndSoulPrefab) as GameObject;
                m_RndSoul[ii].transform.SetParent(m_RndSoulPos[ii], false);
            }
        }
        else
        {
            m_RndSoul[0] = Instantiate(m_RndSoulPrefab) as GameObject;
            m_RndSoul[0].transform.SetParent(m_RndSoulPos[2], false);
        }
    }

    public void AllOpenBtnClick()
    {
        for(int ii = 0; ii < m_RndSoul.Length; ii++)
        {
            if(m_RndSoul[ii] != null)
            {
                RandomSoul a_RndSoulObj = m_RndSoul[ii].GetComponent<RandomSoul>();
                a_RndSoulObj.PickBtnDown();
            }
        }

        Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);
    }

    public void ResturnStoreBtnClick()
    {
        Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

        //간헐적으로 데이터 저장이 누락되는 경우 가 있어 한번더 저장
        PlayfabMgr.inst.SetPlayerData("Soul_Dark", GlobalValue.m_DarkSoul.ToString());
        PlayfabMgr.inst.SetPlayerData("Soul_Fire", GlobalValue.m_FireSoul.ToString());
        PlayfabMgr.inst.SetPlayerData("Soul_Ice", GlobalValue.m_IceSoul.ToString());
        PlayfabMgr.inst.SetPlayerData("Soul_Nature", GlobalValue.m_NatureSoul.ToString());
        PlayfabMgr.inst.SetPlayerData("Soul_Water", GlobalValue.m_WaterSoul.ToString());

        SceneManager.LoadScene("StoreScene");
    }
}
