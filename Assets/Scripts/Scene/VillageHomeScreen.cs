using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CartoonHeroes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class VillageHomeScreen : MonoBehaviour
{
    private static VillageHomeScreen _instance;
    public static VillageHomeScreen Instance => _instance;
    public GameObject[] modelNpcList;
    
    public enum ShopId : int
    {
        WeaponShop = 0,
        SkillsShop = 1,
        GroceryShop = 2,
        FashionShop = 5,
    }
    public GameObject[] listShop;

    public GameObject dialoguePopup;
    public GameObject character3DModelController;
    public PlayerModelEquipManager playerModelEquipManager;

    [Header("Game Objects List To Deactives To Show Open Egg")]
    public List<GameObject> listObjectsToDeactivate;

    public int petIdx;

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

    private void OnEnable()
    {
        ActionManager.OnOpenEggScreen += ActiveOpenEgg;
        
        DataManager.Instance.LoadDictDataPet();
        var petId = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.PetId);
        if (petId != 0)
        {
            var petLv = DataManager.Instance.PetData[petId].Item2;
            Debug.Log("village : pet id " + petId + " pet lv " + petLv);
            ActionManager.OnUpdatePetInventoryModelView?.Invoke(petId,petLv);
        }
        ActionManager.OnUpdateWeaponPlayer?.Invoke(true,DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.WeaponId));
    }

    private void OnDisable()
    {
        ActionManager.OnOpenEggScreen -= ActiveOpenEgg;
    }

    void Start()
    {
        SoundManager.Instance.PlayBgm(EnumManager.EBgmSoundName.MildFlight);
        Debug.Log("Tutorial State + " + DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Tutorial));
        Debug.Log("Tutorial step + " + DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.TutorialStep));
        if (DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Tutorial) == 0)
        {
            foreach (var model in modelNpcList)
            {
                model.SetActive(false);
            }
            var step = DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.TutorialStep);
            ActionManager.OnUpdatenextStepTutorial?.Invoke(step);
        }
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
        character3DModelController.SetActive(true);
        listShop[4].SetActive(true);
    }
    
    public void CloseAllChildScreens()
    {
        foreach (var shop in listShop)
        {
            shop.SetActive(false);
        }
        playerModelEquipManager.ReloadFashionEquip();
        character3DModelController.SetActive(false);
        dialoguePopup.SetActive(false);
    }

    public void OpenShopWithId(ShopId shopId)
    {
        listShop[(int)shopId].SetActive(true);
        switch ((int)shopId)
        {
            case 0:
                character3DModelController.SetActive(true);
                break;
            case 5:
                character3DModelController.SetActive(true);
                break;
        }
    }
    public void ActiveDialoguePopup(int idNpc)
    {
        dialoguePopup.SetActive(true);
        DialogueManager dialogueManager = dialoguePopup.GetComponent<DialogueManager>();
        dialogueManager.SetDataDialogue(DataManager.Instance.GetNpcDataByID(idNpc));
        dialogueManager.ActiveDialogue();
        dialogueManager.SetButtonFunc(idNpc);
        
        if (DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Tutorial) == 0)
        {
            dialogueManager.btnShop.onClick.RemoveAllListeners();
            Debug.Log(idNpc + " : idnpc");
            Debug.Log(DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.TutorialStep) + " : idstep");
        
            if (idNpc == DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.TutorialStep))
            {
                dialogueManager.SetButtonFunc(idNpc);
            }
        
            if (DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.TutorialStep) == 5)
            {
                DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.Tutorial, 1);
            }
        }
        else
        {
            dialogueManager.SetButtonFunc(idNpc);
        }
    }
    
    public void OpenSetting()
    {
        GameManager.Instance.OpenSettingScreen();
    }
    
    public void OpenGuide()
    {
        GameManager.Instance.OpenGuideScreen();
    }

    public void ActiveOpenEgg(int petId)
    {
        petIdx = petId;
        listObjectsToDeactivate.ForEach(obj => obj.SetActive(false));
    }
    
    public void DeactiveOpenEgg()
    {
        listObjectsToDeactivate.ForEach(obj => obj.SetActive(true));
    }

    public void InvokeNextStepTutorial()
    {
        if (DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Tutorial) == 0)
        {
            Debug.Log("tutorial step action invoke+ " + DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.TutorialStep));
            ActionManager.OnUpdatenextStepTutorial?.Invoke(DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.TutorialStep));
        }
    }
}
