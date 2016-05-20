using UnityEngine;
using System.Collections;
using S_M_D.Camp.Class;

public class CemeteryScript : MonoBehaviour {

    private GameObject _cemeteryGameObject;
    private Cemetery _cemetery;

    // Use this for initialization
    void Start () {
        _cemeteryGameObject = GameScript.BuildingsGameObjects.Find(b => b.name == BuildingName.Cemetery.ToString());
        _cemetery = GameScript.GameContext.PlayerInfo.GetBuilding(BuildingName.Cemetery) as Cemetery;
    }
	// Update is called once per frame
	void Update () {
	
	}

    private void showName()
    {
        var position = Camera.main.WorldToScreenPoint(_cemeteryGameObject.transform.position);
        var size = Camera.main.WorldToScreenPoint(_cemeteryGameObject.GetComponent<Collider2D>().bounds.size);
        GUI.Box(new Rect(position.x - (size.x / 15), position.y + (size.y/1.4f), 70, 22), _cemetery.Name.ToString());
    }

    private bool CanShowNews()
    {
        return _cemetery.Level > 0;
    }
    void OnGUI()
    {
        if (CanShowNews())
        {
            showName();
        }
    }
}
