using System;
using System.Collections;
using System.Collections.Generic;
using CartoonHeroes;
using UnityEngine;
using UnityEngine.Events;

public static class ActionManager
{
    public static Action OnUpdateHoverLevelScren;
    public static Action<int> OnUpdateInformationWeaponTab;
    public static Action<int> OnUpdateInformationPotionTab;
    public static Action<string> OnUpdateAnoucement;
    public static Action OnUpdateCoin;
    public static Action<int,int> OnUpdateInformationPetTab;
    public static Action<int,int> OnUpdatePetInventoryModelView;
    public static Action<int, int> OnUpdateFashionPlayer;
    public static Action<bool,int> OnUpdateWeaponPlayer;
    public static Action<int> OnOpenEggScreen;
    public static Action<int> OnUpdateXpAndLevelPlayer;
    public static Action<int> OnUpdatenextStepTutorial;
    public static Action<int> OnUpdateNextStepPetScreenTutorial;
    public static Action<int> OnUpdatePotionChoiceEquip;
    public static Action OnUpdateUIPlayerDie;

    public static Action<int> OnOpenGateNarrow;
    public static Action<string> OnUpdateAnoucementText;
    //enemy boss actions
    public static Action<int> OnBossChangePhase;
}
