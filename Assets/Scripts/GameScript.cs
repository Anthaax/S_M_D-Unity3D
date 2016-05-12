using UnityEngine;
using System.Collections;
using S_M_D;
using System.Collections.Generic;
using Assets.TagsName;
using S_M_D.Camp.Class;

public class GameScript : MonoBehaviour {

    private static List<GameObject> _buildingsGameObjects;
    private static Dictionary<string, Sprite> _buildingsSprites;
    public List<Sprite> _spritesBuildings;

    void Awake()
    {
        _buildingsSprites = new Dictionary<string, Sprite>();
        _buildingsGameObjects = new List<GameObject>();

        InitializeBuildingsSprites();
        InitializeBuildingsGameObjects();
        AddColliderToObjects();
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
        foreach (KeyValuePair<string, Sprite> b in _buildingsSprites)
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
        for (int i = 0; i < _spritesBuildings.Count; i++)
        {
            if(_spritesBuildings[i].name == BuildingName.Armory.ToString())
                _buildingsSprites.Add(BuildingName.Armory.ToString(), _spritesBuildings[i]);
            else if(_spritesBuildings[i].name == BuildingName.Bar.ToString())
                _buildingsSprites.Add(BuildingName.Bar.ToString(), _spritesBuildings[i]);
            else if (_spritesBuildings[i].name == BuildingName.Caravan.ToString())
                _buildingsSprites.Add(BuildingName.Caravan.ToString(), _spritesBuildings[i]);
            else if (_spritesBuildings[i].name == BuildingName.Casern.ToString())
                _buildingsSprites.Add(BuildingName.Casern.ToString(), _spritesBuildings[i]);
            else if (_spritesBuildings[i].name == BuildingName.Cemetery.ToString())
                _buildingsSprites.Add(BuildingName.Cemetery.ToString(), _spritesBuildings[i]);
            else if (_spritesBuildings[i].name == BuildingName.Hospital.ToString())
                _buildingsSprites.Add(BuildingName.Hospital.ToString(), _spritesBuildings[i]);
            else if (_spritesBuildings[i].name == BuildingName.Hotel.ToString())
                _buildingsSprites.Add(BuildingName.Hotel.ToString(), _spritesBuildings[i]);
            else if (_spritesBuildings[i].name == BuildingName.MentalHospital.ToString())
                _buildingsSprites.Add(BuildingName.MentalHospital.ToString(), _spritesBuildings[i]);
            else if (_spritesBuildings[i].name == BuildingName.Townhall.ToString())
                _buildingsSprites.Add(BuildingName.Townhall.ToString(), _spritesBuildings[i]);
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
        GameObject townhall = _buildingsGameObjects.Find(t => t.name == BuildingName.Townhall.ToString());
        Sprite sp = null;
        _buildingsSprites.TryGetValue(BuildingName.Townhall.ToString(), out sp);
        townhall.GetComponent<SpriteRenderer>().sprite = sp;
    }

    private void AddColliderToObjects()
    {
        for (int i = 0; i < _buildingsGameObjects.Count; i++)
        {
           _buildingsGameObjects[i].AddComponent<BoxCollider>();
        }
    }

    public static void AddBuilding(string name)
    {
        GameObject building = _buildingsGameObjects.Find(t => t.name == name);
        Sprite sp = null;
        _buildingsSprites.TryGetValue(name, out sp);
        building.GetComponent<SpriteRenderer>().sprite = sp;
    }

    public static List<GameObject> BuildingsGameObjects { get { return _buildingsGameObjects; } set { _buildingsGameObjects = value; } }
    public static Dictionary<string, Sprite> BuildingsSprites { get { return _buildingsSprites; } set { _buildingsSprites = value; } }
}
