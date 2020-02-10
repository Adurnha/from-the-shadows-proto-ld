using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemy : MonoBehaviour
{
    public float detectionRange = 2f;
    public float speed = 2f;

    private GameObject lightPlayer;
    private GameObject shadowPlayer;

    public GameObject closestPlayer;


    private bool mustChasePlayer = false;

    void Start()
    {
        lightPlayer = GameObject.FindGameObjectWithTag("LightPlayer");
        shadowPlayer = GameObject.FindGameObjectWithTag("DarkPlayer");

    }
    void Update()
    {
        this.transform.GetChild(1).localScale = new Vector3(detectionRange, detectionRange, detectionRange);

        closestPlayer = GetClosestPlayer();


        if (IsInRange(closestPlayer))
        {
            mustChasePlayer = true;
        }

        if(mustChasePlayer)
        {
            ChasePlayer(closestPlayer);
        }
    }

    private void ChasePlayer(GameObject player)
    {
        transform.LookAt(player.transform.position);

        this.transform.position += this.transform.forward * speed * Time.deltaTime;

        
    }

    private GameObject GetClosestPlayer()
    {
        if(Vector3.Distance(this.transform.position, lightPlayer.transform.position) <= Vector3.Distance(this.transform.position, shadowPlayer.transform.position))
        {
            return lightPlayer;
        }
        return shadowPlayer;
    }

    private bool IsInRange(GameObject player)
    {
        if(Vector3.Distance(this.transform.position, player.transform.position)*2 <= detectionRange)
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Sword")
        {
            Destroy(this.gameObject);
        }

        if ((other.tag == "Player" || other.tag == "DarkPlayer" || other.tag == "LightPlayer") && other.GetType() == typeof(BoxCollider))
        {
            other.GetComponent<PlayerController>().Kill();
        }
    }
}
