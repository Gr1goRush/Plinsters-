using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class LevelButton1 : MonoBehaviour
{
    public int levelID;
    public int BossToLoad;

    public TextMeshProUGUI LevelText;
    public GameObject lockObject;
    public Color LockedColor;

    private bool IsLocked => (levelID > SaveDataManager.CompletedLevels);

    private void OnEnable()
    {
        if (IsLocked)
        {
            LevelText.color = LockedColor;
            lockObject.SetActive(true);
        }
        else
        {
            LevelText.color = Color.white;
            lockObject.SetActive(false);
        }
    }

    private void OnValidate()
    {
        LevelText.text = "LEVEL " + levelID.ToString();
    }


    public void LoadLevel()
    {
        if (!IsLocked)
        {
            GameplayBoot.LevelID = levelID;
            GameplayBoot.BossToLoad = BossToLoad;
            SceneHelper.LoadScene(ProjectScene.Gameplay);
        }
        else
        {
            lockObject.transform.DOShakeScale(0.3f, 0.3f).onComplete = () =>
            {
                lockObject.transform.localScale = Vector3.one;
            };
        }
    }

    
}
