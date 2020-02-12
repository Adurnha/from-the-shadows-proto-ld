using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamera : MonoBehaviour
{
    public bool hasControl;
    public bool isHorizontallyFixed;
    public float cameraOffset;

    public GameObject cameraReference;

    private GameObject camera;
    private CameraManager cameraManager;

    private bool mustMove = false;
    private bool spawned = false;

    public List<GameObject> levelsToSpawn = new List<GameObject>();

    private bool triggered = false;
    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraManager = camera.GetComponent<CameraManager>();
    }

    private void SpawnLevels()
    {   
        foreach(GameObject level in GameObject.FindGameObjectsWithTag("Level"))
        {
            level.SetActive(false);
        }

        foreach(GameObject levelToSpawn in levelsToSpawn)
        {
            levelToSpawn.SetActive(true);
        }
    }

    private void Update()
    {
        if(this.transform.GetChild(0).GetComponent<ChildTrigger>().playerInZone >= 2 && !triggered)
        {
            Debug.Log("Update");
            mustMove = true;
            cameraManager.hasControl = hasControl;
            cameraManager.isHorizontallyFixed = isHorizontallyFixed;
            cameraManager.cameraOffset = cameraOffset;

            this.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);

            cameraManager.cameraStartingPosition = cameraReference.transform.position;

            GameObject.FindGameObjectWithTag("LightPlayer").GetComponent<PlayerController>().SetSpawnPoint(transform.GetChild(2).position);
            GameObject.FindGameObjectWithTag("DarkPlayer").GetComponent<PlayerController>().SetSpawnPoint(transform.GetChild(2).position);

            if (!spawned)
            {
                SpawnLevels();
                spawned = true;
            }

            StartCoroutine(StopMovement());

            triggered = true;
        }

        if(mustMove)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, cameraReference.transform.position, 2.5f * Time.deltaTime);
        }

        if(mustMove && Vector3.Distance(camera.transform.position, cameraReference.transform.position) <= 0.2f)
        {
            mustMove = false;
        }
    }

    IEnumerator StopMovement()
    {
        yield return new WaitForSeconds(2.5f);
        mustMove = false;
    }
}
