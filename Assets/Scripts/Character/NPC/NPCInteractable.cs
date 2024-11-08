using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    public NpcCharacter npcCharacter;

    private void Awake()
    {
        npcCharacter = GetComponent<NpcCharacter>();
    }

    public void NpcInteract()
    {
        Debug.Log("InteractNPC:  " + npcCharacter.npcName);
        VillageHomeScreen.Instance.ActiveDialoguePopup((int)npcCharacter.npcName);
    }
}
