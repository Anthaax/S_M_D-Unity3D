using UnityEngine;
using System.Collections;
using S_M_D.Character;
using S_M_D.Camp.Class;
using UnityEngine.UI;

public class SetHerosCaravan : MonoBehaviour {
    
	public void OnClick () {
        Caravan caravan = Start.Gtx.PlayerInfo.GetBuilding(S_M_D.Camp.Class.BuildingNameEnum.Caravan) as Caravan;
        /*
        int x = 1;
        //Debug.Log("Nb : " + caravan.HerosDispo.Count);
        foreach (BaseHeros heros in caravan.HerosDispo )
        {
            GameObject.Find("HeroDispo" + x).SetActive(true);
            GameObject.Find("HeroDispo" + x + "T").GetComponent<Text>().text = heros.CharacterName;
            GameObject.Find("HeroDispo" + x + "Prix").GetComponent<Text>().text += heros.Price.ToString();
            if (heros.IsMale == true) GameObject.Find("HeroDispo" + x + "I").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeM");
            else GameObject.Find("HeroDispo" + x + "I").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeF");
            x++;

        }
        */
        int x = 1;
        for(x = 1; x < (caravan.HerosDispo.Count)+1; x++)
        {
                GameObject.Find("HeroDispo" + x).SetActive(true);
                GameObject.Find("HeroDispo" + x + "T").GetComponent<Text>().text = caravan.HerosDispo[x-1].CharacterName;
                GameObject.Find("HeroDispo" + x + "Prix").GetComponent<Text>().text += caravan.HerosDispo[x-1].Price.ToString();
                if (caravan.HerosDispo[x-1].IsMale == true) GameObject.Find("HeroDispo" + x + "I").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + caravan.HerosDispo[x-1].CharacterClassName + "IconeM");
                else GameObject.Find("HeroDispo" + x + "I").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + caravan.HerosDispo[x-1].CharacterClassName + "IconeF");
        }
        while (x < 5)
        {
            GameObject.Find("HeroDispo" + x).SetActive(false);
            /*
            GameObject.Find("HeroDispo" + x + "T").GetComponent<Text>().text = "";
            GameObject.Find("HeroDispo" + x + "Prix").GetComponent<Text>().text += "";
            GameObject.Find("HeroDispo" + x + "I").GetComponent<Image>().sprite = null;
            */
            x += 1;
        }
    }
}
