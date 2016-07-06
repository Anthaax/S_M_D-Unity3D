using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CombatNetworkMan : NetworkManager
{
    public static bool Launch;
    private bool alreadyLaunched;
    private bool CNMLaunched = false;

    int updates, callOnServer;
    bool serverCalled;

    // Use this for initialization
    void Start()
    {
        OnLevelWasLoaded(1);
    }

    public void OnLevelWasLoaded(int i)
    {
        if (CNMLaunched)
            return;
        else
            CNMLaunched = true;
        NetworkServer.DisconnectAll();
        updates = 0;
        callOnServer = 0;
        Debug.Log("Level loaded.");
        serverCalled = false;
        alreadyLaunched = false;
        if (Launch)
        {
            StopServer();
        }
        else
        {
            Launch = true;
        }
        if (AdventureBoard.HostAddress != null)
            networkAddress = AdventureBoard.HostAddress;
        else
            networkAddress = "127.0.0.1";
        if (AdventureBoard.Port != 0)
            networkPort = AdventureBoard.Port;
        else
            networkPort = 25016;

        if (AdventureBoard.Online == "Offline" || AdventureBoard.Online == null)
        {
            StartServer();
        }
        else
        {
            if (AdventureBoard.HostState == "Meneur")
            {
                StartServer();
            }
        }
        AdventureBoard.Port++;
    }

    void OnFailedToConnect(NetworkConnectionError error)
    {
        Debug.Log("Could not connect to server: " + error);
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
        GameObject combatLogic = GameObject.Find("CombatLogic");
        bool assigned = combatLogic.GetComponent<NetworkIdentity>().AssignClientAuthority(conn);
        NetworkServer.Spawn(combatLogic);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (updates < 240)
            updates++;
        if (updates >= 240 && updates < 520)
            updates = 240;
        if (callOnServer < 400)
            callOnServer++;
        if (updates == 240)
        {
            Debug.Log("Updates == 240");
            if (AdventureBoard.Online != "Offline" && AdventureBoard.HostState != "Meneur")
            {
                Debug.Log("CLIENT STARTED");
                StartClient();
            }
            updates = 550;
        }
        if (callOnServer >= 400 && !serverCalled && AdventureBoard.HostState == "Meneur" && AdventureBoard.Online != "Offline")
        {
            serverCalled = true;
            GameObject combatLogic = GameObject.Find("CombatLogic");
            NetworkServer.Spawn(combatLogic);
        }
        if (AdventureBoard.HostState != "Meneur" && client != null && client.connection != null && !alreadyLaunched)
        {
            ClientScene.RegisterPrefab(Resources.Load<GameObject>("Prefabs/CombatLogic"));
            Debug.Log("Inside condition");
            if (NetworkClient.active && !ClientScene.ready)
            {
                ClientScene.Ready(client.connection);
                ClientScene.AddPlayer(1);
                alreadyLaunched = true;
                Debug.Log("ADDED PLAYER");
            }
            else
            {
                ClientScene.AddPlayer(1);
                alreadyLaunched = true;
            }
        }
    }
}
