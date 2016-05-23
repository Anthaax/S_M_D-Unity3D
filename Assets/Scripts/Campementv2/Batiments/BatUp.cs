using UnityEngine;
using System.Collections;
using S_M_D.Camp;

public class BatUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        string name = gameObject.name;
        foreach(BaseBuilding building in BaseCampement.ListeOfBuildings)
        {

            if (building.Name.ToString() == name)
            {
                BaseCampement.ActivBuilding = building;
                Debug.Log(BaseCampement.ActivBuilding.Name);
            }
            
        }
    }
}
