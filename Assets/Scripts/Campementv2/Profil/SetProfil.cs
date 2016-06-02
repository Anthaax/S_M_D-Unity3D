using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using S_M_D.Character;
using S_M_D.Camp.Class;
using S_M_D.Camp;

public class SetProfil : MonoBehaviour {

    public static BaseHeros[] coupleHerosHotel = new BaseHeros[2];
    public static BaseHeros[] coupleHerosBar = new BaseHeros[2];

    public void Show()
    {
        string name = gameObject.name;
        int index = int.Parse("" + name[name.Length - 2]);
        BaseHeros heros = Start.Gtx.PlayerInfo.MyHeros[index - 1];
        
        Start.MenuProfil.SetActive(false);

        if (Start.MenuBGArmory.activeInHierarchy)
        {
            Armory armory = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Armory) as Armory;
            armory.SetHero(heros);
            GameObject IconeHero = GameObject.Find("ArmoryHero");
            if(heros.IsMale==true)IconeHero.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeM");
            else IconeHero.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeF");

            foreach(Button b in Start.ButtonsArmor)
            {
                if(b.name == "Armor")
                {
                    b.GetComponentsInChildren<Text>()[0].text = heros.Equipement[0].ItemName;
                }
                else if (b.name == "Weapon")
                {
                    b.GetComponentsInChildren<Text>()[0].text = heros.Equipement[1].ItemName;
                }
                else if (b.name == "Trinket1")
                {
                    b.GetComponentsInChildren<Text>()[0].text = heros.Equipement[2].ItemName;
                }
                else if (b.name == "Trinket2")
                {
                    b.GetComponentsInChildren<Text>()[0].text = heros.Equipement[3].ItemName;
                }
            }
            /*
            IconeArmor.GetComponent<Image>().sprite =;
            GameObject IconeWeapon = GameObject.Find("Weapon");
            GameObject IconeTrinket1 = GameObject.Find("Trinket1");
            GameObject IconeTrinket2 = GameObject.Find("Trinket2");
            */
        }
        else if(Start.MenuBGHospital.activeInHierarchy)
        {
            Hospital hospital = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Hospital) as Hospital;
            hospital.setHero(heros);
            hospital.LevelUP();
            heros.GetSickness(new Fever());
            GameObject IconeHero = GameObject.Find("HospitalHero");
            IconeHero.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeF");
            HospitalBoard.CheckSicknesses(heros, hospital);
        }
        else if (Start.MenuBGCasern.activeInHierarchy)
        {
            Casern casern = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Casern) as Casern;
            casern.setHero(heros);
        }
        else if (Start.MenuBGMentalhospital.activeInHierarchy)
        {
            MentalHospital mentalHospital = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.MentalHospital) as MentalHospital;
            mentalHospital.setHero(heros);
            GameObject IconeHero = GameObject.Find("MentalHospitalHero");
            IconeHero.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeF");
            MentalHospitalBoard.CheckSicknesses(heros, mentalHospital);
        }
        else if (Start.MenuBGHotel.activeInHierarchy)
        {
            Hotel hotel = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Hotel) as Hotel;
            GameObject IconeHero1 = GameObject.Find("HotelHero1");
            GameObject IconeHero2 = GameObject.Find("HotelHero2");

            if(IconeHero1.GetComponent<Image>().sprite == null)
            {
                coupleHerosHotel[0] = heros;
                string sex = heros.IsMale ? "M" : "F";
                IconeHero1.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "Icone"+sex);
            }
            else
            {
                coupleHerosHotel[1] = heros;
                string sex = heros.IsMale ? "M" : "F";
                IconeHero2.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "Icone" + sex);

            }

        }
        else if (Start.MenuBGBar.activeInHierarchy)
        {
            Bar bar = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Bar) as Bar;
            GameObject IconeHero1 = GameObject.Find("BarHero1");
            GameObject IconeHero2 = GameObject.Find("BarHero2");

            if (IconeHero1.GetComponent<Image>().sprite == null)
            {
                coupleHerosBar[0] = heros;
                string sex = heros.IsMale ? "M" : "F";
                IconeHero1.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "Icone" + sex);
            }
            else
            {
                coupleHerosBar[1] = heros;
                string sex = heros.IsMale ? "M" : "F";
                IconeHero2.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "Icone" + sex);

            }

        }


        else
        {
            Start.MenuProfil.SetActive(true);
            //GameObject.Find("Icone").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeF");
            if (heros.IsMale == true) GameObject.Find("Icone").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeM");
            else GameObject.Find("Icone").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeF");
            GameObject.Find("ArmorT").GetComponent<Text>().text = heros.EffectivDefense.ToString();
            GameObject.Find("AttackT").GetComponent<Text>().text = heros.EffectivDamage.ToString();
            GameObject.Find("HitChanceT").GetComponent<Text>().text = heros.EffectivHitChance.ToString();
            GameObject.Find("CritT").GetComponent<Text>().text = heros.EffectCritChance.ToString();
            GameObject.Find("SpeedT").GetComponent<Text>().text = heros.EffectivSpeed.ToString();
            GameObject.Find("DodgeT").GetComponent<Text>().text = heros.EffectivDodgeChance.ToString();
            GameObject.Find("FireResT").GetComponent<Text>().text = heros.EffectivFireRes.ToString();
            GameObject.Find("MagicResT").GetComponent<Text>().text = heros.EffectivMagicRes.ToString();
            GameObject.Find("PoisonResT").GetComponent<Text>().text = heros.EffectivPoisonRes.ToString();
            GameObject.Find("BleedingResT").GetComponent<Text>().text = heros.EffectivBleedingRes.ToString();
            GameObject.Find("WaterResT").GetComponent<Text>().text = heros.EffectivWaterRes.ToString();
            GameObject.Find("AffectResT").GetComponent<Text>().text = heros.EffectivAffectRes.ToString();

            GameObject.Find("ArmorProfilText").GetComponent<Text>().text = heros.Equipement[0].ItemName;
            GameObject.Find("WeaponProfilText").GetComponent<Text>().text = heros.Equipement[0].ItemName;
            GameObject.Find("Trinket1ProfilText").GetComponent<Text>().text = heros.Equipement[0].ItemName;
            GameObject.Find("Trinket2ProfilText").GetComponent<Text>().text = heros.Equipement[0].ItemName;
        }
        
        
    }


    public void ShowDispo()
    {
        string name = gameObject.name;
        Caravan caravan = Start.Gtx.PlayerInfo.GetBuilding(S_M_D.Camp.Class.BuildingNameEnum.Caravan) as Caravan;
        int index = int.Parse("" + name[name.Length - 2]);
        BaseHeros heros = caravan.HerosDispo[index - 1];
        // GameObject.Find("Icone").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeF");
        if (heros.IsMale == true) GameObject.Find("Icone").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeM");
        else GameObject.Find("Icone").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeF");
        GameObject.Find("ArmorT").GetComponent<Text>().text = heros.EffectivDefense.ToString();
        GameObject.Find("AttackT").GetComponent<Text>().text = heros.EffectivDamage.ToString();
        GameObject.Find("HitChanceT").GetComponent<Text>().text = heros.EffectivHitChance.ToString();
        GameObject.Find("CritT").GetComponent<Text>().text = heros.EffectCritChance.ToString();
        GameObject.Find("SpeedT").GetComponent<Text>().text = heros.EffectivSpeed.ToString();
        GameObject.Find("DodgeT").GetComponent<Text>().text = heros.EffectivDodgeChance.ToString();
        GameObject.Find("FireResT").GetComponent<Text>().text = heros.EffectivFireRes.ToString();
        GameObject.Find("MagicResT").GetComponent<Text>().text = heros.EffectivMagicRes.ToString();
        GameObject.Find("PoisonResT").GetComponent<Text>().text = heros.EffectivPoisonRes.ToString();
        GameObject.Find("BleedingResT").GetComponent<Text>().text = heros.EffectivBleedingRes.ToString();
        GameObject.Find("WaterResT").GetComponent<Text>().text = heros.EffectivWaterRes.ToString();
        GameObject.Find("AffectResT").GetComponent<Text>().text = heros.EffectivAffectRes.ToString();
    }
   
}
