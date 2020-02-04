using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCollectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player" || other.tag == "DarkPlayer" || other.tag == "LightPlayer") && other.GetType() == typeof(BoxCollider))
        {
            if(other.GetComponent<PlayerController>().playerNumber == 1)
                Destroy(this.gameObject);
        }
    }
}
