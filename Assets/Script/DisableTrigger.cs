using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTrigger : MonoBehaviour
{
    private void OnDisable()
    {
        this.transform.parent.GetComponent<Torch>().OnChildDisable();
    }
}
