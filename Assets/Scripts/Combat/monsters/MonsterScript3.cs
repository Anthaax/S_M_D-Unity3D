using UnityEngine;
using System.Collections;
using S_M_D.Character;
using S_M_D.Combat;
using System.Collections.Generic;
using Assets.Scripts.Combat.heros;

public class MonsterScript3 : MonoBehaviour
{

    Combat combat;
    public List<Sprite> _sprites;
    BaseMonster monster;


    // Use this for initialization
    void Start()
    {

        HeroAndMonsterSprite monsterSprite = new HeroAndMonsterSprite();
        combat = FindObjectOfType(typeof(Combat)) as Combat;
        int index = monsterSprite.chooseMonsterSprite(combat.Comba.Monsters[2]);
        gameObject.GetComponent<SpriteRenderer>().sprite = _sprites[index];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public BaseMonster Monster
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
}