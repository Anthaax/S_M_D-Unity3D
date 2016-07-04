using UnityEngine;
using System.Collections;
using S_M_D.Camp.Class;
using S_M_D.Character;
using S_M_D.Camp;
using UnityEngine.UI;

public class MentalHospitalBoard : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
	
	}
    public static bool MentalSicknessRemove;

    public static void Init()
    {
        SetProfil.InitBoardMentalHospital();

    }

    public void TryRemoveMentalPsycho()
    {
        MentalHospital mentalHospital = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.MentalHospital) as MentalHospital;
        string buttonName = gameObject.name;
        if (buttonName == "Agressivity")
        {
            Agressivity ag = mentalHospital.Hero.Psycho.Find(t => t.Name == "Agressivity") as Agressivity;
            mentalHospital.DeletePsychologyHero(ag);
        }
        else if (buttonName == "Arrogant")
        {
            Arrogant ar = mentalHospital.Hero.Psycho.Find(t => t.Name == "Arrogant") as Arrogant;
            mentalHospital.DeletePsychologyHero(ar);
        }
        else if (buttonName == "Crazyness")
        {
            Crazyness c = mentalHospital.Hero.Psycho.Find(t => t.Name == "Crazyness") as Crazyness;
            mentalHospital.DeletePsychologyHero(c);
        }
        else if (buttonName == "Fragil")
        {
            Fragil f = mentalHospital.Hero.Psycho.Find(t => t.Name == "Fragil") as Fragil;
            mentalHospital.DeletePsychologyHero(f);
        }

        MentalSicknessRemove = true;
        mentalHospital.SetHero(mentalHospital.Hero);
        GameObject.Find("RemoveHero").SetActive(false);
        CheckSicknesses(mentalHospital.Hero, mentalHospital);
    }

    public static void SetToInactiveButton(Button button)
    {
        button.GetComponent<Image>().color = Color.gray;
        button.enabled = false;
    }
    public static void SetToActiveButton(Button button)
    {
        button.GetComponent<Image>().color = Color.white;
        button.enabled = true;
    }

    public void RemoveHero()
    {
        MentalHospital mentalHospital = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.MentalHospital) as MentalHospital;
        if (mentalHospital.Hero != null)
        {
            BaseHeros h = mentalHospital.Hero;
            SetProfil.SetToActiveButton(Start.pHeroes.Find(t => t.GetComponentInChildren<Text>().text == h.CharacterName).GetComponentInChildren<Button>());

            mentalHospital.DeleteHero();
            GameObject IconeHero = GameObject.Find("MentalHospitalHero");
            IconeHero.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/noprofil");
            InitializedButtonsMentalHospitalBoard();
        }
    }

    public static void InitializedButtonsMentalHospitalBoard()
    {
        MentalSicknessRemove = false;
        foreach (Button button in Start.ButtonsMentalPsycho)
        {
            if (button.name != "Close")
                if (button.name != "RemoveHero")
                    SetToInactiveButton(button);
        }
    }

    public static bool isInBuilding()
    {
        MentalHospital mentalHospital = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.MentalHospital) as MentalHospital;
        if (mentalHospital.Hero != null && mentalHospital.Hero.InBuilding != null)
            return true;
        else
            return false;
    }

    public static void CheckSicknesses(BaseHeros heros, BaseBuilding building)
    {

        for (int i = 0; i < Start.ButtonsMentalPsycho.Length; i++)
        {
            Button button = Start.ButtonsMentalPsycho[i];
            if (button.name != "Close")
            {
                if (button.name != "RemoveHero")
                {
                    button.GetComponentsInChildren<Text>()[1].text = "" + (1000 / building.Level);
                    SetToInactiveButton(button);
                }

            }

        }
        for (int i = 0; i < heros.Psycho.Count; i++)
        {
            for (int j = 0; j < Start.ButtonsMentalPsycho.Length; j++)
            {
                if (heros.Psycho[i].Name == Start.ButtonsMentalPsycho[j].name)
                {
                    SetToActiveButton(Start.ButtonsMentalPsycho[j]);
                }
            }
        }
    }
}
