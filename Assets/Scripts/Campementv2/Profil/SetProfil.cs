using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using S_M_D.Character;
using S_M_D.Camp.Class;
using S_M_D.Camp;
using System.Collections.Generic;

public class SetProfil : MonoBehaviour {

    public static BaseHeros[] coupleHerosHotel = new BaseHeros[2];
    public static BaseHeros[] coupleHerosBar = new BaseHeros[2];
    public static BaseHeros[] HerosAdventure = new BaseHeros[4];
    public static BaseHeros HeroOpen;
    public void Show()
    {
        string name = gameObject.name;
        int index = int.Parse("" + name[name.Length - 2]);
        BaseHeros heros = Start.Gtx.PlayerInfo.MyHeros[index - 1];
        Start.MenuProfil.SetActive(false);

        if (Start.MenuBGArmory.activeInHierarchy)
        {
            Armory armory = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Armory) as Armory;

            //---
            if(armory.Hero != null)
            {
                BaseHeros h = armory.Hero;
                SetToActiveButton(Start.pHeroes.Find(t => t.GetComponentInChildren<Text>().text == h.CharacterName).GetComponentInChildren<Button>());
            }
            armory.SetHero(heros);
            GameObject IconeHero = GameObject.Find("ArmoryHero");
            if (heros.IsMale == true) IconeHero.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeM");
            else IconeHero.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeF");

            SetToInactiveButton(gameObject.GetComponentInChildren<Button>());
            //---

            foreach (Button b in Start.ButtonsArmor)
            {
                if (b.name == "Armor")
                {
                    b.GetComponentsInChildren<Text>()[0].text = heros.Equipement[0].ItemName+"\n Prix => "+armory.ActionPrice;
                    b.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Sprites/Stats/A_Armor04");
                }
                else if (b.name == "Weapon")
                {
                    b.GetComponentsInChildren<Text>()[0].text = heros.Equipement[1].ItemName + "\n Prix => " + armory.ActionPrice;
                    b.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Sprites/Stats/S_Sword10");
                }
                else if (b.name == "Trinket1")
                {
                    b.GetComponentsInChildren<Text>()[0].text = heros.Equipement[2].ItemName + "\n Prix => " + armory.ActionPrice;
                    b.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Sprites/Stats/S_Light01");
                }
                else if (b.name == "Trinket2")
                {
                    b.GetComponentsInChildren<Text>()[0].text = heros.Equipement[3].ItemName + "\n Prix => " + armory.ActionPrice;
                    b.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Sprites/Stats/S_Light01");
                }
            }
            /*
            IconeArmor.GetComponent<Image>().sprite =;
            GameObject IconeWeapon = GameObject.Find("Weapon");
            GameObject IconeTrinket1 = GameObject.Find("Trinket1");
            GameObject IconeTrinket2 = GameObject.Find("Trinket2");
            */
        }

        else if(Start.MenuBGCasern.activeInHierarchy)
        {
            Casern casern = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Casern) as Casern;
            
