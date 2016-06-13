using UnityEngine;
using System.Collections;
using System.Linq;
using S_M_D.Character;
using S_M_D.Combat;
using S_M_D;
using System.Collections.Generic;
using UnityEngine.UI;
using S_M_D.Dungeon;

public class BaseCombat : MonoBehaviour {

    private static CombatManager _combat;
    private static GameContext gtx;
    private static HeroAttack attack;
    private static BaseHeros[] heros;
    private static Map map;


    void Awake()
    {
        attack = new HeroAttack();

        GameObject.Find("Arrow1").GetComponent<Image>().enabled = false;
        GameObject.Find("Arrow2").GetComponent<Image>().enabled = false;
        GameObject.Find("Arrow3").GetComponent<Image>().enabled = false;
        GameObject.Find("Arrow4").GetComponent<Image>().enabled = false;

        Gtx.DungeonManager.InitializedCatalogue();
        Gtx.DungeonManager.CreateDungeon(Heros, Map);
        Gtx.DungeonManager.LaunchCombat();
        Combat = Gtx.DungeonManager.CbtManager;
        Combat.Monsters.ToList().ForEach( c => 
                                        {
                                            c.HP = 200;
                                            c.HPmax = 200;
                                        });
        
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static CombatManager Combat
    {
        get
        {
            return _combat;
        }

        set
        {
            _combat = value;
        }
    }

    public static GameContext Gtx
    {
        get
        {
            return gtx;
        }

        set
        {
            gtx = value;
        }
    }

    public static HeroAttack Attack
    {
        get
        {
            return attack;
        }

        set
        {
            attack = value;
        }
    }

    public static BaseHeros[] Heros
    {
        get
        {
            return heros;
        }

        set
        {
            heros = value;
        }
    }

    public static Map Map
    {
        get
        {
            return map;
        }

        set
        {
            map = value;
        }
    }
}
