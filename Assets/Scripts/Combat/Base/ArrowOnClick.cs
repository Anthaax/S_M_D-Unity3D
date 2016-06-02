using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ArrowOnClick : MonoBehaviour {

	public void OnClick()
    {
        for (int i = 1; i < 5; i++)
        {
            if (GameObject.Find("Arrow" + i) != null)
            {
                GameObject.Find("Arrow" + i).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Combat/1024px-Red_Arrow_Down.svg");
            }
        }
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Combat/greenarrow");
        string result = gameObject.name.Substring(gameObject.name.Length - 1);
        int R = Convert.ToInt32(result);
        BaseCombat.Attack.Monster = R-1;

    }
}
