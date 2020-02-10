using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCameraFollow : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    [SerializeField]
    private GameObject skeletonArmyHorizontal;
    
    [SerializeField]
    private GameObject skeletonArmy2;

    public bool isTrigger2 = false;

    private bool start = false;
    public bool end = false;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered)
        {
            camera.GetComponent<CameraManager>().hasControl = false;
            start = true;

            hasTriggered = true;
        }
    }

    private void Update()
    {
        if (start)
        {
            if (!isTrigger2)
                camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(31.14f, 80.21f, -10.61f), 3f * Time.deltaTime);

            if (isTrigger2)
                camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(138.9f, 100.77f, -12.7f), 3f * Time.deltaTime);

            StartCoroutine(GainControl());
        }
        if (end)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(camera.transform.position.x, camera.transform.position.y, -22.14f), 3f * Time.deltaTime);
        }
    }

    IEnumerator GainControl()
    {
        if(!isTrigger2)
        {
            yield return new WaitForSeconds(0.8f);
            camera.GetComponent<CameraManager>().hasControl = true;
            camera.GetComponent<CameraManager>().isHorizontallyFixed = false;
            skeletonArmyHorizontal.SetActive(true);


            start = false;

            yield return new WaitForSeconds(1.3f);
            end = true;

            yield return new WaitForSeconds(2f);
            end = false;
            start = false;
            hasTriggered = false;
        }
        else
        {
            yield return new WaitForSeconds(0.8f);
            skeletonArmy2.SetActive(true);
            camera.GetComponent<CameraManager>().hasControl = true;
            camera.GetComponent<CameraManager>().isHorizontallyFixed = true;

            start = false;

            yield return new WaitForSeconds(1f);
            skeletonArmyHorizontal.SetActive(false);
        }
    }
}
