using UnityEngine;
using System.Collections;
using S_M_D.Camp.Class;
using UnityEngine.UI;
using S_M_D.Character;
using System;

public class BarBoard : MonoBehaviour {

    // Update is called once per frame
    void Update()
    {

    }
    public static bool HeroesValid;

    public static void Init()
    {
        SetProfil.InitBoardBar();

    }

    public void ValidHeroes()
    {
        Bar bar = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Bar) as Bar;
        if (SetProfil.coupleHerosBar[0] != null && SetProfil.coupleHerosBar[1] != null)
        {
            bar.SetHeros(SetProfil.coupleHerosBar[0], SetProfil.coupleHerosBar[1]);
            HeroesValid = true;
            GameObject.Find("RemoveHero1Bar").SetActive(false);
            GameObject.Find("RemoveHero2Bar").SetActive(false);
            GameObject.Find("Valid").SetActive(false);
            Start.MenuBGBar.SetActive(false);
            Array.Clear(SetProfil.coupleHerosBar, 0, SetProfil.coupleHerosBar.Length);
        }
        //
    }

    public void RemoveHeros()
    {
        GameObject IconeHero1 = GameObject.Find("BarHero1");
        GameObject IconeHero2 = GameObject.Find("BarHero2");
        if (gameObject.name == "RemoveHero1Bar")
        {
            if (SetProfil.coupleHerosBar[0] != null)
            {
                BaseHeros h = SetProfil.coupleHerosBar[0];
                SetProfil.SetToActiveButton(Start.pHeroes.Find(t => t.GetComponentInChildren<Text>().text == h.CharacterName).GetComponentInChildren<Button>());
                SetProfil.coupleHerosBar[0] = null;
                IconeHero1.GetComponent<Image>().sprite = null;
            }
            
        }
        else if (gameObject.name == "RemoveHero2Bar")
        {
            if (SetProfil.coupleHerosBar[1] != null)
            {
                BaseHeros h = SetProfil.coupleHerosBar[1];
                SetProfil.SetToActiveButton(Start.pHeroes.Find(t => t.GetComponentInChildren<Text>().text == h.CharacterName).GetComponentInChildren<Button>());
                SetProfil.coupleHerosBar[1] = null;
                IconeHero2.GetComponent<Image>().sprite = null;
            }
            
        }
    }
}
