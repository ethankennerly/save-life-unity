using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TouchLog", menuName = "TouchReplay/TouchLog", order = 1)]
public class TouchLogAsset : ScriptableObject
{
    [Header("Session Seed for Deterministic Random")]
    public int randomSeed;

    [Header("Recorded Touch Events")]
    public List<TouchLogEntry> touches = new List<TouchLogEntry>();
}
