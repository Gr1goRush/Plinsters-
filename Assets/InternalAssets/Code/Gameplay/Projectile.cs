using DG.Tweening;
using System;
using System.Diagnostics;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    

    public event Action<Projectile> onRelease;
    private bool _isPlayAnim = false;

    


    [SerializeField, HideInInspector] private AudioSource source;
    [SerializeField, HideInInspector] private Rigidbody2D rigidBody;
    public AudioClip hitSound;
    public AudioClip killMiniSound;
    

    private void OnValidate()
    {
        source ??= GetComponent<AudioSource>();
        rigidBody ??= GetComponent<Rigidbody2D>();
    }


    public void Activate()
    {
        _isPlayAnim = false;
        this.gameObject.SetActive(true);
    }

    public void Disable()
    {
        _isPlayAnim = false;
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (PauseManager.Paused)
        {
            rigidBody.velocity = Vector3.zero;
            rigidBody.gravityScale = 0;
        }
        else
        {
            rigidBody.gravityScale = 1;
        }

        if (transform.position.y < -10) { Dead(); }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (!_isPlayAnim)
        {
            source.PlayOneShot(hitSound);
            _isPlayAnim = true;
            transform.localScale = Vector3.one * 0.8f;

            transform.DOShakeScale(0.07f, 0.3f).
                onComplete = () =>
                {
                    transform.localScale = Vector3.one * 0.8f;
                    _isPlayAnim = false;
                };
        }

        if (collision.gameObject.TryGetComponent<BossController>( out BossController boss))
        {
            boss.TakeDamage();
            Dead();
        }
        else if (collision.gameObject.TryGetComponent<MiniMonsterBehaviour>(out MiniMonsterBehaviour mini))
        {
            source.PlayOneShot(killMiniSound);
            mini.Death();
          
        }
        else if (collision.gameObject.TryGetComponent<Bonus>(out Bonus bonus))
        {
            bonus.CastEffect();

        }
    }


    

    private void Dead()
    {
        onRelease?.Invoke(this);
    }
}
