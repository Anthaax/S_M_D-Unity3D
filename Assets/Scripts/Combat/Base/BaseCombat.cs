using UnityEngine;
using System.Collections;
using System.Linq;
using S_M_D.Character;
using S_M_D.Combat;
using S_M_D;
using System.Collections.Generic;

public class BaseCombat : MonoBehaviour {

    private static CombatManager _combat;
    private static GameContext gtx;
    private static BaseHeros herosPLaying;
    private static int _heroPosition;

    void Awake()
    {
        Gtx = GameContext.CreateNewGame();
        BaseHeros[] list = Gtx.PlayerInfo.MyHeros.ToArray();

        GameObject.Find("Arrow1").GetComponent<Renderer>().enabled = false;
        GameObject.Find("Arrow2").GetComponent<Renderer>().enabled = false;
        GameObject.Find("Arrow3").GetComponent<Renderer>().enabled = false;
        GameObject.Find("Arrow4").GetComponent<Renderer>().enabled = false;


        Combat = new CombatManager(list, Gtx);
        HerosPLaying = Combat.Heros[2];
        _heroPosition = 2;
        
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

    public static BaseHeros HerosPLaying
    {
        get
        {
            return herosPLaying;
        }

        set
        {
            herosPLaying = value;
        }
    }

    public static int HeroPosition
    {
        get
        {
            return _heroPosition;
        }

        set
        {
            _heroPosition = value;
        }
    }
}
