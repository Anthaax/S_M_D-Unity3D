using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using S_M_D.Character;
using S_M_D.Combat;
using System;
using System.Collections.Generic;
using S_M_D.Spell;
using System.Linq;

public class Attacking : MonoBehaviour {

    public void OnClick()
    {
        if (BaseCombat.Attack.Spell != null && BaseCombat.Attack.Monster >= 0) 
        {

            StartCoroutine(Movement());
            Debug.Log( "Monstre n°" + BaseCombat.Attack.Monster );
            BaseCombat.Combat.SpellManager.HeroLaunchSpell(BaseCombat.Attack.Spell, BaseCombat.Attack.Monster);
            BaseCombat.Combat.NextTurn();
            BaseCombat.Attack.Monster = -1;
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

    void Update()
    {
        BaseMonster monster = BaseCombat.Combat.GetCharacterTurn() as BaseMonster;

        if (monster != null)
        {
            StartCoroutine( MonsterAttack( monster ) );
            BaseCombat.Combat.NextTurn();
            Timer T = GameObject.Find( "Timer" ).GetComponent<Timer>();
            T.timeLeft = 30.0f;
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

    public IEnumerator MonsterAttack(BaseMonster Monster)
    {
        Debug.Log( Monster.Spells.Count );
        List<Spells> canLauncSpell = Monster.Spells
                                    .Where( c => c.TargetManager.WhoCanBeTargetable( Monster.Position ) != new bool[4] { false, false, false, false } )
                                    .Where( c => c.CooldownManager.IsOnCooldown == false )
                                    .ToList();
        Debug.Log( canLauncSpell.Count );
        Spells spellToLaunch = canLauncSpell[BaseCombat.Combat.GameContext.Rnd.Next( canLauncSpell.Count )];
        int position = BaseCombat.Combat.GameContext.Rnd.Next( 4 );
        if (canLauncSpell.Count > 0)
        {
            BaseCombat.Combat.SpellManager.MonsterLaunchSpell( spellToLaunch, position );
            GameObject.Find( "SpellsAttack" ).GetComponent<Text>().text = spellToLaunch.Description;
        }
            
        yield return new WaitForSeconds( 2 );
        GameObject.Find( "SpellsAttack" ).GetComponent<Text>().text = "";
        

    }
}
