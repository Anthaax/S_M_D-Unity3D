using UnityEngine;
using System.Collections;

public class ProfilUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        
        if (BaseCampement.ActivBuilding != null)
        {
            Debug.Log("Lucas pd");
            gameObject.SetActive(true);
        }
            
    }
}
