using UnityEngine;
using System.Collections;
using S_M_D.Character;
using S_M_D.Camp;
using S_M_D;
using System.Collections.Generic;
using UnityEngine.UI;

public class Start : MonoBehaviour {

    private static GameContext _gtx;
    private static List<BaseHeros> _characterList;
    private static List<BaseBuilding> _buildingList;


    // Use this for initialization
    void Awake () {

         Gtx = GameContext.CreateNewGame();
        CharacterList = Gtx.PlayerInfo.MyHeros;
        BuildingList = Gtx.PlayerInfo.MyBuildings;

	
	}
	
	// Update is called once per frame
	void Update () {
        int x = 1;
        foreach (BaseHeros heros in CharacterList)
        {
            GameObject.Find("Hero" + x + "T").GetComponent<Text>().text = heros.CharacterName;
            GameObject.Find("Hero" + x + "I").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeF");
            x++;

        }
	}

    public static GameContext Gtx
    {
        get
        {
            return _gtx;
        }

        set
        {
            _gtx = value;
        }
    }

    public static List<BaseBuilding> BuildingList
    {
        get
        {
            return _buildingList;
        }

        set
        {
            _buildingList = value;
        }
    }

    public static List<BaseHeros> CharacterList
    {
        get
        {
            return _characterList;
        }

        set
        {
            _characterList = value;
        }
    }
}
