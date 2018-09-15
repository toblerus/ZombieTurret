﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    private Vector2 _spawnPosition;
    public GameObject Enemy;

    public int XPosition;
    [SerializeField] private float _minimumY;
    [SerializeField] private float _maximumY;

    public void SpawnEnemy()
    {
        _spawnPosition = new Vector2(XPosition, Random.Range(_minimumY, _maximumY));
        Instantiate(Enemy, _spawnPosition, Quaternion.identity);
    }
}