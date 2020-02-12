using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    PlayerController playerController;
    private List<IPlatform> lightedPlatforms = new List<IPlatform>();

    private void Start()
    {
        playerController = this.transform.parent.GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (playerController.playerNumber == 1)
        {
            IPlatform lp = other.GetComponent<IPlatform>();

            if (lp != null)
            {
                lightedPlatforms.Add(lp);

                lp.LightSources++;
            }
        }
    }

    private void OnDisable()
    {
        foreach (IPlatform lp in lightedPlatforms)
        {
            lp.LightSources--;
        }
        lightedPlatforms.Clear();
    }
    private void OnTriggerExit(Collider other)
    {
        if (playerController.playerNumber == 1)
        {
            IPlatform lp = other.GetComponent<IPlatform>();

            if (lp != null)
            {
                lightedPlatforms.Remove(lp);

                lp.LightSources--;
            }
        }
    }
}
