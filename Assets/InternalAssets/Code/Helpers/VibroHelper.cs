using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class VibroHelper
{
#if UNITY_IPHONE

    public static void PlayVibro()
    {
        if (SaveDataManager.SettingsData.VibroEnabled)
        Handheld.Vibrate();
    }

#else

     public static void PlayVibro()
    {
        Debug.Log("Vibro");
    }

#endif
}
