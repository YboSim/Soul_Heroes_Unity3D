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
    public TextAsset m_ItemDB; //��ü ������ ������ ���̽�
    public List<ItemData> AllItemList,MyItemList, CurItemList;     //All: ���ӳ� ��ü������, My: ���簮���ִ� ��ü������, Cur: m_CurItemType�� ��ġ�ϴ� ������

    public string m_CurItemType = "All"; //tabŬ������ ItemType������ �������� ���� ����� ����
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
        {//PlayFab�� ����Ǿ� �ִ� ���� ������ ����Ʈ ��������
            MyItemList = JsonConvert.DeserializeObject<List<ItemData>>(a_Item);
        }

        TabClick("All");

        if (CurItemList != null && CurItemList.Count > 0)
        {
            for (int ii = 0; ii < CurItemList.Count; ii++)
            {//����Ǿ� �ִ� ������ ����
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
        //���� ������ ����Ʈ�� Ŭ���� Ÿ�Ը� �߰�
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
        //���� ������ ����Ʈ�� Ŭ���� Ÿ�Ը� �߰�
        if (CurItemList != null && CurItemList.Count > 0)
        {
            //���԰� �ؽ�Ʈ ���̰� �ϱ�
            for (int i = 0; i < CurItemList.Count; i++)
            {
                m_ItemSlot[i].SetActive(i < CurItemList.Count);
                //�����۽��Կ� ��������
                Item a_Item = m_ItemSlot[i].GetComponent<Item>();

                a_Item._Grade = CurItemList[i].m_Grade;
                if (a_Item._Grade == "���")
                    a_Item.m_BackgroundImg.sprite = m_GreenSprite;
                else if (a_Item._Grade == "���")
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
                //�����۽��Կ� ��������
            }
            //���԰� �ؽ�Ʈ ���̰� �ϱ�

            for (int ii = CurItemList.Count; ii < MyItemList.Count; ii++)
            { 
                m_ItemSlot[ii].SetActive(false);
            }
        }
        else
        {//���� �ǿ� �����ִ� �������� ������
            for (int iii = 0; iii < m_ItemSlot.Length; iii++)
            {
                m_ItemSlot[iii].SetActive(false);
            }
        }

        //Tab��ư �̹��� �ٲ��ֱ�
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
        //Tab��ư �̹��� �ٲ��ֱ�

        if (MyItemList.Count == m_MaxItemSlot) //���â�� �������� ������ ������
            m_MyItemCountText.text = "<color=red>" + MyItemList.Count + "/" + m_MaxItemSlot + "</color>";
        else
            m_MyItemCountText.text = MyItemList.Count + "/" + m_MaxItemSlot;
    }

    bool IsItemSlot(Item a_Item) //���������� ���콺�� �ִ��� Ȯ���ϴ� �Լ�
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

    void MousePointOnItem() //���콺�� ������ ���� ���ٴ��� ��
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

                if (a_Item._Grade == "���")
                    m_IDGradeText.text = "<color=green>[��� : " + a_Item._Grade + "]</color>";
                else if(a_Item._Grade == "���")
                    m_IDGradeText.text = "<color=blue>[��� : " + a_Item._Grade + "]</color>";

                m_IDNameText.text = a_Item._Name;

                m_IDClassText.text = a_Item._Type;

                m_IDEffectText.text = "";
                if (a_Item._Att != 0)
                {
                    m_IDEffectText.text += "���ݷ� : + " + a_Item._Att + "\n" +
                                           "���� : + " + a_Item._Acc;
                }
                else
                {
                    m_IDEffectText.text += "���� : + " + a_Item._Def + "\n" +
                                           "ü�� : + " + a_Item._Hp;
                }

                m_IDSpawnAreaText.text = "<color=orange>[" + a_Item._SpawnArea + "]</color>";

                m_ItemDescriptionWd.transform.position = Input.mousePosition;
                m_ItemDescriptionWd.SetActive(true);

                a_ItemIdx = ii;
                break;
            }
        }

        if (a_ItemIdx == -1) //���콺�� �� ���� ��ų������ �ƴϸ�(���ֱ�)
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
            if (a_CurItem.m_DetailType == "�Ӹ����(����)")
            {
                InputItem(a_CurItem, m_WearingSlot[(int)ItemType.Helmet], a_ItemSlot);
                Sound_Mgr.Instance.PlayEffSound("Cloth", 1.0f);
            }
            else if (a_CurItem.m_DetailType == "����(����)")
            {
                InputItem(a_CurItem, m_WearingSlot[(int)ItemType.Top], a_ItemSlot);
                Sound_Mgr.Instance.PlayEffSound("Cloth", 1.0f);
            }
            else if (a_CurItem.m_DetailType == "����(����)")
            {
                InputItem(a_CurItem, m_WearingSlot[(int)ItemType.Ring], a_ItemSlot);
                Sound_Mgr.Instance.PlayEffSound("Ring", 1.0f);
            }
            else if (a_CurItem.m_DetailType == "�Ź�(����)")
            {
                InputItem(a_CurItem, m_WearingSlot[(int)ItemType.Boots], a_ItemSlot);
                Sound_Mgr.Instance.PlayEffSound("Cloth", 1.0f);
            }
        }
        //else if(a_CurItem.m_ItemType == "�Ҹ�ǰ")
        //{

        //}

        a_CurItem.m_IsUsing = true;
        GameMgr.Inst.UIRefresh();
        Save();
    }

    void InputItem(ItemData a_Item,Item a_WearingSlot, Item a_ItemSlot) //a_Item : ������ ������ , a_WearingSlot : ���� �Ǿ��ִ� ������, a_ItemSlot : ������ �������� ����
    {
        // ����Ǿ� �ִ� �������� ������ ����
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
        // ����Ǿ� �ִ� �������� ������ ����

        //UI �� ���� ����
        a_WearingSlot.m_BackgroundImg.gameObject.SetActive(true);
        if (a_Item.m_Grade == "���")
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
        //UI �� ���� ����

        //�ɷ�ġ
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
        //�ɷ�ġ
    }

    public void WearingSlotClick(int a_SlotNum) //��������� ����
    {
        Item a_CurItem = m_WearingSlot[a_SlotNum];

        if (a_CurItem._IsUsing == false)
            return;

        if(a_CurItem._Type == "�Ӹ����(����)" || a_CurItem._Type == "����(����)" ||
                a_CurItem._Type == "����(����)" || a_CurItem._Type == "�Ź�(����)")
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

        //��������� ���� �ʱ�ȭ
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

        int a_RdmIdx = Random.Range(0, 1); //(0, 2); (���� óġ �� ���ϴ� Ȯ���� ���� �������� ȹ�� ���� ����)
        if (a_RdmIdx == 0) // 100% Ȯ���� ������ ����ǵ��� ����
        {
            int a_RandomIdx = Random.Range(0, AllItemList.Count);
            ItemData a_GainItem = AllItemList[a_RandomIdx];
            if (a_GainItem.m_SpawnArea == a_Monster.m_SpawnArea) //50%, �� ������ȹ�� Ȯ�� 50%
            {
                if (MyItemList == null) //������ ùȹ��
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
                if (a_GainItem.m_Grade == "���")
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
