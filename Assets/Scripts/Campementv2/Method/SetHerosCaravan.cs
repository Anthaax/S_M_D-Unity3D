using UnityEngine;
using System.Collections;
using S_M_D.Character;
using S_M_D.Camp.Class;
using UnityEngine.UI;

public class SetHerosCaravan : MonoBehaviour {
    bool _firstTime = true;
	public void OnClick () {
        Caravan caravan = Start.Gtx.PlayerInfo.GetBuilding(S_M_D.Camp.Class.BuildingNameEnum.Caravan) as Caravan;
        int x = 1;
        for(x = 1; x <= 5; x++)
        {
            Debug.Log(x + "Nombre de tours");
            if(_firstTime)
                GameObject.Find("HeroDispo" + x).SetActive(true);
            if (x <= caravan.HerosDispo.Count)
            {
                GameObject.Find( "HeroDispo" + x + "T" ).GetComponent<Text>().text = caravan.HerosDispo[x - 1].CharacterName;
                GameObject.Find( "HeroDispo" + x + "I" ).GetComponent<Button>().enabled = true;

                GameObject.Find( "HeroDispo" + x + "Prix" ).GetComponent<Text>().text = caravan.HerosDispo[x - 1].Price.ToString();

                GameObject.Find( "UpHeroDispo" + x ).GetComponent<Button>().enabled = true;
                GameObject.Find( "UpHeroDispo" + x ).GetComponent<Image>().color = Color.white;

                string sex = caravan.HerosDispo[x - 1].IsMale ? "M" : "F";
                GameObject.Find( "HeroDispo" + x + "I" ).GetComponent<Image>().sprite = Resources.Load<Sprite>( "Sprites/Icones/" + caravan.HerosDispo[x - 1].CharacterClassName + "Icone" + sex );
            }
            else if (x <= caravan.MaxNewHero)
            {
                GameObject.Find( "HeroDispo" + x + "T" ).GetComponent<Text>().text = "Indisponible";

                GameObject.Find( "HeroDispo" + x + "Prix" ).GetComponent<Text>().text = "";

                GameObject.Find( "HeroDispo" + x + "I" ).GetComponent<Image>().sprite = Resources.Load<Sprite>( "Sprites/Icones/noprofil" );
                GameObject.Find( "HeroDispo" + x + "I" ).GetComponent<Button>().enabled = false;

                GameObject.Find( "UpHeroDispo" + x ).GetComponent<Button>().enabled = false;
                GameObject.Find( "UpHeroDispo" + x ).GetComponent<Image>().color = Color.red;
            }
            else
            {
                GameObject.Find( "HeroDispo" + x + "I" ).GetComponent<Image>().sprite = Resources.Load<Sprite>( "Sprites/Icones/noprofil" );
                GameObject.Find( "HeroDispo" + x + "I" ).GetComponent<Button>().enabled = false;

                GameObject.Find( "HeroDispo" + x + "Prix" ).GetComponent<Text>().text = "";

                GameObject.Find( "HeroDispo" + x + "T" ).GetComponent<Text>().text = "Niveau" +( x - 2) + "Requis";

                GameObject.Find( "UpHeroDispo" + x ).GetComponent<Button>().enabled = false;
                GameObject.Find( "UpHeroDispo" + x ).GetComponent<Image>().color = Color.red;
            }
        }
    }

}
