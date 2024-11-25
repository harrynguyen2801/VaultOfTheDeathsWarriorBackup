using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;

public class DataManager : MonoBehaviour
{
    public enum EDataPrefName
    {
        FirstGame,
        StartScreen,
        LevelOpen,
        Coin,
        LevelPlay,
        TutorialVillage,
        TutorialStep,
        TutorialLevel,
    }

    public enum EDataPlayerEquip
    {
        PlayerSex,
        Leg,
        Torso,
        Head,
        Hair,
        WeaponId,
        PetId,
        LevelPlayer,
        Xp,
    }
    
    public enum EEnemyType
    {
        Skeleton,
        MageSkeleton,
        DragonNight,
        DragonUsu,
        EarthElementals,
        LavaElementals,
        Treant,
        Reaper,
    }
    
    public enum ESkills : int
    {
        Guard,
        Magic,
        Sword,
    }

    public enum Epotion: int
    {
        Potion1 = 1,
        Potion2 = 2,
    }

    public Dictionary<EEnemyType, Tuple<int, int>> DataEnemy = new Dictionary<EEnemyType, Tuple<int, int>>()
    {
        { EEnemyType.Skeleton , Tuple.Create(120,30)},
        { EEnemyType.MageSkeleton ,Tuple.Create(100,40)},
        { EEnemyType.DragonNight ,Tuple.Create(1200,500)},
        { EEnemyType.DragonUsu ,Tuple.Create(900,50)},
        { EEnemyType.EarthElementals ,Tuple.Create(400,120)},
        { EEnemyType.LavaElementals ,Tuple.Create(400,120)},
        { EEnemyType.Treant ,Tuple.Create(260,100)},
        { EEnemyType.Reaper ,Tuple.Create(350,150)},
    };

    private readonly Dictionary<EDataPrefName, string> _dataPrefGame = new Dictionary<EDataPrefName, string>()
    {
        {EDataPrefName.StartScreen,"StartScreen"},
        {EDataPrefName.LevelOpen,"LevelOpen"},
        {EDataPrefName.LevelPlay,"LevelPlay"},
        {EDataPrefName.Coin,"Coin"},
        {EDataPrefName.FirstGame,"FirstGame"},
        {EDataPrefName.TutorialVillage,"TutorialVillage"},
        {EDataPrefName.TutorialStep,"TutorialStep"},
    };
    
    private readonly Dictionary<EDataPlayerEquip, string> _dataPrefPlayer = new Dictionary<EDataPlayerEquip, string>()
    {
        {EDataPlayerEquip.PlayerSex,"PlayerSex"},
        {EDataPlayerEquip.WeaponId,"WeaponId"},
        {EDataPlayerEquip.Hair,"HairId"},
        {EDataPlayerEquip.Head,"HeadId"},
        {EDataPlayerEquip.Torso,"TorsoId"},
        {EDataPlayerEquip.Leg,"LegId"},
        {EDataPlayerEquip.PetId,"PetId"},
        {EDataPlayerEquip.LevelPlayer,"LevelPlayer"},
        {EDataPlayerEquip.Xp,"XpPlayer"},
    };
    
    private readonly Dictionary<ESkills, string> _dataSkills = new Dictionary<ESkills, string>()
    {
        {ESkills.Guard,"Guard"},
        {ESkills.Sword,"Sword"},
        {ESkills.Magic,"Magic"},

    };
    
    private readonly Dictionary<Epotion, string> _dataPotions = new Dictionary<Epotion, string>()
    {
        {Epotion.Potion1,"Potion1"},
        {Epotion.Potion2,"Potion2"},
    };


    #region Data Weapons

