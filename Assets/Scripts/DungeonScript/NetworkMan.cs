﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkMan : NetworkManager {

	// Use this for initialization
	void Start () {
        networkAddress = AdventureBoard.HostAddress;
        if (AdventureBoard.Online == "Offline")
        {
            StartServer();
        }
        else
        {
            if (AdventureBoard.HostState == "Meneur")
            {
                StartServer();
            }
            else
            {
                StartClient();
            }
        }
        if (NetworkClient.active && !ClientScene.ready)
        {
            ClientScene.Ready(client.connection);

           if (ClientScene.localPlayers.Count == 0)
           {
              ClientScene.AddPlayer(0);
           }
           
        }
        GameObject.Find( "menu" ).SetActive( false );
    }

    void OnServerInitialized()
    {
        Debug.Log("Server Initializied");
    }

    public void OnPlayerConnected()
    {
        Debug.Log("Player connected");
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("Client connected");

    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Debug.Log("Added player.");
        GameObject boardManager = GameObject.Find("BoardManager");
        bool assigned = boardManager.GetComponent<NetworkIdentity>().AssignClientAuthority(conn);
        Debug.Log("Assigned authority = " + assigned);
        NetworkServer.Spawn(boardManager);
        //var player = (GameObject)GameObject.Instantiate(playerPrefab, playerSpawnPos, Quaternion.identity);
       // NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
