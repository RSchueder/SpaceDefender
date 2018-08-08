using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour {

    [Range(0.1f,10f)] [SerializeField] float speed = 1f;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool isAutoplayOn;

    int score = 0;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameStatus>().Length;
        if (gameStatusCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start ()
    {
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update () {
        Time.timeScale = 1f * speed;
	}
    
    public void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
    public void ResetGame()
    {
        Destroy(gameObject);
    }
    public bool IsAutoplayEnabled()
    {
        return isAutoplayOn;
    }
}
