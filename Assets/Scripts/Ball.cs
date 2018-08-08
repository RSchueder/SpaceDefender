using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // config parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush;
    [SerializeField] float yPush;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.5f;

    // state
    Vector2 paddle2ballVector;
    bool hasStarted = false;

    // Cached component references
    // good when "get" is done many times, will prevent uneccessary getting
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    // Use this for initialization
    void Start ()
    {
        paddle2ballVector = transform.position - paddle1.transform.position;
        // more efficient to only get it once
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidBody2D.velocity = new Vector2(xPush, yPush);
        }
     }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddle2ballVector;     
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasStarted)
        {
            Vector2 velocityTweak = new Vector2(UnityEngine.Random.Range(-randomFactor,randomFactor),UnityEngine.Random.Range(-randomFactor,randomFactor));
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            //GetComponent<AudioSource>().Play();
            // play through entire clip even if another audio clip is triggered
            myAudioSource.PlayOneShot(clip);
            // add small velocity component
            myRigidBody2D.velocity = myRigidBody2D.velocity + velocityTweak;

        }
    }
}
