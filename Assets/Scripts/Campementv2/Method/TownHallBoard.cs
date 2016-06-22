using UnityEngine;
using System.Collections;
using S_M_D.Camp.Class;
using UnityEngine.UI;
using S_M_D.Camp;
using System.Collections.Generic;

public class TownHallBoard : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BuyBuilding()
    {
        string buildingName = gameObject.name;
        TownHall townHall = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Townhall) as TownHall;

        if (buildingName == "Armory")
        {
            Armory building = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Armory) as Armory;
            townHall.BuyBuilding(building);
        }
        else if(buildingName == "Bar")
        {
            Bar building = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Bar) as Bar;
            townHall.BuyBuilding(building);
        }
        else if (buildingName == "Caravan")
        {
            Caravan building = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Caravan) as Caravan;
            townHall.BuyBuilding(building);
        }
        else if (buildingName == "Casern")
        {
            Casern building = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Casern) as Casern;
            townHall.BuyBuilding(building);
        }
        else if (buildingName == "Hospital")
        {
            Hospital building = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Hospital) as Hospital;
            townHall.BuyBuilding(building);
        }
        else if (buildingName == "MentalHospital")
        {
            MentalHospital building = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.MentalHospital) as MentalHospital;
            townHall.BuyBuilding(building);
        }
        else if (buildingName == "Hotel")
        {
            Hotel building = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Hotel) as Hotel;
            townHall.BuyBuilding(building);
        }

        UpdatePanelBuilding(buildingName);
        UpdateBoard();
    }

    private void UpdatePanelBuilding(string buildingName)
    {
        Debug.Log("Entrer !!!!!!!");
        foreach(GameObject building in Start.ButtonsBuildings)
        {
            if(building.name == buildingName)
            {
                Debug.Log("Entrer 222 !!!!!!!");
                building.GetComponent<Button>().enabled = true;
                building.GetComponent<Button>().image.sprite = Resources.Load<Sprite>("Sprites/Buildings/" + buildingName);
                break;
            }
        }
    }


    public void UpgradeBuilding()
    {
        string buildingName = gameObject.name;
        TownHall townHall = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Townhall) as TownHall;

        if (buildingName == "Armory")
        {
            Armory building = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Armory) as Armory;
            building.LevelUP();
        }
        else if (buildingName == "Bar")
        {
            Bar building = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Bar) as Bar;
            building.LevelUP();
        }
        else if (buildingName == "Caravan")
        {
            Caravan building = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Caravan) as Caravan;
            building.LevelUP();
        }
        else if (buildingName == "Casern")
        {
            Casern building = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Casern) as Casern;
            building.LevelUP();
        }
        else if (buildingName == "Hospital")
        {
            Hospital building = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Hospital) as Hospital;
            building.LevelUP();
        }
        else if (buildingName == "MentalHospital")
        {
            MentalHospital building = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.MentalHospital) as MentalHospital;
            building.LevelUP();
        }
        else if (buildingName == "Hotel")
        {
            Hotel building = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Hotel) as Hotel;
            building.LevelUP();
        }

        UpdateBoard();
    }

    public void UpdateBoard()
    {
        List<Button> buttons = new List<Button>(Start.ButtonsTownHall);
        foreach (BaseBuilding b in Start.Gtx.PlayerInfo.MyBuildings)
        {
            if(b.Level > 0 && b.Name.ToString() != "Townhall" && b.Name.ToString() != "Cemetery")
            {
                SetToInactiveButton(buttons.Find(t => t.name == "Buy" + b.Name.ToString()));
                SetToActiveButton(buttons.Find(t => t.name == "Up" + b.Name.ToString()));
            }
            if (b.Level == 0 && b.Name.ToString() != "Townhall" && b.Name.ToString() != "Cemetery")
            {
                SetToInactiveButton(buttons.Find(t => t.name == "Up" + b.Name.ToString()));
            }
        }

        List<GameObject> infos = new List<GameObject>(GameObject.FindGameObjectsWithTag("InfoBTownHall"));
        foreach(GameObject g in infos)
        {
            if (g.name.EndsWith(BuildingNameEnum.Armory.ToString()))
            {
                BaseBuilding building = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Armory);
                g.GetComponent<Text>().text = BuildingNameEnum.Armory.ToString() + ". Lv: " + building.Level;
            }
            else if (g.name.EndsWith(BuildingNameEnum.Bar.ToString()))
            {
                BaseBuilding building = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Bar);
                g.GetComponent<Text>().text = BuildingNameEnum.Bar.ToString() + ". Lv: " + building.Level;
            }
            else if (g.name.EndsWith(BuildingNameEnum.Caravan.ToString()))
            {
                BaseBuilding building = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Caravan);
                g.GetComponent<Text>().text = BuildingNameEnum.Caravan.ToString() + ". Lv: " + building.Level;
            }
            else if (g.name.EndsWith(BuildingNameEnum.Casern.ToString()))
            {
                BaseBuilding building = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Casern);
                g.GetComponent<Text>().text = BuildingNameEnum.Casern.ToString() + ". Lv: " + building.Level;
            }
            else if (g.name.EndsWith(BuildingNameEnum.MentalHospital.ToString()))
            {
                BaseBuilding building = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.MentalHospital);
                g.GetComponent<Text>().text = BuildingNameEnum.MentalHospital.ToString() + ". Lv: " + building.Level;
            }
            else if (g.name.EndsWith(BuildingNameEnum.Hospital.ToString()))
            {
                BaseBuilding building = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Hospital);
                g.GetComponent<Text>().text = BuildingNameEnum.Hospital.ToString() + ". Lv: " + building.Level;
            }
            
            else if (g.name.EndsWith(BuildingNameEnum.Hotel.ToString()))
            {
                BaseBuilding building = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Hotel);
                g.GetComponent<Text>().text = BuildingNameEnum.Hotel.ToString() + ". Lv: " + building.Level;
            }
        }
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

}
