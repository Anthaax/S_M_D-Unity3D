using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using S_M_D.Character;
using Assets.Scripts.Combat.heros;
using UnityEngine.UI;

public class ShowSpells1 : MonoBehaviour {

    Combat combat;
    public List<Sprite> _sprites;
    public BaseHeros[] _heros;
    // Use this for initialization
    void Start () {
        HeroAndMonsterSprite heroSprit = new HeroAndMonsterSprite();
        combat = FindObjectOfType(typeof(Combat)) as Combat;
        _heros = Combat.Comba.Heros;

    }
	
	// Update is called once per frame
	void Update () {        
	
	}

    void OnMouseOver()
    {
        if (Input.GetMouseButton(0)){
            GameObject.Find("Spell1").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Combat/Characters/Spells/" + _heros[0].Spells[0].Name);
            GameObject.Find("Spell2").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Combat/Characters/Spells/" + _heros[0].Spells[1].Name);
            GameObject.Find("Spell3").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Combat/Characters/Spells/" + _heros[0].Spells[2].Name);
            GameObject.Find("Spell4").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Combat/Characters/Spells/" + _heros[0].Spells[3].Name);

            Combat.Heros = _heros[0];
            
        }
        
    }
}
