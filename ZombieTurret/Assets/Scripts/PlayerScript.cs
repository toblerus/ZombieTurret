using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.UI_Scripts;
using Enemy;
using UniRx;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private int _maxLife;
    private int _life;


    Vector3 aimPosition;

    public GameObject bulletPrefab;

    public float projectileForce = 500;

    [SerializeField] private SpriteRenderer _shaftSpriteRenderer;

    [SerializeField] private Sprite BowPulled;
    [SerializeField] private Sprite BowRest;
    private bool canShoot;

    private GameManager Manager;

    public Sprite[] ArrowSprites;

    // Use this for initialization
    void Start()
    {
        _life = _maxLife;
        canShoot = true;
        BroadcastLife();

        Manager = FindObjectOfType<GameManager>();

        MessageBroker.Default.Receive<DamagePlayerEvent>().Select(evt => evt.Amount).Subscribe(TakeDamage);
    }

    // Update is called once per frame
    void Update()
    {
        aimPosition = Input.mousePosition;
        //subtract depth
        aimPosition.z = transform.position.z - Camera.main.transform.position.z;
        //normalize
        aimPosition = Camera.main.ScreenToWorldPoint(aimPosition);

        ChangeRotation();

        if (Input.GetMouseButtonDown(0))
        {
            if (canShoot)
                StartCoroutine(Shoot());
        }
    }

    void ChangeRotation()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }

    void TakeDamage(int dmg)
    {
        _life -= dmg;
        BroadcastLife();
        if (_life <= 0)
        {
            MessageBroker.Default.Publish(new PlayerDiedEvent());
            Debug.Log("Player died");
        }
    }

    private void BroadcastLife()
    {
        MessageBroker.Default.Publish(new PlayerLifeUpdatedEvent {Life = _life});
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        _shaftSpriteRenderer.sprite = BowPulled;
        var q = Quaternion.FromToRotation(Vector3.up, aimPosition - transform.position);
        var bullet = Instantiate(bulletPrefab, transform.position, q);
        var arrowScript = bullet.GetComponent<ArrowScript>();
        arrowScript.Damage = Manager.Damage;
        bullet.GetComponentInChildren<SpriteRenderer>().sprite = GetSpriteForTurretTear();
        bullet.transform.SetParent(_shaftSpriteRenderer.transform);
        yield return new WaitForSeconds(0.2f);
        _shaftSpriteRenderer.sprite = BowRest;
        bullet.transform.SetParent(null);
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * GetArrowForce());
        arrowScript.EnableArrowCollisions();
        canShoot = true;
    }

    private Sprite GetSpriteForTurretTear()
    {
        return ArrowSprites[Manager.TurretLevel - 1];
    }

    private float GetArrowForce()
    {
        return projectileForce * Manager.TurretLevel;
    }
}