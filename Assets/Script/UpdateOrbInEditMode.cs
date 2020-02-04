using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UpdateOrbInEditMode : MonoBehaviour
{
    void Update()
    {
        if (GetComponent<PureOrb>().isShadow)
        {
            this.transform.GetChild(1).gameObject.GetComponent<Light>().color = Color.blue;
            this.transform.GetChild(1).gameObject.GetComponent<Light>().intensity = 5f;
            this.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = GetComponent<PureOrb>().shadowMaterial;
        }
        else
        {
            this.transform.GetChild(1).gameObject.GetComponent<Light>().color = Color.yellow;
            this.transform.GetChild(1).gameObject.GetComponent<Light>().intensity = 2f;
            this.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = GetComponent<PureOrb>().lightMaterial;
        }
    }
}
