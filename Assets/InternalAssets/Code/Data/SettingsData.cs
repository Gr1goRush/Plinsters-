using System;

[Serializable]
public class SettingsData
{
    public float MusicVolume;
    public float SoundsVolume;
    public bool VibroEnabled;

    public SettingsData(float musicEnabled, float soundsEnabled, bool vibroEnabled)
    {
        MusicVolume = musicEnabled;
        SoundsVolume = soundsEnabled;
        VibroEnabled = vibroEnabled;
    }

    public SettingsData() 
    {
        MusicVolume = 0;
        SoundsVolume = 0;
        VibroEnabled = true;
    }
}
