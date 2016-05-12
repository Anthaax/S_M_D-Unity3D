using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using S_M_D.Camp.Class;

public class TownHallScript : MonoBehaviour {

    private bool PopUp;
    public string Info;
    private Rect windowInfo;
    private List<GameObject> _buildingsGameObjects;

    int widhtRect = 300, heightRect = 200;
    int widhtClose = 18, heightClose = 18;
    int decalage = 30;

    // Use this for initialization
    void Start()
    {
        Info = "Informations";
        _buildingsGameObjects = GameScript.BuildingsGameObjects;
        windowInfo = new Rect((Screen.width / 2) - (widhtRect / 2), (Screen.height / 2) - (heightRect / 2), widhtRect, heightRect);
    }

    void OnMouseDown()
    {
        PopUp = true;
    }

    void showWindow()
    {
        if (PopUp)
        {
            windowInfo = GUI.Window(0, windowInfo, winFunction, "Inventaire");
        }

    }

    void winFunction(int windowID)
    {
        if (GUI.Button(new Rect(widhtRect - (widhtClose + 2), 2, widhtClose, heightClose), "x"))
        {
            PopUp = false;
        }
        int i = 1;
        for (int j = 0; j < _buildingsGameObjects.Count; j++)
        {
            GameObject building = _buildingsGameObjects[j];
            if(building.name != BuildingName.Townhall.ToString())
            {
                GUI.Label(new Rect(20, 20 + (decalage * i), 100, 50), "" + building.name);
                //GUI.Label(new Rect(20 + 100, 20 + (decalage * i), 100, 50), "" + building.Cost);
                string action = "BUY"; //building.Level < 1 ? "BUY" : "UPG";
                if (GUI.Button(new Rect(20 + 180, 20 + (decalage * i), 50, 20), action))
                {
                    Buy(building.name);
                }
                i++;
            }
            
        }

    }

    void Buy(string name)
    {
        GameScript.AddBuilding(name);
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnGUI()
    {
        showWindow();
    }
}
