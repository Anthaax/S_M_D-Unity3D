using UnityEngine;
using System.Collections;
using S_M_D.Camp.Class;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using S_M_D.Character;

public class AdventureBoard : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ValidHeroes()
    {
        if (SetProfil.HerosAdventure[0] != null && SetProfil.HerosAdventure[1] != null
            && SetProfil.HerosAdventure[2] != null && SetProfil.HerosAdventure[3] != null)
        {
            SceneManager.LoadScene("Dungeon");
            Start.PanelBoardMission.SetActive(false);
        }
        //
    }

    public void RemoveHeros()
    {
        GameObject IconeHero1 = GameObject.Find("AdvHero1");
        GameObject IconeHero2 = GameObject.Find("AdvHero2");
        GameObject IconeHero3 = GameObject.Find("AdvHero3");
        GameObject IconeHero4 = GameObject.Find("AdvHero4");

        if (gameObject.name == "RemoveHero1Adv")
        {
            SetProfil.HerosAdventure[0] = null;
            IconeHero1.GetComponent<Image>().sprite = null;
        }
        else if (gameObject.name == "RemoveHero2Adv")
        {
            SetProfil.HerosAdventure[1] = null;
            IconeHero2.GetComponent<Image>().sprite = null;
        }
        else if (gameObject.name == "RemoveHero3Adv")
        {
            SetProfil.HerosAdventure[2] = null;
            IconeHero3.GetComponent<Image>().sprite = null;
        }
        else if (gameObject.name == "RemoveHero4Adv")
        {
            SetProfil.HerosAdventure[3] = null;
            IconeHero4.GetComponent<Image>().sprite = null;
        }
    }

    public static bool ContainsHero(BaseHeros heros)
    {
        foreach(BaseHeros h in SetProfil.HerosAdventure)
        {
            if (h == heros)
                return true;
        }
        return false;
    }
}
