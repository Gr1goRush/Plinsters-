using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    public UnityEvent OnPlayerLost;
    public UnityEvent OnPlayerTakeDamage;
    private int health = 2;

    [SerializeField] private Image[] _healthImages;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private void OnEnable()
    {
        MiniMonsterBehaviour.OnReachTop += TakeDamage;
    }

    private void OnDisable()
    {
        MiniMonsterBehaviour.OnReachTop -= TakeDamage;
    }

    public void TakeDamage()
    {
        health -= 1;
        OnPlayerTakeDamage?.Invoke();
        VibroHelper.PlayVibro();

        for (int i = 0; i < _healthImages.Length; i++)
        {
            if (i <= health) _healthImages[i].sprite = fullHeart;
            else _healthImages[i].sprite = emptyHeart;
        }

        if (health < 0) OnPlayerLost?.Invoke();
    }
}
