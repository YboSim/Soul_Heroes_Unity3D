using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Helmet,
    Top,
    Weapon,
    Ring,
    Boots,
    Etc
}

[System.Serializable]
public class ItemData
{
    public ItemData( string a_Name, string a_Grade, string a_ItemType, string a_DetailType, int a_Att, int a_Def,
                     int a_Acc, int a_Hp, string a_SpawnArea, int a_HaveCount, bool a_IsUsing)
    {   m_Name = a_Name; m_Grade = a_Grade; m_ItemType = a_ItemType; m_DetailType = a_DetailType; m_Att = a_Att; m_Def = a_Def;
        m_Acc = a_Acc; m_Hp = a_Hp; m_SpawnArea = a_SpawnArea; m_HaveCount = a_HaveCount; m_IsUsing = a_IsUsing; }

    public string m_Name = "";
    public string m_Grade = "";
    public string m_ItemType = "";
    public string m_DetailType = "";
    public int m_Att = 0;
    public int m_Def = 0;
    public int m_Acc = 0;
    public int m_Hp = 0;
    public string m_SpawnArea = "";
    public int m_HaveCount = 0;
    public bool m_IsUsing = false;
}

public class ItemPanel : MonoBehaviour
{
    public TextAsset m_ItemDB; //전체 아이템 데이터 베이스
    public List<ItemData> AllItemList,MyItemList, CurItemList;     //All: 게임내 전체아이템, My: 현재갖고있는 전체아이템, Cur: m_CurItemType에 일치하는 아이템

    public string m_CurItemType = "All"; //tab클릭으로 ItemType에따른 아이템을 볼때 사용할 변수
    public Button[] m_TypeTabBtn;
    public Sprite TabIdleSprite, TabSelectSprite;
    public GameObject[] m_ItemSlot;
    public Sprite m_GreenSprite, m_BlueSprite;
    public Sprite[] m_ItemIconSprite;
    public int m_MaxItemSlot = 20;

    [Header("--- ItemDescriptionWd ---")]
    public GameObject m_ItemDescriptionWd;
    public Text m_IDGradeText;
    public Text m_IDNameText;
    public Text m_IDClassText;
    public Text m_IDEffectText;
    public Text m_IDSpawnAreaText;

    [Header("--- WearingSlot ---")]
    public Item[] m_WearingSlot;

    [Header("--- GainItem ---")]
    public GameObject m_GainItemRoot;
    public Image m_BackImg;
    Color m_BackImgColor = new Color(1,1,1,0);
    public Image m_IconImg;
    Color m_IconImgColor = new Color(1,1,1,0);
    float m_Timer = 0.0f;
    public List<Sprite> m_ItemIconImgList = new List<Sprite>();
    public Text m_MyItemCountText;

