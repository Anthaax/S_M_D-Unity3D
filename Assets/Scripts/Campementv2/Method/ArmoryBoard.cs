using UnityEngine;
using System.Collections;
using S_M_D.Camp.Class;
using UnityEngine.UI;
using S_M_D.Character;

public class ArmoryBoard : MonoBehaviour {


    public void RemoveHeros()
    {
        Armory armory = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Armory) as Armory;
        if (armory.Hero != null)
        {
            BaseHeros h = armory.Hero;
            SetProfil.SetToActiveButton(Start.pHeroes.Find(t => t.GetComponentInChildren<Text>().text == h.CharacterName).GetComponentInChildren<Button>());
            GameObject IconeHero = GameObject.Find("ArmoryHero");
            armory.DeleteHero();
            IconeHero.GetComponent<Image>().sprite = null;
        }
        
    }

    public void Initialized()
    {

    }
    
}
