using System.Collections;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadGameCoroutine());
    }


    private IEnumerator LoadGameCoroutine()
    {
        yield return SaveDataManager.LoadSettingsData();
        yield break;
    }
}
