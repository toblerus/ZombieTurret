using Enemy;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PickupSpawner : MonoBehaviour {

    public GameObject[] prefabs;
    public float spawningRate;
	void Start () {
        MessageBroker.Default.Receive<EnemyDiedEvent>().Subscribe(a => SpawnRandomPickup(a.position));

    }

    public void SpawnRandomPickup(Vector3 refactor) 
    {
        if (Random.Range(0.0f, 1.0f) > (1 - spawningRate)) {
            int randomObject = Random.Range(0, prefabs.Length);
            Debug.Log(randomObject);
            Instantiate(prefabs[randomObject], refactor, Quaternion.identity);
        }
        
        

    }

}
	


