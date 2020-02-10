using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caisse : MonoBehaviour, IInteractable
{
    public LayerMask layerMask;
    public bool hasInteracted = false;
    public bool isEquipped = false;

    public bool onGroundLeft;
    public bool onGroundRight;

    public void InteractAutreSens(PlayerController playerController) { }

    public void Interact(PlayerController playerController)
    {
        if (!hasInteracted && isEquipped)
        {
            Debug.Log("a");
            this.transform.parent = null;
            isEquipped = false;
            hasInteracted = true;
            playerController.isCarryingCaisse = false;

            StartCoroutine(ResetInteract());
        }
        else if (!hasInteracted && !isEquipped)
        {
            this.transform.parent = playerController.transform;
            playerController.isCarryingCaisse = true;
            isEquipped = true;
            hasInteracted = true;

            StartCoroutine(ResetInteract());
        }
    }

    private void Update()
    {
        //RaycastHit hitLeft;
        //RaycastHit hitRight;

        //Vector3 rayLeftOrigin = new Vector3(transform.position.x - (transform.localScale.x / 2), transform.position.y - (transform.localScale.y / 2), transform.position.z);
        //Vector3 rayRight = new Vector3(transform.position.x + (transform.localScale.x / 2), transform.position.y - (transform.localScale.y / 2), transform.position.z);

        //if (Physics.Raycast(rayLeftOrigin, Vector3.down, out hitLeft, 0.5f, layerMask))
        //{
        //    onGroundLeft = true;
        //}
        //else
        //{
        //    onGroundLeft = false;
        //}

        //if (Physics.Raycast(rayRight, Vector3.down, out hitRight, 0.5f, layerMask))
        //{
        //    onGroundRight = true;
        //}
        //else
        //{
        //    onGroundRight = false;
        //}

        //Debug.DrawRay(rayLeftOrigin, Vector3.down, Color.yellow);
        //Debug.DrawRay(rayRight, Vector3.down, Color.yellow);
    }
    private IEnumerator ResetInteract()
    {
        yield return new WaitForSeconds(0.2f);
        hasInteracted = false;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other);
    }

}
