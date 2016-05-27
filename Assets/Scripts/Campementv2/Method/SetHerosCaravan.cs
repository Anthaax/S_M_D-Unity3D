using UnityEngine;
using System.Collections;
using S_M_D.Character;
using S_M_D.Camp.Class;
using UnityEngine.UI;

public class SetHerosCaravan : MonoBehaviour {
    
	public void OnClick () {
        Caravan caravan = Start.Gtx.PlayerInfo.GetBuilding(S_M_D.Camp.Class.BuildingName.Caravan) as Caravan;
        int x = 1;
        caravan.Initialized();
        foreach (BaseHeros heros in caravan.HerosDispo )
        {
            GameObject.Find("HeroDispo" + x + "T").GetComponent<Text>().text = heros.CharacterName;
            //GameObject.Find("HeroDispo" + x + "I").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeF");
            if (heros.IsMale == true) GameObject.Find("HeroDispo" + x + "I").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeF");
            else GameObject.Find("HeroDispo" + x + "I").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeM");
            x++;

        }
    }
}
