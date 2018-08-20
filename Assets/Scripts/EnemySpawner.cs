using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Also called formation controller
public class EnemySpawner : MonoBehaviour 
{
    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    private bool movingRight = true;
    float spawnDelay = 0.5f;
    [SerializeField] float speed;
    [SerializeField] float padding;

    float movement;
    float xmin;
    float xmax;
    float ymin;
    float ymax;

    // Use this for initialization
    void Start ()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        Vector3 topmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance));
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;
        ymin = leftmost.y + padding;
        ymax = topmost.y - padding;

        SpawnUntilFull();
    }

    /*private void SpawnEnemies()
    {
        foreach (Transform child in transform) // for each position object we placed in the enemy spawner
        {
            // for all of the children (sphere meshes we childed to EnemyFormation which holder this script)
            // we asssign a new game object which is of class enemy prefab to this child
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            // we make the child (position sphere mesh) the parent of the instantiated object
            enemy.transform.parent = child;
        }
    }*/
    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if (freePosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
        if (NextFreePosition())
        {
        Invoke("SpawnUntilFull", spawnDelay);
        }

    }
    Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }
        }
        return null;
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }
    // Update is called once per frame
    void Update () 
    {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (transform.position.x <= xmin) 
        {
            movingRight = true;
        }
        else if (transform.position.x >= xmax)
        {
            movingRight = false;
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xmin, xmax), Mathf.Clamp(transform.position.y, ymin, ymax), 0);
        
        if(AllMembersDead())
        {
            SpawnUntilFull();
        }
        
    }
    
    bool AllMembersDead()
    {
        foreach(Transform childPositionGameObject in transform)
        // for each transform position in the formation (children of the formation)
        {
            if(childPositionGameObject.childCount > 0)
            // if there is an enemy that is a child of the position
            {
                return false;
            }     
        }
        return true;
    }
}

