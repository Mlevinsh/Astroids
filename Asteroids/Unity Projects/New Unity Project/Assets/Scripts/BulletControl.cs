using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 1.0f); // bullet destroys itself after one second

        GetComponent <Rigidbody2D>().AddForce(transform.up * 400); //push the bullet in direction it's facing
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
