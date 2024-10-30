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
        FashionShop = 5,
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
        if (DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.PlayerSex) == 1)
        {
            femaleCharacter.SetActive(true);
            weaponSetup = femaleCharacter.GetComponent<SetPlayer>();
        }
        else
        {
            maleCharacter.SetActive(true);
            weaponSetup = maleCharacter.GetComponent<SetPlayer>();
        }
        SoundManager.Instance.PlayBgm(EnumManager.EBgmSoundName.MildFlight);
    }
    
    public void LoadSceneMain()
    {
        LoadingScreen.Instance.LoadScene("MainScene");
    }
    
    public void OpenLevelScreen()
    {
        listShop[3].SetActive(true);
    }
    
    public void OpenPersonalScreen()
    {
        listShop[4].SetActive(true);
    }
    
    public void CloseAllChildScreens()
    {
        foreach (var shop in listShop)
        {
            shop.SetActive(false);
        }
        weaponSetup.SetWeaponPlayer();
        dialoguePopup.SetActive(false);
    }

    public void OpenShopWithId(ShopId shopId)
    {
        listShop[(int)shopId].SetActive(true);
    }
    public void ActiveDialoguePopup(int idNpc)
    {
        dialoguePopup.SetActive(true);
        DialogueManager dialogueManager = dialoguePopup.GetComponent<DialogueManager>();
        dialogueManager.SetDataDialogue(DataManager.Instance.GetNpcDataByID(idNpc));
        dialogueManager.ActiveDialogue();
        dialogueManager.SetButtonFunc(idNpc);
    }
    
    public void OpenSetting()
    {
        GameManager.Instance.OpenSettingScreen();
    }
    
    public void OpenGuide()
    {
        GameManager.Instance.OpenGuideScreen();
    }
}