        private readonly Dictionary<int, Tuple<string, string, int, int, int, string, int,Tuple<int>>> _weaponsDataDefault = new Dictionary<int, Tuple<string, string, int, int, int, string, int, Tuple<int>>>()
    {
        {1,Tuple.Create("Sacrifial","Sword",30,100,100,"The sword of a knight that symbolizes the restored honor of Dvalin. The blessings of the Anemo Archon rest on the fuller of the blade",100,1)},
        {2,Tuple.Create("Bloodtainted","Polearm",25,110,120,"A greatsword as light as the sigh of grass in the breeze, yet as merciless to the corrupt as a typhoon.",100,1)}, 
        {3,Tuple.Create("Harbinger","Polearm",30,130,120,"A symbol of a legendary pact, this sharp blade once cut off the peak of a mountain.",100,1)}, 
        {4,Tuple.Create("Deathmatch","Claymore",45,150,110,"A weapon once used by a young maiden who forsook her family name, stained with the blood of enemies and loved ones both.",100,1)},
        {5,Tuple.Create("Aquila Favonia","Sword",55,150,100,"The soul of the Knights of Favonius. Millennia later, it still calls on the winds of swift justice to vanquish all evil — just like the last heroine who wielded it.",100,0)},
        {6,Tuple.Create("Calamity Queller","Sword",45,150,100,"A keenly honed weapon forged from some strange crystal. Its faint blue light seems to whisper of countless matters now past.",100,0)},
        {7,Tuple.Create("Black Tassel","Sword",30,165,100,"A naginata used to cut grass. Any army that stands before this weapon will probably be likewise cut down.",100,0)},
        {8,Tuple.Create("Skyward Blade","Sword",45,150,120,"The sword of a knight that symbolizes the restored honor of Dvalin The blessings of the Anemo Archon rest on the fuller of the blade.",400,0)},
        {9,Tuple.Create("Staff of Homa","Sword",55,100,120,"A firewood staff that was once used in ancient and long-lost rituals.",400,0)},
        {10,Tuple.Create("Akuoumaru","Sword",35,160,130,"The beloved sword of the legendary Akuou. The blade is huge and majestic, but is surprisingly easy to wield.",500,0)},
        {11,Tuple.Create("Blackcliff Pole","Sword",65,150,150,"A weapon made of blackstone and aerosiderite. There is a dark crimson glow on its cold black sheen.",500,0)},
        {12,Tuple.Create("Festering Desire","Sword",55,150,160,"A creepy straight sword that almost seems to yearn for life. It drips with a shriveling venom that could even corrupt a mighty dragon.",600,0)},
        {13,Tuple.Create("Hamayumi","Claymore",75,150,130,"A certain shrine maiden once owned this warbow. It was made with surpassing skill, and is both intricate and sturdy.",700,0)},
        {14,Tuple.Create("Ibis Piercer","Claymore",65,160,150,"A golden bow forged from the description in the story. If you use it as a normal weapon,",800,0)},
        {15,Tuple.Create("Sacrificial Jade","Claymore",55,170,140,"An ancient jade pendant that gleams like clear water. It seems to have been used in ancient ceremonies.",950,0)},
        {16,Tuple.Create("Tidal Shadow","Claymore",55,190,120,"An exquisitely-crafted. standard-model sword forged for the high-ranking officers and flagship captains of Fontaine's old navy.",1050,0)},
    };
    
    public Dictionary<int, Tuple<string, string, int, int, int, string, int ,Tuple<int>>> WeaponsDatas =
        new Dictionary<int, Tuple<string, string, int, int, int, string, int ,Tuple<int>>>() { };

    #endregion
    
    #region Data Potions

    public Dictionary<int, Tuple<string, string, int, int, int , int, string, Tuple<Tuple<int, int>>>> PotionsDataDefault = new Dictionary<int, Tuple<string, string, int, int, int, int, string, Tuple<Tuple<int, int>>>>()
    {
        {1,Tuple.Create("Sacrifial", "Consume", 30, 0, 0, 0, "The sword of a knight that symbolizes the restored honor of Dvalin. The blessings of the Anemo Archon rest on the fuller of the blade", Tuple.Create(100, 15))},
        {2,Tuple.Create("Bloodtainted","Consume",25,0,0,0,"A greatsword as light as the sigh of grass in the breeze, yet as merciless to the corrupt as a typhoon.", Tuple.Create(100, 15))}, 
        {3,Tuple.Create("Harbinger","Consume",0,0,30,20,"A symbol of a legendary pact, this sharp blade once cut off the peak of a mountain.", Tuple.Create(300, 15))}, 
        {4,Tuple.Create("Deathmatch","Consume",50,15,20,0,"A weapon once used by a young maiden who forsook her family name, stained with the blood of enemies and loved ones both.", Tuple.Create(400, 15))},
        {5,Tuple.Create("Aquila Favonia","Consume",30,50,10,10,"The soul of the Knights of Favonius. Millennia later, it still calls on the winds of swift justice to vanquish all evil — just like the last heroine who wielded it.", Tuple.Create(9999, 100))},
        {6,Tuple.Create("Calamity Queller","Consume",0,0,150,0,"A keenly honed weapon forged from some strange crystal. Its faint blue light seems to whisper of countless matters now past.", Tuple.Create(9999, 100))},
        {7,Tuple.Create("Calamity Queller","Consume",0,0,150,0,"A keenly honed weapon forged from some strange crystal. Its faint blue light seems to whisper of countless matters now past.", Tuple.Create(9999, 100))},
        {8,Tuple.Create("Calamity Queller","Consume",0,0,150,0,"A keenly honed weapon forged from some strange crystal. Its faint blue light seems to whisper of countless matters now past.", Tuple.Create(9999, 100))},
        {9,Tuple.Create("Calamity Queller","Consume",0,0,150,0,"A keenly honed weapon forged from some strange crystal. Its faint blue light seems to whisper of countless matters now past.", Tuple.Create(9999, 100))},
        {10,Tuple.Create("Calamity Queller","Consume",0,0,150,0,"A keenly honed weapon forged from some strange crystal. Its faint blue light seems to whisper of countless matters now past.", Tuple.Create(9999, 100))},
        {11,Tuple.Create("Calamity Queller","Consume",0,0,150,0,"A keenly honed weapon forged from some strange crystal. Its faint blue light seems to whisper of countless matters now past.", Tuple.Create(9999, 100))},
        {12,Tuple.Create("Calamity Queller","Consume",0,0,150,0,"A keenly honed weapon forged from some strange crystal. Its faint blue light seems to whisper of countless matters now past.", Tuple.Create(9999, 100))},
    };
    
