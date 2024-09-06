using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMonsterBehaviour : MonoBehaviour
{
    public static event Action OnReachTop;

    [SerializeField] private float _speed;
    private List<Vector3> _path = new List<Vector3>();

    public GameObject particleProjectile;

    private void OnDrawGizmosSelected()
    {
        if (_path.Count <= 0) return;
        Vector3[] pathArray = _path.ToArray();

        for (int i = 0; i < pathArray.Length - 1; i++) 
        { 
            Gizmos.color = Color.red;

            Gizmos.DrawLine(pathArray[i], pathArray[i + 1]);
        }
    }

    private void Start()
    {
        BuildPath(OnComplete: StartMove);
    }

    private void OnEnable()
    {
        Bonus.OnUltimateBonusRelease += Death;
    }

    private void OnDisable()
    {
        Bonus.OnUltimateBonusRelease -= Death;
    }

    public void StartMove()
    {
        StartCoroutine(MoveByPath());
    }

    public void BuildPath(Action OnComplete)
    {
        _path.Clear();

        GridStartPositions positions = FindObjectOfType<GridStartPositions>();
        GridObject obj = positions.StartGrids[UnityEngine.Random.Range(0, positions.StartGrids.Length)];

        float DirectionStart = obj.transform.position.x > transform.position.x ? 0.2f : -0.2f;
        for (float i = -0.3f; i <= 0.7f; i += 0.1f)
        {
            _path.Add(obj.transform.position - (Vector3.up / 3) + (Vector3.right * (DirectionStart + i) / 2) + (Vector3.up * i / 3));
        }

        GridObject prevObj = null;
        _path.Add(obj.transform.position);

        while (obj._connectedGrids.Length > 0)
        {
            prevObj = obj;
            obj = obj._connectedGrids[UnityEngine.Random.Range(0, obj._connectedGrids.Length)];

            float Direction = obj.transform.position.x > prevObj.transform.position.x ? 0.2f : -0.2f;

            for (float i = -0.3f; i <= 0.7f; i += 0.1f)
            {
                _path.Add(obj.transform.position - (Vector3.up / 3) + (Vector3.right * (Direction + i) / 2) + (Vector3.up * i / 3));
            }

            _path.Add(obj.transform.position + Vector3.up * 0.3f);
        }

        OnComplete.Invoke();

    }

    private IEnumerator MoveByPath()
    {
        foreach (var point in  _path)
        {
            while (transform.position != point)
            {
                if (!PauseManager.Paused)
                {
                    transform.position = Vector3.MoveTowards(transform.position, point, _speed * Time.deltaTime);
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    yield return new WaitForEndOfFrame();
                }
            }
        }

        OnReachTop?.Invoke();
        Destroy(gameObject);
    }

    public void Death()
    {
        Instantiate(particleProjectile, transform.position, Quaternion.identity);
        transform.DOKill();
        Destroy(gameObject);
    }
}
