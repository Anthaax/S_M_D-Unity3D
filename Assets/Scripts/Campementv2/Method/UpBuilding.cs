using UnityEngine;
using System.Collections;
using S_M_D;
using S_M_D.Camp.Class;
using UnityEngine.UI;
using S_M_D.Camp;
using S_M_D.Camp.Class;

public class UpBuilding : MonoBehaviour {

    public void OnClick()
    {
        string name = gameObject.name;
        TownHall t = Start.Gtx.PlayerInfo.GetBuilding(BuildingName.Townhall) as TownHall;
        foreach (BaseBuilding B in Start.Gtx.PlayerInfo.MyBuildings)
        {
            if (B.Name.ToString() == name)
                t.UpgradeBuilding(B);               
        }

    }
}
