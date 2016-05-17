using UnityEngine;
using System.Collections;
using S_M_D.Camp.Class;

public class ArmoryScript : MonoBehaviour
{

    private GameObject _armoryGameObject;
    private Armory _armory;

    // Use this for initialization
    void Start()
    {
        _armoryGameObject = GameScript.BuildingsGameObjects.Find(b => b.name == BuildingName.Armory.ToString());
        _armory = GameScript.GameContext.PlayerInfo.GetBuilding(BuildingName.Armory) as Armory;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void showName()
    {
        var position = Camera.main.WorldToScreenPoint(_armoryGameObject.transform.position);
        var size = Camera.main.WorldToScreenPoint(_armoryGameObject.GetComponent<Collider2D>().bounds.size);
        GUI.Box(new Rect(position.x - (size.x/16), position.y + (size.y/3), 60, 22), _armory.Name.ToString());

        //Debug.Log("x = " + size.x + " ; y = " + size.y);
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
        }
    }
}
