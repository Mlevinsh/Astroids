using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour {

    float rotationSpeed = 100.0f;
    float thrustForce = 3.5f;

    public AudioClip shipCrash;
    public AudioClip shipShoot;

    public GameObject bullet;

    private GameControl gameController;
	// Use this for initialization
	void Start () {
        GameObject gameControllerObject = GameObject.FindWithTag("GameControl");
        gameController = gameControllerObject.GetComponent<GameControl>();
		
	}

    void FixedUpdate()
    {
        // Rotate Ship
        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);

        // Thrust Ship
        GetComponent<Rigidbody2D>().AddForce(transform.up * thrustForce * Input.GetAxis("Vertical"));

        // Bullet Shot
        if (Input.GetKey("space"))
        {
            Shoot();
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {

        // Anything except a bullet is an asteroid
        if (c.gameObject.tag != "Bullet")
        {

            AudioSource.PlayClipAtPoint
                (shipCrash, Camera.main.transform.position);

            // Move the ship to the centre of the screen
            transform.position = new Vector3(0, 0, 0);

            // Remove all velocity from the ship
            GetComponent<Rigidbody2D>().
                velocity = new Vector3(0, 0, 0);

            gameController.DecrementLives();
        }
    }

    void Shoot()
    {

        // Spawn a bullet
        Instantiate(bullet,
            new Vector3(transform.position.x, transform.position.y, 0),
            transform.rotation);

        // Play a shoot sound
        AudioSource.PlayClipAtPoint(shipShoot, Camera.main.transform.position);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
