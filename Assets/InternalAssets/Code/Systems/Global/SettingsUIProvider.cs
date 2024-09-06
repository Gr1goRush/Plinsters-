using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SettingsMonoProvider))]
public class SettingsUIProvider : MonoBehaviour
{
    [SerializeField, HideInInspector] private SettingsMonoProvider m_MonoProvider;

    private const float minValue = -80f;

    public Slider SoundSlider;
    public Image SoundImage;

    public Sprite SoundEnabledSprite;
    public Sprite SoundDisabledSprite;

    [Space(30f)]

    public Slider MusicSlider;
    public Image MusicImage;
    public Sprite MusicEnabledSprite;
    public Sprite MusicDisabledSprite;

    [Space(30f)]

    public Image VibroOffButtonImage;
    public Sprite VibroOffStageOff;
    public Sprite VibroOffStageOn;

    public Image VibroOnButtonImage;
    public Sprite VibroOnStageOff;
    public Sprite VibroOnStageOn;

    [Space(30f)]

    public Image VibroImage;
    public Sprite VibroDisabledSprite;
    public Sprite VibroEnabledSprite;

    private void OnValidate()
    {
        m_MonoProvider ??= GetComponent<SettingsMonoProvider>();
    }

    private void OnEnable()
    {
        LoadData();
    }

    public void SetMusicValue()
    {
        float value = MusicSlider.value;
        m_MonoProvider.SettingsSetMusicVolume(MusicSlider.value);

        MusicImage.sprite = value == minValue ? MusicDisabledSprite : MusicEnabledSprite;
    }


    public void SetSoundValue()
    {
        float value = SoundSlider.value;
        m_MonoProvider.SettingsSetSoundsVolume(value);

        SoundImage.sprite = value == minValue ? SoundDisabledSprite : SoundEnabledSprite;
    }

    public void EnableVibro()
    {
        m_MonoProvider.SettingsSetVibroEnabled(true);
        VibroOffButtonImage.sprite = VibroOffStageOff;
        VibroOnButtonImage.sprite = VibroOnStageOn;
        VibroImage.sprite = VibroEnabledSprite;
    }

    public void DisableVibro()
    {
        m_MonoProvider.SettingsSetVibroEnabled(false);
        VibroOffButtonImage.sprite = VibroOffStageOn;
        VibroOnButtonImage.sprite = VibroOnStageOff;
        VibroImage.sprite = VibroDisabledSprite;
    }

    public void SetVibroState(bool state)
    {
        m_MonoProvider.SettingsSetVibroEnabled(state);

        VibroOffButtonImage.sprite = state ? VibroOffStageOff : VibroOffStageOn;
        VibroOnButtonImage.sprite = state ? VibroOnStageOn : VibroOnStageOff;
        VibroImage.sprite = state ? VibroEnabledSprite : VibroDisabledSprite;
    }

    public void LoadData()
    {
        SoundSlider.value = SaveDataManager.SettingsData.SoundsVolume;
        MusicSlider.value = SaveDataManager.SettingsData.MusicVolume;

        SetMusicValue();
        SetSoundValue();
        SetVibroState(SaveDataManager.SettingsData.VibroEnabled);
    }
}