    public static ItemPanel Inst = null;
    void Awake()
    {
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Load());
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf == true)
            MousePointOnItem();

        if (m_GainItemRoot.activeSelf == true && m_Timer <= 0.0f)
        {
            m_BackImgColor += new Color(0,0,0,0.02f);
            m_IconImgColor += new Color(0,0,0,0.02f);
            m_BackImg.color = m_BackImgColor;
            m_IconImg.color = m_IconImgColor;
            if(m_BackImgColor.a > 1.0f || m_IconImgColor.a > 1.0f)
            {
                m_Timer = 3.0f;
            }
        }

        if (m_Timer > 0.0f)
        {
            m_Timer -= Time.deltaTime;
            if (m_Timer < 0.0f)
            {
                m_GainItemRoot.gameObject.SetActive(false);
                m_Timer = 0.0f;
            }
        }
    }

    public void Save()
    {
        string Jdata = JsonConvert.SerializeObject(MyItemList);
        PlayfabMgr.inst.SetPlayerData("Item", Jdata);
        //File.WriteAllText(Application.dataPath + "/Resources/MyItemText.txt", Jdata);

        TabClick(m_CurItemType);
    }

    IEnumerator Load()
    {
        yield return new WaitUntil(() => GameMgr.Inst.m_Player != null);

        string[] line = m_ItemDB.text.Substring(0, m_ItemDB.text.Length - 1).Split("\n");
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            AllItemList.Add(new ItemData(row[0], row[1], row[2], row[3], int.Parse(row[4]), int.Parse(row[5]), int.Parse(row[6]),
                                         int.Parse(row[7]), row[8], int.Parse(row[9]), row[10] == "TRUE"));
        }

        //string Jdata = File.ReadAllText(Application.dataPath + "/Resources/MyItemText.txt");
        if (PlayfabMgr.inst.m_Data.TryGetValue("Item", out string a_Item))
        {//PlayFab에 저장되어 있는 나의 아이템 리스트 가져오기
            MyItemList = JsonConvert.DeserializeObject<List<ItemData>>(a_Item);
        }

        TabClick("All");

        if (CurItemList != null && CurItemList.Count > 0)
        {
            for (int ii = 0; ii < CurItemList.Count; ii++)
            {//착용되어 있던 아이템 세팅
                if (CurItemList[ii].m_IsUsing == true)
                {
                    SlotClick(ii);
                }
            }
        }

        GameMgr.Inst.UIRefresh();
    }

    public void TabClick(string a_TabName)
    {
        //현재 아이템 리스트에 클릭한 타입만 추가
        m_CurItemType = a_TabName;
        if (m_CurItemType == "All")
        {
            if (MyItemList != null && MyItemList.Count > 0)
                CurItemList = MyItemList;
            else
            {
                MyItemList = new List<ItemData>();
                CurItemList = MyItemList;
            }
        }
        else
        {
            Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);
            CurItemList = MyItemList.FindAll(a_Item => a_Item.m_ItemType == a_TabName);
        }
        //현재 아이템 리스트에 클릭한 타입만 추가
        if (CurItemList != null && CurItemList.Count > 0)
        {
            //슬롯과 텍스트 보이게 하기
            for (int i = 0; i < CurItemList.Count; i++)
            {
                m_ItemSlot[i].SetActive(i < CurItemList.Count);
                //아이템슬롯에 정보저장
                Item a_Item = m_ItemSlot[i].GetComponent<Item>();

                a_Item._Grade = CurItemList[i].m_Grade;
                if (a_Item._Grade == "고급")
                    a_Item.m_BackgroundImg.sprite = m_GreenSprite;
                else if (a_Item._Grade == "희귀")
                    a_Item.m_BackgroundImg.sprite = m_BlueSprite;
                a_Item._Name = CurItemList[i].m_Name;
                a_Item._Type = CurItemList[i].m_DetailType;
                a_Item._Att = CurItemList[i].m_Att;
                a_Item._Def = CurItemList[i].m_Def;
                a_Item._Acc = CurItemList[i].m_Acc;
                a_Item._Hp = CurItemList[i].m_Hp;
                a_Item._SpawnArea = CurItemList[i].m_SpawnArea;
                a_Item._HaveCount = CurItemList[i].m_HaveCount;
                a_Item._IsUsing = CurItemList[i].m_IsUsing;
                a_Item.m_UsingImg.gameObject.SetActive(a_Item._IsUsing);

                bool isExist = i < CurItemList.Count;
                if (isExist)
                    a_Item.m_ItemIconImg.sprite = m_ItemIconSprite[AllItemList.FindIndex(x => x.m_Name == CurItemList[i].m_Name)];
                //아이템슬롯에 정보저장
            }
            //슬롯과 텍스트 보이게 하기

            for (int ii = CurItemList.Count; ii < MyItemList.Count; ii++)
            { 
                m_ItemSlot[ii].SetActive(false);
            }
        }
        else
        {//현재 탭에 갖고있는 아이템이 없으면
            for (int iii = 0; iii < m_ItemSlot.Length; iii++)
            {
                m_ItemSlot[iii].SetActive(false);
            }
        }

        //Tab버튼 이미지 바꿔주기
        int a_TabNum = -1;
        switch (m_CurItemType)
        {
            case "All": a_TabNum = 0; break;
            case "Armor": a_TabNum = 1; break;
            case "Weapon": a_TabNum = 2; break;
            case "Etc": a_TabNum = 3; break;
        }

        for (int ii = 0; ii < m_TypeTabBtn.Length; ii++)
        {
            m_TypeTabBtn[ii].GetComponent<Image>().sprite = ii == a_TabNum ? TabSelectSprite : TabIdleSprite;
        }
        //Tab버튼 이미지 바꿔주기

        if (MyItemList.Count == m_MaxItemSlot) //장비창에 아이템이 가득차 있으면
            m_MyItemCountText.text = "<color=red>" + MyItemList.Count + "/" + m_MaxItemSlot + "</color>";
        else
            m_MyItemCountText.text = MyItemList.Count + "/" + m_MaxItemSlot;
    }

    bool IsItemSlot(Item a_Item) //아이템위에 마우스가 있는지 확인하는 함수
    {
        Vector3[] v = new Vector3[4];
        a_Item.GetComponent<RectTransform>().GetWorldCorners(v);

        if (v[0].x <= Input.mousePosition.x && Input.mousePosition.x <= v[2].x &&
            v[0].y <= Input.mousePosition.y && Input.mousePosition.y <= v[2].y)
        {
            return true;
        }

        return false;
    }

    void MousePointOnItem() //마우스를 아이템 위로 갖다댔을 때
    {
        int a_ItemIdx = -1;

        Item a_Item;

        for (int ii = 0; ii < m_ItemSlot.Length; ii++)
        {
            a_Item = m_ItemSlot[ii].GetComponent<Item>();

            if (IsItemSlot(a_Item) == true)
            {
                if (a_Item.gameObject.activeSelf == false)
                    return;

                if (a_Item._Grade == "고급")
                    m_IDGradeText.text = "<color=green>[등급 : " + a_Item._Grade + "]</color>";
                else if(a_Item._Grade == "희귀")
                    m_IDGradeText.text = "<color=blue>[등급 : " + a_Item._Grade + "]</color>";

                m_IDNameText.text = a_Item._Name;

                m_IDClassText.text = a_Item._Type;

                m_IDEffectText.text = "";
                if (a_Item._Att != 0)
                {
                    m_IDEffectText.text += "공격력 : + " + a_Item._Att + "\n" +
                                           "명중 : + " + a_Item._Acc;
                }
                else
                {
                    m_IDEffectText.text += "방어력 : + " + a_Item._Def + "\n" +
                                           "체력 : + " + a_Item._Hp;
                }

                m_IDSpawnAreaText.text = "<color=orange>[" + a_Item._SpawnArea + "]</color>";

                m_ItemDescriptionWd.transform.position = Input.mousePosition;
                m_ItemDescriptionWd.SetActive(true);

                a_ItemIdx = ii;
                break;
            }
        }

        if (a_ItemIdx == -1) //마우스를 땐 곳이 스킬슬롯이 아니면(없애기)
        {
            m_ItemDescriptionWd.SetActive(false);
        }
    }

    public void SlotClick(int a_SlotNum)
    {
        ItemData a_CurItem = CurItemList[a_SlotNum];
        Item a_ItemSlot = m_ItemSlot[a_SlotNum].GetComponent<Item>();

        if (a_CurItem.m_ItemType == "Weapon")
        {
            InputItem(a_CurItem, m_WearingSlot[(int)ItemType.Weapon], a_ItemSlot);
            Sound_Mgr.Instance.PlayEffSound("Weapon", 1.0f);
        }
        else if (a_CurItem.m_ItemType == "Armor")
        {
            if (a_CurItem.m_DetailType == "머리장식(공용)")
            {
                InputItem(a_CurItem, m_WearingSlot[(int)ItemType.Helmet], a_ItemSlot);
                Sound_Mgr.Instance.PlayEffSound("Cloth", 1.0f);
            }
            else if (a_CurItem.m_DetailType == "갑옷(공용)")
            {
                InputItem(a_CurItem, m_WearingSlot[(int)ItemType.Top], a_ItemSlot);
                Sound_Mgr.Instance.PlayEffSound("Cloth", 1.0f);
            }
            else if (a_CurItem.m_DetailType == "반지(공용)")
            {
                InputItem(a_CurItem, m_WearingSlot[(int)ItemType.Ring], a_ItemSlot);
                Sound_Mgr.Instance.PlayEffSound("Ring", 1.0f);
            }
            else if (a_CurItem.m_DetailType == "신발(공용)")
            {
                InputItem(a_CurItem, m_WearingSlot[(int)ItemType.Boots], a_ItemSlot);
                Sound_Mgr.Instance.PlayEffSound("Cloth", 1.0f);
            }
        }
        //else if(a_CurItem.m_ItemType == "소모품")
        //{

        //}

        a_CurItem.m_IsUsing = true;
        GameMgr.Inst.UIRefresh();
        Save();
    }

    void InputItem(ItemData a_Item,Item a_WearingSlot, Item a_ItemSlot) //a_Item : 착용할 아이템 , a_WearingSlot : 착용 되어있는 아이템, a_ItemSlot : 착용할 아이템의 슬롯
    {
        // 착용되어 있는 아이템이 있으면 제거
        for (int ii = 0; ii < CurItemList.Count; ii++)
        {
            if (CurItemList[ii].m_IsUsing == true && a_Item.m_DetailType == CurItemList[ii].m_DetailType)
                CurItemList[ii].m_IsUsing = false;
        }

        if (a_Item.m_ItemType == "Weapon")
        {
            GlobalValue.m_Att -= a_WearingSlot._Att;
            GlobalValue.m_Acc -= a_WearingSlot._Acc;
        }
        else
        {
            GlobalValue.m_Def -= a_WearingSlot._Def;
            GameMgr.Inst.m_Player.m_MaxHp -= a_WearingSlot._Hp;
        }
        // 착용되어 있는 아이템이 있으면 제거

        //UI 및 정보 저장
        a_WearingSlot.m_BackgroundImg.gameObject.SetActive(true);
        if (a_Item.m_Grade == "고급")
            a_WearingSlot.m_BackgroundImg.sprite = m_GreenSprite;
        else
            a_WearingSlot.m_BackgroundImg.sprite = m_BlueSprite;
        a_WearingSlot.m_ItemIconImg.gameObject.SetActive(true);
        a_WearingSlot.m_ItemIconImg.sprite = a_ItemSlot.m_ItemIconImg.sprite;
        a_WearingSlot._Grade = a_Item.m_Grade;
        a_WearingSlot._Name = a_Item.m_Name;
        a_WearingSlot._Type = a_Item.m_DetailType;
        a_WearingSlot._Att = a_Item.m_Att;
        a_WearingSlot._Acc = a_Item.m_Acc;
        a_WearingSlot._Def = a_Item.m_Def;
        a_WearingSlot._Hp = a_Item.m_Hp;
        a_WearingSlot._SpawnArea = a_Item.m_SpawnArea;
        a_WearingSlot._HaveCount = a_Item.m_HaveCount;
        a_WearingSlot._IsUsing = true;
        a_ItemSlot.m_UsingImg.gameObject.SetActive(true);
        //UI 및 정보 저장

        //능력치
        if (a_Item.m_ItemType == "Weapon")
        {
            GlobalValue.m_Att += a_Item.m_Att;
            GlobalValue.m_Acc += a_Item.m_Acc;
        }
        else
        {
            GlobalValue.m_Def += a_Item.m_Def;
            GameMgr.Inst.m_Player.m_MaxHp += a_Item.m_Hp;
            GameMgr.Inst.m_Player.m_CurHp += a_Item.m_Hp;
        }
        //능력치
    }

    public void WearingSlotClick(int a_SlotNum) //착용아이템 제거
    {
        Item a_CurItem = m_WearingSlot[a_SlotNum];

        if (a_CurItem._IsUsing == false)
            return;

        if(a_CurItem._Type == "머리장식(공용)" || a_CurItem._Type == "갑옷(공용)" ||
                a_CurItem._Type == "반지(공용)" || a_CurItem._Type == "신발(공용)")
        {
            GlobalValue.m_Def -= a_CurItem._Def;
            GameMgr.Inst.m_Player.m_MaxHp -= a_CurItem._Hp;
        }
        else
        {
            GlobalValue.m_Att -= a_CurItem._Att;
            GlobalValue.m_Acc -= a_CurItem._Acc;
        }

        a_CurItem.m_BackgroundImg.gameObject.SetActive(false);
        a_CurItem.m_ItemIconImg.gameObject.SetActive(false);

        for (int ii = 0; ii < MyItemList.Count; ii++)
        {
            if (MyItemList[ii].m_Name == a_CurItem._Name)
                MyItemList[ii].m_IsUsing = false;
        }

        for(int ii = 0; ii < m_ItemSlot.Length; ii++)
        {
            if (a_CurItem._Name == m_ItemSlot[ii].GetComponent<Item>()._Name)
                m_ItemSlot[ii].GetComponent<Item>().m_UsingImg.gameObject.SetActive(false);
        }

        //착용아이템 정보 초기화
        a_CurItem._Grade = "";
        a_CurItem._Name = "";
        a_CurItem._Type = "";
        a_CurItem._Att = 0;
        a_CurItem._Def = 0;
        a_CurItem._Acc = 0;
        a_CurItem._Hp = 0;
        a_CurItem._SpawnArea = "";
        a_CurItem._HaveCount = 0;
        a_CurItem._IsUsing = false;

        GameMgr.Inst.UIRefresh();
        Save();
    }

    public void GainItem(MonsterCtrl a_Monster)
    {
        if (ItemPanel.Inst.MyItemList.Count >= ItemPanel.Inst.m_MaxItemSlot)
            return;

        int a_RdmIdx = Random.Range(0, 1); //(0, 2); (몬스터 처치 시 원하는 확률에 따라 아이템을 획득 여부 결정)
        if (a_RdmIdx == 0) // 100% 확률로 아이템 드랍되도록 설정
        {
            int a_RandomIdx = Random.Range(0, AllItemList.Count);
            ItemData a_GainItem = AllItemList[a_RandomIdx];
            if (a_GainItem.m_SpawnArea == a_Monster.m_SpawnArea) //50%, 총 아이템획득 확률 50%
            {
                if (MyItemList == null) //아이템 첫획득
                    MyItemList = new List<ItemData>() { a_GainItem };
                else
                    MyItemList.Add(a_GainItem);

                if (MyItemList.Count == m_MaxItemSlot)
                    m_MyItemCountText.text = "<color=red>" + MyItemList.Count + "/" + m_MaxItemSlot + "</color>";
                else
                    m_MyItemCountText.text = MyItemList.Count + "/" + m_MaxItemSlot;

                for (int ii = 0; ii < AllItemList.Count; ii++)
                {
                    if (a_GainItem.m_Name == m_ItemIconImgList[ii].name)
                    {
                        m_IconImg.sprite = m_ItemIconImgList[ii];
                    }
                }
                if (a_GainItem.m_Grade == "고급")
                    m_BackImg.sprite = m_GreenSprite;
                else
                    m_BackImg.sprite = m_BlueSprite;

                m_GainItemRoot.gameObject.SetActive(true); 
                TabClick(m_CurItemType);
                Save();
            }
        }


    }
}
