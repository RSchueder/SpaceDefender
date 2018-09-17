using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour {
    [SerializeField] float rad;
    [SerializeField] float speed;
    private bool movingDown = true;
    float xmin;
    float xmax;
    float ymin;
    float ymax;
    //orig = new Vector3(transform.position.x, transform.position.y, transform.position.z);

    // Use this for initialization
    void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        Vector3 topmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance));
        Vector3 bottommost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));

        /*xmin = leftmost.x;
        xmax = rightmost.x;
        ymin = leftmost.y;
        ymax = topmost.y;
        */
        ymin = -4;
        ymax = 4;
 
    }
	
	// Update is called once per frame
	void Update () {
        if (movingDown)
        {
            transform.position += speed * Vector3.down * Time.deltaTime;
        }
        else
        {
            transform.position += speed * Vector3.up * Time.deltaTime;
        }

        if (transform.position.y <= ymin)
        {
            movingDown = false;
        }
        else if (transform.position.y >= ymax)
        {
            movingDown = true;
        }
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, ymin, ymax), 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, rad);
    }
}
