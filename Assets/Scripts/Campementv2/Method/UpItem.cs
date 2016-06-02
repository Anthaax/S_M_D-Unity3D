using UnityEngine;
using System.Collections;
using S_M_D.Camp.Class;
using S_M_D.Character;

public class UpItem : MonoBehaviour {

    public void OnClick()
    {

        Armory t = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Armory) as Armory;
        BaseHeros heros = t.Hero;
        if (gameObject.name == "Armor")
            {
                t.UpgrateItemOfAnHero(heros.Equipement[0]);
            Debug.Log(heros.Equipement[0].Lvl);
            }
            if (gameObject.name == "Weapon")
            {
            t.UpgrateItemOfAnHero(heros.Equipement[1]);
        }
            if (gameObject.name == "Trinket1")
            {
            t.UpgrateItemOfAnHero(heros.Equipement[2]);
        }
            if (gameObject.name == "Trinket2")
            {
            t.UpgrateItemOfAnHero(heros.Equipement[3]);
            }
    }
}
