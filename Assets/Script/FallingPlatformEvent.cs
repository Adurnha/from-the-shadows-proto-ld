using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformEvent : MonoBehaviour
{
    private PlatformMaster platformMaster;

    private void Start()
    {
        platformMaster = this.transform.parent.GetChild(1).GetComponent<PlatformMaster>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetType() == typeof(CharacterController))
        {
            if (platformMaster.platformAttributes.isUnstable)
            {
                if (!platformMaster.mustFall)
                {
                    platformMaster.mustFall = true;

                    platformMaster.GetComponent<Animator>().SetTrigger("Tremble");
                    StartCoroutine(FallingDelay());
                }
            }
        }
    }

    private IEnumerator FallingDelay()
    {
        yield return new WaitForSeconds(platformMaster.platformAttributes.fallingTimer);
        platformMaster.GetComponent<Animator>().SetTrigger("Fall");

        yield return new WaitForSeconds(platformMaster.platformAttributes.respawnTimer);
        platformMaster.mustFall = false;
        platformMaster.GetComponent<Animator>().SetTrigger("Reset");

    }
}
