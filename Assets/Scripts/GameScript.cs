using UnityEngine;
using System.Collections;
using S_M_D;
using S_M_D.Character;
using System.Collections.Generic;
using Assets.ParticularName;
using S_M_D.Camp.Class;
using S_M_D.Camp;
using UnityEngine.UI;
public class GameScript : MonoBehaviour {

    #region
    
    private static List<GameObject> _buildingsGameObjects;
    private static Dictionary<string, Sprite> _buildingsSpritesDico;
    public List<Sprite> _spritesOfBuildings;
    private static List<string> _particularBuildings;
    #endregion Buildings

    private static bool _popStats;
    private static bool _popDialog;
    private static Rect windowDialog;

    private static GameContext _gameContext;

    void Awake()
    {
        _buildingsSpritesDico = new Dictionary<string, Sprite>();
        _buildingsGameObjects = new List<GameObject>();
        _particularBuildings = new List<string>();
        _gameContext = GameContext.CreateNewGame();

        InitializeParticularBuildings();
        InitializeBuildingsSprites();
        InitializeBuildingsGameObjects();
        InitializeBuildingsClasses();
        PrintBuildings();


    }
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        DisplayMoney();
        
    }

    private void DisplayMoney()
    {
        int width = 100, height = 25;
        GUI.Box(new Rect(Screen.width / 2 - width / 2, 0, width, height), "Money : "+_gameContext.MoneyManager.Money);
    }

    public static void ShowDialog(string message)
    {
        int widhtRect = 150, heightRect = 100;
        windowDialog = new Rect((Screen.width / 2) - (widhtRect / 2), (Screen.height / 2) - (heightRect / 2), widhtRect, heightRect);
        windowDialog = GUI.Window(0, windowDialog, winFunction, "Information");
    }
    private static void winFunction(int windowID)
    {
        GUI.Label(new Rect(20, 0, 100, 50), "");
    }

    private void PrintBuildings()
    {
        /*for(int i = 0; i < _buildingsGameObjects.Count; i++)
        {
            Debug.Log(_buildingsGameObjects[i].name);
        }*/
        foreach (KeyValuePair<string, Sprite> b in _buildingsSpritesDico)
        {
            Debug.Log("Key = "+b.Key+" ; Value = "+b.Value);
        }
        /*for (int i = 0; i < _spritesBuildings.Count; i++)
        {
            Debug.Log(_spritesBuildings[i].name);
        }*/
    }

    private void InitializeParticularBuildings()
    {
        _particularBuildings.Add(BuildingName.Townhall.ToString());
        _particularBuildings.Add(BuildingName.Cemetery.ToString());
    }

    private void InitializeBuildingsSprites()
    {
        for (int i = 0; i < _spritesOfBuildings.Count; i++)
        {
            if(_spritesOfBuildings[i].name == BuildingName.Armory.ToString())
                _buildingsSpritesDico.Add(BuildingName.Armory.ToString(), _spritesOfBuildings[i]);
            else if(_spritesOfBuildings[i].name == BuildingName.Bar.ToString())
                _buildingsSpritesDico.Add(BuildingName.Bar.ToString(), _spritesOfBuildings[i]);
            else if (_spritesOfBuildings[i].name == BuildingName.Caravan.ToString())
                _buildingsSpritesDico.Add(BuildingName.Caravan.ToString(), _spritesOfBuildings[i]);
            else if (_spritesOfBuildings[i].name == BuildingName.Casern.ToString())
                _buildingsSpritesDico.Add(BuildingName.Casern.ToString(), _spritesOfBuildings[i]);
            else if (_spritesOfBuildings[i].name == BuildingName.Cemetery.ToString())
                _buildingsSpritesDico.Add(BuildingName.Cemetery.ToString(), _spritesOfBuildings[i]);
            else if (_spritesOfBuildings[i].name == BuildingName.Hospital.ToString())
                _buildingsSpritesDico.Add(BuildingName.Hospital.ToString(), _spritesOfBuildings[i]);
            else if (_spritesOfBuildings[i].name == BuildingName.Hotel.ToString())
                _buildingsSpritesDico.Add(BuildingName.Hotel.ToString(), _spritesOfBuildings[i]);
            else if (_spritesOfBuildings[i].name == BuildingName.MentalHospital.ToString())
                _buildingsSpritesDico.Add(BuildingName.MentalHospital.ToString(), _spritesOfBuildings[i]);
            else if (_spritesOfBuildings[i].name == BuildingName.Townhall.ToString())
                _buildingsSpritesDico.Add(BuildingName.Townhall.ToString(), _spritesOfBuildings[i]);
        }
        // 
    }

    private void InitializeBuildingsGameObjects()
    {
        var list = GameObject.FindGameObjectsWithTag(TagName.Building.ToString());
        for (int i = 0; i < list.Length; i++)
        {
            list[i].AddComponent<SpriteRenderer>();

            _buildingsGameObjects.Add(list[i]);
        }
    }

    private void InitializeBuildingsClasses()
    {
        List<BaseBuilding> buildings = _gameContext.PlayerInfo.MyBuildings;
        for(int i = 0; i < buildings.Count; i++)
        {
            if(buildings[i].Level > 0)
            {
                AddBuilding(buildings[i].Name.ToString());
            }
        }
    }

    private static void AddColliderToObjects(string name)
    {
        for (int i = 0; i < _buildingsGameObjects.Count; i++)
        {
            if (_buildingsGameObjects[i].name == name)
            {
                _buildingsGameObjects[i].AddComponent<BoxCollider2D>();
                Vector2 S = _buildingsGameObjects[i].GetComponent<SpriteRenderer>().sprite.bounds.size;
                _buildingsGameObjects[i].GetComponent<BoxCollider2D>().size = S;
                break;
            }
        }
    }

    public static void AddBuilding(string name)
    {
        GameObject building = _buildingsGameObjects.Find(t => t.name == name);
        Sprite sp = null;

        _buildingsSpritesDico.TryGetValue(name, out sp);
        building.GetComponent<SpriteRenderer>().sprite = sp;
        AddColliderToObjects(name);
    }

    public static bool IsParticularBuilding(string name)
    {
        return _particularBuildings.Contains(name);
    }


    public static List<GameObject> BuildingsGameObjects { get { return _buildingsGameObjects; } set { _buildingsGameObjects = value; } }
    public static Dictionary<string, Sprite> BuildingsSpritesDico { get { return _buildingsSpritesDico; } set { _buildingsSpritesDico = value; } }
    public static GameContext GameContext { get { return _gameContext; } set { _gameContext = value; } }

    public static bool PopStats { get { return _popStats; } set { _popStats = value; } }
    public static bool PopDialog { get { return _popDialog; } set { _popDialog = value; } }
}
