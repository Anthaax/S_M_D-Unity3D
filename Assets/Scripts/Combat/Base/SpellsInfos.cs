using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SpellsInfos : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        string result = gameObject.name.Substring(gameObject.name.Length - 1);;
        int R = Convert.ToInt32(result);
        GameObject.Find("SpellInfo").GetComponent<Text>().text = BaseCombat.HerosPLaying.Spells[R - 1].Description;
    }
}
