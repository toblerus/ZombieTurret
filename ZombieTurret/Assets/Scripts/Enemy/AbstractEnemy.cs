using System;
using System.Security.Cryptography.X509Certificates;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Enemy
{
    public abstract class AbstractEnemy : MonoBehaviour
    {
        [SerializeField] private int _maxLife;
        private int _life;
        [SerializeField] private int _gold;

        [SerializeField] protected float _maxMovementSpeed;
        [SerializeField] protected float _minMovementSpeed;

        [SerializeField] private Slider HealthBar;

        [SerializeField] private int _damageAmount;


        protected Rigidbody2D _rigidBody;

        private readonly ReactiveProperty<int> _distanceToPlayer = new ReactiveProperty<int>();

        protected readonly CompositeDisposable _movementDisposable = new CompositeDisposable();

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _life = _maxLife;
            HealthBar.maxValue = _maxLife;
            HealthBar.value = _maxLife;

            Observable.EveryUpdate().Subscribe(x =>
            {
                Movement();
            }).AddTo(_movementDisposable);

            _distanceToPlayer.Skip(1).Subscribe(DistanceToPlayer).AddTo(gameObject);

            Observable.EveryUpdate().Subscribe(x =>
            {
                GetDistanceToPlayer();
            }).AddTo(gameObject);
        }

        protected abstract void DistanceToPlayer(int distance);

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
        }

        void GetDistanceToPlayer()
        {
            var player = FindObjectOfType<PlayerScript>();
            var distance = Vector2.Distance(player.gameObject.transform.position, transform.position);
            _distanceToPlayer.Value = (int)distance;
            Debug.Log(distance);

        }

        private void ReceiveDamage(Collision2D other)
        {
            _life -= other.gameObject.GetComponent<ArrowScript>().Damage;

            HealthBar.value = _life;
            
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