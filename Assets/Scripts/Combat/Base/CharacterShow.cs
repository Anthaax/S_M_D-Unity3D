using UnityEngine;
using System.Collections;
using S_M_D.Character;
using System.Linq;

public class CharacterShow : MonoBehaviour {

    BaseHeros[] heros;
    BaseMonster[] monsters;
	// Use this for initialization
	void Start () {
        heros = StartCombat.Combat.Heros;
        monsters = StartCombat.Combat.Monsters;

        int i = 1;
        int j = 1;

        foreach (BaseHeros H in heros)
        {
            Animator animator = GameObject.Find("Heros" + i).GetComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load("Animations/AnimationsController/" + H.CharacterClassName + "F") as RuntimeAnimatorController;
            i++;
        }

        foreach(BaseMonster M in monsters)
        {
            Animator animator = GameObject.Find("Monstre" + j).GetComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load("Animations/AnimationsController/" + M.Type) as RuntimeAnimatorController;
            j++;
        }
    }
	
	// Update is called once per frame
	void Update () {

        heros = StartCombat.Combat.Heros;
        monsters = StartCombat.Combat.Monsters;

        int j = 1;
        int x = 1;

        foreach (BaseMonster M in monsters)
        {
            Animator animator = GameObject.Find("Monstre" + j).GetComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load("Animations/AnimationsController/" + M.Type) as RuntimeAnimatorController;
            if (M.HP <=0)
            {                
                animator.SetBool("IsDead", true); 
            }
            
            j++;
        }

        foreach (BaseHeros H in heros)
        {
            Animator animator = GameObject.Find("Heros" + x).GetComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load("Animations/AnimationsController/" + H.CharacterClassName + "F") as RuntimeAnimatorController;
            if (H.HP <= 0)
            {               
                animator.SetBool("IsDead", true);
            }

            x++;
        }


    }
}
