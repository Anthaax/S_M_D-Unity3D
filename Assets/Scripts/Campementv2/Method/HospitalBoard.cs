using UnityEngine;
using System.Collections;
using S_M_D.Camp.Class;
using S_M_D.Character;
using S_M_D.Camp;
using UnityEngine.UI;

public class HospitalBoard : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TryRemoveSickness()
    {
        Hospital hospital = Start.Gtx.PlayerInfo.GetBuilding( BuildingNameEnum.Hospital) as Hospital;
        string buttonName = gameObject.name;
        if (buttonName == "Fever")
        {
            Fever f = hospital.Hero.Sicknesses.Find(t => t.Name == "Fever") as Fever;
            hospital.HealHero(f);
        }
        else if(buttonName == "Cancer")
        {
            Cancer c = hospital.Hero.Sicknesses.Find(t => t.Name == "Cancer") as Cancer;
            hospital.HealHero(c);
        }
        else if (buttonName == "Diarrhea")
        {
            Diarrhea d = hospital.Hero.Sicknesses.Find(t => t.Name == "Diarrhea") as Diarrhea;
            hospital.HealHero(d);
        }
        else if (buttonName == "Staphyloccocus")
        {
            Staphyloccocus s = hospital.Hero.Sicknesses.Find(t => t.Name == "Staphyloccocus") as Staphyloccocus;
            hospital.HealHero(s);
        }

        CheckSicknesses(hospital.Hero, hospital);
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
        Hospital hospital = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Hospital) as Hospital;
        if(hospital.Hero != null)
        {
            BaseHeros h = hospital.Hero;
            SetProfil.SetToActiveButton(Start.pHeroes.Find(t => t.GetComponentInChildren<Text>().text == h.CharacterName).GetComponentInChildren<Button>());
            hospital.deleteHeros();
            GameObject IconeHero = GameObject.Find("HospitalHero");
            IconeHero.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/noprofil");
            InitializedButtonsHospitalBoard();
        }
    }

    public static void InitializedButtonsHospitalBoard()
    {
        foreach (Button button in Start.ButtonsSicknesses)
        {
            if (button.name != "Close")
                if (button.name != "RemoveHero")
                    SetToInactiveButton(button);
        }
    }

    public static void CheckSicknesses(BaseHeros heros, BaseBuilding building)
    {
       
        for (int i = 0; i < Start.ButtonsSicknesses.Length; i++)
        {
            Button button = Start.ButtonsSicknesses[i];
            if (button.name != "Close")
            {
                if(button.name != "RemoveHero")
                {
                    button.GetComponentsInChildren<Text>()[1].text = "" + (1000 / building.Level);
                    SetToInactiveButton(button);
                }
                
            }

        }
        for (int i = 0; i < heros.Sicknesses.Count; i++)
        {
            for (int j = 0; j < Start.ButtonsSicknesses.Length; j++)
            {
                if (heros.Sicknesses[i].Name == Start.ButtonsSicknesses[j].name)
                {
                    SetToActiveButton(Start.ButtonsSicknesses[j]);
                }
            }
        }
    }
}
