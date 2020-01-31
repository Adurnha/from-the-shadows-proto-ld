using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UpdatePlatformInEditMode : MonoBehaviour
{
    private PlatformAttributes platformAttributes;

    private bool updated = false;
    private string currentState;
    private string previousState;

    private Transform[] platformParts;

    void Start()
    {
        platformAttributes = this.transform.parent.GetComponent<PlatformAttributes>();
    }

    void Update()
    {
        if (platformAttributes.platformType == PlatformAttributes.PlatformType.Shadow)
        {
            if (platformAttributes.isUnstable)
                currentState = "UnstableShadow";
            else
                currentState = "Shadow";
        }
        if (platformAttributes.platformType == PlatformAttributes.PlatformType.Light)
        {
            if (platformAttributes.isUnstable)
                currentState = "UnstableLight";
            else
                currentState = "Light";
        }
        if (platformAttributes.platformType == PlatformAttributes.PlatformType.Neutral)
        {
            if (platformAttributes.isUnstable)
                currentState = "UnstableNeutral";
            else
                currentState = "Neutral";
        }


        if (currentState != previousState)
        {
            if (platformAttributes.platformType == PlatformAttributes.PlatformType.Shadow)
            {

                if (platformAttributes.isUnstable)
                {
                    this.GetComponent<MeshRenderer>().material = this.transform.parent.GetChild(1).GetComponent<PlatformMaster>().shadowMaterialUnstable;
                }
                else
                {
                    this.GetComponent<MeshRenderer>().material = this.transform.parent.GetChild(1).GetComponent<PlatformMaster>().shadowMaterial;
                }

            }
            if (platformAttributes.platformType == PlatformAttributes.PlatformType.Light)
            {
                if (platformAttributes.isUnstable)
                {
                    this.GetComponent<MeshRenderer>().material = this.transform.parent.GetChild(1).GetComponent<PlatformMaster>().lightMaterialUnstable;
                }
                else
                {
                    this.GetComponent<MeshRenderer>().material = this.transform.parent.GetChild(1).GetComponent<PlatformMaster>().lightMaterial;
                }

            }
            if (platformAttributes.platformType == PlatformAttributes.PlatformType.Neutral)
            {
                if (platformAttributes.isUnstable)
                {
                    this.GetComponent<MeshRenderer>().material = this.transform.parent.GetChild(1).GetComponent<PlatformMaster>().neutralMaterialUnstable;
                }
                else
                {
                    this.GetComponent<MeshRenderer>().material = this.transform.parent.GetChild(1).GetComponent<PlatformMaster>().neutralMaterial;
                }
            }
            previousState = currentState;
        }
    }
}
