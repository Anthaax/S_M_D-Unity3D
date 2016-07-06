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

    public static GameObject[] monstersGO;

    public GameObject CharacterH;
    public GameObject CharacterM;
    public BaseHeros Htest;
    public HerosIni Hini;
    public BaseMonster[] monstersInFight;

    public GameObject[] herosGo;
    public BaseHeros[] herosInBattle;
    // Use this for initialization
    void Awake()
    {
        Debug.Log(Heros);
        Debug.Log(Heros[0]);
        Debug.Log(Heros[1]);
        Debug.Log(Heros[2]);
        Debug.Log(Heros[3]);
        Gtx.DungeonManager.CreateDungeon(Heros, Map);
        Gtx.DungeonManager.LaunchCombat();
        Combat = Gtx.DungeonManager.CbtManager;

        herosGo = new GameObject[4];
        herosInBattle = new BaseHeros[4];
        int x = -2;
        int i = 0;
        int yolo = 0;
        Htest = Combat.Heros[0];
        foreach(BaseHeros H in Combat.Heros)
        {
            
            GameObject data = Instantiate(Resources.Load<GameObject>("Prefabs/" + H.CharacterClassName), new Vector3(x, 0, 0), Quaternion.identity) as GameObject;
            data.name = Combat.Heros[yolo].CharacterName;
            herosGo[i] = data;
            herosInBattle[i++] = H;
            HerosIni heroIni = data.GetComponent<HerosIni>();
            heroIni.init(H);
            x -= 2;
            //Htest = H;
            Hini = heroIni;
            yolo++;
        }
        
        int y = 0;
        int idx = 0;
        monstersGO = new GameObject[4];
        monstersInFight = new BaseMonster[4];
        foreach(BaseMonster M in Combat.Monsters)
        {
            GameObject monsterGO = Instantiate(CharacterM, new Vector3(y, 0, 0), Quaternion.identity) as GameObject;
            GameObject data = Instantiate(Resources.Load<GameObject>("Prefabs/HpMonsterBar"), new Vector3(y*36, 40, 0), Quaternion.identity) as GameObject;
            data.GetComponent<HpBarCheck>().monster = M;
            data.transform.SetParent(GameObject.Find("SuperCanvas").transform, false);
            monstersGO[idx] = monsterGO;
            monstersInFight[idx] = M;
            y += 2;
            idx++;
        }

        Hini.ShowSpellAndSpellInfo(Htest);
        HerosIni.SetMenu(Htest);
    }

    public void ChangeSpells(BaseHeros hero)
    {
        foreach (Transform t in GameObject.Find("HeroSpells").transform)
        {
            Destroy(t.gameObject);
        }
        Hini.ShowSpellAndSpellInfo(hero);
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