            if (casern.Hero != null)
            {
                BaseHeros h = casern.Hero;
                SetToActiveButton(Start.pHeroes.Find(t => t.GetComponentInChildren<Text>().text == h.CharacterName).GetComponentInChildren<Button>());
            }
            casern.SetHero(heros);
            GameObject IconeHero = GameObject.Find("CasernHero");
            string sex = heros.IsMale ? "M" : "F";
            IconeHero.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "Icone" + sex);

            SetToInactiveButton(gameObject.GetComponentInChildren<Button>());
            //---
            CasernBoard.SetBoard();
        }

        else if (Start.MenuBGHospital.activeInHierarchy)
        {
            if(!HospitalBoard.SicknessRemove)
            {
                Hospital hospital = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Hospital) as Hospital;
                //---
                if (hospital.Hero != null)
                {
                    BaseHeros h = hospital.Hero;
                    SetToActiveButton(Start.pHeroes.Find(t => t.GetComponentInChildren<Text>().text == h.CharacterName).GetComponentInChildren<Button>());
                }
                hospital.SetHero(heros);
                GameObject IconeHero = GameObject.Find("HospitalHero");
                string sex = heros.IsMale ? "M" : "F";
                IconeHero.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "Icone" + sex);
                HospitalBoard.CheckSicknesses(heros, hospital);

                SetToInactiveButton(gameObject.GetComponentInChildren<Button>());
                //---
            }

        }
        else if (Start.MenuBGCasern.activeInHierarchy)
        {
            Casern casern = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Casern) as Casern;
            casern.SetHero(heros);
        }
        else if (Start.MenuBGMentalhospital.activeInHierarchy)
        {
            if (!MentalHospitalBoard.MentalSicknessRemove)
            {
                MentalHospital mentalHospital = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.MentalHospital) as MentalHospital;
                heros.GetPsycho(new Crazyness());
                //---
                if (mentalHospital.Hero != null)
                {
                    BaseHeros h = mentalHospital.Hero;
                    SetToActiveButton(Start.pHeroes.Find(t => t.GetComponentInChildren<Text>().text == h.CharacterName).GetComponentInChildren<Button>());
                }
                mentalHospital.SetHero(heros);
                GameObject IconeHero = GameObject.Find("MentalHospitalHero");
                string sex = heros.IsMale ? "M" : "F";
                IconeHero.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "Icone" + sex);
                MentalHospitalBoard.CheckSicknesses(heros, mentalHospital);

                SetToInactiveButton(gameObject.GetComponentInChildren<Button>());
                //---
            }

        }
        else if (Start.MenuBGHotel.activeInHierarchy)
        {
            if(!HotelBoard.HeroesValid)
            {
                Hotel hotel = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Hotel) as Hotel;
                GameObject IconeHero1 = GameObject.Find("HotelHero1");
                GameObject IconeHero2 = GameObject.Find("HotelHero2");

                if (IconeHero1.GetComponent<Image>().sprite == null)
                {
                    coupleHerosHotel[0] = heros;
                    string sex = heros.IsMale ? "M" : "F";
                    IconeHero1.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "Icone" + sex);
                    SetToInactiveButton(gameObject.GetComponentInChildren<Button>());
                    //---
                }
                else
                {
                    //---
                    if (coupleHerosHotel[1] != null)
                    {
                        BaseHeros h = coupleHerosHotel[1];
                        SetToActiveButton(Start.pHeroes.Find(t => t.GetComponentInChildren<Text>().text == h.CharacterName).GetComponentInChildren<Button>());
                    }
                    coupleHerosHotel[1] = heros;
                    string sex = heros.IsMale ? "M" : "F";
                    IconeHero2.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "Icone" + sex);

                    SetToInactiveButton(gameObject.GetComponentInChildren<Button>());
                    //---
                }
            }
            

        }
        else if (Start.MenuBGBar.activeInHierarchy)
        {
            if(!BarBoard.HeroesValid)
            {
                Bar bar = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Bar) as Bar;
                GameObject IconeHero1 = GameObject.Find("BarHero1");
                GameObject IconeHero2 = GameObject.Find("BarHero2");

                if (IconeHero1.GetComponent<Image>().sprite == null)
                {
                    coupleHerosBar[0] = heros;
                    string sex = heros.IsMale ? "M" : "F";
                    IconeHero1.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "Icone" + sex);
                    SetToInactiveButton(gameObject.GetComponentInChildren<Button>());
                    //---
                }
                else
                {
                    //---
                    if (coupleHerosBar[1] != null)
                    {
                        BaseHeros h = coupleHerosBar[1];
                        SetToActiveButton(Start.pHeroes.Find(t => t.GetComponentInChildren<Text>().text == h.CharacterName).GetComponentInChildren<Button>());
                    }
                    coupleHerosBar[1] = heros;
                    string sex = heros.IsMale ? "M" : "F";
                    IconeHero2.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "Icone" + sex);
                    SetToInactiveButton(gameObject.GetComponentInChildren<Button>());
                    //---
                }
            }
            

        }
        else if (Start.PanelBoardMission.activeInHierarchy)
        {
            List<GameObject> IconeHeros = new List<GameObject>();
            IconeHeros.Add(GameObject.Find("AdvHero1"));
            IconeHeros.Add(GameObject.Find("AdvHero2"));
            IconeHeros.Add(GameObject.Find("AdvHero3"));
            IconeHeros.Add(GameObject.Find("AdvHero4"));
            Debug.Log("Contient : " + AdventureBoard.ContainsHero(heros));
            for (int i = 0; i < IconeHeros.Count; i++)
            {
                if (IconeHeros[i].GetComponent<Image>().sprite == null && !AdventureBoard.ContainsHero(heros))
                {
                    HerosAdventure[i] = heros;
                    string sex = heros.IsMale ? "M" : "F";
                    IconeHeros[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "Icone" + sex);
                    SetToInactiveButton(gameObject.GetComponentInChildren<Button>());
                    //---
                    break;
                }
            }
        }


        else
        {
            HeroOpen = heros;
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
            /*
            Diarrhea d = new Diarrhea();
            Fever f = new Fever();
            heros.GetSickness(d);
            heros.GetSickness(f);
            Crazyness c = new Crazyness();
            heros.GetPsycho(c);
            */
            int i = 1;
            foreach(Sickness p in heros.Sicknesses)
            {
                Debug.Log(p.Name);
                GameObject.Find("Maladie"+i).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Sicknesses/Normal/" + p.Name);
                i += 1;
            }
            i = 1;
            foreach (Psychology p in heros.Psycho)
            {
                Debug.Log(p.Name);
                GameObject.Find("Sick" + i).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Sicknesses/Mental/" + p.Name);
                i += 1;
            }
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

    public static void InitBoardBar()
    {
        Bar bar = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Bar) as Bar;
        GameObject IconeHero1 = GameObject.Find("BarHero1");
        GameObject IconeHero2 = GameObject.Find("BarHero2");
        Debug.Log("Hero1 = " + bar.Hero1 + "; Hero2 = " + bar.Hero2);
        if (bar.Hero1 != null && bar.Hero2 != null)
        {
            BarBoard.HeroesValid = true;
            string sex = bar.Hero1.IsMale ? "M" : "F";
            IconeHero1.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + bar.Hero1.CharacterClassName + "Icone" + sex);
            string sex2 = bar.Hero2.IsMale ? "M" : "F";
            IconeHero2.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + bar.Hero2.CharacterClassName + "Icone" + sex2);

            GameObject.Find("RemoveHero1Bar").SetActive(false);
            GameObject.Find("RemoveHero2Bar").SetActive(false);
            GameObject.Find("Valid").SetActive(false);

            RemoveHeroesFromList(bar.Hero1, bar.Hero2);
        }
        else
        {
            BarBoard.HeroesValid = false;
        }
    }

    public static void InitBoardHotel()
    {
        Hotel hotel = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Hotel) as Hotel;
        GameObject IconeHero1 = GameObject.Find("HotelHero1");
        GameObject IconeHero2 = GameObject.Find("HotelHero2");

        if (hotel.Hero1 != null && hotel.Hero2 != null)
        {
            HotelBoard.HeroesValid = true;
            string sex = hotel.Hero1.IsMale ? "M" : "F";
            IconeHero1.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + hotel.Hero1.CharacterClassName + "Icone" + sex);
            string sex2 = hotel.Hero2.IsMale ? "M" : "F";
            IconeHero2.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + hotel.Hero2.CharacterClassName + "Icone" + sex2);

            GameObject.Find("RemoveHero1").SetActive(false);
            GameObject.Find("RemoveHero2").SetActive(false);
            GameObject.Find("Valid").SetActive(false);
            
            RemoveHeroesFromList(hotel.Hero1, hotel.Hero2);
        }
        else
        {
            HotelBoard.HeroesValid = false;
        }
    }
   public void ShowProfilPlayer()
    {

        GameObject.Find("GoldT").GetComponent<Text>().text = Start.Gtx.MoneyManager.Money.ToString();
        int x = 1;
        foreach(BaseItem item in Start.Gtx.PlayerInfo.MyItems)
        {
            GameObject.Find("Item" + x + "T").GetComponent<Text>().text = item.ItemName;
            if(item.Itemtype==BaseItem.ItemTypes.Armor) GameObject.Find("Item"+x).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Stats/A_Armor04");
            if (item.Itemtype == BaseItem.ItemTypes.Weapon) GameObject.Find("Item" + x).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Stats/S_Sword10");
            if (item.Itemtype == BaseItem.ItemTypes.Trinket) GameObject.Find("Item" + x).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Stats/S_Light01");
        }
    }

    public static void RemoveHeroesFromList(params BaseHeros[] heroes)
    {
        foreach(BaseHeros h in heroes)
        {
            Debug.Log("h : " + h.CharacterName);
        }
        foreach(BaseHeros h in heroes)
            SetToInactiveButton(Start.pHeroes.Find(t => t.GetComponentInChildren<Text>().text == h.CharacterName).GetComponentInChildren<Button>());
    }
    public static void SetToInactiveButton(Button button)
    {
        button.GetComponent<Image>().color = Color.black;
        button.enabled = false;
    }
    public static void SetToActiveButton(Button button)
    {
        button.GetComponent<Image>().color = Color.white;
        button.enabled = true;
    }
    public static Sprite ShowImage(BaseHeros heros)
    {
        string sex = heros.IsMale ? "M" : "F";
        return Resources.Load<Sprite>( "Sprites/Icones/" + heros.CharacterClassName + "Icone" + sex );
    }
}
