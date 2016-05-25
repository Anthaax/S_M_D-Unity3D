using UnityEngine;
using System.Collections;
using S_M_D.Character;
using S_M_D.Camp;
using S_M_D;
using System.Collections.Generic;
using UnityEngine.UI;

public class Start : MonoBehaviour {

    private static GameContext _gtx;


    // Use this for initialization
    void Awake () {

         Gtx = GameContext.CreateNewGame();
        GameObject.Find("MenuBGArmory").SetActive(false);
        GameObject.Find("MenuBGTownhall").SetActive(false);
        GameObject.Find("MenuBGBar").SetActive(false);
        GameObject.Find("MenuBGCaravan").SetActive(false);
        GameObject.Find("MenuBGCasern").SetActive(false);
        GameObject.Find("MenuBGCemetery").SetActive(false);
        GameObject.Find("MenuBGHospital").SetActive(false);
        GameObject.Find("MenuBGMentalhospital").SetActive(false);
        GameObject.Find("MenuBGHotel").SetActive(false);
        GameObject.Find("Profil").SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        int x = 1;
        foreach (BaseHeros heros in _gtx.PlayerInfo.MyHeros)
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
}
