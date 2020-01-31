using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightReflector : MonoBehaviour, IInteractable
{
    public void Interact(PlayerController playerController)
    {
        this.transform.parent.Rotate(new Vector3(0, 0, 15));
    }
}
