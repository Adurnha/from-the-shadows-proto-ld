using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    PlayerController playerController;

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
                lp.LightSources++;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (playerController.playerNumber == 1)
        {
            IPlatform lp = other.GetComponent<IPlatform>();

            if (lp != null)
            {
                lp.LightSources--;
            }
        }
    }
}
