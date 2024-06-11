using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavContent : MonoBehaviour
{
    public GameObject content;

    public GameObject pbWeapon;

    public List<GameObject> listWeaponItem = new List<GameObject>();

    private Dictionary<int,Tuple<string,string,int,int,int,int,string>> _weaponsData = new Dictionary<int, Tuple<string,string,int,int,int,int,string>>()
    {
        {1,Tuple.Create("Sacrifial","Sword",30,100,50,100,"The sword of a knight that symbolizes the restored honor of Dvalin. The blessings of the Anemo Archon rest on the fuller of the blade")},
        {2,Tuple.Create("Bloodtainted","Polearm",25,110,50,100,"A greatsword as light as the sigh of grass in the breeze, yet as merciless to the corrupt as a typhoon.")}, 
        {3,Tuple.Create("Harbinger","Polearm",20,130,50,100,"A symbol of a legendary pact, this sharp blade once cut off the peak of a mountain.")}, 
        {4,Tuple.Create("Deathmatch","Claymore",45,150,50,100,"A weapon once used by a young maiden who forsook her family name, stained with the blood of enemies and loved ones both.")},
        {5,Tuple.Create("Deathmatch","Claymore",45,150,50,100,"A weapon once used by a young maiden who forsook her family name, stained with the blood of enemies and loved ones both.")},
        {6,Tuple.Create("Deathmatch","Claymore",45,150,50,100,"A weapon once used by a young maiden who forsook her family name, stained with the blood of enemies and loved ones both.")},
        {7,Tuple.Create("Deathmatch","Claymore",45,150,50,100,"A weapon once used by a young maiden who forsook her family name, stained with the blood of enemies and loved ones both.")},
        {8,Tuple.Create("Deathmatch","Claymore",45,150,50,100,"A weapon once used by a young maiden who forsook her family name, stained with the blood of enemies and loved ones both.")},
        {9,Tuple.Create("Deathmatch","Claymore",45,150,50,100,"A weapon once used by a young maiden who forsook her family name, stained with the blood of enemies and loved ones both.")},
        {10,Tuple.Create("Deathmatch","Claymore",45,150,50,100,"A weapon once used by a young maiden who forsook her family name, stained with the blood of enemies and loved ones both.")},
        {11,Tuple.Create("Deathmatch","Claymore",45,150,50,100,"A weapon once used by a young maiden who forsook her family name, stained with the blood of enemies and loved ones both.")},
        {12,Tuple.Create("Deathmatch","Claymore",45,150,50,100,"A weapon once used by a young maiden who forsook her family name, stained with the blood of enemies and loved ones both.")},
        {13,Tuple.Create("Deathmatch","Claymore",45,150,50,100,"A weapon once used by a young maiden who forsook her family name, stained with the blood of enemies and loved ones both.")},
        {14,Tuple.Create("Deathmatch","Claymore",45,150,50,100,"A weapon once used by a young maiden who forsook her family name, stained with the blood of enemies and loved ones both.")},
        {15,Tuple.Create("Deathmatch","Claymore",45,150,50,100,"A weapon once used by a young maiden who forsook her family name, stained with the blood of enemies and loved ones both.")},
        {16,Tuple.Create("Deathmatch","Claymore",45,150,50,100,"A weapon once used by a young maiden who forsook her family name, stained with the blood of enemies and loved ones both.")},
    };

    public void ShowWeaponList()
    {
        listWeaponItem.Clear();
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Debug.Log(content.transform.childCount);
            Destroy(content.transform.GetChild(i).gameObject);
        }
        foreach (var data in _weaponsData)
        {
            var weapon = Instantiate(pbWeapon,content.transform);
            weapon.GetComponent<WeaponItem>().SetDataWeapon(data.Key,data.Value);
            listWeaponItem.Add(weapon);
        }
    }
}
