using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumManager
{
    public enum ClassEnemy
    {
        Normal,
        Boss,
    }

    public enum ETabSetting: int
    {
        Gameplay = 0,
        Graphic = 1,
        Sound = 2,
    }
    
    public enum EBgmSoundName
    {
        AssassinCreed,
        MildFlight,
        DungeonLoop,
    }
    
    public enum ESfxSoundName
    {
        ClickBtn,
        CheckBox,
        NotiError,
        NotiWarning,
        NotiAlert,
        SwordSlash,
        Hover,
    }
    
    public enum EButtonType
    {
        ClickBtn,
        CheckBox,
        Hover,
    }

    public enum EGuideType
    {
        EGuidePlayer,
        EGuideEnemy,
    }

    public enum EGuidePlayer : int
    {
        Movement = 0,
        Sprint = 1,
        Roll = 2,
        Jump = 3,
        Skill = 4,
    }
    
    public enum EGuideEnemy : int
    {
        Skeleton = 0,
        Necromancer = 1,
        LavaElemental = 2,
        EarthElemental = 3,
        SkeletonReaper = 4,
        DragonUrus = 5,
        DragonNightMare = 6,
    }
    
    public enum EPet : int
    {
        Gulpuff = 1,
        GlacioPrism = 2,
        Hooscamp = 3,
        Crownless = 4,
        MourningAix = 5,
        Spearback = 6,
        Flautist = 7,
    }
    
    public enum EElement : int
    {
        Frozen = 1,
        Darknest = 2,
        Wind = 3,
        Plant = 4,
        Light = 5,
        Fire = 6,
        Thunder = 7,
    }

    public enum EFashionType: int
    {
        Leg = 1,
        Torso = 2,
        Head = 3,
        Hair = 4,
    }
}
