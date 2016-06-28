using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonManager : MonoBehaviour
{

    private BoardManager boardScript;
    private bool online;
    private bool server;

    // Use this for initialization
    void Awake()
    {
        online = true;
        server = true;
        boardScript = GetComponent<BoardManager>();

    }

    public void InitGame()
    {
        boardScript.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
