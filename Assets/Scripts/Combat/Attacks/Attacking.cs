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
        if (BaseCombat.Attack.Spell != null && BaseCombat.Attack.Target >= 0) 
        {

            StartCoroutine(Movement());
            if (BaseCombat.Attack.Spell.KindOfEffect.DamageType == DamageTypeEnum.Move)
                MoveAction();
            else
            {
                Debug.Log( "Monstre n°" + BaseCombat.Attack.Target );
                BaseCombat.Combat.SpellManager.HeroLaunchSpell( BaseCombat.Attack.Spell, BaseCombat.Attack.Target );
                BaseCombat.Combat.NextTurn();
            }
            BaseCombat.Attack.Target = -1;
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
            else if (BaseCombat.Attack.Spell.KindOfEffect.DamageType == DamageTypeEnum.Heal || BaseCombat.Attack.Spell.KindOfEffect.DamageType == DamageTypeEnum.Move)
            {
                //ajout de fleche pour les heros
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
        Animator animator = GameObject.Find("Heros" + y).GetComponent<Animator>();
        animator.Play(H.CharacterClassName+"Attack");
        yield return new WaitForSeconds(2);
        animator.Play(H.CharacterClassName + "Idle");

    }

    public IEnumerator MonsterAttack(BaseMonster Monster)
    {
        Debug.Log( Monster.Spells.Count );
        List<Spells> canLauncSpell = Monster.Spells
                                    .Where( c => c.TargetManager.WhoCanBeTargetable( Monster.Position ) != new bool[4] { false, false, false, false } )
                                    .Where( c => c.CooldownManager.IsOnCooldown == false )
                                    .ToList();
        Debug.Log( canLauncSpell.Count );
        if (canLauncSpell.Count > 0)
        {
            Spells spellToLaunch = canLauncSpell[BaseCombat.Combat.GameContext.Rnd.Next( canLauncSpell.Count )];
            int position = BaseCombat.Combat.GameContext.Rnd.Next( 4 );
            BaseCombat.Combat.SpellManager.MonsterLaunchSpell( spellToLaunch, position );
            GameObject.Find( "SpellsAttack" ).GetComponent<Text>().text = spellToLaunch.Description;
        }
            
        yield return new WaitForSeconds( 1 );
        
        GameObject.Find( "SpellsAttack" ).GetComponent<Text>().text = "";
        

    }

    public void MoveAction()
    {
        BaseHeros hero = BaseCombat.Combat.GetCharacterTurn() as BaseHeros;
        BaseHeros hero2 = BaseCombat.Combat.Heros[BaseCombat.Attack.Target];

        BaseCombat.Combat.SpellManager.MoveCharacter<BaseHeros>( GetPositionOfTheHero( hero ), BaseCombat.Attack.Target );
    }

    private int GetPositionOfTheHero( BaseHeros hero )
    {
        for (int i = 0; i < BaseCombat.Combat.Heros.Length; i++)
        {
            if (hero == BaseCombat.Combat.Heros[i])
                return i;
        }
        return 0;
    }
}
