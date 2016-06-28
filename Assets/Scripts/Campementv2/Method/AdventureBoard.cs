using UnityEngine;
using System.Collections;
using S_M_D.Camp.Class;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using S_M_D.Character;
using UnityEngine.Networking;
using S_M_D.Dungeon;
using System.Linq;

public class AdventureBoard : MonoBehaviour {

    public static string HostAddress;
    public static string HostState;
    public static string Online;
    public static int Port;

	// Update is called once per frame
	void Update () {
	
	}

    public void ValidHeroes()
    {
        GameObject LineMode = GameObject.Find("OnOffLine");
        GameObject Host = GameObject.Find("ClientServer");

        string SelectMode = LineMode.GetComponent<Dropdown>().captionText.text;
        string SelectHost = Host.GetComponent<Dropdown>().captionText.text;

        HostState = SelectHost;
        Online = SelectMode;
        Port = 25015;

        if(SelectMode == "Offline")
        {
            if (SetProfil.HerosAdventure[0] != null && SetProfil.HerosAdventure[1] != null
            && SetProfil.HerosAdventure[2] != null && SetProfil.HerosAdventure[3] != null)
            {
                //Initialiser les héros dans le dongeon!!!!!

                SceneManager.LoadScene(2);
                BoardManager.hero = SetProfil.HerosAdventure;
                BoardManager.Gtx = Start.Gtx;
                Start.Gtx.DungeonManager.InitializedCatalogue();
                Map map = Start.Gtx.DungeonManager.MapCatalogue.First();
                BoardManager.Map = map;


                //Start.Gtx.DungeonManager.cr
                Start.PanelBoardMission.SetActive(false);
            }
        }
        else
        {
            if(NumberOfHeroes() == 2)
            {
                HostAddress = GameObject.Find("IpAddress").GetComponent<InputField>().text;
                SceneManager.LoadScene(2);
                BoardManager.hero = SetProfil.HerosAdventure;
                BoardManager.Gtx = Start.Gtx;
            }
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
            Remove(0, IconeHero1);
        }
        else if (gameObject.name == "RemoveHero2Adv")
        {
            Remove(1, IconeHero2);
        }
        else if (gameObject.name == "RemoveHero3Adv")
        {
            Remove(2, IconeHero3);
        }
        else if (gameObject.name == "RemoveHero4Adv")
        {
            Remove(3, IconeHero4);
        }
    }

    private void Remove(int i, GameObject gameObj)
    {
        if(SetProfil.HerosAdventure[i] != null)
        {
            SetProfil.SetToActiveButton(Start.pHeroes.Find(t => t.GetComponentInChildren<Text>().text == SetProfil.HerosAdventure[i].CharacterName).GetComponentInChildren<Button>());
            SetProfil.HerosAdventure[i] = null;
            gameObj.GetComponent<Image>().sprite = null;
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

    private int NumberOfHeroes()
    {
        int nb = 0;
        for(int i = 0; i < SetProfil.HerosAdventure.Length; i++)
        {
            if (SetProfil.HerosAdventure[i] != null)
                nb++;
        }
        return nb;
    }
}
