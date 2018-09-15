using System.Collections;
using System.Collections.Generic;
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

    // Use this for initialization
    void Start()
    {
        _life = _maxLife;
        BroadcastLife();

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
            Shoot();
        }
    }

    void ChangeRotation()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }

    void Shoot()
    {
        var q = Quaternion.FromToRotation(Vector3.up, aimPosition - transform.position);
        var bullet = Instantiate(bulletPrefab, transform.position, q);
        //add specific logic from framework
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * projectileForce);
    }

    void TakeDamage(int dmg)
    {
        _life -= dmg;
        BroadcastLife();
        if (_life == 0)
        {
            MessageBroker.Default.Publish(new PlayerDiedEvent());
        }
    }

    private void BroadcastLife()
    {
        MessageBroker.Default.Publish(new PlayerLifeUpdatedEvent {Life = _life});
    }
}