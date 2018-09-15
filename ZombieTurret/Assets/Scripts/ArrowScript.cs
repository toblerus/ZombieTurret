using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {

    Rigidbody2D rb;
    float killVelocity = 10;

	// Use this for initialization
	void Start () {
		rb  = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        if (rb.velocity.magnitude < killVelocity) {
            //var c = GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject,1);
        }
		
	}

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}
