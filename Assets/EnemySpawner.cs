using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;

	// Use this for initialization
	void Start () {
        foreach( Transform child in transform) // for each position object we placed in the enemy spawner
        {
            // for all of the children (sphere meshes we childed to EnemyFormation which holder this script)
            // we asssign a new game object which is of class enemy prefab to this child
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            // we make the child (position sphere mesh) the parent of the instantiated object
            enemy.transform.parent = child;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }
    // Update is called once per frame
    void Update () {

		
	}
}
