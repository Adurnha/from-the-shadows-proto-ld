using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonArmy : MonoBehaviour
{
    [SerializeField]
    private float actualSpeed;

    [SerializeField]
    private float startingSpeed = 2f;

    [SerializeField]
    private float endingSpeed = 2f;

    [SerializeField]
    private float timeToReachMaxSpeed = 10f;

    private float t = 0.0f;

    public enum Direction { Up, Down, Left, Right };

    public Direction direction = Direction.Up;

    private void Start()
    {
        
    }

    void Update()
    {
        actualSpeed = Mathf.Lerp(startingSpeed, endingSpeed, t);

        t += (1/timeToReachMaxSpeed) * Time.deltaTime;

        switch(direction)
        {
            case Direction.Up:
                this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + (actualSpeed * Time.deltaTime));
                break;
            case Direction.Down:
                this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - (actualSpeed * Time.deltaTime));
                break;
            case Direction.Left:
                this.transform.position = new Vector2(this.transform.position.x - (actualSpeed * Time.deltaTime), this.transform.position.y);
                break;
            case Direction.Right:
                this.transform.position = new Vector2(this.transform.position.x + (actualSpeed * Time.deltaTime), this.transform.position.y);
                break;
            default:
                break;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetType() == typeof(CharacterController))
        {
            other.GetComponent<PlayerController>().Kill();
        }
    }
}
