using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLustre : MonoBehaviour
{
    public bool mustMove;
    public float speed;
    public float maxAngle;

    public enum Direction { Left, Right }
    public Direction direction;

    private Transform lustreMesh;
    private Transform chain;

    private float angle;

    private void Start()
    {
        lustreMesh = this.transform.GetChild(0);
        chain = this.transform.GetChild(1);

        chain.localScale = new Vector3(1f, Vector3.Distance(chain.position, lustreMesh.position)/2f, 1f);
    }

    void Update()
    {
        if(mustMove)
        {
            angle = (this.transform.eulerAngles.z > 180) ? this.transform.eulerAngles.z - 360 : this.transform.eulerAngles.z;

            if (direction == Direction.Right)
            {
                transform.Rotate(Vector3.forward, 10f * speed * Time.deltaTime);

                if (angle >= maxAngle)
                {
                    direction = Direction.Left;
                }
            }

            if (direction == Direction.Left)
            {
                transform.Rotate(Vector3.forward, -10f * speed * Time.deltaTime);

                if (angle <= -maxAngle)
                {
                    direction = Direction.Right;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IPlatform lp = other.GetComponent<IPlatform>();

        if (lp != null)
        {
            lp.LightSources++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IPlatform lp = other.GetComponent<IPlatform>();

        if (lp != null)
        {
            lp.LightSources--;
        }
    }

}
