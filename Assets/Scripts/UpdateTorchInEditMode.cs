using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class UpdateTorchInEditMode : MonoBehaviour
{
    void Update()
    {
        this.transform.GetChild(1).gameObject.SetActive(this.GetComponent<Torch>().isLighted);
    }
}
