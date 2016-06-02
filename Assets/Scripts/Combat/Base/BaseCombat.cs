using UnityEngine;
using System.Collections;
using System.Linq;
using S_M_D.Character;
using S_M_D.Combat;
using S_M_D;
using System.Collections.Generic;
using UnityEngine.UI;

public class BaseCombat : MonoBehaviour {

    private static CombatManager _combat;
    private static GameContext gtx;
    private static HeroAttack attack;


    void Awake()
    {
        Gtx = GameContext.CreateNewGame();
        BaseHeros[] list = Gtx.PlayerInfo.MyHeros.ToArray();
        attack = new HeroAttack();

        GameObject.Find("Arrow1").GetComponent<Image>().enabled = false;
        GameObject.Find("Arrow2").GetComponent<Image>().enabled = false;
        GameObject.Find("Arrow3").GetComponent<Image>().enabled = false;
        GameObject.Find("Arrow4").GetComponent<Image>().enabled = false;

        Gtx.DungeonManager.InitializedCatalogue();
        Gtx.DungeonManager.CreateDungeon(list, Gtx.DungeonManager.MapCatalogue.First());
        Gtx.DungeonManager.LaunchCombat();
        Combat = Gtx.DungeonManager.CbtManager;

        
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
}
