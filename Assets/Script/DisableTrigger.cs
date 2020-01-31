using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTrigger : MonoBehaviour
{
    private void OnDisable()
    {
        if(this.transform.parent.TryGetComponent(out Torch torch))
        {
            torch.OnChildDisable();
        }
        Debug.Log("Test");
    }
}
