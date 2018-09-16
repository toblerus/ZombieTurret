using System;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class SkeletonKnight : AbstractEnemy
    {
        protected override void DistanceToPlayer(float distance)
        {
            AttackLogic(distance);

            MovementLogic(distance);
        }

        public float AttackDistance;

        private void MovementLogic(float distance)
        {
            if (distance > AttackDistance)
            {
                if (_movementDisposable.Count == 0)
                {
                    Observable.EveryUpdate().Subscribe(x => { Movement(); }).AddTo(_movementDisposable)
                        .AddTo(gameObject);
                }
            }
            else
            {
                _movementDisposable.Dispose();
                _rigidBody.velocity = Vector2.zero;
            }
        }

        private void AttackLogic(float distance)
        {
            if (distance <= AttackDistance)
            {
                Attack();
            }
            else
            {
                if (_knightAttackTimer.Count > 0)
                    _knightAttackTimer.Dispose();
            }
        }

        readonly CompositeDisposable _knightAttackTimer = new CompositeDisposable();

        protected override void Attack()
        {
            if (_knightAttackTimer.Count == 0)
                Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(x => { DoDamage(); })
                    .AddTo(_knightAttackTimer)
                    .AddTo(gameObject);
        }

        protected override void Movement()
        {
            var teste = GetComponentInChildren<Animation>();
            teste["Horse"].speed = GetLifePercentage;

            var rnd = Random.Range(_minMovementSpeed, _maxMovementSpeed) * GetLifePercentage;
            _rigidBody.velocity = Vector2.left * rnd;
        }
    }
}