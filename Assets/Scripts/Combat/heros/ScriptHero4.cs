﻿using UnityEngine;
using System.Collections;
using S_M_D.Character;
using S_M_D.Combat;
using System.Collections.Generic;
using Assets.Scripts.Combat.heros;

public class ScriptHero4 : MonoBehaviour
{

    Combat combat;
    public List<Sprite> _sprites;
    BaseHeros heros;

    // Use this for initialization
    void Start()
    {

        HeroAndMonsterSprite heroSprit = new HeroAndMonsterSprite();
        combat = FindObjectOfType(typeof(Combat)) as Combat;
        int index = heroSprit.chooseHeroSprite(Combat.Comba.Heros[3]);
        gameObject.GetComponent<SpriteRenderer>().sprite = _sprites[index];
        gameObject.AddComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public BaseHeros Heros
    {
        get
        {
            return heros;
        }

        set
        {
            heros = value;
        }
    }
}