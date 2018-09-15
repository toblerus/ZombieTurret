using Enemy;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PickupSpawner : MonoBehaviour {

    public GameObject[] prefabs;

	void Start () {
        MessageBroker.Default.Receive<EnemyDiedEvent>().Subscribe(a => SpawnRandomPickup(a.position));

    }

    public void SpawnRandomPickup(Vector3 refactor) 
    {
        if (Random.Range(0.0f, 1.0f) > 0.5) {
            int randomObject = Random.Range(0, prefabs.Length - 1);
            Instantiate(prefabs[randomObject], refactor, Quaternion.identity);
        }
        
        

    }

}
	


