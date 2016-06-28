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
                if(SetProfil.HeroOpen.Equipement[0]!=null)SetProfil.HeroOpen.RemoveItem(SetProfil.HeroOpen.Equipement[0]);
            }
            else if (gameObject.name == "WeaponProfil" && count< 5)
            {
                if (SetProfil.HeroOpen.Equipement[1] != null) SetProfil.HeroOpen.RemoveItem(SetProfil.HeroOpen.Equipement[1]);
            }
            else if (gameObject.name == "Trinket1Profil" && count < 5)
            {
                if (SetProfil.HeroOpen.Equipement[2] != null) SetProfil.HeroOpen.RemoveItem(SetProfil.HeroOpen.Equipement[2]);
            }
            else if (gameObject.name == "Trinket2Profil" && count < 5)
            {
                if (SetProfil.HeroOpen.Equipement[3] != null) SetProfil.HeroOpen.RemoveItem(SetProfil.HeroOpen.Equipement[3]);
            }
            UpdatePanel();
            GameObject.Find("ArmorT").GetComponent<Text>().text = SetProfil.HeroOpen.EffectivDefense.ToString();
            GameObject.Find("AttackT").GetComponent<Text>().text = SetProfil.HeroOpen.EffectivDamage.ToString();
            GameObject.Find("HitChanceT").GetComponent<Text>().text = SetProfil.HeroOpen.EffectivHitChance.ToString();
            GameObject.Find("CritT").GetComponent<Text>().text = SetProfil.HeroOpen.EffectCritChance.ToString();
            GameObject.Find("SpeedT").GetComponent<Text>().text = SetProfil.HeroOpen.EffectivSpeed.ToString();
            GameObject.Find("DodgeT").GetComponent<Text>().text = SetProfil.HeroOpen.EffectivDodgeChance.ToString();
            GameObject.Find("FireResT").GetComponent<Text>().text = SetProfil.HeroOpen.EffectivFireRes.ToString();
            GameObject.Find("MagicResT").GetComponent<Text>().text = SetProfil.HeroOpen.EffectivMagicRes.ToString();
            GameObject.Find("PoisonResT").GetComponent<Text>().text = SetProfil.HeroOpen.EffectivPoisonRes.ToString();
            GameObject.Find("BleedingResT").GetComponent<Text>().text = SetProfil.HeroOpen.EffectivBleedingRes.ToString();
            GameObject.Find("WaterResT").GetComponent<Text>().text = SetProfil.HeroOpen.EffectivWaterRes.ToString();
            GameObject.Find("AffectResT").GetComponent<Text>().text = SetProfil.HeroOpen.EffectivAffectRes.ToString();
        }
    }
    public void OnClickPlayerItem()
    {
        if (Start.MenuProfil.activeInHierarchy)
        {
            if (gameObject.name == "Item1")
            {
                if(Start.Gtx.PlayerInfo.MyItems[0]!=null)SwitchItem(Start.Gtx.PlayerInfo.MyItems[0],1);
            }
            else if (gameObject.name == "Item2")
            {
                if (Start.Gtx.PlayerInfo.MyItems[1] != null) SwitchItem(Start.Gtx.PlayerInfo.MyItems[1],2);
            }
            else if (gameObject.name == "Item3")
            {
                if (Start.Gtx.PlayerInfo.MyItems[2] != null) SwitchItem(Start.Gtx.PlayerInfo.MyItems[2],3);
            }
            else if (gameObject.name == "Item4")
            {
                if (Start.Gtx.PlayerInfo.MyItems[3] != null) SwitchItem(Start.Gtx.PlayerInfo.MyItems[3],4);
            }
            UpdatePanel();
        }
    }
    private void SwitchItem(BaseItem item,int numItem)
    {
        Debug.Log(item.Itemtype);
        if (item.Itemtype == BaseItem.ItemTypes.Armor)
        {
            if (SetProfil.HeroOpen.Equipement[0] == null)
            {
                SetProfil.HeroOpen.GetNewItem(item);
                Start.Gtx.PlayerInfo.MyItems.Remove(item);
            }
            else
            {
                SetProfil.HeroOpen.RemoveAndAddAnItem(SetProfil.HeroOpen.Equipement[0], item);
            }
        }
        else if (item.Itemtype == BaseItem.ItemTypes.Weapon)
        {
            if (SetProfil.HeroOpen.Equipement[1] == null)
            {
                SetProfil.HeroOpen.GetNewItem(item);
                Start.Gtx.PlayerInfo.MyItems.Remove(item);
            }
            else
            {
                SetProfil.HeroOpen.RemoveAndAddAnItem(SetProfil.HeroOpen.Equipement[1], item);
            }
        }
        else if (item.Itemtype == BaseItem.ItemTypes.Trinket)
        {
            if (SetProfil.HeroOpen.Equipement[2] == null || SetProfil.HeroOpen.Equipement[3]==null)
            {
                SetProfil.HeroOpen.GetNewItem(item);
                Start.Gtx.PlayerInfo.MyItems.Remove(item);
            }
            else
            {
                SetProfil.HeroOpen.RemoveAndAddAnItem(SetProfil.HeroOpen.Equipement[2], item);
            }
        }
        GameObject.Find("ArmorT").GetComponent<Text>().text = SetProfil.HeroOpen.EffectivDefense.ToString();
        GameObject.Find("AttackT").GetComponent<Text>().text = SetProfil.HeroOpen.EffectivDamage.ToString();
        GameObject.Find("HitChanceT").GetComponent<Text>().text = SetProfil.HeroOpen.EffectivHitChance.ToString();
        GameObject.Find("CritT").GetComponent<Text>().text = SetProfil.HeroOpen.EffectCritChance.ToString();
        GameObject.Find("SpeedT").GetComponent<Text>().text = SetProfil.HeroOpen.EffectivSpeed.ToString();
        GameObject.Find("DodgeT").GetComponent<Text>().text = SetProfil.HeroOpen.EffectivDodgeChance.ToString();
        GameObject.Find("FireResT").GetComponent<Text>().text = SetProfil.HeroOpen.EffectivFireRes.ToString();
        GameObject.Find("MagicResT").GetComponent<Text>().text = SetProfil.HeroOpen.EffectivMagicRes.ToString();
        GameObject.Find("PoisonResT").GetComponent<Text>().text = SetProfil.HeroOpen.EffectivPoisonRes.ToString();
        GameObject.Find("BleedingResT").GetComponent<Text>().text = SetProfil.HeroOpen.EffectivBleedingRes.ToString();
        GameObject.Find("WaterResT").GetComponent<Text>().text = SetProfil.HeroOpen.EffectivWaterRes.ToString();
        GameObject.Find("AffectResT").GetComponent<Text>().text = SetProfil.HeroOpen.EffectivAffectRes.ToString();
    }
    public void UpdatePanel()
    {
        int x = 1;
            foreach (BaseItem item in Start.Gtx.PlayerInfo.MyItems)
            {
                GameObject.Find("Item" + x + "T").GetComponent<Text>().text = item.ItemName;
                if (item.Itemtype == BaseItem.ItemTypes.Armor) GameObject.Find("Item" + x).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Stats/A_Armor04");
                else if (item.Itemtype == BaseItem.ItemTypes.Weapon) GameObject.Find("Item" + x).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Stats/S_Sword10");
                else if (item.Itemtype == BaseItem.ItemTypes.Trinket) GameObject.Find("Item" + x).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Stats/S_Light01");
                x += 1;
            }
            for(int y= x; y < 5; y++)
            {
                GameObject.Find("Item" + y + "T").GetComponent<Text>().text = "";
                GameObject.Find("Item" + y).GetComponent<Image>().sprite = null;
            }
        
        BaseHeros heros = SetProfil.HeroOpen;
        if (heros.Equipement[0] != null) GameObject.Find("ArmorProfilText").GetComponent<Text>().text = heros.Equipement[0].ItemName;
        else GameObject.Find("ArmorProfilText").GetComponent<Text>().text = "";
        if (heros.Equipement[1] != null) GameObject.Find("WeaponProfilText").GetComponent<Text>().text = heros.Equipement[1].ItemName;
        else GameObject.Find("WeaponProfilText").GetComponent<Text>().text = "";
        if (heros.Equipement[2] != null) GameObject.Find("Trinket1ProfilText").GetComponent<Text>().text = heros.Equipement[2].ItemName;
        else GameObject.Find("Trinket1ProfilText").GetComponent<Text>().text = "";
        if (heros.Equipement[3] != null) GameObject.Find("Trinket2ProfilText").GetComponent<Text>().text = heros.Equipement[3].ItemName;
        else GameObject.Find("Trinket2ProfilText").GetComponent<Text>().text = "";
        if (heros.Equipement[0] != null) GameObject.Find("ArmorProfil").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Stats/A_Armor04");
        else GameObject.Find("ArmorProfil").GetComponent<Image>().sprite = null;
        if (heros.Equipement[1] != null) GameObject.Find("WeaponProfil").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Stats/S_Sword10");
        else GameObject.Find("WeaponProfil").GetComponent<Image>().sprite = null;
        if (heros.Equipement[2] != null) GameObject.Find("Trinket1Profil").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Stats/S_Light01");
        else GameObject.Find("Trinket1Profil").GetComponent<Image>().sprite = null;
        if (heros.Equipement[3] != null) GameObject.Find("Trinket2Profil").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Stats/S_Light01");
        else GameObject.Find("Trinket2Profil").GetComponent<Image>().sprite = null;
    }
}
