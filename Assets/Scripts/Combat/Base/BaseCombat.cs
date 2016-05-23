using UnityEngine;
using System.Collections;
using S_M_D.Character;
using S_M_D.Combat;
using S_M_D;
using System.Collections.Generic;

public class BaseCombat : MonoBehaviour {

    private static CombatManager _combat;
    private static GameContext gtx;
    private static BaseHeros herosPLaying;

    void Awake()
    {
        Gtx = new GameContext();
        BaseHeros[] list = new BaseHeros[4];
        list[0] = Gtx.HeroManager.Find(HerosEnum.Paladin.ToString()).CreateHero();
        list[1] = Gtx.HeroManager.Find(HerosEnum.Warrior.ToString()).CreateHero();
        list[2] = Gtx.HeroManager.Find(HerosEnum.Priest.ToString()).CreateHero();
        list[3] = Gtx.HeroManager.Find(HerosEnum.Mage.ToString()).CreateHero();

        Combat = new CombatManager(list, Gtx);
        HerosPLaying = Combat.Heros[3];
        
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
}
