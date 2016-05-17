using UnityEngine;
using System.Collections;
using S_M_D.Camp.Class;

public class HospitalScript : MonoBehaviour
{

    private GameObject _hospitalGameObject;
    private Hospital _hospital;

    // Use this for initialization
    void Start()
    {
        _hospitalGameObject = GameScript.BuildingsGameObjects.Find(b => b.name == BuildingName.Hospital.ToString());
        _hospital = GameScript.GameContext.PlayerInfo.GetBuilding(BuildingName.Hospital) as Hospital;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void showName()
    {
        var position = Camera.main.WorldToScreenPoint(_hospitalGameObject.transform.position);
        var size = Camera.main.WorldToScreenPoint(_hospitalGameObject.GetComponent<Collider2D>().bounds.size);
        GUI.Box(new Rect(position.x - (size.x / 20), position.y - (size.y / 1.3f), 60, 22), _hospital.Name.ToString());
        
    }

    private bool CanShowNews()
    {
        return _hospital.Level > 0;
    }
    void OnGUI()
    {
        if (CanShowNews())
        {
            showName();
        }
    }
}
