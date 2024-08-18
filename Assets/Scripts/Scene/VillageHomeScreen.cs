using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageHomeScreen : MonoBehaviour
{
    public GameObject maleCharacter;
    public GameObject femaleCharacter;
    public SetPlayer weaponSetup;
    
    private static VillageHomeScreen _instance;
    public static VillageHomeScreen Instance => _instance;

    public enum ShopId : int
    {
        WeaponShop = 0,
        SkillsShop = 1,
        GroceryShop = 2,
    }
    public GameObject[] listShop;

    public GameObject dialoguePopup;
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        if (DataManager.Instance.GetDataInt(DataManager.EDataPrefName.PlayerSex) == 1)
        {
            femaleCharacter.SetActive(true);
            weaponSetup = femaleCharacter.GetComponent<SetPlayer>();
        }
        else
        {
            maleCharacter.SetActive(true);
            weaponSetup = maleCharacter.GetComponent<SetPlayer>();
        }
    }
    
    public void NextScene()
    {
        LoadingScreen.Instance.LoadScene("MainScene");
    }

    public void CloseAllShop()
    {
        foreach (var shop in listShop)
        {
            shop.SetActive(false);
        }
        weaponSetup.SetWeaponPlayer();
        dialoguePopup.SetActive(false);
    }

    public void OpenShopWeapon()
    {
        foreach (var shop in listShop)
        {
            shop.SetActive(false);
        }
        listShop[(int)ShopId.WeaponShop].SetActive(true);
    }
    
    public void OpenShopSkills()
    {
        foreach (var shop in listShop)
        {
            shop.SetActive(false);
        }
        listShop[(int)ShopId.SkillsShop].SetActive(true);
    }
    
    public void OpenShopGroceryShop()
    {
        foreach (var shop in listShop)
        {
            shop.SetActive(false);
        }
        listShop[(int)ShopId.GroceryShop].SetActive(true);
    }

    public void ActiveDialoguePopup(int idNpc)
    {
        dialoguePopup.SetActive(true);
        DialogueManager dialogueManager = dialoguePopup.GetComponent<DialogueManager>();
        dialogueManager.ActiveDialogue();
        dialogueManager.SetDataDialogue(DataManager.Instance.GetNpcDataByID(idNpc));
        dialogueManager.SetButtonFunc(idNpc);
    }
}
