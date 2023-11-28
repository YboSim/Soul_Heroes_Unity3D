using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ObjectType
{
    Hero,
    Hero_ally,
    Monster,
}

public class ObjectInfo : MonoBehaviour , IPunObservable
{
    public string m_ObjName = "";
    public ObjectType m_ObjType;
    HeroCtrl m_HeroCtrl;
    [HideInInspector]public PhotonView m_PhotonView;

    //TargetMark 관련 변수
    public Text m_NameText;
    string m_TargetName = "";

    //Stun 관련 변수
    public GameObject m_StunObj;
    float m_StunTimer = 0.0f;
    public bool m_IsStun = false;

    public bool m_IsDie = false;

    //tart is called before the first frame update
    void Start()
    {
        m_HeroCtrl = GetComponent<HeroCtrl>();
        m_PhotonView = GetComponent<PhotonView>();

        if (m_ObjType == ObjectType.Monster)
            m_TargetName = m_NameText.text;
        else
            m_TargetName = GameMgr.Inst.m_Player.m_PhotonView.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_StunTimer > 0.0f)
        {
            m_StunTimer -= Time.deltaTime;

            if(m_StunTimer < 0.0f)
            {
                m_StunObj.SetActive(false);
                m_IsStun = false;

                if (gameObject.tag == "Monster")
                {
                    //MonsterCtrl a_MonsterCtrl = GetComponent<MonsterCtrl>();
                    //a_MonsterCtrl.AnimationChange();
                }
                else if(gameObject.tag == "Hero")
                {
                    m_HeroCtrl.AnimationChange();
                }

                m_StunTimer = 0.0f;
            }
        }
    }

    public void InitObject(ObjectType a_ObjType, string a_ObjName)
    {
        m_ObjType = a_ObjType;
        m_ObjName = a_ObjName;
    }

    [PunRPC]
    public void Stun()
    {
        if (gameObject.name.Contains("Boss"))
            return;

        m_StunObj.SetActive(true);

        if (gameObject.tag == "Monster")
        {
            MonsterCtrl a_MonsterCtrl = GetComponent<MonsterCtrl>();
            a_MonsterCtrl.AnimationChange("Stun");
        }

        m_IsStun = true;
        m_StunTimer = 3.0f;

        if (gameObject.tag == "Hero")
        {
            m_HeroCtrl.AnimationChange("Stun");
        }
    }

    public void TargetMarkOn()
    {
        m_NameText.text = "<color=red>[ </color>" + m_TargetName + "<color=red> ]</color>";
    }

    public void TargetMarkOff()
    {
        m_NameText.text = m_TargetName;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
