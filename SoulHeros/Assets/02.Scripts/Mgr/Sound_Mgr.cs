using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sound_Mgr : G_Singleton<Sound_Mgr>
{
    [HideInInspector] public AudioSource m_AudioSrc = null;
    Dictionary<string, AudioClip> m_ADClipList = new Dictionary<string, AudioClip>();

    //--- ȿ���� ����ȭ�� ���� ���� �Լ�
    int m_EffSdCount = 10; //������ 10���� ���̾�� �÷���...
    int m_SoundCount = 0; //�ִ� 10������ ����ǰ� ����(�������� ����...)
    List<GameObject> m_SndObjList = new List<GameObject>();
    AudioSource[] m_SndSrcList = new AudioSource[10];
    float[] m_EffVolume = new float[10];
    //--- ȿ���� ����ȭ�� ���� ���� �Լ�

    float m_bgmVolume = 1.0f;
    [HideInInspector] public bool m_SoundOnOff = true;
    [HideInInspector] public float m_EffSoundVolume = 1.0f;
    [HideInInspector] public float m_BGMSoundVolume = 1.0f;

    protected override void Init() //Awake() �Լ� ��� ���
    {
        base.Init(); //�θ��ʿ� �ִ� Init()�Լ� ȣ��

        LoadChildGameObj();
    }

    // Start is called before the first frame update
    void Start()
    {
        //���� �̸� �ε�
        AudioClip a_GAudioClip = null;
        object[] temp = Resources.LoadAll("Sounds"); //LoadAll : "Sounds" �������� ���ϵ��� ���� �ε��Ѵ�.
        for (int ii = 0; ii < temp.Length; ii++)
        {
            a_GAudioClip = temp[ii] as AudioClip;

            if (m_ADClipList.ContainsKey(a_GAudioClip.name) == true)
                continue;

            m_ADClipList.Add(a_GAudioClip.name, a_GAudioClip);
        }
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    public void LoadChildGameObj()
    {
        m_AudioSrc = this.gameObject.AddComponent<AudioSource>();

        //--- ���� ȿ���� �÷��̸� ���� 10���� ���̾� ���� �ڵ�
        for (int ii = 0; ii < m_EffSdCount; ii++)
        {
            GameObject newSoundObj = new GameObject();
            newSoundObj.transform.SetParent(this.transform);
            newSoundObj.transform.localPosition = Vector3.zero;
            AudioSource a_AudioSrc = newSoundObj.AddComponent<AudioSource>();
            a_AudioSrc.playOnAwake = false;
            a_AudioSrc.loop = false;
            newSoundObj.name = "SoundEffObj";

            m_SndSrcList[ii] = a_AudioSrc;
            m_SndObjList.Add(newSoundObj);
        }
        //--- ���� ȿ���� �÷��̸� ���� 5���� ���̾� ���� �ڵ�
    }

    public void PlayBGM(string a_FileName, float fVolume = 1.0f)
    {
        AudioClip a_GAudioClip = null;
        if (m_ADClipList.ContainsKey(a_FileName) == true)
        {
            a_GAudioClip = m_ADClipList[a_FileName] as AudioClip;

        }
        else
        {
            a_GAudioClip = Resources.Load("Sounds/" + a_FileName) as AudioClip;
            m_ADClipList.Add(a_FileName, a_GAudioClip);
        }


        if (m_AudioSrc == null)
            return;

        if (m_AudioSrc.clip != null && m_AudioSrc.clip.name == a_FileName)
            return;

        m_AudioSrc.clip = a_GAudioClip;
        m_AudioSrc.volume = fVolume * m_BGMSoundVolume;
        m_bgmVolume = fVolume;
        m_AudioSrc.loop = true;
        m_AudioSrc.Play();
    }

    public void PlayEffSound(string a_FileName, float fVolume = 0.3f)
    {
        if (m_SoundOnOff == false)
            return;

        AudioClip a_GAudioClip = null;
        if (m_ADClipList.ContainsKey(a_FileName) == true)
        {
            a_GAudioClip = m_ADClipList[a_FileName] as AudioClip;
        }
        else
        {
            a_GAudioClip = Resources.Load("Sounds/" + a_FileName) as AudioClip;
            m_ADClipList.Add(a_FileName, a_GAudioClip);
        }

        if (a_GAudioClip == null)
            return;

        if (m_SndSrcList[m_SoundCount] != null)
        {
            m_SndSrcList[m_SoundCount].volume = fVolume * m_EffSoundVolume;
            m_SndSrcList[m_SoundCount].PlayOneShot(a_GAudioClip, fVolume * m_EffSoundVolume);
            m_EffVolume[m_SoundCount] = fVolume;

            m_SoundCount++;
            if (m_EffSdCount <= m_SoundCount)
                m_SoundCount = 0;
        }
    }

    public void PlayGUISound(string a_FileName, float fVolume = 0.2f)
    {//GUI ȿ���� �÷��� �ϱ� ���� �Լ�
        if (m_SoundOnOff == false)
            return;

        AudioClip a_GAudioClip = null;
        if (m_ADClipList.ContainsKey(a_FileName) == true)
        {
            a_GAudioClip = m_ADClipList[a_FileName] as AudioClip;
        }
        else
        {
            a_GAudioClip = Resources.Load("Sounds/" + a_FileName) as AudioClip;
            m_ADClipList.Add(a_FileName, a_GAudioClip);
        }

        if (m_AudioSrc == null)
            return;

        m_AudioSrc.PlayOneShot(a_GAudioClip, fVolume * m_EffSoundVolume);
    }

    public void SoundOnOff(bool a_OnOff = true) //BGM�� EFF ���� OnOff �������ִ� �Լ�
    {
        bool a_MuteOnOff = !a_OnOff;

        if (m_AudioSrc != null)
        {
            m_AudioSrc.mute = a_MuteOnOff; //mute == true ���� mute == false �ѱ�
            if (a_MuteOnOff == false)
                m_AudioSrc.time = 0;      //ó������ �ٽ� �÷���
        }

        for (int ii = 0; ii < m_EffSdCount; ii++)
        {
            if (m_SndSrcList[ii] != null)
            {
                m_SndSrcList[ii].mute = a_MuteOnOff;

                if (a_MuteOnOff == false)
                    m_SndSrcList[ii].time = 0;
            }
        }

        m_SoundOnOff = a_OnOff;
    }

    //������� ���� ������ ������ �Ŀ� �÷��� �� �ش�.
    public void EffSoundVolume(float fVolume) //EFF ���� ���� �������ִ� �Լ�
    {
        for (int ii = 0; ii < m_EffSdCount; ii++)
        {
            if (m_SndSrcList[ii] != null)
                m_SndSrcList[ii].volume = m_EffVolume[ii] * fVolume;
        }

        m_EffSoundVolume = fVolume;
    }

    public void BGMSoundVolume(float fVolume) //BGM ���� ���� �������ִ� �Լ�
    {
        if (m_AudioSrc != null)
            m_AudioSrc.volume = m_bgmVolume * fVolume;

        m_BGMSoundVolume = fVolume;
    }
}
