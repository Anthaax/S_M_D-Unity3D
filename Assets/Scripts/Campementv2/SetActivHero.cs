using UnityEngine;
using System.Collections;
using System;

public class SetActivHero : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        string objectName;
        objectName = gameObject.name;
       char last = objectName[objectName.Length - 2];
        int index = last-49;
        BaseCampement.ActivHeros = BaseCampement.ListeOfHeros[index];
    }
}
