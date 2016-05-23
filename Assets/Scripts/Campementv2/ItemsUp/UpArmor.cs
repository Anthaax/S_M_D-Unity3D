using UnityEngine;
using System.Collections;
using S_M_D.Camp.Class;

public class UpArmor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        if (BaseCampement.ActivHeros != null)
        {
            Armory armory = BaseCampement.ActivBuilding as Armory;
            if (gameObject.name == "Armor")
                armory.UpgrateItemOfAnHero(BaseCampement.ActivHeros.Equipement[0]);
            else if (gameObject.name == "Weapon")
                armory.UpgrateItemOfAnHero(BaseCampement.ActivHeros.Equipement[1]);
            else if (gameObject.name == "Trinket1")
                armory.UpgrateItemOfAnHero(BaseCampement.ActivHeros.Equipement[2]);
            else if (gameObject.name == "Trinket2")
                armory.UpgrateItemOfAnHero(BaseCampement.ActivHeros.Equipement[3]);

        }
    }
}
