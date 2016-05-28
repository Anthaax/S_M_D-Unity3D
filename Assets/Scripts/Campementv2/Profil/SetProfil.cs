using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using S_M_D.Character;
using S_M_D.Camp.Class;
using S_M_D.Camp;

public class SetProfil : MonoBehaviour {


    public void Show()
    {
        string name = gameObject.name;
        int index = int.Parse("" + name[name.Length - 2]);
        BaseHeros heros = Start.Gtx.PlayerInfo.MyHeros[index - 1];
        
        Start.MenuProfil.SetActive(false);

        if (Start.MenuBGArmory.activeInHierarchy)
        {
            Armory armory = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Armory) as Armory;
            armory.Hero = heros;
            GameObject IconeHero = GameObject.Find("ArmoryHero");
            if(heros.IsMale==true)IconeHero.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeF");
            else IconeHero.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeM");
        }
        else if(Start.MenuBGHospital.activeInHierarchy)
        {
            Hospital hospital = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Hospital) as Hospital;
            hospital.setHero(heros);
            hospital.LevelUP(); hospital.LevelUP();
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
        }
        /*else if (Start.MenuBGHotel.activeInHierarchy)
        {
            Hotel hotel = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Hotel) as Hotel;
            if (hotel.Hero1 == null)
            {
                hotel.setHeros1(heros);
                GameObject IconeHero = GameObject.Find("HotelHero1");
                if (heros.IsMale == true) IconeHero.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeF");
                else IconeHero.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeM");
            }
            else {
                hotel.setHeros2(heros);
                hotel.setHeros1(heros);
                GameObject IconeHero = GameObject.Find("HotelHero2");
                if (heros.IsMale == true) IconeHero.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeF");
                else IconeHero.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeM");
            }
        }*/


        else
        {
            Start.MenuProfil.SetActive(true);
            //GameObject.Find("Icone").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeF");
            if (heros.IsMale == true) GameObject.Find("Icone").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeF");
            else GameObject.Find("Icone").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeM");
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


    public void ShowDispo()
    {
        string name = gameObject.name;
        Caravan caravan = Start.Gtx.PlayerInfo.GetBuilding(S_M_D.Camp.Class.BuildingNameEnum.Caravan) as Caravan;
        int index = int.Parse("" + name[name.Length - 2]);
        BaseHeros heros = caravan.HerosDispo[index - 1];
        // GameObject.Find("Icone").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeF");
        if (heros.IsMale == true) GameObject.Find("Icone").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeF");
        else GameObject.Find("Icone").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeM");
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
