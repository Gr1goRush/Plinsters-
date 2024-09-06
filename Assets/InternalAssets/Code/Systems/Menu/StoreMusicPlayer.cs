using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreMusicPlayer : MonoBehaviour
{
    public AudioSource source;

    public AudioClip[] clips;

    public StoreTab musicTab;

    private bool IsReady = false;

    private void OnEnable()
    {
        musicTab.OnNewID += PlayMusic;
    }

    private void OnDisable()
    {
        musicTab.OnNewID -= PlayMusic;
    }


    public void PlayMusic(int id)
    {
        if (!IsReady)
        {
            IsReady = true;
            return;
        }
        StopAllCoroutines();
        StartCoroutine(Music(id));
    }

    private IEnumerator Music(int id)
    {
        //Debug.Log("MUSIC CHECK");
        source.clip = clips[id + 1];
        source.DOFade(0, 1f).onComplete = () =>
        {
            source.Play();
            source.DOFade(1f, 1f);
        };
        yield return new WaitForSeconds(5);


        source.clip = clips[0];

        source.DOFade(0, 1f).onComplete = () =>
        {
            source.Play();
            source.DOFade(1f, 1f);
        };

    }
}
