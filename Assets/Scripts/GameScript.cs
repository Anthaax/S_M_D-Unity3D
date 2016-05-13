using UnityEngine;
using System.Collections;
using S_M_D;
using System.Collections.Generic;
using Assets.TagsName;
using S_M_D.Camp.Class;

public class GameScript : MonoBehaviour {

    private static List<GameObject> _buildingsGameObjects;
    private static Dictionary<string, Sprite> _buildingsSpritesDico;
    public List<Sprite> _spritesOfBuildings;
    private static GameContext _gameContext;

    void Awake()
    {
        _buildingsSpritesDico = new Dictionary<string, Sprite>();
        _buildingsGameObjects = new List<GameObject>();
        _gameContext = GameContext.CreateNewGame();

        InitializeBuildingsSprites();
        InitializeBuildingsGameObjects();
        PrintBuildings();
    }
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
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
            _buildingsGameObjects.Add(list[i]);
        }
        AddBuilding(BuildingName.Townhall.ToString());
    }

    private static void AddColliderToObjects(string name)
    {
        for (int i = 0; i < _buildingsGameObjects.Count; i++)
        {
            if (_buildingsGameObjects[i].name == name)
            {
                _buildingsGameObjects[i].AddComponent<BoxCollider>();
                break;
            }
        }
    }

    public static void AddBuilding(string name)
    {
        GameObject building = _buildingsGameObjects.Find(t => t.name == name);
        AddColliderToObjects(name);
        Sprite sp = null;
        _buildingsSpritesDico.TryGetValue(name, out sp);
        building.GetComponent<SpriteRenderer>().sprite = sp;
    }

    public static List<GameObject> BuildingsGameObjects { get { return _buildingsGameObjects; } set { _buildingsGameObjects = value; } }
    public static Dictionary<string, Sprite> BuildingsSpritesDico { get { return _buildingsSpritesDico; } set { _buildingsSpritesDico = value; } }
}
