using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    Vector3 aimPosition;

    GameObject bulletPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            aimPosition = Input.mousePosition;
            //subtract depth
            aimPosition.z = transform.position.z - Camera.main.transform.position.z;
            //normalize
            aimPosition = Camera.main.ScreenToWorldPoint(aimPosition);

            shoot();            
        }
	}

    void shoot() {
        var q = Quaternion.FromToRotation(Vector3.up, aimPosition - transform.position);
        var bullet = Instantiate(bulletPrefab, transform.position, q);
        //add specific logic from framework
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up *  500);

    }
}
