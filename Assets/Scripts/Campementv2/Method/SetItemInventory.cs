using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetItemInventory : MonoBehaviour {

	public void OnClickHeroItem()
    {
        if (Start.MenuProfilPlayer.activeInHierarchy)
        {
            int count = Start.Gtx.PlayerInfo.MyItems.Count+1;
            if (gameObject.name == "ArmorProfil" && count<5)
            {
                Start.Gtx.PlayerInfo.MyItems.Add(SetProfil.HeroOpen.Equipement[0]);
                GameObject.Find("ArmorProfilText").GetComponent<Text>().text = "";
                GameObject.Find("Item"+count+"T").GetComponent<Text>().text = SetProfil.HeroOpen.Equipement[0].ItemName;
                SetProfil.HeroOpen.Equipement[0] = null;
            }
            else if (gameObject.name == "WeaponProfil" && count< 5)
            {
                Start.Gtx.PlayerInfo.MyItems.Add(SetProfil.HeroOpen.Equipement[1]);
                GameObject.Find("WeaponProfilText").GetComponent<Text>().text = "";
                GameObject.Find("Item" + count + "T").GetComponent<Text>().text = SetProfil.HeroOpen.Equipement[1].ItemName;
                SetProfil.HeroOpen.Equipement[1] = null;
            }
            else if (gameObject.name == "Trinket1Profil" && count < 5)
            {
                Start.Gtx.PlayerInfo.MyItems.Add(SetProfil.HeroOpen.Equipement[2]);
                GameObject.Find("Trinket1Text").GetComponent<Text>().text = "";
                GameObject.Find("Item" + count + "T").GetComponent<Text>().text = SetProfil.HeroOpen.Equipement[2].ItemName;
                SetProfil.HeroOpen.Equipement[2] = null;
            }
            else if (gameObject.name == "Trinket2Profil" && count < 5)
            {
                Start.Gtx.PlayerInfo.MyItems.Add(SetProfil.HeroOpen.Equipement[3]);
                GameObject.Find("Trinket2Text").GetComponent<Text>().text = "";
                GameObject.Find("Item" + count + "T").GetComponent<Text>().text = SetProfil.HeroOpen.Equipement[3].ItemName;
                SetProfil.HeroOpen.Equipement[3] = null;
            }
        }
    }
}
