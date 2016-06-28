﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

    public float timeLeft = 30.0f;
    
    void Update()
    {
        timeLeft -= Time.deltaTime;
        CombatIsOver();
        if (timeLeft <= 0)
        {
            BaseCombat.Combat.NextTurn();
            SpellsAndStats.UpdateSpell();
            timeLeft = 30.0f;
        }
        GameObject.Find("TimerText").GetComponent<Text>().text = timeLeft.ToString();
    }

    private void CombatIsOver()
    {
        //if (BaseCombat.Combat.CheckIfTheCombatWasOver())
        // SceneManager.LoadScene( 1 );
        //BoardManager.Map = BaseCombat.Map;
        //BoardManager.Gtx = BaseCombat.Gtx;
        //BoardManager.hero = BaseCombat.Heros;
        BaseCombat.Combat.ApplyRewward();
        BaseCombat.Gtx.DungeonManager.EndOfTheDuengon();
        Start.Gtx = BaseCombat.Gtx;

    }
}
