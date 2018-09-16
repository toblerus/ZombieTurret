using Enemy;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class HealthBagScript : AbstractEnemy {

    protected override void OnDeath() {

        var death = Instantiate(_deathEffect, transform, false);
        death.transform.SetParent(null);
        _movementDisposable.Dispose();
        MessageBroker.Default.Publish(new HealPlayerEvent());
        Destroy(gameObject);

    }

    protected override void Attack()
    {
    }

    protected override void DistanceToPlayer(float distance)
    {

    }

    protected override void Movement()
    {
    }


    void Start () {
    }
	
	void Update () {
		
	}
}
