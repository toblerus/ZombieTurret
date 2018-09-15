using System;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class SkeletonKnight : AbstractEnemy
    {
        protected override void Attack()
        {
            Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(x =>
            {
                DoDamage();
                //Debug.Log("DOING DAMAGE");
            }).AddTo(gameObject);
        }

        protected override void Movement()
        {
            var rnd = Random.Range(_minMovementSpeed, _maxMovementSpeed);

            _rigidBody.velocity = Vector2.left * rnd;
        }
    }
}