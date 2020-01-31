using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour, IInteractable
{
    public bool isLighted;

    private List<IPlatform> lightedPlatforms = new List<IPlatform>();

    public void Interact(PlayerController playerController)
    {
        if(isLighted)
        {
            isLighted = false;
        }
        else if(!isLighted)
        {
            isLighted = true;
        }
    }

    private void Update()
    {
        this.transform.GetChild(1).gameObject.SetActive(isLighted);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isLighted)
        {
            IPlatform lp = other.GetComponent<IPlatform>();

            if (lp != null)
            {
                lightedPlatforms.Add(lp);

                lp.LightSources++;
            }
        }
    }

    public void OnChildDisable()
    {
        foreach (IPlatform lp in lightedPlatforms)
        {
            lp.LightSources--;
        }
        lightedPlatforms.Clear();
    }

}
