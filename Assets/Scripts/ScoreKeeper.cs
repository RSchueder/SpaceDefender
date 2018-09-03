using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour {
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI healthText;
    Spaceship spaceship;
    SceneLoader sceneLoader;
    int score = 0;
    
	// Use this for initialization
	void Start ()
    {
        spaceship = FindObjectOfType<Spaceship>();
        sceneLoader = FindObjectOfType<SceneLoader>();

        scoreText.text = score.ToString();
        healthText.text = spaceship.health.ToString();
    }

    // Update is called once per frame
    void Update () {
        healthText.text = spaceship.health.ToString();
        scoreText.text = score.ToString();
        if(spaceship.health <= 0)
        {
            sceneLoader.Restart();
            Reset();
        }
    }

    public void AddScore()
    {
        score += 1;
    }
    private void Reset()
    {
        score = 0;
        spaceship.Reset();
    }
}
