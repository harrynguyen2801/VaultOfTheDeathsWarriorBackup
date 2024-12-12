using System.Collections;
using System.Collections.Generic;
using Michsky.UI.Dark;
using UnityEngine;

public class AnoucementPositionInGame : MonoBehaviour
{
    public CustomDropdown dropdown;
    public void SavePositionAnoucement()
    {
        DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.Noti,dropdown.selectedItemIndex);
        Debug.Log("Saved position " + dropdown.selectedItemIndex);
    }
}
