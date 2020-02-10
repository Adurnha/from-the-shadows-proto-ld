using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlatform
{
    int LightSources { get; set; }
    bool IsLightedByPureShadow { get; set; }
}
