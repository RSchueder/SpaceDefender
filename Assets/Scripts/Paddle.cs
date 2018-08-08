using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
    [SerializeField] float screenWidthInUnits;
    [SerializeField] float minX;
    [SerializeField] float maxX;

    // cache
    GameStatus gameStatus;
    Ball ball;
    // Use this for initialization
    void Start ()
    {
        // only cache once to minimize the number of times we call FindObjextOfType
        gameStatus = FindObjectOfType<GameStatus>();
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update () {
        // when specifying a new position, you need to do so in units apparently
        // Debug.Log(Input.mousePosition.x);
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform.position = paddlePos;
	}
    private float GetXPos()
    {
        if (gameStatus.IsAutoplayEnabled())
        {
            return ball.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
