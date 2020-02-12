using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : Mecanism
{
    Vector3 basePosition;


    public float speed;
    public float movementLength;

    public enum Direction { Up, Down, Left, Right };
    public Direction direction = Direction.Up;

    public bool isChildOf = false;

    public float movement;

    private int activatedTimes = 0;

    private bool activated = false;
    private bool goBack = false;

    void Start()
    {
        basePosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            if (!isChildOf && movement <= movementLength)
            {
                if (direction == Direction.Right)
                    transform.localPosition += Vector3.right * speed * Time.deltaTime;
                if (direction == Direction.Down)
                    transform.localPosition += Vector3.down * speed * Time.deltaTime;
                if (direction == Direction.Up)
                    transform.localPosition += Vector3.up * speed * Time.deltaTime;
                if (direction == Direction.Left)
                    transform.localPosition += Vector3.left * speed * Time.deltaTime;

                movement += speed * Time.deltaTime;
            }

            else if (isChildOf && movement <= movementLength)
            {
                if (direction == Direction.Right)
                    transform.localPosition -= transform.right * speed / 10f * Time.deltaTime;
                if (direction == Direction.Down)
                    transform.localPosition += transform.up * speed / 10f * Time.deltaTime;
                if (direction == Direction.Up)
                    transform.localPosition -= transform.up * speed / 10f * Time.deltaTime;
                if (direction == Direction.Left)
                    transform.localPosition += transform.right * speed / 10f * Time.deltaTime;

                movement += speed * Time.deltaTime;
            }
        }

        if(goBack)
        {
            if (!isChildOf && movement >= 0)
            {
                if (direction == Direction.Right)
                    transform.localPosition -= Vector3.right * speed * Time.deltaTime;
                if (direction == Direction.Down)
                    transform.localPosition -= Vector3.down * speed * Time.deltaTime;
                if (direction == Direction.Up)
                    transform.localPosition -= Vector3.up * speed * Time.deltaTime;
                if (direction == Direction.Left)
                    transform.localPosition -= Vector3.left * speed * Time.deltaTime;

                movement -= speed * Time.deltaTime;
            }

            else if (isChildOf && movement > 0)
            {
                if (direction == Direction.Right)
                    transform.localPosition += transform.right * speed / 10f * Time.deltaTime;
                if (direction == Direction.Down)
                    transform.localPosition -= transform.up * speed / 10f * Time.deltaTime;
                if (direction == Direction.Up)
                    transform.localPosition += transform.up * speed / 10f * Time.deltaTime;
                if (direction == Direction.Left)
                    transform.localPosition -= transform.right * speed / 10f * Time.deltaTime;

                movement -= speed * Time.deltaTime;
            }
        }
    }

    public override void ActivateMecanism()
    {
        activatedTimes++;

        if (activatedTimes == 1)
        {
            activated = true;
            goBack = false;
        }
    }

    public override void DeactivateMecanism()
    {
        activatedTimes--;

        if (activatedTimes == 0)
        {
            goBack = true;
            activated = false;
        }
    }

}
