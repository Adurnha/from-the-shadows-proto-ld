﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressionPlate : MonoBehaviour
{
    [SerializeField]
    Mecanism mecanism;

    [SerializeField]
    Mecanism mecanism2;

    [SerializeField]
    Mecanism mecanism3;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player" || other.tag == "DarkPlayer" || other.tag == "LightPlayer") && other.GetType() == typeof(BoxCollider))
        {
            mecanism.ActivateMecanism();

            if (mecanism2 != null)
                mecanism2.ActivateMecanism();

            if (mecanism3 != null)
                mecanism3.ActivateMecanism();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((other.tag == "Player" || other.tag == "DarkPlayer" || other.tag == "LightPlayer") && other.GetType() == typeof(BoxCollider))
        {
            mecanism.DeactivateMecanism();

            if (mecanism2 != null)
                mecanism2.DeactivateMecanism();

            if (mecanism3 != null)
                mecanism3.DeactivateMecanism();
        }
    }
}
