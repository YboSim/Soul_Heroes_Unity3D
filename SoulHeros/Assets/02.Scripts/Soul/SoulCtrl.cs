using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoulCtrl : MonoBehaviour
{
    Rigidbody m_RigidBody;
    Transform m_Target;
    HeroCtrl m_Hero;
    ObjectInfo m_ObjInfo;
    public GameObject m_HitEffect;
    public int m_SoulIdx; //어떤 소울인지(dakr,fire ...)

    Vector3 m_TargetPos = Vector3.zero;
    public float m_MaxSpeed = 20.0f;
    public float m_CurSpeed;

    float m_Att = 10;

    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Hero = GameMgr.Inst.m_Player;
        StartCoroutine(Attack());

        Destroy(gameObject,3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Target != null && m_ObjInfo.m_IsDie == false)
        {
            m_TargetPos = m_Target.transform.position;

            if (m_CurSpeed <= m_MaxSpeed)
                m_CurSpeed += m_MaxSpeed * Time.deltaTime;

            transform.position += transform.up * m_CurSpeed * Time.deltaTime;

            Vector3 a_SoulDir = (m_TargetPos - transform.position).normalized
                                  + new Vector3(0, 0.6f, 0);

            transform.up = Vector3.Lerp(transform.up, a_SoulDir, 0.6f);
        }
        else if(m_Target != null && m_ObjInfo.m_IsDie == true)
        {
            if (transform.localEulerAngles.y > 1.0f)
                transform.position += transform.up * m_CurSpeed * Time.deltaTime;
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitUntil(() => transform.localPosition.y > 1.0f); //소울 위치가 1m이상 올라갈 때까지

        if (m_Hero.m_AttTarget == null)
            Destroy(gameObject);
        else
        {
            m_Target = m_Hero.m_AttTarget.transform;

            m_ObjInfo = m_Hero.m_AttTarget.GetComponent<ObjectInfo>();
            if(m_ObjInfo != null)
            {
                if (m_ObjInfo.m_IsDie == true)
                    Destroy(gameObject);
            }
        }
    }

    void HitEffect()
    {
        GameObject a_HitEffect = Instantiate(m_HitEffect, transform.position, Quaternion.identity);

        Sound_Mgr.Instance.PlayEffSound("SoulAttack", 1.0f);
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Monster")
        {
            if (coll.gameObject == m_Hero.m_AttTarget)
            {
                HitEffect();

                int a_AddDmg = 0;
                if (m_SoulIdx == 0)
                    a_AddDmg = GlobalValue.m_DarkSoul;
                else if (m_SoulIdx == 1)
                    a_AddDmg = GlobalValue.m_FireSoul;
                else if (m_SoulIdx == 2)
                    a_AddDmg = GlobalValue.m_IceSoul;
                else if (m_SoulIdx == 3)
                    a_AddDmg = GlobalValue.m_NatureSoul;
                else if (m_SoulIdx == 4)
                    a_AddDmg = GlobalValue.m_WaterSoul;

                MonsterCtrl a_Mon = coll.GetComponent<MonsterCtrl>();
                if (a_Mon != null)
                    a_Mon.m_PhotonView.RPC("TakeDamage", RpcTarget.AllBuffered, m_Att + a_AddDmg);

                Destroy(gameObject);
            }
        }
        else if(coll.gameObject.tag == "Hero")
        {
            HeroCtrl a_Hero = coll.gameObject.GetComponent<HeroCtrl>();
            if(a_Hero.m_PhotonView.IsMine == false)
            {
                if(a_Hero.gameObject == m_Hero.m_AttTarget)
                {
                    HitEffect();

                    int a_AddDmg = 0;
                    if (m_SoulIdx == 0)
                        a_AddDmg = GlobalValue.m_DarkSoul;
                    else if (m_SoulIdx == 1)
                        a_AddDmg = GlobalValue.m_FireSoul;
                    else if (m_SoulIdx == 2)
                        a_AddDmg = GlobalValue.m_IceSoul;
                    else if (m_SoulIdx == 3)
                        a_AddDmg = GlobalValue.m_NatureSoul;
                    else if (m_SoulIdx == 4)
                        a_AddDmg = GlobalValue.m_WaterSoul;

                    a_Hero.m_PhotonView.RPC("TakeDamage",RpcTarget.AllBuffered, m_Att + a_AddDmg);

                    Destroy(gameObject);
                }
            }
        }
    }

}
