using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMecanism : Mecanism
{
    Vector3 targetedPosition;
    Vector3 basePosition;

    public enum Direction { Up, Down, Left, Right };

    public Direction direction = Direction.Up;

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
        switch(direction)
        {
            case Direction.Up:
                targetedPosition = new Vector3(this.transform.position.x, this.transform.position.y + this.transform.localScale.x, this.transform.position.z);
                break;
            case Direction.Down:
                targetedPosition = new Vector3(this.transform.position.x, this.transform.position.y - this.transform.localScale.x, this.transform.position.z);
                break;
            case Direction.Left:
                targetedPosition = new Vector3(this.transform.position.x - this.transform.localScale.x, this.transform.position.y, this.transform.position.z);
                break;
            case Direction.Right:
                targetedPosition = new Vector3(this.transform.position.x + this.transform.localScale.x, this.transform.position.y, this.transform.position.z);
                break;
            default:
                break;
        }
    }

    public override void DeactivateMecanism()
    {
        targetedPosition = basePosition;
    }
}
