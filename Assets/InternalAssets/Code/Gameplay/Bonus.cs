using DG.Tweening;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Bonus : MonoBehaviour
{
    public static event System.Action OnBonusCatch;
    public static event System.Action OnUltimateBonusRelease;

    public CircleCollider2D _collider;
    public AudioSource source;
    public ProjectileType type;
    public AudioClip coinSound;
    public AudioClip ultimateSound;

    private void OnValidate()
    {
        _collider ??= GetComponent<CircleCollider2D>();
        source ??= GetComponent<AudioSource>();
    }

    private void Start()
    {
        transform.localScale = Vector3.zero;
        transform.transform.DOScale(0.8f, 0.7f);
    }

    public void CastEffect()
    {
        _collider.enabled = false;
        OnBonusCatch?.Invoke();
        switch (type)
        {
            case ProjectileType.None: break;

            case ProjectileType.Coin: MoneyHelper.AddMoney(1); source.PlayOneShot(coinSound); break;

            case ProjectileType.Ultimate: OnUltimateBonusRelease?.Invoke(); source.PlayOneShot(ultimateSound); break;
        }

        transform.DOScale(0f, 0.7f).onComplete = () =>
        {
            Destroy(gameObject);
        };
    }

    public enum ProjectileType
    {
        None,
        Coin,
        Ultimate
    }
}
