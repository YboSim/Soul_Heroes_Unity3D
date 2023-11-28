using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBtn : MonoBehaviour
{
    [HideInInspector]public int m_Dist;
    public Text m_NameText;
    public Text m_DistText;
    public GameObject m_Enemy;

    HeroCtrl m_Hero = null;

    // Start is called before the first frame update
    void Start()
    {
        m_Hero = GameMgr.Inst.m_Player;
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public void Attack()
    {
        if (m_Hero == null)
            return;

        if (m_Enemy == null)
            return;

        m_Hero.Attack(m_Enemy);
    }
}
