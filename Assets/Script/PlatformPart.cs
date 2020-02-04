using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPart : MonoBehaviour, IPlatform
{
    Renderer renderer;
    BoxCollider collider;

    public int LightSources { get; set; }
    public bool IsLightedByPureShadow { get; set; }

    [HideInInspector]
    public int lightSource;

    private int previousLightSources;
    private bool previouslyLightedByPureShadow;


    void Start()
    {
        renderer = this.GetComponent<Renderer>();
        collider = this.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        lightSource = LightSources;

        if((lightSource != previousLightSources) || (IsLightedByPureShadow != previouslyLightedByPureShadow))
        {
            if (!IsLightedByPureShadow && this.tag != "NeutralPlatform")
            {
                if (lightSource == 0)
                {
                    HidePlatform();
                }
                else
                {
                    ShowPlatform();
                }
            }
            else if (IsLightedByPureShadow && this.tag != "NeutralPlatform")
            {
                HidePlatform();
            }

            previousLightSources = lightSource;
            previouslyLightedByPureShadow = IsLightedByPureShadow;
        }
    }
    private void ShowPlatform()
    {
        if (this.tag == "LightPlatform")
        {
            collider.isTrigger = false;
            renderer.enabled = true;
            gameObject.layer = 9;
        }

        if (this.tag == "DarkPlatform")
        {
            collider.isTrigger = true;
            renderer.enabled = false;
            gameObject.layer = 15;
        }

    }
    private void HidePlatform()
    {
        if (this.tag == "LightPlatform")
        {
            collider.isTrigger = true;
            renderer.enabled = false;
            gameObject.layer = 15;
        }

        if (this.tag == "DarkPlatform")
        {
            collider.isTrigger = false;
            renderer.enabled = true;
            gameObject.layer = 9;
        }
    }
}
