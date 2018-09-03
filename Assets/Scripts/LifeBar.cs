using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    [SerializeField] float width;
    [SerializeField] float height;
    public GameObject healthCapsule;
    [SerializeField] GameObject blockSparklesVFX;



    // Use this for initialization
    void Start()
    {
        FillBar();
    }


    // Update is called once per frame
    void Update()
    {

    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    private void FillBar()
    {
        foreach (Transform child in transform)
        {
            GameObject capsule = Instantiate(healthCapsule, child.transform.position, Quaternion.identity) as GameObject;
            capsule.transform.parent = child;
        }
    }
    public void LoseHealth()
    {
        DeletePosition();
    }

    void DeletePosition()
    {
        int ii = 0;
        bool flag = false;
        foreach (Transform child in transform)
        {
            if (child.childCount != 0 & !flag)
            {
                GameObject sparkles = Instantiate(blockSparklesVFX, child.position, child.rotation);
                Destroy(child.gameObject);
                Destroy(sparkles, 1f);

                flag = true;
            }
            ii += 1;
        }
        
    }

}
