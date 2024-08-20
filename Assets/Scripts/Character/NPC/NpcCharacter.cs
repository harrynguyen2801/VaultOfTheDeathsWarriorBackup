using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCharacter : MonoBehaviour
{
    public enum Npc:int
    {
        None = 0,
        Bruto = 1,
        Hatarana = 2,
        Kiriana = 3,
        MeiMei = 4,
    }

    public Npc npcName;
}
