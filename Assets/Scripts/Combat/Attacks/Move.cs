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
        if( StartCombat.Attack.Target <= 0 && StartCombat.Attack.Spell == null)
        {
            MoveAction();
            StartCombat.Combat.NextTurn();
        }
        Attacking.AfterAction();
    }
    public void MoveAction()
    {
        BaseHeros hero = StartCombat.Combat.GetCharacterTurn() as BaseHeros;
        BaseHeros hero2 = StartCombat.Combat.Heros[StartCombat.Attack.Target];

        StartCombat.Combat.SpellManager.MoveCharacter<BaseHeros>( GetPositionOfTheHero( hero ), StartCombat.Attack.Target );
    }

    private int GetPositionOfTheHero( BaseHeros hero )
    {
        for (int i = 0; i < StartCombat.Combat.Heros.Length; i++)
        {
            if (hero == StartCombat.Combat.Heros[i])
                return i;
        }
        return 0;
    }
}
