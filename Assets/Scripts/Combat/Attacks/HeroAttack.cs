using UnityEngine;
using System.Collections;
using S_M_D.Character;
using S_M_D.Combat;
using S_M_D;
using System.Collections.Generic;
using System.Threading;
using S_M_D.Spell;

public class HeroAttacks {

    int _target;
    Spells spell;
    int index;

    public int Target
    {
        get
        {
            return _target;
        }

        set
        {
            _target = value;
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
