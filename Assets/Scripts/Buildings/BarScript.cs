using UnityEngine;
using System.Collections;
using S_M_D.Camp.Class;

public class BarScript : MonoBehaviour {

    private GameObject _barGameObject;
    private Bar _bar;
    
	// Use this for initialization
	void Start () {
        _barGameObject = GameScript.BuildingsGameObjects.Find(b => b.name == BuildingName.Bar.ToString());
        _bar = GameScript.GameContext.PlayerInfo.GetBuilding(BuildingName.Bar) as Bar;
        
    }
	
	// Update is called once per frame
	void Update () {
	
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
        }
    }
}
