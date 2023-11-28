using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern1 : MonoBehaviour
{
    public GameObject m_ExplosionEff;

    float m_ExplosionTimer = 3.0f;

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        if(m_ExplosionTimer > 0.0f)
        {
            m_ExplosionTimer -= Time.deltaTime;
            if(m_ExplosionTimer < 0.0f)
            {
                Bomb();
            }
        }
    }

    void Bomb()
    {
        GameObject a_ExplosionObj = Instantiate(m_ExplosionEff, transform.position, Quaternion.identity);
        Destroy(a_ExplosionObj, 1.0f);

        Collider[] colls = Physics.OverlapSphere(transform.position, 1.5f);
        HeroCtrl a_Hero;

        foreach (Collider coll in colls)
        {
            a_Hero = coll.GetComponent<HeroCtrl>();
            if (a_Hero == null)
                continue;

            float a_Dmg = (100 - GlobalValue.m_Def / 2) / 2;
            if (a_Dmg < 0.0f)
                a_Dmg = 0.0f;

            a_Hero.m_PhotonView.RPC("TakeDamage", Photon.Pun.RpcTarget.AllBuffered, a_Dmg);
        }

        Sound_Mgr.Instance.PlayEffSound("Pattern1", 8.0f);

        Destroy(this.gameObject);
    }
}
