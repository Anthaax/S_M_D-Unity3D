using UnityEngine;
using System.Collections;
using S_M_D.Camp.Class;
using S_M_D.Character;

public class HotelScript : MonoBehaviour {

    private bool _popUp;
    private GameObject _hotelGameObject;
    private Hotel _hotel;

    private BaseHeros _hero1;
    private BaseHeros _hero2;


    private Rect _windowInfo;
    int widhtRect = 300, heightRect = 200;
    int widhtClose = 18, heightClose = 18;
    int decalage = 25;


    // Use this for initialization
    void Start () {
        _hotelGameObject = GameScript.BuildingsGameObjects.Find(b => b.name == BuildingName.Hotel.ToString());
        _hotel = GameScript.GameContext.PlayerInfo.GetBuilding(BuildingName.Hotel) as Hotel;
        _hotel.setHeros1( GameScript.GameContext.PlayerInfo.MyHeros[0]);
        _hotel.setHeros2( GameScript.GameContext.PlayerInfo.MyHeros[1]);
        _windowInfo = new Rect((Screen.width / 2) - (widhtRect / 2), (Screen.height / 2) - (heightRect / 2), widhtRect, heightRect);
    }
	
	// Update is called once per frame
	void Update () {
	}

    private void showName()
    {
        var position = Camera.main.WorldToScreenPoint(_hotelGameObject.transform.position);
        var size = Camera.main.WorldToScreenPoint(_hotelGameObject.GetComponent<Collider2D>().bounds.size);
        GUI.Box(new Rect(position.x - (size.x / 20), position.y - (size.y + 20), 60, 22), _hotel.Name.ToString());
    }

    private bool CanShowNews()
    {
        return _hotel.Level > 0;
    }

    void OnGUI()
    {
        if (CanShowNews())
        {
            showName();
            showWindow();
        }
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
            _windowInfo = GUI.Window(0, _windowInfo, winFunction, _hotel.Name.ToString() + " Lv " + _hotel.Level);
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
        if (_hotel.Hero1 != null)
        {
            GUI.Label(new Rect(20, 20 , 100, 50), "" + _hotel.Hero1.CharacterClassName);
            GUI.Label(new Rect(20 + 100, 20 , 100, 50), "" + _hotel.Hero1.CharacterName);
            GUI.Label(new Rect(20 + 160, 20 , 100, 50), "" + _hotel.Hero1.Price);
            if (GUI.Button(new Rect(20 + 210, 20 , 50, 20), "delete"))
            {
                    _hotel.deleteHeros();
            }
        }
        if (_hotel.Hero2 != null)
        {
            GUI.Label(new Rect(20, 20 + (decalage), 100, 50), "" + _hotel.Hero2.CharacterClassName);
            GUI.Label(new Rect(20 + 100, 20 + (decalage), 100, 50), "" + _hotel.Hero2.CharacterName);
            GUI.Label(new Rect(20 + 160, 20 + (decalage), 100, 50), "" + _hotel.Hero2.Price);
        }
        }
    }
