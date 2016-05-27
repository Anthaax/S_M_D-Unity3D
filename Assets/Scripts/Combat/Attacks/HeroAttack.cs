using UnityEngine;
using System.Collections;
using S_M_D.Character;
using S_M_D.Combat;
using S_M_D;
using System.Collections.Generic;
using System.Threading;
using S_M_D.Spell;

public class HeroAttack : MonoBehaviour {

    int monster;
    Spells spell;
    int index;

    public void Attack()
    {
        BaseCombat.Combat.SpellManager.HeroLaunchSpell(spell, monster);
    }
    public int Monster
    {
        get
        {
            return monster;
        }

        set
        {
            monster = value;
        }
    }

    public Spells Spell
    {
        get
        {
            return spell;
        }

        set
        {
            spell = value;
        }
    }
}
