using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PureOrb : MonoBehaviour, IInteractable
{
    private bool hasInteracted = false;

    public bool isShadow;

    public Material shadowMaterial;
    public Material lightMaterial;

    private bool isEquipped = false;

    private List<IPlatform> lightedPlatforms = new List<IPlatform>();
    public void InteractAutreSens(PlayerController playerController) { }

    void Awake()
    {
        if(isShadow)
        {
            this.transform.GetChild(1).gameObject.GetComponent<Light>().color = Color.blue;
            this.transform.GetChild(1).gameObject.GetComponent<Light>().intensity = 5f;
            this.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = shadowMaterial;
        }
        else
        {
            this.transform.GetChild(1).gameObject.GetComponent<Light>().color = Color.yellow;
            this.transform.GetChild(1).gameObject.GetComponent<Light>().intensity = 2f;
            this.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = lightMaterial;
        }
    }
    public void Interact(PlayerController playerController)
    {
        if(!hasInteracted && isEquipped)
        {
            this.transform.parent = null;
            isEquipped = false;
            hasInteracted = true;

            StartCoroutine(ResetInteract());
        }
        else if(!hasInteracted && !isEquipped)
        {
            this.transform.parent = playerController.transform;
            isEquipped = true;
            hasInteracted = true;

            StartCoroutine(ResetInteract());
        }
    }

    private IEnumerator ResetInteract()
    {
        yield return new WaitForSeconds(0.2f);
        hasInteracted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isShadow)
        {
            IPlatform lp = other.GetComponent<IPlatform>();

            if (lp != null)
            {
                lightedPlatforms.Add(lp);
                lp.LightSources++;
            }
        }

        if (isShadow)
        {
            IPlatform lp = other.GetComponent<IPlatform>();

            if(lp != null)
            {
                lp.IsLightedByPureShadow = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isShadow)
        {
            IPlatform lp = other.GetComponent<IPlatform>();

            if (lp != null)
            {
                lightedPlatforms.Remove(lp);
                lp.LightSources--;
            }
        }

        if (isShadow)
        {
            IPlatform lp = other.GetComponent<IPlatform>();

            if (lp != null)
            {
                lp.IsLightedByPureShadow = false;
            }
        }
    }

}
