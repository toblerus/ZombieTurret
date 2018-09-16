using System.Security.Cryptography.X509Certificates;
using UniRx;
using Unity.Linq;
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
        [SerializeField] private GameObject _bloodEffect;
        [SerializeField] private GameObject _deathEffect;
        [SerializeField] private ObjectType _gameObjectType;

        protected Rigidbody2D _rigidBody;

        private readonly ReactiveProperty<float> _distanceToPlayer = new ReactiveProperty<float>();

        protected readonly CompositeDisposable _movementDisposable = new CompositeDisposable();

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _life = _maxLife;
            HealthBar.maxValue = _maxLife;
            HealthBar.value = _maxLife;

            _distanceToPlayer.Skip(1).Subscribe(DistanceToPlayer).AddTo(gameObject);

            Observable.EveryUpdate().Subscribe(x =>
            {
                GetDistanceToPlayer();
            }).AddTo(gameObject);

            MessageBroker.Default.Receive<DestroyGameObjectsOfTypeEvent>()
                .Subscribe(evt => SelfdestructIfNecessary(evt.ObjectTypeToDestroy))
                .AddTo(gameObject);
        }

        private void SelfdestructIfNecessary(ObjectType objObjectTypeToDestroy)
        {
            if (objObjectTypeToDestroy == ObjectType.All || objObjectTypeToDestroy == _gameObjectType)
            {
                gameObject.Destroy();
            }
        }

        protected abstract void DistanceToPlayer(float distance);

        protected void DoDamage()
        {
            MessageBroker.Default.Publish(new DamagePlayerEvent {Amount = _damageAmount});
        }

        protected abstract void Attack();

        protected abstract void Movement();

        private void OnDeath()
        {
            var death = Instantiate(_deathEffect, transform, false);
            death.transform.SetParent(null);
            _movementDisposable.Dispose();
            MessageBroker.Default.Publish(new EnemyDiedEvent {Gold = _gold, position = transform.position});
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (TookDamage(other))
            {
                ReceiveDamage(other);
            }
        }

        private void GetDistanceToPlayer()
        {
            var player = FindObjectOfType<PlayerScript>();
            var distance = Mathf.Abs(player.gameObject.transform.position.x - transform.position.x);
            _distanceToPlayer.Value = distance;
        }

        private void ReceiveDamage(Collision2D other)
        {
            _life -= other.gameObject.GetComponent<ArrowScript>().Damage;

            other.gameObject.GetComponent<Rigidbody2D>().simulated = false;
            other.gameObject.transform.SetParent(transform);

            HealthBar.value = _life;

            Instantiate(_bloodEffect, transform, false);

            if (NoLifeLeft)
            {
                OnDeath();
            }
        }

        private bool NoLifeLeft
        {
            get { return _life <= 0; }
        }

        private static bool TookDamage(Collision2D other)
        {
            return other.gameObject.CompareTag("Projectile");
        }

        public void applyDamage(int damage) {

            _life -= damage;

            HealthBar.value = _life;

            if (NoLifeLeft)
            {
                OnDeath();
            }
        }

        protected float GetLifePercentage
        {
            get
            {
                return ((_life * 100f) / _maxLife)/100f;
            }
        }

    }
}