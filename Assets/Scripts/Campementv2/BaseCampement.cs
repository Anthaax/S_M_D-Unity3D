using UnityEngine;
using System.Collections;
using S_M_D.Character;
using S_M_D.Combat;
using S_M_D;
using System.Collections.Generic;
using UnityEngine.UI;
using S_M_D.Camp;

public class BaseCampement : MonoBehaviour {

    private static List<BaseHeros> listeOfHeros;
    private static GameContext gtx;
    private static BaseHeros activHeros;
    private static BaseBuilding activBuilding;
    private static List<BaseBuilding> listeOfBuildings;

    void Awake()
    {
        Gtx = new GameContext();
        listeOfHeros = Gtx.PlayerInfo.MyHeros;
        listeOfBuildings = Gtx.PlayerInfo.MyBuildings;

    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        int y = 1;
        foreach (BaseHeros heros in listeOfHeros)
        {
            GameObject.Find("Hero" + y + "T").GetComponent<Text>().text = heros.CharacterName;
            GameObject.Find("Hero"+y+"I").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeF");
            y++;
        }
	
	}

    public static List<BaseHeros> ListeOfHeros
    {
        get
        {
            return listeOfHeros;
        }

        set
        {
            listeOfHeros = value;
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

    public static BaseHeros ActivHeros
    {
        get
        {
            return activHeros;
        }

        set
        {
            activHeros = value;
        }
    }

    public static BaseBuilding ActivBuilding
    {
        get
        {
            return activBuilding;
        }

        set
        {
            activBuilding = value;
        }
    }

    public static List<BaseBuilding> ListeOfBuildings
    {
        get
        {
            return listeOfBuildings;
        }

        set
        {
            listeOfBuildings = value;
        }
    }
}
