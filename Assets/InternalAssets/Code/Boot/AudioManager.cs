using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;


    private void OnEnable()
    {
        SettingsMonoProvider.OnSettingsChanged += ApplySettings; 
    }

    private void OnDisable()
    {
        SettingsMonoProvider.OnSettingsChanged -= ApplySettings;
    }

    private void Start()
    {
        ApplySettings(SaveDataManager.SettingsData);
    }

    public void ApplySettings(SettingsData data)
    {
        float soundVolume = data.SoundsVolume;
        audioMixer.SetFloat("SoundsVolume", soundVolume);

        float musicVolume = data.MusicVolume;
        audioMixer.SetFloat("MusicVolume", musicVolume);
    }
}
