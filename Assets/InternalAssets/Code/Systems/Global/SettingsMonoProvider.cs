using System;
using UnityEngine;

public class SettingsMonoProvider : MonoBehaviour
{
    public static event Action<SettingsData> OnSettingsChanged;
    public static SettingsData ChachedSettingsData => SaveDataManager.SettingsData;

    private float _chachedSoundsVolume;
    public void SettingsSetMusicVolume(float state)
    {
        SaveDataManager.SettingsData.MusicVolume = state;
        OnSettingsChanged?.Invoke(SaveDataManager.SettingsData);
        SaveDataManager.SaveChachedSettingsData();
    }

    public void SettingsSetVibroEnabled(bool state)
    {
        SaveDataManager.SettingsData.VibroEnabled = state;
        OnSettingsChanged?.Invoke(SaveDataManager.SettingsData);
        SaveDataManager.SaveChachedSettingsData();
    }

    public void SettingsSetSoundsVolume(float state)
    {
        _chachedSoundsVolume = SaveDataManager.SettingsData.SoundsVolume;
        SaveDataManager.SettingsData.SoundsVolume = state;
        OnSettingsChanged?.Invoke(SaveDataManager.SettingsData);
        SaveDataManager.SaveChachedSettingsData();
    }

    public void ReturnSoundsFromChached()
    {
        SettingsSetSoundsVolume(_chachedSoundsVolume);
    }
}
