using UnityEngine;
using System.Collections;
using System.Linq;
using S_M_D.Character;
using S_M_D.Combat;
using S_M_D;
using System.Collections.Generic;
using UnityEngine.UI;
using S_M_D.Dungeon;

public class StartCombat : MonoBehaviour {

    private static CombatManager _combat;
    private static GameContext gtx;
    private static HeroAttacks attack;
    private static BaseHeros[] heros;
    private static Map map;
    public GameObject CharacterH;
    public GameObject CharacterM;
    // Use this for initialization
    void Awake()
    {
        Gtx = GameContext.CreateNewGame();
        Gtx.DungeonManager.InitializedCatalogue();
        Map = Gtx.DungeonManager.MapCatalogue.First();
        Heros = Gtx.PlayerInfo.MyHeros.ToArray();
        Gtx.DungeonManager.CreateDungeon(Heros, Map);
        Gtx.DungeonManager.LaunchCombat();
        Combat = Gtx.DungeonManager.CbtManager;
        Combat.Monsters.ToList().ForEach(c =>
        {
            c.HP = 200;
            c.HPmax = 200;
        });

        int x = -1;
        foreach(BaseHeros H in Combat.Heros)
        {
            GameObject data = Instantiate(CharacterH, new Vector3(x, 0, 0), Quaternion.identity) as GameObject;
            HerosIni yo = data.GetComponent<HerosIni>();
            yo.init(H);
            x -= 2;
        }

        int y = 0;
        foreach(BaseMonster M in Combat.Monsters)
        {
            Instantiate(CharacterM, new Vector3(y, 0, 0), Quaternion.identity);
            y += 2;
        }
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        
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

    public static HeroAttacks Attack
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
