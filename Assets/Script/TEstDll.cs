using UnityEngine;
using System.Collections;
using S_M_D.Character;
using System.Collections.Generic;

public class TEstDll : MonoBehaviour {
    string _name;
	// Use this for initialization
	void Start () {
        InitializedHero();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void InitializedHero()
    {
        List<BaseHeros> myHero = new List<BaseHeros>();
        HerosManager hm = new HerosManager(myHero);
        name = hm.Find( HerosEnum.Warrior.ToString() ).CharacterName;
    }

    void OnGUI()
    {
        GUILayout.Label( _name );
    }
}
