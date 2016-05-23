using UnityEngine;
using System.Collections;
using Assets.ParticularName;

public class CollisionHeroScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == TagName.Building.ToString())
        {
            Debug.Log("Voila la collisoin !!!");
            //gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }
}
