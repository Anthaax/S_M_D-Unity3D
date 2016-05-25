using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetProfil : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (BaseCampement.ActivHeros !=null)
        {
            if (BaseCampement.ActivBuilding==null)
            {
                GameObject.Find("Icone").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + BaseCampement.ActivHeros.CharacterClassName + "IconeF");
                GameObject.Find("AttackT").GetComponent<Text>().text = BaseCampement.ActivHeros.EffectivDamage.ToString();
                GameObject.Find("ArmorT").GetComponent<Text>().text = BaseCampement.ActivHeros.EffectivDefense.ToString();
                GameObject.Find("SpeedT").GetComponent<Text>().text = BaseCampement.ActivHeros.EffectivSpeed.ToString();

                if (BaseCampement.ActivHeros.InBuilding == null)
                    GameObject.Find("ArmoryHero").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + BaseCampement.ActivHeros.CharacterClassName + "IconeF");

            }

        }
        
    }
}
