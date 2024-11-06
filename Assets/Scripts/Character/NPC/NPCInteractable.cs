using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    private NpcCharacter _npcCharacter;

    private void Awake()
    {
        _npcCharacter = GetComponent<NpcCharacter>();
    }

    public void NpcInteract()
    {
        Debug.Log("InteractNPC:  " + _npcCharacter.npcName);
        VillageHomeScreen.Instance.ActiveDialoguePopup((int)_npcCharacter.npcName);
    }
}
