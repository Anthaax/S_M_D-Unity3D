using UnityEngine;
using System.Collections;
using S_M_D.Camp.Class;
using System.Collections.Generic;
using S_M_D.Character;

public class BarScript : MonoBehaviour {

    private GameObject _barGameObject;
    private Bar _bar;
    private bool _popUp;
    private List<BaseHeros> _heroesAvailable;


    private Rect _windowInfo;
    int widhtRect = 300, heightRect = 200;
    int widhtClose = 18, heightClose = 18;
    int decalage = 25;

    // Use this for initialization
    void Start () {
        _barGameObject = GameScript.BuildingsGameObjects.Find(b => b.name == BuildingName.Bar.ToString());
        _bar = GameScript.GameContext.PlayerInfo.GetBuilding(BuildingName.Bar) as Bar;
        _heroesAvailable = GameScript.GameContext.PlayerInfo.MyHeros;

        _windowInfo = new Rect((Screen.width / 2) - (widhtRect / 2), (Screen.height / 2) - (heightRect / 2), widhtRect, heightRect);
    }

    // Update is called once per frame
    void Update () {
	
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
            _windowInfo = GUI.Window(0, _windowInfo, winFunction, _bar.Name.ToString() + " Lv " + _bar.Level);
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
        for (int j = 0; j < _heroesAvailable.Count; j++)
        {
            BaseHeros hero = _heroesAvailable[j];
            GUI.Label(new Rect(20, 20 + (decalage * i), 100, 50), "" + hero.CharacterClassName);
            GUI.Label(new Rect(20 + 100, 20 + (decalage * i), 100, 50), "" + hero.CharacterName);
            GUI.Label(new Rect(20 + 160, 20 + (decalage * i), 100, 50), "" + hero.Price);
            string action = hero.EffectivHPMax == 100 ? "RMV" : "ADD";
            if (GUI.Button(new Rect(20 + 210, 20 + (decalage * i), 50, 20), action))
            {
                if (action == "RMV")
                    hero.EffectivHPMax = 20;
                else
                    hero.EffectivHPMax = 100;
            }
            i++;

        }

    }

    private void showName()
    {
        var position = Camera.main.WorldToScreenPoint(_barGameObject.transform.position);
        var size = Camera.main.WorldToScreenPoint(_barGameObject.GetComponent<Collider2D>().bounds.size);
        GUI.Box(new Rect(position.x - (size.x / 40), position.y + (size.y / 4), 35, 22), _bar.Name.ToString());
    }

    private bool CanShowNews()
    {
        return _bar.Level > 0;
    }
    void OnGUI()
    {
        if(CanShowNews())
        {
            showName();
            showWindow();
        }
    }
}
