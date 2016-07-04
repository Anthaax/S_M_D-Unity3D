using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pass : MonoBehaviour {

	public void Onclick()
    {
        StartCombat.Combat.NextTurn();
        Timer T = GameObject.Find("Timer").GetComponent<Timer>();
        T.timeLeft = 30.0f;
        GameObject.Find("SpellInfo").GetComponent<Text>().text = "";

        for (int i = 1; i < 5; i++)
        {
            if (GameObject.Find("Arrow" + i) != null)
            {
                GameObject.Find("Arrow" + i).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Combat/1024px-Red_Arrow_Down.svg");
                GameObject.Find("Arrow" + i).GetComponent<Image>().enabled = false;
            }
        }
    }
}
