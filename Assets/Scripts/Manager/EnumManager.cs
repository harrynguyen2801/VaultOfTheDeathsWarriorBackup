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
    }
    
    public enum EButtonType
    {
        ClickBtn,
        CheckBox,
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
        MageSkeleton = 1,
        Roll = 2,
        Jump = 3,
        Skill = 4,
        Trean = 5,
        Dragon = 6,
        Elemental = 7,

    }
}
