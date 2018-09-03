using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    public GameObject laser;
    float shoot;
    float health = 300;
    public float beamSpeed = 1;
    ScoreKeeper scoreKeeper;
    AudioSource myAudioSource;
    [SerializeField] float firingRate = 0.03f;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] AudioClip[] enemyFire;
    [SerializeField] AudioClip[] enemyDamage;
    [SerializeField] AudioClip[] enemyDeath;


    private void Start()
    {
        //InvokeRepeating("Fire", 1f, firingRate);
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        myAudioSource = GetComponent<AudioSource>();

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
            AudioClip clip = enemyDamage[0];
            AudioSource.PlayClipAtPoint(clip, transform.position);
            if (health <= 0)
            {
                Die();
            }
            missle.Hit();
        }
        else if(spaceship)
        {
            Debug.Log("SHIP");
        }
    }

    private void Die()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        AudioClip clip = enemyDeath[0];
        AudioSource.PlayClipAtPoint(clip, transform.position);
        Destroy(gameObject);
        Destroy(sparkles, 1f);
        scoreKeeper.AddScore();
    }

    private void Hit()
    {
        Destroy(gameObject);
    }

    private void Fire()
    {
        GameObject laserBeam = Instantiate(laser, transform.position + new Vector3(0, 0, -1), Quaternion.identity) as GameObject;
        AudioClip clip = enemyFire[0];
        AudioSource.PlayClipAtPoint(clip, transform.position);
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
