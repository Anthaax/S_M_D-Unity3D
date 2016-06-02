using UnityEngine;
using System.Collections;

public class Client : MonoBehaviour {

    private HostData[ ] hostList;

	// Use this for initialization
	void Start ()
    {
        MasterServer.ipAddress = "92.103.197.250";
        MasterServer.RequestHostList( "Synouk_must_die" );
	}
	
    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        if ( msEvent == MasterServerEvent.HostListReceived )
        {
            hostList = MasterServer.PollHostList( );
            Debug.Log( "Connecté!" );
        }
            

    }

	// Update is called once per frame
	void Update () {
	
	}
}
