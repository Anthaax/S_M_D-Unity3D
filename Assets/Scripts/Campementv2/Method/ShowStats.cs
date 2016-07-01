using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using S_M_D.Character;

public class ShowStats : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowStatsItem()
    {
        BaseItem item = null;

        if (gameObject.name.ToLower().StartsWith("item"))
        {
            int itemIndex = int.Parse("" + gameObject.name[gameObject.name.Length - 1]);
            Debug.Log("itemName : " + itemIndex);

            if (Start.Gtx.PlayerInfo.MyItems.Count > 0)
                item = Start.Gtx.PlayerInfo.MyItems[itemIndex - 1];
        }
        else
        {
            Debug.Log("Numero : 3");

            int index = 0;
            if (gameObject.name == "ArmorProfil")
                index = 0;
            else if (gameObject.name == "WeaponProfil")
                index = 1;
            else if (gameObject.name == "Trinket1Profil")
                index = 2;
            else if (gameObject.name == "Trinket2Profil")
                index = 3;

            item = SetProfil.HeroOpen.Equipement[index];
        }

        Debug.Log("Numero : 5");

        if (item != null)
        {
            Start.MenuProfilStuff.SetActive(true);

            GameObject.Find("StuffName").GetComponent<Text>().text = item.ItemName;
            GameObject.Find("sArmorT").GetComponent<Text>().text = item.Defense.ToString();
            GameObject.Find("sAttackT").GetComponent<Text>().text = item.Damage.ToString();
            GameObject.Find("sHitChanceT").GetComponent<Text>().text = item.HitChance.ToString();
            GameObject.Find("sCritT").GetComponent<Text>().text = item.CritChance.ToString();
            GameObject.Find("sSpeedT").GetComponent<Text>().text = item.Speed.ToString();
            GameObject.Find("sDodgeT").GetComponent<Text>().text = item.DodgeChance.ToString();
            GameObject.Find("sFireResT").GetComponent<Text>().text = item.FireRes.ToString();
            GameObject.Find("sMagicResT").GetComponent<Text>().text = item.MagicRes.ToString();
            GameObject.Find("sPoisonResT").GetComponent<Text>().text = item.PoisonRes.ToString();
            GameObject.Find("sBleedingResT").GetComponent<Text>().text = item.BleedingRes.ToString();
            GameObject.Find("sWaterResT").GetComponent<Text>().text = item.WaterRes.ToString();
            GameObject.Find("sAffectResT").GetComponent<Text>().text = item.AffectRes.ToString();

            
        }

    }
    
}
