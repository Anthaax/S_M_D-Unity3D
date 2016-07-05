using UnityEngine;
using System.Collections;
using S_M_D.Character;
using UnityEngine.UI;

public class CemeteryBoard : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetBoard()
    {
        for (int i = Start.Gtx.PlayerInfo.DeadHero.Count - 1; i >= 0; i--)
        {
            BaseHeros heros = Start.Gtx.PlayerInfo.DeadHero[i];
            string sex = heros.IsMale ? "M" : "F";
            GameObject.Find("Hero" + (i + 1) + "T").GetComponent<Text>().text = heros.CharacterName;
            GameObject.Find("Hero" + (i + 1) + "I").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "Icone" + sex);
        }
    }
}
