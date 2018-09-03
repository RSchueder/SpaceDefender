using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour {
    Spaceship spaceship;
	// Use this for initialization
	void Start () {
        spaceship = FindObjectOfType<Spaceship>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = spaceship.transform.position;
    }

    public void RemoveThruster()
    {
        Destroy(gameObject);
    }
}
