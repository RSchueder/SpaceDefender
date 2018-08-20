using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    public GameObject laser;
    float shoot;
    float health = 300;
    public float beamSpeed = 1;
    [SerializeField] float firingRate = 0.03f;

    private void Start()
    {
        //InvokeRepeating("Fire", 1f, firingRate);

    }
    private void Update()
    {
        ProbFire();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        Projectile missle = collider.gameObject.GetComponent<Projectile>();
        Spaceship spaceship = collider.gameObject.GetComponent<Spaceship>();

        if (missle)
        {
            health -= missle.GetDamage();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            missle.Hit();
        }
        else if(spaceship)
        {
            Debug.Log("SHIP");
        }
    }

    private void Hit()
    {
        Destroy(gameObject);
    }

    private void Fire()
    {
        GameObject laserBeam = Instantiate(laser, transform.position + new Vector3(0, 0, -1), Quaternion.identity) as GameObject;
        laserBeam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -beamSpeed, 0);
    }
    private void ProbFire()
    {
        float probability = firingRate * Time.deltaTime;
        if (UnityEngine.Random.value < probability)
        {
            Fire();
        }
    }
}
