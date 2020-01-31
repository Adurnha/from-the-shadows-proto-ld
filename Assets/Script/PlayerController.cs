using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public int playerNumber = 1;

    [SerializeField]
    private float moveSpeed = 10f;

    [SerializeField]
    private float jumpSpeed = 20f;

    [SerializeField]
    private float gravityForce = 50f;

    private bool hasDoubleJumped = false;
    private bool hasJumped = false;

    private Vector3 moveDirection;

    private CharacterController controller;

    private Vector3 velocity;

    private List<IInteractable> itemToInteractWith = new List<IInteractable>();


    private void Start()
    {
        controller = this.GetComponent<CharacterController>();
    }


    private void Update()
    {

        if (Input.GetButtonDown("p" + playerNumber + "_Interact"))
        {
            foreach (IInteractable item in itemToInteractWith)
            {
                item.Interact(this);
            }
        }

        if (controller.isGrounded)
        {
            hasJumped = false;
            moveDirection = new Vector3(Input.GetAxis("p" + playerNumber + "_Horizontal"), 0.0f);
            moveDirection *= moveSpeed;
        }

        if (playerNumber == 1 && Input.GetButtonDown("p" + playerNumber + "_Jump") && !hasDoubleJumped && hasJumped)
        {
            hasDoubleJumped = true;
            moveDirection.y = jumpSpeed;
        }

        if (Input.GetButton("p" + playerNumber + "_Jump") && !hasJumped)
        {
            moveDirection.y = jumpSpeed;
            hasDoubleJumped = false;
            hasJumped = true;
        }

        if (!controller.isGrounded)
        {
            moveDirection.x = Input.GetAxis("p" + playerNumber + "_Horizontal") * moveSpeed;
            moveDirection.y -= gravityForce * Time.deltaTime;
        }

        controller.Move(moveDirection * Time.deltaTime);
    }

    public void Kill()
    {
        Debug.Log("Player " + playerNumber + " killed.");
        Destroy(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable item = other.GetComponent<IInteractable>();


        if (item != null)
        {
            itemToInteractWith.Add(item);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable item = other.GetComponent<IInteractable>();

        if (item != null)
        {
            itemToInteractWith.Remove(item);
        }

    }
}


