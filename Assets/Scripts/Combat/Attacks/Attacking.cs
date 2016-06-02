using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using S_M_D.Character;
using System;

public class Attacking : MonoBehaviour {

    public void OnClick()
    {
        if (BaseCombat.Attack.Spell!=null && BaseCombat.Attack.Monster!=0)
        {

            StartCoroutine(Movement());
            BaseCombat.Combat.SpellManager.HeroLaunchSpell(BaseCombat.Attack.Spell, BaseCombat.Attack.Monster);
            BaseCombat.Combat.NextTurn();

            Timer T = GameObject.Find("Timer").GetComponent<Timer>();
            T.timeLeft = 30.0f;

        }

        GameObject.Find("SpellInfo").GetComponent<Text>().text = "";
        for (int i =1; i<5; i++)
        {
            if (GameObject.Find("Arrow"+i) != null )
            {
                GameObject.Find("Arrow"+i).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Combat/1024px-Red_Arrow_Down.svg");
                GameObject.Find("Arrow" + i).GetComponent<Image>().enabled = false;
            }
        }
       
    }

    public IEnumerator Movement()
    {
        int y = 0;
        int x = 1;
        BaseHeros H = BaseCombat.Combat.GetCharacterTurn() as BaseHeros;
        foreach (BaseHeros HS in BaseCombat.Combat.Heros)
        {
            if (H == HS)
            {
                y = x;
            }
            x++;
        }
        GameObject.Find("Heros"+y).GetComponent<Transform>().transform.Translate(0, 3, 0);
        yield return new WaitForSeconds(2);
        GameObject.Find("Heros"+y).GetComponent<Transform>().transform.Translate(0, -3, 0);

    }
}
