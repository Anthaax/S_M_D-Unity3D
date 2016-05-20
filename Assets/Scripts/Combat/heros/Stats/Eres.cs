using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Eres : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Combat.Heros != null)
        {
            Text txt = gameObject.GetComponent<Text>();
            txt.text = Combat.Heros.EffectivAffectRes.ToString();

        }
    }
}
