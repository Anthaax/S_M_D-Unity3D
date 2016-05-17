using UnityEngine;
using System.Collections;
using S_M_D.Camp.Class;

public class HotelScript : MonoBehaviour {

    private bool _popUp;
    private GameObject _hotelGameObject;
    private Hotel _hotel;


    // Use this for initialization
    void Start () {
        _hotelGameObject = GameScript.BuildingsGameObjects.Find(b => b.name == BuildingName.Hotel.ToString());
        _hotel = GameScript.GameContext.PlayerInfo.GetBuilding(BuildingName.Hotel) as Hotel;
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
        }
    }
}
