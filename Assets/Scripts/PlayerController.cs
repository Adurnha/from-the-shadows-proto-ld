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

    public bool canJump = false;

    public float timeInAir = 0f;

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

    public int nbController = 0;

    public string firstController;
    public string secondController;


    private void Start()
    {
        controller = this.GetComponent<CharacterController>();
    }

    IEnumerator DisableAttackZone()
    {
        yield return new WaitForSeconds(0.2f);
        this.transform.GetChild(3).gameObject.SetActive(false);
    }

    private void Update()
    {
        if (playerNumber == 2 && Input.GetButtonDown("p" + playerNumber + "_Attack"))
        {
            this.transform.GetComponent<Animator>().SetTrigger("Attack");
            this.transform.GetChild(3).gameObject.SetActive(true);

            StartCoroutine(DisableAttackZone());
        }

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

        //if (playerNumber == 1 && Input.GetKeyDown(KeyCode.C))
        //{
        //    isShadowPlayer = !isShadowPlayer;
        //}

        //if (isShadowPlayer != previouslyShadowPlayer)
        //{
        //    if (switchType == SwitchType.SameCharacter)
        //    {
        //        this.transform.GetChild(3).gameObject.SetActive(!isShadowPlayer);

        //        if (isShadowPlayer)
        //        {
        //            this.GetComponent<MeshRenderer>().material = shadowMaterial;
        //            this.transform.GetChild(0).GetComponent<MeshRenderer>().material = shadowMaterial;
        //            this.transform.GetChild(1).GetComponent<MeshRenderer>().material = shadowMaterial;
        //            this.transform.GetChild(2).GetComponent<MeshRenderer>().material = shadowMaterial;
        //        }
        //        else
        //        {
        //            this.GetComponent<MeshRenderer>().material = lightMaterial;
        //            this.transform.GetChild(0).GetComponent<MeshRenderer>().material = lightMaterial;
        //            this.transform.GetChild(1).GetComponent<MeshRenderer>().material = lightMaterial;
        //            this.transform.GetChild(2).GetComponent<MeshRenderer>().material = lightMaterial;
        //        }
        //    }

        //    if (switchType == SwitchType.BetweenTwoCharacters && !hasSwitched)
        //    {
        //        //if(this.tag == "LightPlayer")
        //        //{
        //        //    Debug.Log("LP " + this.tag);
        //        //    otherPlayer = GameObject.FindGameObjectWithTag("DarkPlayer").GetComponent<PlayerController>();

        //        //    this.playerNumber = 2;
        //        //    otherPlayer.playerNumber = 1;

        //        //    hasSwitched = true;
        //        //}

        //        //if(this.tag == "DarkPlayer")
        //        //{
        //        //    Debug.Log("DP " + this.tag);
        //        //    otherPlayer = GameObject.FindGameObjectWithTag("LightPlayer").GetComponent<PlayerController>();

        //        //    this.playerNumber = 2;
        //        //    otherPlayer.playerNumber = 1;

        //        //    hasSwitched = true;
        //        //}
        //    }

        //    previouslyShadowPlayer = isShadowPlayer;
        //    hasSwitched = false;
        //}

        if (controller.isGrounded)
        {
            timeInAir = 0f;
            hasJumped = false;
            hasDoubleJumped = false;
            canJump = true;

            if(Input.GetAxis("p" + playerNumber + "_Horizontal_joystick") != 0f)
                moveDirection = new Vector3(Input.GetAxis("p" + playerNumber + "_Horizontal_joystick"), 0.0f);
            else
                moveDirection = new Vector3(Input.GetAxis("p" + playerNumber + "_Horizontal"), 0.0f);


            moveDirection *= moveSpeed;
        }

        if (playerNumber == 1 && (Input.GetButtonDown("p1_Jump")))
        {
            if (canJump && !isCarryingCaisse)
            {
                moveDirection.y = jumpSpeed;
                hasDoubleJumped = false;
                hasJumped = true;
            }
            else if (!hasDoubleJumped)
            {
                moveDirection.y = jumpSpeed;
                hasDoubleJumped = true;
            }
        }

        if (playerNumber == 2 && Input.GetButton("p2_Jump") && !hasJumped && canJump)
        {
            if (!isCarryingCaisse)
            {
                moveDirection.y = jumpSpeed;
                hasDoubleJumped = false;
                hasJumped = true;
            }
        }

        if (!controller.isGrounded)
        {
            timeInAir += Time.deltaTime;

            if (timeInAir >= 0.12f)
            {
                canJump = false;
            }

            if (Input.GetAxis("p" + playerNumber + "_Horizontal_joystick") != 0f)
                moveDirection.x = Input.GetAxis("p" + playerNumber + "_Horizontal_joystick") * moveSpeed;
            else
                moveDirection.x = Input.GetAxis("p" + playerNumber + "_Horizontal") * moveSpeed;



            moveDirection.y -= gravityForce * Time.deltaTime;
        }

        if (moveDirection.x > 0)
        {
            this.transform.eulerAngles = new Vector3(0, 90, 0);
        }
        if (moveDirection.x < 0)
        {
            this.transform.eulerAngles = new Vector3(0, -90, 0);
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


