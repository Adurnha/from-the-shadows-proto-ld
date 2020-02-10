using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UpdateRangeInEditMode : MonoBehaviour
{
    void Update()
    {
        this.transform.GetChild(1).localScale = new Vector3(this.transform.GetComponent<GhostEnemy>().detectionRange, this.transform.GetComponent<GhostEnemy>().detectionRange, this.transform.GetComponent<GhostEnemy>().detectionRange);
    }
}
