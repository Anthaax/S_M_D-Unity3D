using UnityEngine;
using System.Collections;
using S_M_D.Camp.Class;

public class MentalHospitalScript : MonoBehaviour {

    private GameObject _mentalHospitalGameObject;
    private MentalHospital _mentalHospital;

    // Use this for initialization
    void Start () {
        _mentalHospitalGameObject = GameScript.BuildingsGameObjects.Find(b => b.name == BuildingName.MentalHospital.ToString());
        _mentalHospital = GameScript.GameContext.PlayerInfo.GetBuilding(BuildingName.MentalHospital) as MentalHospital;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void showName()
    {
        var position = Camera.main.WorldToScreenPoint(_mentalHospitalGameObject.transform.position);
        var size = Camera.main.WorldToScreenPoint(_mentalHospitalGameObject.GetComponent<Collider2D>().bounds.size);
        GUI.Box(new Rect(position.x - (size.x/9.5f), position.y - (size.y/1.25f), 100, 22), _mentalHospital.Name.ToString());

    }

    private bool CanShowNews()
    {
        return _mentalHospital.Level > 0;
    }
    void OnGUI()
    {
        if (CanShowNews())
        {
            showName();
        }
    }

}
