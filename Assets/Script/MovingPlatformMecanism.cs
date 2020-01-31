using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformMecanism : Mecanism
{
    Vector3 targetedPosition;
    Vector3 basePosition;


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
        targetedPosition = new Vector3(this.transform.position.x, this.transform.position.y - 8f, this.transform.position.z);
    }

    public override void DeactivateMecanism()
    {
        targetedPosition = basePosition;
    }

}
