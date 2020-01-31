using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAttributes : MonoBehaviour
{
    public enum PlatformType { Shadow, Neutral, Light };

    public PlatformType platformType;
    public bool isUnstable = false;
    public float fallingTimer = 2f;
    public float respawnTimer = 5f;

}
