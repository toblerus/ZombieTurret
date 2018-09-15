using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour {

    private Vector2 _spawnPosition;
    public GameObject EnemyPrefab;

    public int XPosition;
    [SerializeField] private float _minimumY;
    [SerializeField] private float _maximumY;

    public int EnemySpawnDelay = 2;
    public CompositeDisposable SpawnerDisposable = new CompositeDisposable();


    private void Start()
    {
        StartSpawning();
    }

    public void StartSpawning()
    {
        SpawnerDisposable = new CompositeDisposable();
        Observable.Interval(TimeSpan.FromSeconds(EnemySpawnDelay)).Subscribe(_ => SpawnEnemy()).AddTo(SpawnerDisposable).AddTo(gameObject);

    }

    public void SpawnEnemy()
    {
        _spawnPosition = new Vector2(XPosition, Random.Range(_minimumY, _maximumY));
        Instantiate(EnemyPrefab, _spawnPosition, Quaternion.identity);
    }
}
