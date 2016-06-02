using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float timeLeft;
    // Use this for initialization
    void Start () {
        timeLeft = 30.0f;
    }


    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            BaseCombat.Combat.NextTurn();
            timeLeft = 30.0f;
        }
        GameObject.Find("TimerText").GetComponent<Text>().text = timeLeft.ToString();
    }

}
