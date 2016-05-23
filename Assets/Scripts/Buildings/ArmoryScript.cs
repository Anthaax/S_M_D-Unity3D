using UnityEngine;
using System.Collections;
using S_M_D.Camp.Class;
using S_M_D.Character;

public class ArmoryScript : MonoBehaviour
{
    private GameObject _armoryGameObject;
    private Armory _armory;
    private bool _popUp;
    private BaseHeros _hero;
    private Rect _windowInfo;
    int widhtRect = 300, heightRect = 200;
    int widhtClose = 18, heightClose = 18;
    int decalage = 25;


    // Use this for initialization
    void Start()
    {
        _armoryGameObject = GameScript.BuildingsGameObjects.Find(b => b.name == BuildingName.Armory.ToString());
        _armory = GameScript.GameContext.PlayerInfo.GetBuilding(BuildingName.Armory) as Armory;
        _hero = null;
        _windowInfo = new Rect((Screen.width / 2) - (widhtRect / 2), (Screen.height / 2) - (heightRect / 2), widhtRect, heightRect);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void showName()
    {
        var position = Camera.main.WorldToScreenPoint(_armoryGameObject.transform.position);
        var size = Camera.main.WorldToScreenPoint(_armoryGameObject.GetComponent<Collider2D>().bounds.size);
        GUI.Box(new Rect(position.x - (size.x / 9.5f), position.y - (size.y / 1.25f), 100, 22), _armory.Name.ToString());

    }

    private bool CanShowNews()
    {
        return _armory.Level > 0;
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
            _windowInfo = GUI.Window(0, _windowInfo, winFunction, _armory.Name.ToString() + " Lv " + _armory.Level);
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
        if (_hero != null)
        {
            GUI.Label(new Rect(20, 20 + (decalage), 100, 50), "" + _hero.CharacterClassName);
            GUI.Label(new Rect(20 + 100, 20 + (decalage), 100, 50), "" + _hero.CharacterName);
            GUI.Label(new Rect(20 + 160, 20 + (decalage), 100, 50), "" + _hero.Price);
            string action = _hero.EffectivHPMax == 100 ? "RMV" : "ADD";
            if (GUI.Button(new Rect(20 + 210, 20 + (decalage), 50, 20), action))
            {
                if (action == "RMV")
                    _armory.Hero = _hero;
                else
                    _armory.deleteHero();
            }
        }
    }
}
