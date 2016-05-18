using UnityEngine;
using System.Collections;
using S_M_D.Character;
using S_M_D.Combat;
using System.Collections.Generic;

public class ScriptHero1 : MonoBehaviour {

    Combat combat;
    public List<Sprite> _sprites;
    public BaseHeros heros;
    // Use this for initialization
    void Start () {
        
        combat = FindObjectOfType(typeof(Combat)) as Combat;
        Debug.Log(combat);
 //       heros = combat.Comba.Heros[1]; 
        gameObject.GetComponent<SpriteRenderer>().sprite = _sprites[1];
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
