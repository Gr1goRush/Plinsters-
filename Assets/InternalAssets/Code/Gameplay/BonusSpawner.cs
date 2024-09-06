using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private Bonus[] BonusArray;
    [SerializeField, HideInInspector] private GridObject[] _spawnRoot;

    [SerializeField] private float CoolDown;
    [SerializeField] private int BonusLimit;

    private float cooldown;
    private float bonusCount;

    private List<Vector3> aviaviableSpawn = new List<Vector3>();
    public Vector3 RandomSpawnPoint 
    {
        get 
        {
            Vector3 value = Vector3.zero;

            if (aviaviableSpawn.Count == 0) 
            {
                foreach (var item in _spawnRoot)
                {
                    aviaviableSpawn.Add(item.transform.position + Vector3.up / 2);
                }
            }

            value = aviaviableSpawn.ElementAt(Random.Range(0, aviaviableSpawn.Count));
            aviaviableSpawn.Remove(value);
            return value;
        
        }

        set { RandomSpawnPoint = value; }
    }

    private void OnValidate()
    {
        _spawnRoot ??= FindObjectsOfType<GridObject>();
    }

    private void OnEnable()
    {
        Bonus.OnBonusCatch += DecreaseBonusCount;
    }

    private void OnDisable()
    {
        Bonus.OnBonusCatch -= DecreaseBonusCount;
    }

    public void DecreaseBonusCount()
    {
        bonusCount--;
    }

    private void Update()
    {
        if (PauseManager.Paused) return;

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            if (bonusCount >= BonusLimit) return; 
            Instantiate(BonusArray[Random.Range(0, BonusArray.Length)], RandomSpawnPoint, Quaternion.identity);
            bonusCount++;
            cooldown = CoolDown;
        }

    }
}
