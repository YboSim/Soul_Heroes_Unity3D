using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigPanel : MonoBehaviour
{
    public Toggle m_SoundToggle;
    public Slider m_OverallVol;
    public Slider m_EffVol;

    // Start is called before the first frame update
    void Start()
    {
        if (m_SoundToggle != null)
            m_SoundToggle.onValueChanged.AddListener(SoundOnOff);

        if (m_OverallVol != null)
            m_OverallVol.onValueChanged.AddListener(BGMSliderChanged);

        if (m_EffVol != null)
            m_EffVol.onValueChanged.AddListener(EffSliderChanged);

        //--- 체크 상태, 슬라이드 상태 로딩 후 UI컨트롤에 적용
        int a_SoundOnOff = PlayerPrefs.GetInt("SoundOnOff", 1);
        if (m_SoundToggle != null)
        {
            if (a_SoundOnOff == 1)
                m_SoundToggle.isOn = true;
            else
                m_SoundToggle.isOn = false;
        }

        if (m_EffVol != null)
            m_EffVol.value = PlayerPrefs.GetFloat("EffSoundVolume", 1.0f);

        if (m_OverallVol != null)
            m_OverallVol.value = PlayerPrefs.GetFloat("BGMSoundVolume", 1.0f);
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    void SoundOnOff(bool value) //체크 상태가 변경 되었을 때 호출되는 함수
    {
        Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

        if (m_SoundToggle != null)
        {
            if (value == true)
                PlayerPrefs.SetInt("SoundOnOff", 1);
            else
                PlayerPrefs.SetInt("SoundOnOff", 0);
        }

        Sound_Mgr.Instance.SoundOnOff(value);
    }

    void EffSliderChanged(float value)
    {
        PlayerPrefs.SetFloat("EffSoundVolume", value);
        Sound_Mgr.Instance.EffSoundVolume(value);
    }

    void BGMSliderChanged(float value)
    {
        PlayerPrefs.SetFloat("BGMSoundVolume", value);
        Sound_Mgr.Instance.BGMSoundVolume(value);
    }
}
