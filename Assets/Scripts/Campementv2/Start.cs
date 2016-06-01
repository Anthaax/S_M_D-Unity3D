using UnityEngine;
using System.Collections;
using S_M_D.Character;
using S_M_D.Camp;
using S_M_D;
using System.Collections.Generic;
using UnityEngine.UI;

public class Start : MonoBehaviour {

    private static GameContext _gtx;

    public static GameObject MenuBGArmory;
    public static GameObject MenuBGTownhall;
    public static GameObject MenuBGBar;
    public static GameObject MenuBGCaravan;
    public static GameObject MenuBGCasern;
    public static GameObject MenuBGCemetery;
    public static GameObject MenuBGHospital;
    public static GameObject MenuBGMentalhospital;
    public static GameObject MenuBGHotel;
    public static GameObject MenuProfil;

    public static Button[] ButtonsSicknesses;
    public static Button[] ButtonsMentalPsycho;
    // Use this for initialization
    void Awake () {


        MenuBGArmory = GameObject.Find("MenuBGArmory");
        MenuBGTownhall = GameObject.Find("MenuBGTownhall");
        MenuBGBar = GameObject.Find("MenuBGBar");
        MenuBGCaravan = GameObject.Find("MenuBGCaravan");
        MenuBGCasern = GameObject.Find("MenuBGCasern");
        MenuBGCemetery = GameObject.Find("MenuBGCemetery");
        MenuBGHospital = GameObject.Find("MenuBGHospital");
        MenuBGMentalhospital = GameObject.Find("MenuBGMentalhospital");
        MenuBGHotel = GameObject.Find("MenuBGHotel");
        MenuProfil = GameObject.Find("Profil");

        MenuBGArmory.SetActive(false);
        MenuBGTownhall.SetActive(false);
        MenuBGBar.SetActive(false);
        MenuBGCaravan.SetActive(false);
        MenuBGCasern.SetActive(false);
        MenuBGCemetery.SetActive(false);
        MenuBGHospital.SetActive(false);
        MenuBGMentalhospital.SetActive(false);
        MenuBGHotel.SetActive(false);
        MenuProfil.SetActive(false);

        ButtonsSicknesses = MenuBGHospital.GetComponentsInChildren<Button>();
        HospitalBoard.InitializedButtonsHospitalBoard();

        ButtonsMentalPsycho = MenuBGMentalhospital.GetComponentsInChildren<Button>();
        MentalHospitalBoard.InitializedButtonsMentalHospitalBoard();
    }
	
    
	// Update is called once per frame
	void Update () {
        int x = 1;
        foreach (BaseHeros heros in _gtx.PlayerInfo.MyHeros)
        {
            GameObject.Find("Hero" + x + "T").GetComponent<Text>().text = heros.CharacterName;
            // GameObject.Find("Hero" + x + "I").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeF");
            if (heros.IsMale == true) GameObject.Find("Hero" + x + "I").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeF");
            else GameObject.Find("Hero" + x + "I").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeM");
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