    public Dictionary<int, Tuple<string, string, int, int, int, int, string,Tuple<Tuple<int, int>>>> PotionsData =
        new Dictionary<int, Tuple<string, string, int, int, int, int, string,Tuple<Tuple<int, int>>>>() { };
    
    public readonly Dictionary<int, Tuple<int, int>> DataPotionPlayerBuyDefault = new Dictionary<int, Tuple<int, int>>()
    {
        { 1, Tuple.Create(1, 1) },
        { 2, Tuple.Create(2, 1) },
        { 3, Tuple.Create(3, 0) },
        { 4, Tuple.Create(4, 0) },
        { 5, Tuple.Create(5, 0) },
        { 6, Tuple.Create(6, 0) },
        { 7, Tuple.Create(7, 0) },
        { 8, Tuple.Create(8, 0) },
        { 9, Tuple.Create(9, 0) },
        { 10, Tuple.Create(10, 0) },
        { 11, Tuple.Create(11, 0) },
        { 12, Tuple.Create(12, 0) },
    };

    public Dictionary<int, Tuple<int, int>> DataPotionPlayerBuy = new Dictionary<int, Tuple<int, int>>();
    #endregion
    
    
    #region Data Pet

    //2 is level, 3 is HP, 4 is ATK, 5 is DEF, 6 is Description, 7 is BuyState 8 is price
    private readonly Dictionary<int, Tuple<string, int, int, int, int, string, int,Tuple<int>>> _petDataDefault = new Dictionary<int, Tuple<string, int, int, int, int, string, int,Tuple<int>>>()
    {
        {1,Tuple.Create("Gulpuff",1,20,20,0,"Summons a shield that blocks all incoming damage for 2 seconds.",0,100)},
        {2,Tuple.Create("Glacio Prism",1,15,20,15,"Use the divine bow to summon a rain of arrows carrying the.",0,200)},
        {3,Tuple.Create("Hooscamp",1,20,20,10,"Summon a storm of swords carrying holy light energy",0,300)},
        {4,Tuple.Create("Crownless",1,20,20,10,"Summon a storm of swords carrying holy light energy",0,200)},
        {5,Tuple.Create("Mourning Aix",1,20,20,10,"Summon a storm of swords carrying holy light energy",0,300)},
        {6,Tuple.Create("Spearback",1,20,20,10,"Summon a storm of swords carrying holy light energy",0,300)},
        {7,Tuple.Create("Flautist",1,20,20,10,"Summon a storm of swords carrying holy light energy",0,300)},
    };

    public Dictionary<int, Tuple<string, int, int, int, int, string, int,Tuple<int>>> PetData =
        new Dictionary<int, Tuple<string, int, int, int, int, string, int,Tuple<int>>>() { };

    #endregion
    
    //item3 is mana, item4 is CD, item5 is damage, item6 is description, item7 is tuple state buy and price

    #region Data Skill Guard

    private readonly Dictionary<int, Tuple<string,int,int,int,int, string, int>> _skillsGuardDataDefault = new Dictionary<int, Tuple<string ,int,int,int,int, string, int>>()
    {
        {1,Tuple.Create("The Steady Sun",0,20,20,0,"Summons a shield that blocks all incoming damage for 2 seconds.",1)},
        {2,Tuple.Create("Wind Of Protection",100,15,20,15,"The winds of the gods will be by your side and protect your next journey.",3)},
        {3,Tuple.Create("The Darkness Resilient",120,20,20,10,"Uses dark power to summon a shield of dark energy that protects you for 2s.",4)},
    };

    public Dictionary<int, Tuple<string, int, int, int, int, string, int>> SkillsGuardData = new Dictionary<int, Tuple<string, int, int, int, int, string,int>>();

    #endregion

    #region Data Skill Sword

