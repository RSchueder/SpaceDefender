using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour {
    [SerializeField] float speed;
    [SerializeField] float padding;
    [SerializeField] float health;
    public GameObject projectile;

    public float beamSpeed = 1;
    public float firingRate = 0.2f;

    float xmin;
    float xmax;
    float ymin;
    float ymax;

	void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        Vector3 topmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance));
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;
        ymin = leftmost.y + padding;
        ymax = topmost.y - padding;
    }
    private void Fire()
    {
        GameObject laserBeam = Instantiate(projectile, transform.position + new Vector3(0, 0, 1), Quaternion.identity) as GameObject;
        laserBeam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, beamSpeed, 0);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        EnemyProjectile missle = collider.gameObject.GetComponent<EnemyProjectile>();
        if (missle)
        {
            health -= missle.GetDamage();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            missle.Hit();
        }
    }

    void Update ()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xmin, xmax), Mathf.Clamp(transform.position.y, ymin, ymax),0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire",0.00001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
    }

}

