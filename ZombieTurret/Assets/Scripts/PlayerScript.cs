using System.Collections;
using System.Collections.Generic;
using Enemy;
using UniRx;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]private int _maxLife;
    private int _life;


    Vector3 aimPosition;

    public GameObject bulletPrefab;

    public float projectileForce = 500;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        aimPosition = Input.mousePosition;
        //subtract depth
        aimPosition.z = transform.position.z - Camera.main.transform.position.z;
        //normalize
        aimPosition = Camera.main.ScreenToWorldPoint(aimPosition);

        changeRotation();

        if (Input.GetMouseButtonDown(0)) {
            shoot();
        }

    }

    void changeRotation() {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }

    void shoot() {
        var q = Quaternion.FromToRotation(Vector3.up, aimPosition - transform.position);
        var bullet = Instantiate(bulletPrefab, transform.position, q);
        //add specific logic from framework
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * projectileForce);
    }

    void TakeDamage()
    {
        MessageBroker.Default.Receive<DamagePlayerEvent>().Subscribe(evt => { }).AddTo(gameObject);
    }

}
