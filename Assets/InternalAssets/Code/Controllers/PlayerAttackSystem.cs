using UnityEngine;
using UnityEngine.Pool;

public class PlayerAttackSystem : MonoBehaviour
{
    [SerializeField] private Transform _playerRoot;
    [SerializeField] private Projectile[] Projectiles;

    public float AttackCoolDown = 0.5f;
    private float coolDown;

    private ObjectPool<Projectile> _projectilePool;

    private void Start()
    {
        _projectilePool = new ObjectPool<Projectile>(CreateProjectile, OnTakeFromPool, OnReturnToPool, OnDestroyObject, false, 200, 200);
    }

    private void Update()
    {
        if (PauseManager.Paused) { return; }

        coolDown -= Time.deltaTime;
        if (coolDown > 0) { return; }


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                coolDown = AttackCoolDown;
                Projectile projectile = _projectilePool.Get();
                projectile.transform.position = _playerRoot.transform.position;
            }
        }
    }

    public Projectile CreateProjectile()
    {
        Projectile obj = Instantiate(Projectiles[Random.Range(0, Projectiles.Length)], _playerRoot.transform.position, Quaternion.identity);
        obj.gameObject.SetActive(false);
        obj.onRelease += ReturnObjectToPool;
        return obj;
    }

    public void OnTakeFromPool(Projectile obj)
    {
        obj.Activate();
    }
    public void OnReturnToPool(Projectile obj)
    {
        obj.Disable();
    }

    public void OnDestroyObject(Projectile obj)
    {
        Destroy(obj);
        obj.onRelease -= ReturnObjectToPool;
    }

    public void ReturnObjectToPool(Projectile obj)
    {
        _projectilePool.Release(obj);
    }
}
