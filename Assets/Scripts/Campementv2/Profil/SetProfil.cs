using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using S_M_D.Character;

public class SetProfil : MonoBehaviour {

	
    public void Show()
    {
        string name = gameObject.name;
        int index = int.Parse(""+name[name.Length - 2]);
        BaseHeros heros = Start.Gtx.PlayerInfo.MyHeros[index - 1];
        GameObject.Find("Icone").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Icones/" + heros.CharacterClassName + "IconeF");
    }
}
