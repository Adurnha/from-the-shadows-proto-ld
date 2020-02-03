﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressionPlate : MonoBehaviour
{
    [SerializeField]
    Mecanism mecanism;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player" || other.tag == "DarkPlayer" || other.tag == "LightPlayer") && other.GetType() == typeof(BoxCollider))
        {
            mecanism.ActivateMecanism();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((other.tag == "Player" || other.tag == "DarkPlayer" || other.tag == "LightPlayer") && other.GetType() == typeof(BoxCollider))
        {
            mecanism.DeactivateMecanism();
        }
    }
}
