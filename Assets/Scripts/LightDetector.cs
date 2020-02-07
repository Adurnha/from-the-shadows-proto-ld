using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetector : MonoBehaviour
{
    [SerializeField]
    Mecanism mecanism;

    [SerializeField]
    Mecanism mecanism2;

    [SerializeField]
    Mecanism mecanism3;

    private bool isActivated = false;

    public void Activate()
    {
        if(!isActivated)
        {
            Debug.Log("Activated");
            mecanism.ActivateMecanism();

            if (mecanism2 != null)
                mecanism2.ActivateMecanism();

            if (mecanism3 != null)
                mecanism3.ActivateMecanism();

            isActivated = true;
        }
    }

    public void Deactivate()
    {
        if(isActivated)
        {
            Debug.Log("Deactivated");
            mecanism.DeactivateMecanism();

            if (mecanism2 != null)
                mecanism2.DeactivateMecanism();

            if (mecanism3 != null)
                mecanism3.DeactivateMecanism();

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
