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
        Attack.Target = -1;
        SpellsAndStats.UpdateSpell();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        BaseHeros Player = BaseCombat.Combat.GetCharacterTurn() as BaseHeros;
        Vector3 placement = new Vector3(0, 0, 0);
        if (Player!= null)
        {

            int i = 1;
            int j = 1;

            

            foreach (BaseHeros H in heros)
            {

                if (Player == H)
                {
                    Animator animator = GameObject.Find("Heros" + i).GetComponent<Animator>();
                    placement = GameObject.Find("Heros" + i).GetComponent<Transform>().position;
                    float X;
                    if (Player.HP > 0)
                    {
                        X = (92.5f * (100f * Player.HP / Player.EffectivHPMax)) / 100;
                        GameObject.Find("HpBar2").GetComponent<RectTransform>().sizeDelta =
                                    new Vector2(X, 11.2f);
                    }
                    else
                    {
                        GameObject.Find("HP").GetComponent<Text>().text = "Dead";
                        animator.SetBool("IsDead", true);

                    }

                    if (Player.Mana > 0)
                    {
                        X = (92.5f * (100f * Player.Mana / Player.EffectivManaMax)) / 100;
                        GameObject.Find("ManaBar2").GetComponent<RectTransform>().sizeDelta =
                                    new Vector2(X, 11.2f);
                    }
                    else
                    {
                        GameObject.Find("ManaBar2").GetComponent<Image>().color = Color.blue;
                    }
                }

                i++;
            }
            Vector3 position = new Vector3(placement.x-0.8f,placement.y+1,2);
            GameObject.Find("ActualPlayer").GetComponent<Transform>().position = position;

        }
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
