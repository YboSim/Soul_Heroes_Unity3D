using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HuntingG
{
    SAndT,
    GAndO,
    Town,
    Boss
}

public class HuntingGround : MonoBehaviour
{
    public HuntingG m_huntingG;

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    private void OnTriggerEnter(Collider coll)
    {
        if (m_huntingG == HuntingG.SAndT) //SlimeAndTurtle
        {
            if (coll.gameObject.tag == "Hero")
            {
                HeroCtrl a_Hero = coll.GetComponent<HeroCtrl>();

                if (a_Hero.m_PhotonView.IsMine)
                {
                    GameMgr.Inst.SetMsg("�߽��� �����");
                    Sound_Mgr.Instance.PlayBGM("BattleBGM", 0.7f);
                }
            }
        }
        else if(m_huntingG == HuntingG.GAndO)//GolemAndOrc
        {
            if (coll.gameObject.tag == "Hero")
            {
                HeroCtrl a_Hero = coll.GetComponent<HeroCtrl>();

                if (a_Hero.m_PhotonView.IsMine)
                {
                    GameMgr.Inst.SetMsg("������ �����");
                    Sound_Mgr.Instance.PlayBGM("BattleBGM", 0.7f);
                }
            }
        }
        else if(m_huntingG == HuntingG.Town)//Town
        {
            if (coll.gameObject.tag == "Hero")
            {
                HeroCtrl a_Hero = coll.GetComponent<HeroCtrl>();

                if (a_Hero.m_PhotonView.IsMine)
                    GameMgr.Inst.SetMsg("����");
            }
        }
        else
        {
            if (coll.gameObject.tag == "Hero")
            {
                HeroCtrl a_Hero = coll.GetComponent<HeroCtrl>();

                if (a_Hero.m_PhotonView.IsMine)
                {
                    GameMgr.Inst.SetMsg("�����̾� ������");
                    Sound_Mgr.Instance.PlayBGM("BossBGM", 0.7f);
                }
            }
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (m_huntingG == HuntingG.SAndT) //SlimeAndTurtle
        {
            if (coll.gameObject.tag == "Hero")
            {
                HeroCtrl a_Hero = coll.GetComponent<HeroCtrl>();

                if (a_Hero.m_PhotonView.IsMine)
                {
                    GameMgr.Inst.SetMsg("�߽��� ����� ���");
                    Sound_Mgr.Instance.PlayBGM("GameBGM", 1.0f);
                }
            }
        }
        else if (m_huntingG == HuntingG.GAndO)//GolemAndOrc
        {
            if (coll.gameObject.tag == "Hero")
            {
                HeroCtrl a_Hero = coll.GetComponent<HeroCtrl>();

                if (a_Hero.m_PhotonView.IsMine)
                {
                    GameMgr.Inst.SetMsg("������ ����� ���");
                    Sound_Mgr.Instance.PlayBGM("GameBGM", 1.0f);
                }
            }
        }
        else if (m_huntingG == HuntingG.Town)//Town
        {
            if (coll.gameObject.tag == "Hero")
            {
                HeroCtrl a_Hero = coll.GetComponent<HeroCtrl>();

                if (a_Hero.m_PhotonView.IsMine)
                    GameMgr.Inst.SetMsg("���� �ܰ�");
            }
        }
        else // ����
        {
            if (coll.gameObject.tag == "Hero")
            {
                HeroCtrl a_Hero = coll.GetComponent<HeroCtrl>();

                if (a_Hero.m_PhotonView.IsMine)
                {
                    GameMgr.Inst.SetMsg("�����̾� ������ ���");
                    Sound_Mgr.Instance.PlayBGM("GameBGM", 1.0f);
                }
            }
        }
    }
}
