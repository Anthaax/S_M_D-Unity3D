using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using S_M_D.Camp.Class;
using S_M_D.Camp;
using Assets.Enumeration;
using S_M_D;

public class TownHallScript : MonoBehaviour {

    private bool _popUp;
    private Rect _windowInfo;
    private List<BaseBuilding> _buildings;
    private TownHall _townHall;
    private MoneyManager _moneyManager;
    private GameObject _townHallGameObject;

    int widhtRect = 300, heightRect = 300;
    int widhtClose = 18, heightClose = 18;
    int decalage = 30;

    // Use this for initialization
    void Start()
    {
        _townHall = GameScript.GameContext.PlayerInfo.GetBuilding(BuildingName.Townhall) as TownHall;
        _moneyManager = GameScript.GameContext.MoneyManager;
        _buildings = GameScript.GameContext.PlayerInfo.MyBuildings;

        _townHallGameObject = GameScript.BuildingsGameObjects.Find(b => b.name == BuildingName.Townhall.ToString());
        _windowInfo = new Rect((Screen.width / 2) - (widhtRect / 2), (Screen.height / 2) - (heightRect / 2), widhtRect, heightRect);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (!GameScript.PopStats)
            _popUp = true;
    }

    void showWindow()
    {
        if (_popUp)
        {
            _windowInfo = GUI.Window(0, _windowInfo, winFunction, _townHall.Name.ToString());
            GameScript.PopStats = true;
        }

    }

    void winFunction(int windowID)
    {
        if (GUI.Button(new Rect(widhtRect - (widhtClose + 2), 2, widhtClose, heightClose), "x"))
        {
            _popUp = false;
            GameScript.PopStats = _popUp;
        }
        int i = 1;
        for (int j = 0; j < _buildings.Count; j++)
        {
            BaseBuilding building = _buildings[j];
            if(!GameScript.IsParticularBuilding(building.Name.ToString()))
            {
                GUI.Label(new Rect(20, 20 + (decalage * i), 100, 50), "" + building.Name.ToString());
                GUI.Label(new Rect(20 + 100, 20 + (decalage * i), 100, 50), "" + building.BuildingCost);
                GUI.Label(new Rect(20 + 160, 20 + (decalage * i), 100, 50), "Lv " + building.Level);
                BuildingAction action = building.Level < 1 ? BuildingAction.BUY : BuildingAction.UPG;
                if (GUI.Button(new Rect(20 + 210, 20 + (decalage * i), 50, 20), action.ToString()))
                {
                    Action(building.Name, action);
                }
                i++;
            }
            
        }

    }

    void Action(BuildingName name, BuildingAction action)
    {
        BaseBuilding building = GameScript.GameContext.PlayerInfo.GetBuilding(name);
        if(_moneyManager.CanBuy(building.BuildingCost))
        {
            switch (action)
            {
                case BuildingAction.BUY:
                    _moneyManager.Buy(building.BuildingCost);
                    _townHall.BuyBuilding(_buildings.Find(b => b.Name == name));
                    GameScript.AddBuilding(name.ToString());
                    break;
                case BuildingAction.UPG:
                    _townHall.UpgradeBuilding(_buildings.Find(b => b.Name == name));
                    _moneyManager.Buy(building.BuildingCost);
                    break;
            }
            Debug.Log("Dans l'action !! Money : "+_moneyManager.Money);
        }
        
        
    }

    private void showName()
    {
        var position = Camera.main.WorldToScreenPoint(_townHallGameObject.transform.position);
        var size = Camera.main.WorldToScreenPoint(_townHallGameObject.GetComponent<Collider2D>().bounds.size);
        GUI.Box(new Rect(position.x - (size.x / 20), position.y - (size.y / 3.5f), 60, 22), _townHall.Name.ToString());
    }

    private bool CanShowNews()
    {
        return _townHall.Level > 0;
    }

    

    void OnGUI()
    {
        if (CanShowNews())
        {
            showName();
            showWindow();
        }
        
    }
}
