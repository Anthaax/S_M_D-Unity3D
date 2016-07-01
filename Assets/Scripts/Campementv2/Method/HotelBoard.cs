using UnityEngine;
using System.Collections;
using S_M_D.Camp.Class;
using UnityEngine.UI;
using S_M_D.Character;
using System;

public class HotelBoard : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
	
	}
    public static bool HeroesValid;

    public static void Init()
    {
        SetProfil.InitBoardHotel();

    }

    public void ValidHeroes()
    {
        if(SetProfil.coupleHerosHotel[0] != null && SetProfil.coupleHerosHotel[1] != null)
        {
            Hotel hotel = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Hotel) as Hotel;
            hotel.SetHeros(SetProfil.coupleHerosHotel[0], SetProfil.coupleHerosHotel[1]);
            HeroesValid = true;
            GameObject.Find("RemoveHero1").SetActive(false);
            GameObject.Find("RemoveHero2").SetActive(false);
            GameObject.Find("Valid").SetActive(false);
            Start.MenuBGHotel.SetActive(false);
            Array.Clear(SetProfil.coupleHerosHotel, 0, SetProfil.coupleHerosHotel.Length);
        }
        
    }

    public void RemoveHeros()
    {
        GameObject IconeHero1 = GameObject.Find("HotelHero1");
        GameObject IconeHero2 = GameObject.Find("HotelHero2");
        if (gameObject.name == "RemoveHero1")
        {
            if(SetProfil.coupleHerosHotel[0] != null)
            {
                BaseHeros h = SetProfil.coupleHerosHotel[0];
                SetProfil.SetToActiveButton(Start.pHeroes.Find(t => t.GetComponentInChildren<Text>().text == h.CharacterName).GetComponentInChildren<Button>());
                SetProfil.coupleHerosHotel[0] = null;
                IconeHero1.GetComponent<Image>().sprite = null;
            }
            
        }
        else if (gameObject.name == "RemoveHero2")
        {
            if(SetProfil.coupleHerosHotel[1] != null)
            {
                BaseHeros h = SetProfil.coupleHerosHotel[1];
                SetProfil.SetToActiveButton(Start.pHeroes.Find(t => t.GetComponentInChildren<Text>().text == h.CharacterName).GetComponentInChildren<Button>());
                SetProfil.coupleHerosHotel[1] = null;
                IconeHero2.GetComponent<Image>().sprite = null;
            }
            
        }
    }
}
