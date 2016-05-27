using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject menu = GameObject.Find("MenuBGArmory");
        Debug.Log("object : " + GameObject.Find("MenuBGArmory"));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
