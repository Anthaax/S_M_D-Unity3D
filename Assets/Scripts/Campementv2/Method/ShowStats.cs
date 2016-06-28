using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowStats : MonoBehaviour {

	// Update is called once per frame
	void Update () {

    }

    public void ShowStatsItem()
    {
        string itemName = gameObject.GetComponentInChildren<Text>().text;
        Debug.Log("itemName : " + itemName);
    }
}
