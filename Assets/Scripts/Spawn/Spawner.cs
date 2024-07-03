using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public enum SpawnType : int
    {
        Skeleton = 0,
        MageSkeleton = 1,
        DragonGreen = 2,
        DragonRed = 3,
    }
    public SpawnType typeSpawner;
}
