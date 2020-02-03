using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightReflector : MonoBehaviour, IInteractable
{
    public void Interact(PlayerController playerController)
    {
        if (playerController.playerNumber == 1)
            this.transform.parent.Rotate(new Vector3(0, 0, -22.5f));
        if (playerController.playerNumber == 2)
            this.transform.parent.Rotate(new Vector3(0, 0, 22.5f));
    }
    public void InteractAutreSens(PlayerController playerController)
    {
        if (playerController.playerNumber == 1)
            this.transform.parent.Rotate(new Vector3(0, 0, 22.5f));
        if (playerController.playerNumber == 2)
            this.transform.parent.Rotate(new Vector3(0, 0, -22.5f));
    }
}
