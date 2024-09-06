using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayBoot : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip[] musicClips;

    [Space(30f)]

    public GameObject[] BossPrefabs;

    public Image BGImage;
    public Sprite[] BGSprites;
    public Transform bossRoot;

    public static int BGToLoad = 0;


    public static int BossToLoad = 0;
    public static int LevelID = 1;
    public static int MusicToLoad = 0;

    private void Start()
    {
        BGImage.sprite = BGSprites[BGToLoad];
        Instantiate(BossPrefabs[BossToLoad], bossRoot.transform.position, Quaternion.identity);
        musicSource.clip = musicClips[MusicToLoad];
        musicSource.Play();
    }

    public void OpenLevel()
    {
        Debug.Log("Unlock new level");
        if (LevelID >= SaveDataManager.CompletedLevels)
        SaveDataManager.UnlockNewLevel();
    }

    public void LoadNext()
    {
        LevelID++;
        BossToLoad++;

        if (LevelID > 4) { SceneHelper.LoadScene(ProjectScene.Menu); return; }
        if (BossToLoad > 2) BossToLoad = 0;

        SceneHelper.LoadScene(ProjectScene.Gameplay); return;
    }
}
