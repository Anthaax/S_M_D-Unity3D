using UnityEngine;
using System.Collections;
using S_M_D.Camp.Class;
using S_M_D.Character;

public class BuyHero : MonoBehaviour {

    public void OnClick()
    {
        if(Start.Gtx.PlayerInfo.MyHeros.Count < Start.pHeroes.Count)
        {
            string name = gameObject.name;
            Caravan caravan = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Caravan) as Caravan;
            int index = int.Parse("" + name[name.Length - 1]);
            BaseHeros heros = caravan.HerosDispo[index - 1];
            caravan.BuyHero(heros);
            caravan.HerosDispo.Remove(heros);
        }
        
    }
}
