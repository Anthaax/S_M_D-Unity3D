using UnityEngine;
using System.Collections;

public class IronMaidenScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeState()
    {
        Animator animator = this.GetComponent<Animator>( );
        if ( animator.GetInteger( "state" ) == 0 )
        {
            this.GetComponent<Animator>( ).SetInteger( "state", 1 );
        }
        else
        {
            this.GetComponent<Animator>( ).SetInteger( "state", 2 );
            this.GetComponent<Animator>( ).SetInteger( "state", 0 );

        }
    }
}
