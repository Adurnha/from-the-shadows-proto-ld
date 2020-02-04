using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismLight : MonoBehaviour, IPlatform
{
    public LayerMask layerMask;
    public GameObject rayPrefab;

    public bool isActivated = false;

    private bool reseted = false;

    private int maxRays = 5;
    private GameObject[] rays;
    private RaycastHit[] hits;

    private LightDetector lightDetector;

    public int LightSources { get; set; }
    public bool IsLightedByPureShadow { get; set; }

    public int lightSource;

    void Start()
    {
        rays = new GameObject[maxRays];
        hits = new RaycastHit[maxRays];

        for (int i = 0; i < maxRays; i++)
        {
            SpawnRay(i);
        }
    }

    void SpawnRay(int index)
    {
        rays[index] = Instantiate(rayPrefab, transform.position, Quaternion.identity, transform);
        rays[index].transform.localScale = new Vector3(0, 0, 0);
    }

    void DrawRay(int index)
    {
        if (index == 0)
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hits[0].distance, Color.yellow);

            rays[index].transform.right = transform.forward;
            rays[index].transform.localScale = new Vector3(hits[0].distance, 1, 1);
            rays[index].transform.position = transform.position;
        }

        else if (index == 1)
        {
            //Debug.DrawRay(hits[index - 1].point, Vector3.Reflect((hits[index - 1].point - transform.position).normalized, hits[index - 1].normal.normalized) * hits[index].distance, Color.yellow);

            rays[index].transform.right = Vector3.Reflect((hits[index - 1].point - transform.position).normalized, hits[index - 1].normal.normalized);
            rays[index].transform.position = hits[index - 1].point;
            rays[index].transform.localScale = new Vector3(hits[index].distance, 1, 1);
        }

        else
        {
            //Debug.DrawRay(hits[index - 1].point, Vector3.Reflect((hits[index - 1].point - hits[index - 2].point).normalized, hits[index - 1].normal.normalized) * hits[index].distance, Color.yellow);

            rays[index].transform.right = Vector3.Reflect((hits[index - 1].point - hits[index - 2].point).normalized, hits[index - 1].normal.normalized);
            rays[index].transform.position = hits[index - 1].point;
            rays[index].transform.localScale = new Vector3(hits[index].distance, 1, 1);
        }
    }

    void ResetRay(int index)
    {
        rays[index].transform.position = transform.position;
        rays[index].transform.localScale = new Vector3(0, 0, 0);
    }

    void DrawAllRays()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hits[0], Mathf.Infinity, layerMask))
        {
            DrawRay(0);

            if (hits[0].transform.gameObject.tag == "Reflector")
            {
                if (Physics.Raycast(hits[0].point, Vector3.Reflect((hits[0].point - transform.position).normalized, hits[0].normal.normalized), out hits[1], Mathf.Infinity, layerMask))
                {
                    DrawRay(1);


                    if (hits[1].transform.gameObject.tag == "Reflector")
                    {
                        if (Physics.Raycast(hits[1].point, Vector3.Reflect((hits[1].point - hits[0].point).normalized, hits[1].normal.normalized), out hits[2], Mathf.Infinity, layerMask))
                        {
                            DrawRay(2);


                            if (hits[2].transform.gameObject.tag == "Reflector")
                            {
                                if (Physics.Raycast(hits[2].point, Vector3.Reflect((hits[2].point - hits[1].point).normalized, hits[2].normal.normalized), out hits[3], Mathf.Infinity, layerMask))
                                {
                                    DrawRay(3);


                                    if (hits[3].transform.gameObject.tag == "Reflector")
                                    {
                                        if (Physics.Raycast(hits[3].point, Vector3.Reflect((hits[3].point - hits[2].point).normalized, hits[3].normal.normalized), out hits[4], Mathf.Infinity, layerMask))
                                        {
                                            DrawRay(4);
                                        }
                                    }
                                    else
                                    {
                                        ResetRay(4);
                                    }
                                }
                            }
                            else
                            {
                                ResetRay(3);
                                ResetRay(4);
                            }
                        }
                    }
                    else
                    {
                        ResetRay(2);
                        ResetRay(3);
                        ResetRay(4);
                    }
                }
            }
            else
            {
                ResetRay(1);
                ResetRay(2);
                ResetRay(3);
                ResetRay(4);
            }
        }
    }

    void Update()
    {
        lightSource = LightSources;

        if (LightSources == 0)
        {
            isActivated = false;
        }
        else
        {
            isActivated = true;
        }


        if (isActivated)
        {
            reseted = false;
            DrawAllRays();
        }
        else if (!isActivated && !reseted)
        {
            ResetRay(0);
            ResetRay(1);
            ResetRay(2);
            ResetRay(3);
            ResetRay(4);

            reseted = true;
        }
    }
}

