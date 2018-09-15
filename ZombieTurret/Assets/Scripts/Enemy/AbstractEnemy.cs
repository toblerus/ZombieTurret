using System;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public abstract class AbstractEnemy : MonoBehaviour
    {
        private int _life;
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
            if (!CollidedToTown(other)) return;

            _rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
           Attack();
        }

        private static bool CollidedToTown(Collision2D other)
        {
            return other.gameObject.name == "Town";
        }
    }
}