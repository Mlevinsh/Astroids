using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidControl : MonoBehaviour {

    public AudioClip destroy;
    public GameObject smallAsteroid;

    private GameControl gameController;

	// Use this for initialization
	void Start () {

        GameObject gameControllerObject = GameObject.FindWithTag("GameControl");

        gameController = gameControllerObject.GetComponent<GameControl>();

        GetComponent<Rigidbody2D>().AddForce(transform.up * Random.Range(-50.0f, 150.0f)); // push asteroid in direction it's facing

        GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-0.0f, 90.0f);//
	}
	
    void OnCollisionEnter2D(Collision2D c)
    {
        Destroy(c.gameObject);

        //if large asteroid spawn new ones
        if (tag.Equals("LargeAsteroid"))
        {
            Instantiate(smallAsteroid, new Vector3(transform.position.x - 0.5f, transform.position.y - 0.5f, 0), Quaternion.Euler(0, 0, 90));
            Instantiate(smallAsteroid, new Vector3(transform.position.x + 0.5f, transform.position.y - 0.0f, 0), Quaternion.Euler(0, 0, 0));
            Instantiate(smallAsteroid, new Vector3(transform.position.x + 0.5f, transform.position.y - 0.5f, 0), Quaternion.Euler(0, 0, 270));
        }
        else
        {
            gameController.DecrementAsteroids();
        }

        AudioSource.PlayClipAtPoint(destroy, Camera.main.transform.position); // plays sound where camera is
        gameController.IncrementScore(); // Increases score
        Destroy(gameObject); // destroys current asteroid

    }

	// Update is called once per frame
	void Update () {
		
	}
}
