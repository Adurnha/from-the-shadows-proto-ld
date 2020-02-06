using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    [SerializeField]
    Mecanism mecanism;

    [SerializeField]
    Mecanism mecanism2;

    private bool isActivated = false;
    public void InteractAutreSens(PlayerController playerController) { }

    public void Interact(PlayerController playerController)
    {
        if (isActivated)
        {
            isActivated = false;
            mecanism.DeactivateMecanism();

            if(mecanism2 != null)
                mecanism2.DeactivateMecanism();
        }
        else if (!isActivated)
        {
            isActivated = true;
            mecanism.ActivateMecanism();

            if (mecanism2 != null)
                mecanism2.ActivateMecanism();

        }
    }
}
