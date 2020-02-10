﻿using System.Collections;
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
    private bool canJump = false;
    public enum SwitchType { SameCharacter, BetweenTwoCharacters };
    public SwitchType switchType;

    public Material lightMaterial;
    public Material shadowMaterial;

    public bool isCarryingCaisse = false;

    public PlayerController otherPlayer;


    private Vector3 moveDirection;

    private CharacterController controller;

    private bool isShadowPlayer = false;
    private bool previouslyShadowPlayer = false;

    private List<IInteractable> itemToInteractWith = new List<IInteractable>();

    private bool hasSwitched = false;

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
                Debug.Log(item);
                item.Interact(this);
            }
        }

        if (Input.GetButtonDown("p" + playerNumber + "_InteractAutreSens"))
        {
            foreach (IInteractable item in itemToInteractWith)
            {
                item.InteractAutreSens(this);
            }
        }

        if (playerNumber == 1 && Input.GetKeyDown(KeyCode.C))
        {
            isShadowPlayer = !isShadowPlayer;
        }

        if (isShadowPlayer != previouslyShadowPlayer)
        {
            if (switchType == SwitchType.SameCharacter)
            {
                this.transform.GetChild(3).gameObject.SetActive(!isShadowPlayer);

                if (isShadowPlayer)
                {
                    this.GetComponent<MeshRenderer>().material = shadowMaterial;
                    this.transform.GetChild(0).GetComponent<MeshRenderer>().material = shadowMaterial;
                    this.transform.GetChild(1).GetComponent<MeshRenderer>().material = shadowMaterial;
                    this.transform.GetChild(2).GetComponent<MeshRenderer>().material = shadowMaterial;
                }
                else
                {
                    this.GetComponent<MeshRenderer>().material = lightMaterial;
                    this.transform.GetChild(0).GetComponent<MeshRenderer>().material = lightMaterial;
                    this.transform.GetChild(1).GetComponent<MeshRenderer>().material = lightMaterial;
                    this.transform.GetChild(2).GetComponent<MeshRenderer>().material = lightMaterial;
                }
            }

            if (switchType == SwitchType.BetweenTwoCharacters && !hasSwitched)
            {
                //if(this.tag == "LightPlayer")
                //{
                //    Debug.Log("LP " + this.tag);
                //    otherPlayer = GameObject.FindGameObjectWithTag("DarkPlayer").GetComponent<PlayerController>();

                //    this.playerNumber = 2;
                //    otherPlayer.playerNumber = 1;

                //    hasSwitched = true;
                //}

                //if(this.tag == "DarkPlayer")
                //{
                //    Debug.Log("DP " + this.tag);
                //    otherPlayer = GameObject.FindGameObjectWithTag("LightPlayer").GetComponent<PlayerController>();

                //    this.playerNumber = 2;
                //    otherPlayer.playerNumber = 1;

                //    hasSwitched = true;
                //}
            }

            previouslyShadowPlayer = isShadowPlayer;
            hasSwitched = false;
        }

        if (controller.isGrounded)
        {
            hasJumped = false;
            hasDoubleJumped = false;
            canJump = true;
            moveDirection = new Vector3(Input.GetAxis("p" + playerNumber + "_Horizontal"), 0.0f);
            moveDirection *= moveSpeed;
        }

        if (playerNumber == 1 && Input.GetButtonDown("p" + playerNumber + "_Jump"))
        {
            if (canJump && !isCarryingCaisse)
            {
                moveDirection.y = jumpSpeed;
                hasDoubleJumped = false;
                hasJumped = true;
            }
            else if(!hasDoubleJumped)
            {
                moveDirection.y = jumpSpeed;
                hasDoubleJumped = true;
            }
        }

        if (playerNumber == 2 && Input.GetButton("p" + playerNumber + "_Jump") && !hasJumped && canJump)
        {
            if(!isCarryingCaisse)
            {
                moveDirection.y = jumpSpeed;
                hasDoubleJumped = false;
                hasJumped = true;
            }
        }

        if (!controller.isGrounded)
        {
            canJump = false;

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