    private readonly Dictionary<int, Tuple<string,int,int,int,int, string,int>> _skillsSwordDataDefault = new Dictionary<int, Tuple<string,int,int,int,int, string,int>>()
    {
        {1,Tuple.Create("Summon Holy Light",0,20,10,120,"Use the power of the creator god to create a rain of sword light to purify all enemies.",1)},
        {2,Tuple.Create("Sword God Exposed",100,15,15,150,"A storm of swords that bombards and tears apart everything it comes across",3)},
        {3,Tuple.Create("The wrath Of God",120,20,10,120,"Summon a storm of swords carrying holy light energy",5)},
    };

    public Dictionary<int, Tuple<string, int, int, int, int, string,int>> SkillsSwordData =
        new Dictionary<int, Tuple<string, int, int, int, int, string,int>>();

    #endregion

    #region Data Skill Magic

    
    private readonly Dictionary<int, Tuple<string,int,int,int,int, string,int>> _skillsMagicDataDefault = new Dictionary<int, Tuple<string,int,int,int,int, string,int>>()
    {
        {1,Tuple.Create("Rain Of Arrows",0,20,10,120,"Use the divine bow to summon a rain of arrows carrying the energy of darkness to destroy all enemies.",1)},
        {2,Tuple.Create("Darkness Burst",100,15,15,150,"Dark energy erupted creating a rain of black energy.",2)},
        {3,Tuple.Create("The God Bless",120,20,10,120,"The god of light creates a healing and energy area around the character.",3)},
        {4,Tuple.Create("The Fury Of The Sky",100,15,10,120,"The whole sky was covered with clouds and thunder and lightning struck constantly there.",4)},
        {5,Tuple.Create("Black Death",120,20,15,150,"Death rays continuously rain down on an area causing quick deaths.",5)},
    };
    
    public Dictionary<int, Tuple<string, int, int, int, int, string,int>> SkillsMagicData =
        new Dictionary<int, Tuple<string, int, int, int, int, string,int>>();
    #endregion
    
    private readonly Dictionary<int, Tuple<string, string>> _npcData = new Dictionary<int, Tuple<string, string>>()
    {
        {1, Tuple.Create("Kiriana","Keep enough food and medicine they will help you survive if needed, do you want to buy something")},
        {2, Tuple.Create("Bruto", "I forge anything you can think of, would you like to buy a weapon that suits your hand.") },
        {3, Tuple.Create("Hatarana","Skills are something you have to practice every day and sometimes learn new skills, try some of your skills")},
        {4, Tuple.Create("Hina","Buy and equip yourself with gorgeous clothes, try to pick up something")},
        {5, Tuple.Create("MeiMei","This trip will be dangerous, are you still ready to go forward and fight ?")},
    };

    public Dictionary<int, string> DataScriptTutorial = new Dictionary<int, string>()
    {
        {0,"Hello traveler, I am Chtholly, let me guide you on your journey ahead"},
        {1,"To find your own companions, come see Kiriana."},
        {2,"Go to Bruto's forge to equip yourself with the best weapons."},
        {3,"Hatarana's shop sells some great adventure items."},
        {4,"Get yourself some powerful armor at Hina's shop."},
        {5,"Go to your personal inventory to see what you have."},
        {6,"You are ready to adventure to Mei Mei's place and start your journey"},
    };

    #region Data Guides

        public Dictionary<int, Tuple<string, string, string>> GuidePlayerData = new Dictionary<int, Tuple<string, string, string>>()
    {
        {0, Tuple.Create("Movement", "Movement", "To move your character around the map, press the WASD navigation buttons to navigate your character.") },
        {1, Tuple.Create("Sprint", "Sprint","To enter sprint mode, hold down shift and WASD at the same time and the character will switch to sprint.")},
        {2, Tuple.Create("Roll", "Roll", "Press F to make the character somersault in the direction of the character a small distance to avoid moves or monsters")},
        {3, Tuple.Create("Jump", "Jump","Press Space to make the character jump into the air, jumping over obstacles.")},
        {4, Tuple.Create("Skill", "Skill","The character will have 3 skills corresponding to 3 RTY keys. To use a skill, press that button.")},
    };
    public Dictionary<int, Tuple<string, string, string, string>> GuideEnemyData = new Dictionary<int, Tuple<string, string, string , string>>()
    {
        {0, Tuple.Create("Skeleton", "Skeleton", "An undead warrior with glowing red eyes, rusted armor, and decayed weapons.","Darknest") },
        {1, Tuple.Create("Necromancer", "Necromancer","A dark sorcerer, who dead. Clad in tattered robes, wielding cursed staffs, they summon and cast dark spells.","Darknest")},
        {2, Tuple.Create("LavaElemental", "Lava Elemental", "A towering creature of molten rock and fire. Its body radiates intense heat.","Fire")},
        {3, Tuple.Create("EarthElemental", "Earth Elemental","A massive creature of stone and soil. It crushes foes with powerful.","Plant")},
        {4, Tuple.Create("SkeletonReaper", "Skeleton Reaper","A skeletal figure with tattered wings and a scythe. It glides silently, delivering swift.","Light")},
        {5, Tuple.Create("DragonUrus", "Dragon Urus","The monster guarding the 1st floor, a colossal, fire-breathing dragon with crimson scales.","Fire")},
        {6, Tuple.Create("DragonNightMare", "Dragon NightMare","he monster guarding the 2nd floor, an eerie, emerald dragon that dwells in shadows.","Darknest")},
    };

