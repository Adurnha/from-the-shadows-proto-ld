using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    PlayerController[] players;

    public bool hasControl = true;

    public bool isHorizontallyFixed = false;

    private float playerCameraMoyenneY;
    private float playerCameraMoyenneX;
    private Vector3 cameraStartingPosition;
    public float cameraOffset;

    void Start()
    {
        players = FindObjectsOfType<PlayerController>();
        cameraStartingPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasControl)
        {
            playerCameraMoyenneY = ((players[0].transform.position.y + players[1].transform.position.y) / 2f) - cameraOffset;
            playerCameraMoyenneX = ((players[0].transform.position.x + players[1].transform.position.x) / 2f);

            if (playerCameraMoyenneY > cameraStartingPosition.y)
                this.transform.position = new Vector3(this.transform.position.x, playerCameraMoyenneY, this.transform.position.z);

            if (playerCameraMoyenneX > cameraStartingPosition.x && !isHorizontallyFixed)
                this.transform.position = new Vector3(playerCameraMoyenneX, this.transform.position.y, this.transform.position.z);
        }
    }
}
