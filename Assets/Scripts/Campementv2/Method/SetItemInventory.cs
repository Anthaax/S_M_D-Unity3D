using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using S_M_D.Character;

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
                GameObject.Find("Trinket1ProfilText").GetComponent<Text>().text = "";
                GameObject.Find("Item" + count + "T").GetComponent<Text>().text = SetProfil.HeroOpen.Equipement[2].ItemName;
                SetProfil.HeroOpen.Equipement[2] = null;
            }
            else if (gameObject.name == "Trinket2Profil" && count < 5)
            {
                Start.Gtx.PlayerInfo.MyItems.Add(SetProfil.HeroOpen.Equipement[3]);
                GameObject.Find("Trinket2ProfilText").GetComponent<Text>().text = "";
                GameObject.Find("Item" + count + "T").GetComponent<Text>().text = SetProfil.HeroOpen.Equipement[3].ItemName;
                SetProfil.HeroOpen.Equipement[3] = null;
            }
        }
    }
    public void OnClickPlayerItem()
    {
        if (Start.MenuProfil.activeInHierarchy)
        {
            if (gameObject.name == "Item1")
            {
                SwitchItem(Start.Gtx.PlayerInfo.MyItems[0]);
            }
            else if (gameObject.name == "Item2")
            {
                SwitchItem(Start.Gtx.PlayerInfo.MyItems[1]);
            }
            else if (gameObject.name == "Item3")
            {
                SwitchItem(Start.Gtx.PlayerInfo.MyItems[2]);
            }
            else if (gameObject.name == "Item4")
            {
                SwitchItem(Start.Gtx.PlayerInfo.MyItems[3]);
            }

        }
    }
    private void SwitchItem(BaseItem item)
    {
        if (item.Itemtype == BaseItem.ItemTypes.Armor)
        {
            if (SetProfil.HeroOpen.Equipement[0] == null)
            {
                SetProfil.HeroOpen.Equipement[0] = item;
                item = null;
                GameObject.Find("ArmorProfilText").GetComponent<Text>().text = "";
            }
            else
            {
                BaseItem temp = SetProfil.HeroOpen.Equipement[0];
                SetProfil.HeroOpen.Equipement[0] = item;
                item = temp;
                GameObject.Find("Item1").GetComponent<Text>().text = SetProfil.HeroOpen.Equipement[0].ItemName;
                GameObject.Find("ArmorProfilText").GetComponent<Text>().text = item.ItemName;
            }
        }
        else if (item.Itemtype == BaseItem.ItemTypes.Weapon)
        {
            if (SetProfil.HeroOpen.Equipement[1] == null)
            {
                SetProfil.HeroOpen.Equipement[1] = item;
                item = null;
                GameObject.Find("WeaponProfilText").GetComponent<Text>().text = "";
            }
            else
            {
                BaseItem temp = SetProfil.HeroOpen.Equipement[1];
                SetProfil.HeroOpen.Equipement[1] = item;
                item = temp;
                GameObject.Find("Item1").GetComponent<Text>().text = SetProfil.HeroOpen.Equipement[1].ItemName;
                GameObject.Find("WeaponProfilText").GetComponent<Text>().text = item.ItemName;
            }
        }
        else if (item.Itemtype == BaseItem.ItemTypes.Trinket)
        {
            if (SetProfil.HeroOpen.Equipement[2] == null)
            {
                SetProfil.HeroOpen.Equipement[2] = item;
                item = null;
                GameObject.Find("Trinket1ProfilText").GetComponent<Text>().text = "";
            }
            else
            {
                BaseItem temp = SetProfil.HeroOpen.Equipement[2];
                SetProfil.HeroOpen.Equipement[2] = item;
                item = temp;
                GameObject.Find("Item1").GetComponent<Text>().text = SetProfil.HeroOpen.Equipement[2].ItemName;
                GameObject.Find("Trinket1ProfilText").GetComponent<Text>().text = item.ItemName;
            }
        }
    }
}
