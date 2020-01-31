using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetector : MonoBehaviour
{
    [SerializeField]
    Mecanism mecanism;

    private bool isActivated = false;

    public void Activate()
    {
        if(!isActivated)
        {
            Debug.Log("Activated");
            mecanism.ActivateMecanism();
            isActivated = true;
        }
    }

    public void Deactivate()
    {
        if(isActivated)
        {
            Debug.Log("Deactivated");
            mecanism.DeactivateMecanism();
            isActivated = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ray")
        {
            Activate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ray")
        {
            Deactivate();
        }
    }


}
