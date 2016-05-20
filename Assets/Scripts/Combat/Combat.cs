using UnityEngine;
using System.Collections;
using S_M_D.Character;
using S_M_D.Combat;
using S_M_D;
using System.Collections.Generic;

public class Combat : MonoBehaviour {
    private static CombatManager _combat;
    public  GameContext _gtx;
    private static BaseHeros _heros;


    void Awake()
    {
        _gtx = new GameContext();
        BaseHeros[] list = new BaseHeros[4];
        list[0] = _gtx.HeroManager.Find(HerosEnum.Paladin.ToString()).CreateHero();
        list[1] = _gtx.HeroManager.Find(HerosEnum.Warrior.ToString()).CreateHero();
        list[2] = _gtx.HeroManager.Find(HerosEnum.Priest.ToString()).CreateHero();
        list[3] = _gtx.HeroManager.Find(HerosEnum.Mage.ToString()).CreateHero();
        Comba= new CombatManager(list, _gtx);
    }
    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public static CombatManager Comba
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

    public static BaseHeros Heros
    {
        get
        {
            return _heros;
        }

        set
        {
            _heros = value;
        }
    }

}
