using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCharacter : MonoBehaviour
{
    public enum Npc:int
    {
        None = 0,
        Kiriana = 1,
        Bruto = 2,
        Hatarana = 3,
        Hina = 4,
        MeiMei = 5,
    }

    public Npc npcName;
}
