using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CombatNetworkMan : NetworkManager
{
    public static bool Launch;
    private bool alreadyLaunched;
    private bool CNMLaunched = false;

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
        Debug.Log("Level loaded.");
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
            else
            {
                StartClient();
            }
        }
        AdventureBoard.Port++;
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
    void Update()
    {

        if (AdventureBoard.HostState != "Meneur" && client != null && client.connection != null && !alreadyLaunched)
            if (NetworkClient.active && !ClientScene.ready)
            {
                ClientScene.Ready(client.connection);


                ClientScene.AddPlayer(0);
                alreadyLaunched = true;
            }
    }
}
