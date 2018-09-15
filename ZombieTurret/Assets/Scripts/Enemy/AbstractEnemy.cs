using System;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public abstract class AbstractEnemy : MonoBehaviour
    {
        [SerializeField] private int _life;
        [SerializeField] private float _maxMovementSpeed;
        [SerializeField] private float _minMovementSpeed;

        [SerializeField] private float _damageAmount;


        private Rigidbody2D _rigidBody;

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();


            Debug.Log("Here we are");
            Movement();
        }

        private void TakeDamage()
        {
        }

        protected void DoDamage()
        {
            MessageBroker.Default.Publish(new DamagePlayerEvent {Amount = _damageAmount});
        }

        protected abstract void Attack();

        private void Movement()
        {
            var rnd = Random.Range(_minMovementSpeed, _maxMovementSpeed);

            _rigidBody.velocity = Vector2.left * rnd;
        }

        private void OnDeath()
        {
//Maybe call an event to give something to the player
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            

            if (TookDamage(other))
            {
                ReceiveDamage(other);
            }

            if (!CollidedToTown(other))
            {
                _rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
                Attack();
            }
        }

        private void ReceiveDamage(Collision2D other)
        {
            _life -= other.gameObject.GetComponent<ArrowScript>().Damage;
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