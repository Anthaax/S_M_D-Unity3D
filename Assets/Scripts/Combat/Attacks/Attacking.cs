using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using S_M_D.Character;
using S_M_D.Combat;
using System;
using System.Collections.Generic;
using S_M_D.Spell;
using System.Linq;

public class Attacking : MonoBehaviour
{

    public void OnClick()
    {
        if (StartCombat.Attack.Spell != null && StartCombat.Attack.Target >= 0)
        {

            StartCoroutine( Movement() );
          

        }

        StartCoroutine(Wait());
    }

    void Update()
    {
        BaseMonster monster = StartCombat.Combat.GetCharacterTurn() as BaseMonster;

        if (monster != null)
        {
            StartCoroutine( MonsterAttack( monster ) );
            Timer T = GameObject.Find( "Timer" ).GetComponent<Timer>();
            T.timeLeft = 30.0f;
        }



    }

    public IEnumerator Movement()
    {
        int y = 0;
        int x = 1;
        BaseHeros H = StartCombat.Combat.GetCharacterTurn() as BaseHeros;
        foreach (BaseHeros HS in StartCombat.Combat.Heros)
        {
            if (H == HS)
            {
                y = x;
            }
            x++;
        }
        Animator animator = GameObject.Find( "Heros" + y ).GetComponent<Animator>();
        animator.Play( H.CharacterClassName + "Attack" );     
        yield return new WaitForSeconds( 2 );
        Debug.Log("Monstre n°" + StartCombat.Attack.Target);
        StartCombat.Combat.SpellManager.HeroLaunchSpell(StartCombat.Attack.Spell, StartCombat.Attack.Target);
        StartCombat.Combat.NextTurn();
        StartCombat.Attack.Target = -1;
        Timer T = GameObject.Find("Timer").GetComponent<Timer>();
        T.timeLeft = 30.0f;
        animator.Play( H.CharacterClassName + "Idle" );

    }

    public IEnumerator MonsterAttack( BaseMonster Monster )
    {
        int y = 0;
        int x = 1;
        foreach (BaseMonster M in StartCombat.Combat.Monsters)
        {
            if (Monster == M)
            {
                y = x;
            }
            x++;
        }
        Animator animator = GameObject.Find("Monstre" + y).GetComponent<Animator>();
        animator.Play(Monster.Type+"Attack");
        StartCombat.Combat.IaMonster.MonsterTurnAndDoNextTurn(Monster);
        GameObject.Find( "SpellsAttack" ).GetComponent<Text>().text = MonsterAction();
        StartCombat.Combat.IaMonster.MosterAction.Clear();

        yield return new WaitForSeconds( 3 );
        animator.Play(Monster.Type + "Idle");
        GameObject.Find("SpellsAttack").GetComponent<Text>().text = "";
    }
    public static void AfterAction()
    {
        GameObject.Find( "SpellInfo" ).GetComponent<Text>().text = "";
        for (int i = 1; i < 5; i++)
        {
            if (GameObject.Find( "Arrow" + i ) != null)
            {
                GameObject.Find( "Arrow" + i ).GetComponent<Image>().sprite = Resources.Load<Sprite>( "Sprites/Combat/1024px-Red_Arrow_Down.svg" );
                GameObject.Find( "Arrow" + i ).GetComponent<Image>().enabled = false;
            }
        }
    }
    private string MonsterAction()
    {
        if (StartCombat.Combat.IaMonster.MosterAction.Count != 0)
            return StartCombat.Combat.IaMonster.MosterAction.Keys.First().Name;
        else
            return "Changement de position";
    }
    
    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        AfterAction();
    }
}
