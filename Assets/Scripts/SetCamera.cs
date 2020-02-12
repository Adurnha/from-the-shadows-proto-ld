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


    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraManager = camera.GetComponent<CameraManager>();
    }

    private void Update()
    {
        if(this.transform.GetChild(0).GetComponent<ChildTrigger>().playerInZone >= 2)
        {
            mustMove = true;
            cameraManager.hasControl = hasControl;
            cameraManager.isHorizontallyFixed = isHorizontallyFixed;
            cameraManager.cameraOffset = cameraOffset;

            this.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);

            cameraManager.cameraStartingPosition = cameraReference.transform.position;
        }

        if(mustMove)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, cameraReference.transform.position, 2f * Time.deltaTime);
        }

        if(Vector3.Distance(camera.transform.position, cameraReference.transform.position) <= 0.2f)
        {
            mustMove = false;
            Debug.Log("aaa");
        }
    }
}
