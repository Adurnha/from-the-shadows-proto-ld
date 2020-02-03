using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    [SerializeField]
    Mecanism mecanism;

    private bool isActivated = false;
    public void InteractAutreSens(PlayerController playerController) { }

    public void Interact(PlayerController playerController)
    {
        if (isActivated)
        {
            isActivated = false;
            mecanism.DeactivateMecanism();
        }
        else if (!isActivated)
        {
            isActivated = true;
            mecanism.ActivateMecanism();
        }
    }
}
