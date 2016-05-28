using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using S_M_D.Camp.Class;
using S_M_D.Camp;
using System.Linq;

public class UpdateLevel : MonoBehaviour {

    void start()
    {

    }
	// Update is called once per frame
	void Update () {
        string Te = gameObject.GetComponent<Text>().text;
        string firstWord = Te.Split(' ').First();
        TownHall t = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Townhall) as TownHall;
        foreach (BaseBuilding B in Start.Gtx.PlayerInfo.MyBuildings)
        {
            if (B.Name.ToString() == firstWord)
                gameObject.GetComponent<Text>().text = firstWord+" lvl" + B.Level;
        }

    }
}
