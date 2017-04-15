using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour {

    // Laser lifetime
    public float lifetime = 2.0f;
    // Laser speed
    public float speed = 5.0f;
    // Laser damage
    public int damage = 1;

	// Use this for initialization
	void Start ()
    {
        // Destroy object when lifetime is zero
        Destroy(gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
	}
}
