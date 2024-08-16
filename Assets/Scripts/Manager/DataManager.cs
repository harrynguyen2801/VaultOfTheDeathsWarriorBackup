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
        PlayerSex,
        StartScreen,
        Level,
        WeaponId,
        Coin,
        Guard,
        Sword,
        Magic
    }
    
    public enum EEnemyType
    {
        Skeleton,
        MageSkeleton,
        DragonNight,
        DragonUsu,
    }
    
    public enum ESkills : int
    {
        Guard,
        Sword,
        Magic,
    }

    public Dictionary<EEnemyType, int> DataHealthEnemy = new Dictionary<EEnemyType, int>()
    {
        { EEnemyType.Skeleton ,120},
        { EEnemyType.MageSkeleton ,100},
        { EEnemyType.DragonNight ,1200},
        { EEnemyType.DragonUsu ,900},

    };

    private Dictionary<EDataPrefName, string> _dataType = new Dictionary<EDataPrefName, string>()
    {
        {EDataPrefName.PlayerSex,"PlayerSex"},
        {EDataPrefName.StartScreen,"StartScreen"},
        {EDataPrefName.Level,"Level"},
        {EDataPrefName.WeaponId,"WeaponId"},
        {EDataPrefName.Coin,"Coin"},
        {EDataPrefName.FirstGame,"FirstGame"},
    };
    
    private Dictionary<ESkills, string> _dataSkills = new Dictionary<ESkills, string>()
    {
        {ESkills.Guard,"Guard"},
        {ESkills.Sword,"Sword"},
        {ESkills.Magic,"Magic"},
    };
    
    
    private Dictionary<int, Tuple<string, string, int, int, int, string, int,Tuple<int>>> _weaponsDataDefault = new Dictionary<int, Tuple<string, string, int, int, int, string, int, Tuple<int>>>()
    {
        {1,Tuple.Create("Sacrifial","Sword",30,100,100,"The sword of a knight that symbolizes the restored honor of Dvalin. The blessings of the Anemo Archon rest on the fuller of the blade",100,1)},
        {2,Tuple.Create("Bloodtainted","Polearm",25,110,100,"A greatsword as light as the sigh of grass in the breeze, yet as merciless to the corrupt as a typhoon.",100,1)}, 
        {3,Tuple.Create("Harbinger","Polearm",30,130,100,"A symbol of a legendary pact, this sharp blade once cut off the peak of a mountain.",100,1)}, 
        {4,Tuple.Create("Deathmatch","Claymore",45,150,100,"A weapon once used by a young maiden who forsook her family name, stained with the blood of enemies and loved ones both.",100,1)},
        {5,Tuple.Create("Aquila Favonia","Sword",55,150,100,"The soul of the Knights of Favonius. Millennia later, it still calls on the winds of swift justice to vanquish all evil — just like the last heroine who wielded it.",100,1)},
        {6,Tuple.Create("Calamity Queller","Sword",45,150,100,"A keenly honed weapon forged from some strange crystal. Its faint blue light seems to whisper of countless matters now past.",100,0)},
        {7,Tuple.Create("Black Tassel","Sword",30,165,100,"A naginata used to cut grass. Any army that stands before this weapon will probably be likewise cut down.",100,0)},
        {8,Tuple.Create("Skyward Blade","Sword",45,150,100,"The sword of a knight that symbolizes the restored honor of Dvalin The blessings of the Anemo Archon rest on the fuller of the blade.",400,0)},
        {9,Tuple.Create("Staff of Homa","Sword",55,100,100,"A firewood staff that was once used in ancient and long-lost rituals.",400,0)},
        {10,Tuple.Create("Akuoumaru","Sword",35,160,100,"The beloved sword of the legendary Akuou. The blade is huge and majestic, but is surprisingly easy to wield.",500,0)},
        {11,Tuple.Create("Blackcliff Pole","Sword",65,150,100,"A weapon made of blackstone and aerosiderite. There is a dark crimson glow on its cold black sheen.",500,0)},
        {12,Tuple.Create("Festering Desire","Sword",55,150,100,"A creepy straight sword that almost seems to yearn for life. It drips with a shriveling venom that could even corrupt a mighty dragon.",600,0)},
        {13,Tuple.Create("Hamayumi","Claymore",75,150,100,"A certain shrine maiden once owned this warbow. It was made with surpassing skill, and is both intricate and sturdy.",700,0)},
        {14,Tuple.Create("Ibis Piercer","Claymore",65,160,100,"A golden bow forged from the description in the story. If you use it as a normal weapon,",800,0)},
        {15,Tuple.Create("Sacrificial Jade","Claymore",55,170,100,"An ancient jade pendant that gleams like clear water. It seems to have been used in ancient ceremonies.",950,0)},
        {16,Tuple.Create("Tidal Shadow","Claymore",55,190,100,"An exquisitely-crafted. standard-model sword forged for the high-ranking officers and flagship captains of Fontaine's old navy.",1050,0)},
    };

    public Dictionary<int, Tuple<string,int,int,int, string>> skillsGuardDataDefault = new Dictionary<int, Tuple<string, int,int,int, string>>()
    {
        {1,Tuple.Create("Summon Holy Light",0,20,10,"Summons a shield that blocks all incoming damage for 2 seconds.")},
        {2,Tuple.Create("Rain of arrows",100,15,15,"Use the divine bow to summon a rain of arrows carrying the energy of darkness to destroy all enemies.")},
        {3,Tuple.Create("The wrath of god",120,20,10,"Summon a storm of swords carrying holy light energy")},
    };
    
    public Dictionary<int, Tuple<string,int,int,int, string>> skillsSwordDataDefault = new Dictionary<int, Tuple<string, int,int,int, string>>()
    {
        {1,Tuple.Create("Summon Holy Light",0,20,10,"Summons a shield that blocks all incoming damage for 2 seconds.")},
        {2,Tuple.Create("Rain of arrows",100,15,15,"Use the divine bow to summon a rain of arrows carrying the energy of darkness to destroy all enemies.")},
        {3,Tuple.Create("The wrath of god",120,20,10,"Summon a storm of swords carrying holy light energy")},
    };
    public Dictionary<int, Tuple<string,int,int,int, string>> skillsMagicDataDefault = new Dictionary<int, Tuple<string, int,int,int, string>>()
    {
        {1,Tuple.Create("Summon Holy Light",0,20,10,"Summons a shield that blocks all incoming damage for 2 seconds.")},
        {2,Tuple.Create("Rain of arrows",100,15,15,"Use the divine bow to summon a rain of arrows carrying the energy of darkness to destroy all enemies.")},
        {3,Tuple.Create("The wrath of god",120,20,10,"Summon a storm of swords carrying holy light energy")},
        {4,Tuple.Create("Deadly Sins",100,15,10,"Take the energy of chaos as master, summon a bombardment of deadly energy spheres")},
        {5,Tuple.Create("The fury of the sky",120,20,15,"The whole sky was covered with clouds and thunder and lightning struck constantly there.")},
    };
    
    public Dictionary<int, Tuple<string, string>> NpcData = new Dictionary<int, Tuple<string, string>>()
    {
        {1, Tuple.Create("Bruto", "I forge anything you can think of, would you like to buy a weapon that suits your hand.") },
        {2, Tuple.Create("Hatarana","Skills are something you have to practice every day and sometimes learn new skills, try some of your skills")},
        {3, Tuple.Create("Kiriana","Keep enough food and medicine they will help you survive if needed, do you want to buy something")}
    };

    public Dictionary<int, Tuple<string, string, int, int, int, string, int ,Tuple<int>>> weaponsData =
        new Dictionary<int, Tuple<string, string, int, int, int, string, int ,Tuple<int>>>() { };


    public static DataManager Instance;
    private void Awake()
    {
        Instance = this;
        if (GetDataInt(EDataPrefName.StartScreen) == 0)
        {
            weaponsData = _weaponsDataDefault;
            SaveDictWeaponToJson();
        }
        else
        {
            LoadDictWeaponFromJson();
        }
    }

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
        var json = JsonConvert.SerializeObject(weaponsData);
        File.WriteAllText(Application.dataPath + "/saveDictWeapon.json",json);
    }

    private void LoadDictWeaponFromJson()
    {
        var json = File.ReadAllText(Application.dataPath + "/saveDictWeapon.json");
        weaponsData = JsonConvert.DeserializeObject<Dictionary<int, Tuple<string, string, int, int, int, string, int, Tuple<int>>>>(json);
    }
    
    public void SaveData(EDataPrefName prefName, string data)
    {
        PlayerPrefs.SetString(_dataType[prefName],data);
        PlayerPrefs.Save();
    }
    
    public void SaveData(EDataPrefName prefName, int data)
    {
        PlayerPrefs.SetInt(_dataType[prefName],data);
        PlayerPrefs.Save();
    }
    
    public void SaveData(EDataPrefName prefName, float data)
    {
        PlayerPrefs.SetFloat(_dataType[prefName],data);
        PlayerPrefs.Save();
    }
    
    public int GetDataInt(EDataPrefName prefName)
    {
        int val = 0;
        if (PlayerPrefs.HasKey(_dataType[prefName]))
        {
            val = PlayerPrefs.GetInt(_dataType[prefName]);
            // Debug.Log(prefName + " is " +  val);
        }
        return val;
    }
    
    public void SaveUserSkill(ESkills prefName, int data)
    {
        PlayerPrefs.SetInt(_dataSkills[prefName],data);
        PlayerPrefs.Save();
    }
    
    public int GetUserSkill(ESkills prefName)
    {
        int val = 0;
        if (PlayerPrefs.HasKey(_dataSkills[prefName]))
        {
            val = PlayerPrefs.GetInt(_dataSkills[prefName]);
        }
        return val;
    }
    
    public Tuple<string,int,int,int, string> GetSkillDataByID(int id, ESkills eSkills)
    {
        switch (eSkills)
        {
            case ESkills.Guard:
                foreach (var skillData in skillsGuardDataDefault)
                {
                    if (skillData.Key == id)
                    {
                        return skillData.Value;
                    }
                }
                break;
            case ESkills.Sword:
                foreach (var skillData in skillsSwordDataDefault)
                {
                    if (skillData.Key == id)
                    {
                        return skillData.Value;
                    }
                }
                break;
            case ESkills.Magic:
                foreach (var skillData in skillsMagicDataDefault)
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
        foreach (var _npcData in NpcData)
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
