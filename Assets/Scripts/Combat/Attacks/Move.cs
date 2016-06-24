using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using S_M_D.Spell;
using UnityEngine;
using UnityEngine.UI;
using S_M_D.Character;

public class MoveHero : MonoBehaviour
{
    void OnCLick()
    {
        if( BaseCombat.Attack.Target <= 0 && BaseCombat.Attack.Spell == null)
        {
            MoveAction();
            BaseCombat.Combat.NextTurn();
        }
        Attacking.AfterAction();
        SpellsAndStats.UpdateSpell();
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
