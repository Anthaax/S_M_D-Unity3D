using UnityEngine;
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
            StartCombat.Combat.NextTurn();
            timeLeft = 30.0f;
        }
        GameObject.Find("TimerText").GetComponent<Text>().text = timeLeft.ToString();
    }

    private void CombatIsOver()
    {
        if (StartCombat.Combat.CheckIfTheCombatWasOver())
        {
            SceneManager.LoadScene( 2 );

            BoardManager.Map = StartCombat.Map;
            BoardManager.Gtx = StartCombat.Gtx;
            BoardManager.hero = StartCombat.Heros;
            BoardManager.Gtx.DungeonManager.CbtManager.ApplyRewward();
        }
    }
}
