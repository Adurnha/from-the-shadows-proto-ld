using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismGrosBoss : MonoBehaviour
{
    private int raysOnPrism = 0;
    public int maxRayToActivate = 4;

    public GameObject rayPrefab;
    public GameObject ray;

    private bool raySpawned = false;

    public LayerMask layerMask;

    private void Update()
    {
        if(raysOnPrism >= maxRayToActivate)
        {
            RaycastHit hit;

            if(Physics.Raycast(this.transform.GetChild(0).GetChild(1).transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                if(!raySpawned)
                {
                    ray = Instantiate(rayPrefab);
                    ray.transform.parent = this.transform;

                    ray.transform.right = transform.forward;
                    ray.transform.localScale = new Vector3(hit.distance, 5, 5);
                    ray.transform.position = this.transform.GetChild(0).GetChild(1).transform.position;

                    raySpawned = true;
                }
            }
        }
        else
        {
            if(raySpawned)
            {
                Destroy(ray.gameObject);
                raySpawned = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.gameObject.tag == "Ray")
        {
            raysOnPrism++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ray")
        {
            raysOnPrism--;       
        }
    }
}