    #endregion

    #region Data LevelOpen

    public Dictionary<int, Tuple<string, string, int, int[]>> LevelDataDescriptions = new Dictionary<int, Tuple<string, string, int, int[]>>()
    {
        {1, Tuple.Create("Wrath Of The Usu","I forge anything you can think of, would you like to buy a weapon that suits your hand.",1,new []{1,2,3,4}) },
        {2, Tuple.Create("Darkness Rises","Skills are something you have to practice every day and sometimes learn new skills, try some of your skills",2,new []{1,2,4})},
        {3, Tuple.Create("Death Spirit","Keep enough food and medicine they will help you survive if needed, do you want to buy something",3,new []{1,3,4})},
        {4, Tuple.Create("Chaos Of The Dead","This trip will be dangerous, are you still ready to go forward and fight ?",4,new []{3,4})},
        {5, Tuple.Create("Black Death","This trip will be dangerous, are you still ready to go forward and fight ?",5,new []{3,4})},
        {6, Tuple.Create("Lord Of Darkness","This trip will be dangerous, are you still ready to go forward and fight ?",6,new []{3,4})},
    };

    private readonly Dictionary<int, Tuple<int, int>> _levelStateDataDefault = new Dictionary<int, Tuple<int, int>>()
    {
        { 1, Tuple.Create(1, 1) },
        { 2, Tuple.Create(2, 0) },
        { 3, Tuple.Create(3, 0) },
        { 4, Tuple.Create(4, 0) },
        { 5, Tuple.Create(5, 0) },
        { 6, Tuple.Create(6, 0) },
    };

    public Dictionary<int, Tuple<int, int>> LevelStateData = new Dictionary<int, Tuple<int, int>>();

    #endregion

    #region Data Fashion

    //Default Data
    private readonly Dictionary<int,Tuple<int,int>> _hairsDataDefault = new Dictionary<int,Tuple<int,int>>()
    {
        {1, Tuple.Create(0,1) },
        {2, Tuple.Create(100,0) },
        {3, Tuple.Create(100,0) },
        {4, Tuple.Create(100,0) },
    };
    
    private readonly Dictionary<int,Tuple<int,int>> _headsDataDefault = new Dictionary<int,Tuple<int,int>>()
    {
        {1, Tuple.Create(0,1) },
        {2, Tuple.Create(100,0) },
        {3, Tuple.Create(100,0) },
    };

    private readonly Dictionary<int,Tuple<int,int>> _torsosDataDefault = new Dictionary<int,Tuple<int,int>>()
    {
        {1, Tuple.Create(0,1) },
        {2, Tuple.Create(100,0) },
        {3, Tuple.Create(100,0) },
        {4, Tuple.Create(100,0) },
    };

    private readonly Dictionary<int,Tuple<int,int>> _legsDataDefault = new Dictionary<int,Tuple<int,int>>(){
        {1, Tuple.Create(0,1) },
        {2, Tuple.Create(100,0) },
        {3, Tuple.Create(100,0) },
        {4, Tuple.Create(100,0) },
    };
    
    //Player Data
    public Dictionary<int,Tuple<int,int>> LegsData = new Dictionary<int,Tuple<int,int>>();
    
    public Dictionary<int,Tuple<int,int>> TorsosData = new Dictionary<int,Tuple<int,int>>();
   
    public Dictionary<int,Tuple<int,int>> HeadsData = new Dictionary<int,Tuple<int,int>>();
    
    public Dictionary<int,Tuple<int,int>> HairsData = new Dictionary<int,Tuple<int,int>>();

    #endregion
    
    public readonly Dictionary<int, Tuple<int, int>> DataPlayerXp = new Dictionary<int, Tuple<int, int>>()
    {
        { 1, Tuple.Create(1, 100) },
        { 2, Tuple.Create(2, 200) },
        { 3, Tuple.Create(3, 300) },
        { 4, Tuple.Create(4, 400) },
        { 5, Tuple.Create(5, 500) },
        { 6, Tuple.Create(6, 600) },
        { 7, Tuple.Create(7, 700) },
        { 8, Tuple.Create(8, 800) },
        { 9, Tuple.Create(9, 900) },
        { 10, Tuple.Create(10, 1000) },
    };
    
