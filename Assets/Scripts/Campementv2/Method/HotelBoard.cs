using UnityEngine;
using System.Collections;
using S_M_D.Camp.Class;
using UnityEngine.UI;

public class HotelBoard : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ValidHeroes()
    {
        if(SetProfil.coupleHeros[0] != null && SetProfil.coupleHeros[1] != null)
        {
            Hotel hotel = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Hotel) as Hotel;
            hotel.SetHeros(SetProfil.coupleHeros[0], SetProfil.coupleHeros[1]);
            Start.MenuBGHotel.SetActive(false);
        }
        
    }

    public void RemoveHeros()
    {
        GameObject IconeHero1 = GameObject.Find("HotelHero1");
        GameObject IconeHero2 = GameObject.Find("HotelHero2");
        if (gameObject.name == "RemoveHero1")
        {
            SetProfil.coupleHeros[0] = null;
            IconeHero1.GetComponent<Image>().sprite = null;
        }
        else if (gameObject.name == "RemoveHero2")
        {
            SetProfil.coupleHeros[1] = null;
            IconeHero2.GetComponent<Image>().sprite = null;
        }
    }
}
