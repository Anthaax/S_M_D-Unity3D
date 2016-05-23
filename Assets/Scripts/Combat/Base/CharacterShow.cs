using UnityEngine;
using System.Collections;
using S_M_D.Character;

public class CharacterShow : MonoBehaviour {

    BaseHeros[] heros;
    BaseMonster[] monsters;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        heros = BaseCombat.Combat.Heros;
        monsters = BaseCombat.Combat.Monsters;

        int i = 1;
        int j = 1;

        foreach (BaseHeros H in heros)
        {
            if (H.IsMale)
                GameObject.Find("Heros"+i).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Combat/Characters/Heros/" + H.CharacterClassName+"M");
            else
                GameObject.Find("Heros" + i).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Combat/Characters/Heros/" + H.CharacterClassName + "F");
            i++;
        }
        foreach (BaseMonster M in monsters)
        {
            
            GameObject.Find("Monstre" + j).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Combat/Characters/Monster/"+M.Type );
            j++;
        }

    }
}
