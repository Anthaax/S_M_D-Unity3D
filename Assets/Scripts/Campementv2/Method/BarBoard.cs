using UnityEngine;
using System.Collections;
using S_M_D.Camp.Class;
using UnityEngine.UI;

public class BarBoard : MonoBehaviour {

    // Update is called once per frame
    void Update()
    {

    }

    public void ValidHeroes()
    {
        if (SetProfil.coupleHerosBar[0] != null && SetProfil.coupleHerosBar[1] != null)
        {
            Bar bar = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Bar) as Bar;
            bar.SetHeros(SetProfil.coupleHerosBar[0], SetProfil.coupleHerosBar[1]);
            Start.MenuBGBar.SetActive(false);
        }
        //
    }

    public void RemoveHeros()
    {
        GameObject IconeHero1 = GameObject.Find("BarHero1");
        GameObject IconeHero2 = GameObject.Find("BarHero2");
        if (gameObject.name == "RemoveHero1Bar")
        {
            SetProfil.coupleHerosBar[0] = null;
            IconeHero1.GetComponent<Image>().sprite = null;
        }
        else if (gameObject.name == "RemoveHero2Bar")
        {
            SetProfil.coupleHerosBar[1] = null;
            IconeHero2.GetComponent<Image>().sprite = null;
        }
    }
}
