using UnityEngine;
using System.Collections;
using S_M_D.Camp.Class;
using System.Collections.Generic;
using S_M_D.Character;

public class CaravanScript : MonoBehaviour
{
    private bool _popUp;
    private GameObject _cavaranGameObject;
    private Caravan _caravan;
    private List<BaseHeros> _heroesAvailable;

    private Rect _windowInfo;
    int widhtRect = 300, heightRect = 200;
    int widhtClose = 18, heightClose = 18;
    int decalage = 30;

    // Use this for initialization
    void Start()
    {
        _cavaranGameObject = GameScript.BuildingsGameObjects.Find(b => b.name == BuildingName.Caravan.ToString());
        _caravan = GameScript.GameContext.PlayerInfo.GetBuilding(BuildingName.Caravan) as Caravan;
        _caravan.Initialized();
        _heroesAvailable = _caravan.HerosDispo;
        
        _windowInfo = new Rect((Screen.width / 2) - (widhtRect / 2), (Screen.height / 2) - (heightRect / 2), widhtRect, heightRect);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if(!GameScript.PopStats)
            _popUp = true;
    }

    void showWindow()
    {
        if (_popUp)
        {
            _windowInfo = GUI.Window(0, _windowInfo, winFunction, _caravan.Name.ToString());
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
            if (GUI.Button(new Rect(20 + 210, 20 + (decalage * i), 50, 20), "BUY"))
            {
                Buy(hero);
            }
            i++;

        }

    }

    private void showName()
    {
        var position = Camera.main.WorldToScreenPoint(_cavaranGameObject.transform.position);
        var size = Camera.main.WorldToScreenPoint(_cavaranGameObject.GetComponent<Collider2D>().bounds.size);
        GUI.Box(new Rect(position.x - (size.x/15), position.y - (size.y + 20), 70, 22), _caravan.Name.ToString());
    }

    private void Buy(BaseHeros hero)
    {
        if (GameScript.GameContext.MoneyManager.Money >= hero.Price)
        {
            GameScript.GameContext.MoneyManager.Buy(hero.Price);
            _caravan.BuyHero(hero);
        }
    }

    private bool CanShowNews()
    {
        return _caravan.Level > 0;
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
