using UnityEngine;
using System.Collections;

public class ButtonCloseMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Onclick()
    {
        GameObject.Find("MenuBGArmory").SetActive(false);

    }
}
