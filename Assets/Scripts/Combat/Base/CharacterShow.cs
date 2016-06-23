using UnityEngine;
using System.Collections;
using S_M_D.Character;
using System.Linq;

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


            Animator animator = GameObject.Find("Heros" + i).GetComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load("Animations/AnimationsController/" + H.CharacterClassName + "F") as RuntimeAnimatorController;


            if (H.IsMale)
                GameObject.Find("Heros"+i).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Combat/Characters/Heros/" + H.CharacterClassName+"M");
            else
                GameObject.Find("Heros" + i).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Combat/Characters/Heros/" + H.CharacterClassName + "F");
            i++;
        }
        foreach (BaseMonster M in monsters)
        {
            if (M != null)
            GameObject.Find("Monstre" + j).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Combat/Characters/Monster/"+M.Type );
            else
            {
                if (GameObject.Find("Monstre" + j) != null)
                GameObject.Find("Monstre" + j).SetActive(false);
                if (GameObject.Find("Arrow" + j)!= null)
                GameObject.Find("Arrow" + j).SetActive(false);
            }

            j++;
        }
        

    }
}
