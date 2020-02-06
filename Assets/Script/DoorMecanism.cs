using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMecanism : Mecanism
{
    Vector3 targetedPosition;
    Vector3 basePosition;

    public enum Direction { Up, Down, Left, Right };

    public Direction direction = Direction.Up;

    private int activatedTimes = 0;

    private void Start()
    {
        targetedPosition = this.transform.position;
        basePosition = this.transform.position;
    }

    private void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, targetedPosition, 1f * Time.deltaTime);
    }
    public override void ActivateMecanism()
    {
        activatedTimes++;

        if (activatedTimes == 1)
        {
            switch (direction)
            {
                case Direction.Up:
                    targetedPosition = new Vector3(basePosition.x, basePosition.y + this.transform.localScale.x, basePosition.z);
                    break;
                case Direction.Down:
                    targetedPosition = new Vector3(basePosition.x, basePosition.y - this.transform.localScale.x, basePosition.z);
                    break;
                case Direction.Left:
                    targetedPosition = new Vector3(basePosition.x - this.transform.localScale.x, basePosition.y, basePosition.z);
                    break;
                case Direction.Right:
                    targetedPosition = new Vector3(basePosition.x + this.transform.localScale.x, basePosition.y, basePosition.z);
                    break;
                default:
                    break;
            }
        }
    }

    public override void DeactivateMecanism()
    {
        activatedTimes--;

        if (activatedTimes == 0)
        {
            targetedPosition = basePosition;
        }
    }
}
