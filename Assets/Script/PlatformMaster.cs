using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMaster : MonoBehaviour
{
    public GameObject lightShadowPlatform;
    public GameObject neutralPlatform;

    public Material shadowMaterial;
    public Material lightMaterial;
    public Material neutralMaterial;

    public Material shadowMaterialUnstable;
    public Material lightMaterialUnstable;
    public Material neutralMaterialUnstable;

    private Transform[] platformParts;

    public GameObject platform;

    [HideInInspector]
    public PlatformAttributes platformAttributes;

    public bool mustFall = false;
    void Start()
    {
        Destroy(this.transform.parent.GetChild(2).gameObject);


        platformAttributes = this.transform.parent.GetComponent<PlatformAttributes>();

        if (platform != null)
        {
            platform = null;
        }

        if (platformAttributes.platformType == PlatformAttributes.PlatformType.Neutral)
        {
            platform = Instantiate(neutralPlatform);
            platform.transform.parent = this.transform;

            platform.transform.position = transform.parent.position;
            //platform.transform.rotation = transform.parent.rotation;
        }
        else
        {
            platform = Instantiate(lightShadowPlatform);
            platform.transform.parent = this.transform;

            platform.transform.position = transform.parent.position;
            //platform.transform.rotation = transform.parent.rotation;
        }
        this.transform.localScale = this.transform.parent.lossyScale;
        this.transform.localRotation = transform.parent.rotation;



        if (platformAttributes.platformType != PlatformAttributes.PlatformType.Neutral)
        {
            platformParts = new Transform[this.transform.GetChild(0).childCount];

            for (int i = 0; i < this.transform.GetChild(0).childCount; i++)
            {
                platformParts[i] = this.transform.GetChild(0).GetChild(i);
            }


            if (platformAttributes.platformType == PlatformAttributes.PlatformType.Shadow)
            {
                this.transform.parent.GetChild(0).gameObject.tag = "DarkPlatform";
                foreach (Transform platformPart in platformParts)
                {
                    platformPart.tag = "DarkPlatform";
                    platformPart.GetComponent<BoxCollider>().isTrigger = false;
                    platformPart.GetComponent<Renderer>().enabled = true;

                    if (platformAttributes.isUnstable)
                    {
                        platformPart.GetComponent<MeshRenderer>().material = shadowMaterialUnstable;
                    }
                    else
                    {
                        platformPart.GetComponent<MeshRenderer>().material = shadowMaterial;
                    }
                }
            }
            if (platformAttributes.platformType == PlatformAttributes.PlatformType.Light)
            {
                this.transform.parent.GetChild(0).gameObject.tag = "LightPlatform";

                foreach (Transform platformPart in platformParts)
                {
                    platformPart.tag = "LightPlatform";
                    platformPart.GetComponent<BoxCollider>().isTrigger = true;
                    platformPart.GetComponent<Renderer>().enabled = false;

                    if (platformAttributes.isUnstable)
                    {
                        platformPart.GetComponent<MeshRenderer>().material = lightMaterialUnstable;
                    }
                    else
                    {
                        platformPart.GetComponent<MeshRenderer>().material = lightMaterial;
                    }
                }
            }

        }
        else
        {
            this.transform.parent.GetChild(0).gameObject.tag = "NeutralPlatform";

            platform.tag = "NeutralPlatform";

            if (platformAttributes.isUnstable)
            {
                platform.transform.GetChild(0).GetComponent<MeshRenderer>().material = neutralMaterialUnstable;
            }
            else
            {
                platform.transform.GetChild(0).GetComponent<MeshRenderer>().material = neutralMaterial;
            }

        }
    }
}
