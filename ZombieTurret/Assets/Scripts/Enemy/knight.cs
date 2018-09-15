using System;
using UnityEngine;
using UniRx;
using Random = UnityEngine.Random;


namespace Enemy
{
    public class Knight : AbstractEnemy
    {
        protected override void DistanceToPlayer(int distance)
        {
            AttackLogic(distance);

            MovementLogic(distance);
        }

        private void MovementLogic(int distance)
        {
            if (distance > 2)
            {
                if (_movementDisposable.IsDisposed)
                {
                    Observable.EveryUpdate().Subscribe(x => { Movement(); }).AddTo(_movementDisposable);
                }
            }
            else
            {
                _movementDisposable.Dispose();
                _rigidBody.velocity = Vector2.zero;
            }
        }

        private void AttackLogic(int distance)
        {
            if (distance < 2)
            {
                Attack();
            }
            else
            {
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
            var rnd = Random.Range(_minMovementSpeed, _maxMovementSpeed);
            _rigidBody.velocity = Vector2.left * rnd;
        }
    }
}