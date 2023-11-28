using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShower : MonoBehaviour
{
    float m_AttDelay;

    // Start is called before the first frame update
    void Start()
    {
        Sound_Mgr.Instance.PlayEffSound("MeteorShower", 4.0f);

        Destroy(gameObject, 3.5f);
        m_AttDelay = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_AttDelay > 0.0f)
        {
            m_AttDelay -= Time.deltaTime;
            if(m_AttDelay < 0.0f)
            {
                Attack();
                m_AttDelay = 0.7f;
            }
        }
    }

    void Attack()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, 4.0f);

        foreach(Collider coll in colls)
        {
            if (coll.gameObject.tag == "Monster")
            {
                if (coll.gameObject.name.Contains("Boss"))
                {
                    BossCtrl a_Boss = coll.GetComponent<BossCtrl>();

                    float a_Dmg = GlobalValue.m_Att / 2;
                    if (a_Dmg < 0.0f)
                        a_Dmg = 0.0f;

                    a_Boss.m_PhotonView.RPC("TakeDamage", RpcTarget.AllBuffered, a_Dmg, GameMgr.Inst.m_Player.m_PhotonView.Owner.NickName);
                }
                else
                {
                    MonsterCtrl a_Mon = coll.GetComponent<MonsterCtrl>();
                    if (a_Mon == null)
                        continue;

                    float a_Dmg = GlobalValue.m_Att / 2 - a_Mon.m_Def;
                    if (a_Dmg < 0.0f)
                        a_Dmg = 0.0f;

                    a_Mon.m_PhotonView.RPC("TakeDamage", RpcTarget.AllBuffered, a_Dmg);
                }
            }
            else if(coll.gameObject.tag == "Hero")
            {
                HeroCtrl a_Hero = coll.GetComponent<HeroCtrl>();
                if (a_Hero == null)
                    continue;

                if (a_Hero.m_PhotonView.IsMine == true || a_Hero.GetComponent<ObjectInfo>().m_ObjType == ObjectType.Hero_ally) //나와 파티원 제외
                    return;

                float a_Dmg = GlobalValue.m_Att / 2;
                if (a_Dmg < 0.0f)
                    a_Dmg = 0.0f;

                a_Hero.m_PhotonView.RPC("TakeDamage", RpcTarget.AllBuffered, a_Dmg);
            }
        }
    }

}