    public static DataManager Instance => _instance;
    private static DataManager _instance;
    
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
        Debug.Log("start scene id : " + PlayerPrefs.GetInt("StartScreen", 0));
        if (!Directory.Exists(Application.persistentDataPath + "/DataDict/"))
            Directory.CreateDirectory(Application.persistentDataPath + "/DataDict/");
        if (PlayerPrefs.GetInt("StartScreen", 0) == 0)
        {
            //Set defaul data weapon
            WeaponsDatas = _weaponsDataDefault;
            SaveDictWeaponToJson();

            //Set defaul data level
            LevelStateData = _levelStateDataDefault;
            SaveDictLevelStateToJson();
            
            //Set defaul data pet
            PetData = _petDataDefault;
            SaveDataPet();

            //Set defaul data potion buy
            DataPotionPlayerBuy = DataPotionPlayerBuyDefault;
            SaveDataPotion();
            SaveUserPotion(Epotion.Potion1,1);
            SaveUserPotion(Epotion.Potion2,2);
            
            //Set defaul data skill
            SkillsGuardData = _skillsGuardDataDefault;
            SkillsMagicData = _skillsMagicDataDefault;
            SkillsSwordData = _skillsSwordDataDefault;
            
            //Set default data fashion
            HairsData = _hairsDataDefault;
            HeadsData = _headsDataDefault;
            LegsData = _legsDataDefault;
            TorsosData = _torsosDataDefault;
            
            SaveAllDataFashion();
            SaveDataPrefPlayer(EDataPlayerEquip.Hair,0);
            SaveDataPrefPlayer(EDataPlayerEquip.Head,0);
            SaveDataPrefPlayer(EDataPlayerEquip.Leg,0);
            SaveDataPrefPlayer(EDataPlayerEquip.Torso,0);
            
            //Free coin to test game features
            var coin = GetDataPrefGame(EDataPrefName.Coin);
            coin = 2000;
            SaveDataPrefGame(EDataPrefName.Coin,coin);
            SaveDataPrefGame(EDataPrefName.LevelOpen,1);

            SaveDataPrefPlayer(EDataPlayerEquip.LevelPlayer,1);
            SaveDataPrefPlayer(EDataPlayerEquip.Xp,0);
        }
        else
        {
            LoadDataPotion();
            LoadDictWeaponFromJson();
            LoadDictLevelStateFromJson();
            LoadDictDataPet();
            
            // test data because data isn't finish setup data load save 
            SkillsGuardData = _skillsGuardDataDefault;
            SkillsMagicData = _skillsMagicDataDefault;
            SkillsSwordData = _skillsSwordDataDefault;
            
            //Data fashion
            LoadAllDataFashion();
            
            LoadDataPotion();
        }
    }

    #region Save Load Weapon

    public void SaveDataWeapon()
    {
        SaveDictWeaponToJson();
    }
    
    public void LoadDataWeapon()
    {
        LoadDictWeaponFromJson();
    }
    private void SaveDictWeaponToJson()
    {
        var json = JsonConvert.SerializeObject(WeaponsDatas);
        File.WriteAllText(Application.persistentDataPath + "/DataDict/saveDictWeapon.json",json);
    }

    private void LoadDictWeaponFromJson()
    {
        var json = File.ReadAllText(Application.persistentDataPath + "/DataDict/saveDictWeapon.json");
        WeaponsDatas = JsonConvert.DeserializeObject<Dictionary<int, Tuple<string, string, int, int, int, string, int, Tuple<int>>>>(json);
    }

    #endregion

    #region Save Load Pet
    
    public void SaveDataPet()
    {
        SaveDictPetDataToJson();
    }
    
    public void LoadDictDataPet()
    {
        LoadDictPetDataFromJson();
    }
    
    private void SaveDictPetDataToJson()
    {
        var json = JsonConvert.SerializeObject(PetData);
        File.WriteAllText(Application.persistentDataPath + "/DataDict/saveDictPet.json",json);
    }
    
    private void LoadDictPetDataFromJson()
    {
        var json = File.ReadAllText(Application.persistentDataPath + "/DataDict/saveDictPet.json");
        PetData = JsonConvert.DeserializeObject<Dictionary<int, Tuple<string, int, int, int, int, string, int,Tuple<int>>>>(json);
        Debug.Log(Application.persistentDataPath + "/DataDict/saveDictPet.json");
    }
    #endregion

    #region Save Load LevelOpen State

    public void SaveDataLevelState()
    {
        SaveDictLevelStateToJson();
    }
    
    public void LoadDataLevelState()
    {
        LoadDictLevelStateFromJson();
    }
    
    private void SaveDictLevelStateToJson()
    {
        var json = JsonConvert.SerializeObject(LevelStateData);
        File.WriteAllText(Application.persistentDataPath + "/DataDict/saveLevelState.json",json);
    }

    private void LoadDictLevelStateFromJson()
    {
        var json = File.ReadAllText(Application.persistentDataPath + "/DataDict/saveLevelState.json");
        LevelStateData = JsonConvert.DeserializeObject<Dictionary<int, Tuple<int, int>>>(json);
    }

    #endregion

    #region Save Load Fashion Data
    public void SaveDataFashionWithType(EnumManager.EFashionType type, Dictionary<int,Tuple<int,int>> data)
    {
        switch (type)
        {
            case EnumManager.EFashionType.Hair:
                HairsData = data;
                SaveDictHairsDataToJson();
                break;
            case EnumManager.EFashionType.Head:
                HeadsData = data;
                SaveDictHeadsDataToJson();
                break;
            case EnumManager.EFashionType.Torso:
                TorsosData = data;
                SaveDictTorsosDataToJson();
                break;
            case EnumManager.EFashionType.Leg:
                LegsData = data;
                SaveDictLegsDataToJson();
                break;
        }
    }
    
    public void LoadDataFashionWithType(EnumManager.EFashionType type)
    {
        switch (type)
        {
            case EnumManager.EFashionType.Hair:
                LoadDictHairsDataFromJson();
                break;
            case EnumManager.EFashionType.Head:
                LoadDictHeadsDataFromJson();
                break;
            case EnumManager.EFashionType.Torso:
                LoadDictTorsosDataFromJson();
                break;
            case EnumManager.EFashionType.Leg:
                LoadDictLegsDataFromJson();
                break;
        }
    }

    public void SaveAllDataFashion()
    {
        SaveDictHairsDataToJson();
        SaveDictHeadsDataToJson();
        SaveDictTorsosDataToJson();
        SaveDictLegsDataToJson();
    }
    
    public void LoadAllDataFashion()
    {
        LoadDictHairsDataFromJson();
        LoadDictHeadsDataFromJson();
        LoadDictTorsosDataFromJson();
        LoadDictLegsDataFromJson();
    }
    
    //Json Hair LS Data
    private void SaveDictHairsDataToJson()
    {
        var json = JsonConvert.SerializeObject(HairsData);
        File.WriteAllText(Application.persistentDataPath + "/DataDict/saveHairsData.json",json);
    }

    private void LoadDictHairsDataFromJson()
    {
        var json = File.ReadAllText(Application.persistentDataPath + "/DataDict/saveHairsData.json");
        HairsData = JsonConvert.DeserializeObject<Dictionary<int, Tuple<int, int>>>(json);
    }
    
    //Json Head LS Data
    private void SaveDictHeadsDataToJson()
    {
        var json = JsonConvert.SerializeObject(HeadsData);
        File.WriteAllText(Application.persistentDataPath + "/DataDict/saveHeadsData.json",json);
    }

    private void LoadDictHeadsDataFromJson()
    {
        var json = File.ReadAllText(Application.persistentDataPath + "/DataDict/saveHeadsData.json");
        HeadsData = JsonConvert.DeserializeObject<Dictionary<int, Tuple<int, int>>>(json);
    }
    
    //Json Torso LS Data
    private void SaveDictTorsosDataToJson()
    {
        var json = JsonConvert.SerializeObject(TorsosData);
        File.WriteAllText(Application.persistentDataPath + "/DataDict/saveTorsosData.json",json);
    }

    private void LoadDictTorsosDataFromJson()
    {
        var json = File.ReadAllText(Application.persistentDataPath + "/DataDict/saveTorsosData.json");
        TorsosData = JsonConvert.DeserializeObject<Dictionary<int, Tuple<int, int>>>(json);
    }
    
    //Json Leg LS Data
    private void SaveDictLegsDataToJson()
    {
        var json = JsonConvert.SerializeObject(LegsData);
        File.WriteAllText(Application.persistentDataPath + "/DataDict/saveLegsData.json",json);
    }

    private void LoadDictLegsDataFromJson()
    {
        var json = File.ReadAllText(Application.persistentDataPath + "/DataDict/saveLegsData.json");
        LegsData = JsonConvert.DeserializeObject<Dictionary<int, Tuple<int, int>>>(json);
    }
    
    #endregion
    
    #region Save Load Potion Data Buy
    //Json Potion Buy Data
    
    public void SaveDataPotion()
    {
        SaveDictPotionDataToJson();
    }
    
    public void LoadDataPotion()
    {
        LoadDictPotionDataFromJson();
    }
    private void SaveDictPotionDataToJson()
    {
        var json = JsonConvert.SerializeObject(DataPotionPlayerBuy);
        File.WriteAllText(Application.persistentDataPath + "/DataDict/savePotionBuy.json",json);
    }

    private void LoadDictPotionDataFromJson()
    {
        var json = File.ReadAllText(Application.persistentDataPath + "/DataDict/savePotionBuy.json");
        DataPotionPlayerBuy = JsonConvert.DeserializeObject<Dictionary<int, Tuple<int, int>>>(json);
    }
    
    #endregion
    
    public void SaveDataPrefGame(EDataPrefName prefName, int data)
    {
        PlayerPrefs.SetInt(_dataPrefGame[prefName],data);
        PlayerPrefs.Save();
    }
    
    public int GetDataPrefGame(EDataPrefName prefName)
    {
        int val = 0;
        if (PlayerPrefs.HasKey(_dataPrefGame[prefName]))
        {
            val = PlayerPrefs.GetInt(_dataPrefGame[prefName]);
        }
        return val;
    }
    
    public void SaveDataPrefPlayer(EDataPlayerEquip prefEquipName, int data)
    {
        PlayerPrefs.SetInt(_dataPrefPlayer[prefEquipName],data);
        PlayerPrefs.Save();
    }
    
    public int GetDataPrefPlayer(EDataPlayerEquip prefEquipName)
    {
        int val = 0;
        if (PlayerPrefs.HasKey(_dataPrefPlayer[prefEquipName]))
        {
            val = PlayerPrefs.GetInt(_dataPrefPlayer[prefEquipName]);
        }
        return val;
    }

    public Dictionary<int,Tuple<int,int>> GetDictDataFashionWithType(EnumManager.EFashionType type)
    {
        Dictionary<int,Tuple<int,int>> _result = null;
        switch (type)
        {
            case EnumManager.EFashionType.Hair:
                _result = HairsData;
                break;
            case EnumManager.EFashionType.Head:
                _result = HeadsData;
                break;
            case EnumManager.EFashionType.Torso:
                _result = TorsosData;
                break;
            case EnumManager.EFashionType.Leg:
                _result = LegsData;
                break;
        }

        return _result;
    }
    
    public void SaveUserPotion(Epotion prefName, int data)
    {
        PlayerPrefs.SetInt(_dataPotions[prefName],data);
        PlayerPrefs.Save();
    }
    
    public int GetUserPotion(Epotion prefName)
    {
        int val;
        if (PlayerPrefs.HasKey(_dataPotions[prefName]))
        {
            val = PlayerPrefs.GetInt(_dataPotions[prefName]);
        }
        else
        {
            val = 0;
        }
        return val;
    }
    
    public Tuple<string, string, int, int, int , int, string, Tuple<Tuple<int, int>>> GetPotionDataByID(int id, Epotion eSkills)
    {
        switch (eSkills)
        {
            case Epotion.Potion1:
                foreach (var skillData in PotionsDataDefault)
                {
                    if (skillData.Key == id)
                    {
                        return skillData.Value;
                    }
                }
                break;
            case Epotion.Potion2:
                foreach (var skillData in PotionsDataDefault)
                {
                    if (skillData.Key == id)
                    {
                        return skillData.Value;
                    }
                }
                break;
        }
        return null;
    }
    
    public void SaveUserSkill(ESkills prefName, int data)
    {
        PlayerPrefs.SetInt(_dataSkills[prefName],data);
        PlayerPrefs.Save();
    }
    
    public int GetUserSkill(ESkills prefName)
    {
        int val;
        if (PlayerPrefs.HasKey(_dataSkills[prefName]))
        {
            val = PlayerPrefs.GetInt(_dataSkills[prefName]);
        }
        else
        {
            val = 1;
        }
        return val;
    }
    
    public Tuple<string,int,int,int,int, string,int> GetSkillDataByID(int id, ESkills eSkills)
    {
        switch (eSkills)
        {
            case ESkills.Guard:
                foreach (var skillData in _skillsGuardDataDefault)
                {
                    if (skillData.Key == id)
                    {
                        return skillData.Value;
                    }
                }
                break;
            case ESkills.Sword:
                foreach (var skillData in _skillsSwordDataDefault)
                {
                    if (skillData.Key == id)
                    {
                        return skillData.Value;
                    }
                }
                break;
            case ESkills.Magic:
                foreach (var skillData in _skillsMagicDataDefault)
                {
                    if (skillData.Key == id)
                    {
                        return skillData.Value;
                    }
                }
                break;
        }
        return null;
    }

    public Tuple<string, string> GetNpcDataByID(int id)
    {
        foreach (var _npcData in _npcData)
        {
            if (_npcData.Key == id)
            {
                return _npcData.Value;
            }
        }
        return null;
    }
    
    public Tuple<string, string, int, int, int, string, int, Tuple<int>> GetWeaponByID(int id)
    {
        foreach (var _weapon in _weaponsDataDefault)
        {
            if (_weapon.Key == id)
            {
                return _weapon.Value;
            }
        }
        return null;
    }
}