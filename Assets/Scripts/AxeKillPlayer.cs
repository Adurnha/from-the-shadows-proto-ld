using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeKillPlayer : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player" || other.tag == "DarkPlayer" || other.tag == "LightPlayer") && other.GetType() == typeof(BoxCollider))
        {
            other.GetComponent<PlayerController>().Kill();
        }
    }
}
