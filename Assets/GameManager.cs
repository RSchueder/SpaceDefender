using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    SceneLoader sceneLoader;
    Spaceship spaceship;
    // Use this for initialization
    void Start () {
        sceneLoader = FindObjectOfType<SceneLoader>();
        spaceship = FindObjectOfType<Spaceship>();
    }

    // Update is called once per frame
    void Update () {
        if (spaceship.health <= 0)
        {
            sceneLoader.Lose();

        }

    }
}
