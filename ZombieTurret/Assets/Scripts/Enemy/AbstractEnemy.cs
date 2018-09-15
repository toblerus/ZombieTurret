using System;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public abstract class AbstractEnemy : MonoBehaviour
    {
        [SerializeField] private int _life;
        [SerializeField] private int _gold;

        [SerializeField] protected float _maxMovementSpeed;
        [SerializeField] protected float _minMovementSpeed;

        [SerializeField] private float _damageAmount;


        protected Rigidbody2D _rigidBody;


        private readonly CompositeDisposable _movementDisposable = new CompositeDisposable();

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();


            Observable.EveryUpdate().Subscribe(x => { Movement(); }).AddTo(_movementDisposable);
        }

        private void TakeDamage()
        {
        }

        protected void DoDamage()
        {
            MessageBroker.Default.Publish(new DamagePlayerEvent {Amount = _damageAmount});
        }

        protected abstract void Attack();

        protected abstract void Movement();

        private void OnDeath()
        {
            _movementDisposable.Dispose();
            MessageBroker.Default.Publish(new EnemyDiedEvent {Gold = _gold});
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (TookDamage(other))
            {
                ReceiveDamage(other);
            }

            if (CollidedToTown(other))
            {
                _movementDisposable.Dispose();
                _rigidBody.velocity = Vector2.zero;
                Attack();
            }
        }

        private void ReceiveDamage(Collision2D other)
        {
            _life -= other.gameObject.GetComponent<ArrowScript>().Damage;

            if (NoLifeLeft)
            {
                OnDeath();
            }
        }

        private bool NoLifeLeft
        {
            get { return _life == 0; }
        }

        private static bool TookDamage(Collision2D other)
        {
            return other.gameObject.CompareTag("Projectile");
        }

        private static bool CollidedToTown(Collision2D other)
        {
            return other.gameObject.name == "Town";
        }
    }
}