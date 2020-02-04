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

    private float angle;


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
