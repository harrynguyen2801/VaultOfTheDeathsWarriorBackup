using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropItem : MonoBehaviour
{
    public enum ItemType
    {
        Coin = 0,
        HealOrb = 1,
    }

    public ItemType type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().PickUpItem(this);
            Destroy(gameObject);
        }
    }
}
