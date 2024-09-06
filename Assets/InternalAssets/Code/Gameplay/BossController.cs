using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BossController : MonoBehaviour
{
    public static event Action<int> OnHealthChanged;
    public static event Action<int> OnBossSpawned;
    public static event Action OnBossLost;
    public int Spawnrate = 2;

    [SerializeField] private int _health;
    [SerializeField] private GameObject[] _spawnUnints;
    [SerializeField] private AudioSource _audioSource;
    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            OnHealthChanged?.Invoke(value);
        }
    }

    private void Start()
    {
        StartCoroutine(spawnUnitsCoroutine());
        OnBossSpawned?.Invoke(Health);
    }

    public void TakeDamage()
    {
        Health--;
        _audioSource.Play();
        if (Health <= 0)
        {
            OnBossLost?.Invoke();
        }

        transform.localScale = Vector3.one;
        transform.DOShakeScale(0.2f, 0.1f).onComplete = () => { transform.localScale = Vector3.one; };
    }

    private IEnumerator spawnUnitsCoroutine()
    {
        while (true)
        {
            if (!PauseManager.Paused)
            {
                for (int i = 0; i < Spawnrate; i++)
                {
                    if (!PauseManager.Paused)
                    {
                        yield return new WaitForSeconds(1);
                    }
                    else
                    {
                        while (PauseManager.Paused)
                        {
                            yield return new WaitForSeconds(Time.deltaTime);
                        }
                    }
                }
                Instantiate(_spawnUnints[UnityEngine.Random.Range(0, _spawnUnints.Length)], transform.position, Quaternion.identity);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
