using UnityEngine;
using System.Collections;
using S_M_D.Character;
using System.Linq;

public class CharacterShow : MonoBehaviour {

    BaseHeros[] heros;
    BaseMonster[] monsters;
	// Use this for initialization
	void Start () {
        heros = BaseCombat.Combat.Heros;
        monsters = BaseCombat.Combat.Monsters;

        int i = 1;
        int j = 1;

        foreach (BaseHeros H in heros)
        {


            Animator animator = GameObject.Find("Heros" + i).GetComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load("Animations/AnimationsController/" + H.CharacterClassName + "F") as RuntimeAnimatorController;


            if (H.IsMale)
                GameObject.Find("Heros" + i).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Combat/Characters/Heros/" + H.CharacterClassName + "M");
            else
                GameObject.Find("Heros" + i).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Combat/Characters/Heros/" + H.CharacterClassName + "F");
            i++;
        }
    }
	
	// Update is called once per frame
	void Update () {

        heros = BaseCombat.Combat.Heros;
        monsters = BaseCombat.Combat.Monsters;

        int j = 1;
        int x = 1;

        foreach (BaseMonster M in monsters)
        {
            if (M.HP <=0)
            {
                Animator animator = GameObject.Find("Monstre" + j).GetComponent<Animator>();
                animator.SetBool("IsDead", true); 
            }
            
            j++;
        }

        foreach (BaseHeros H in heros)
        {
            if (H.HP <= 0)
            {
                Animator animator = GameObject.Find("Heros" + x).GetComponent<Animator>();
                animator.SetBool("IsDead", true);
            }

            x++;
        }


    }
}
