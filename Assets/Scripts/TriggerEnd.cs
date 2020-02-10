using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnd : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if(!triggered)
        {
            triggered = true;
            this.transform.parent.GetComponent<TriggerCameraFollow>().end = true;
        }
    }
}
