using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using S_M_D.Character;
using Assets.ParticularName;
using UnityEngine.UI;

public class HeroesListScript : MonoBehaviour {

    List<BaseHeros> _heroes;
    List<GameObject> _pHeroes;
    public List<Sprite> _spritesHeroes;

    // Use this for initialization
    void Start () {
        _heroes = GameScript.GameContext.PlayerInfo.MyHeros;
        _pHeroes = new List<GameObject>(GameObject.FindGameObjectsWithTag(TagName.pHero.ToString()));
        _pHeroes.Sort((x, y) => x.name.CompareTo(y.name));
        Debug.Log("Connerie : "+_spritesHeroes.Find(s => s.name == "PriestM"));
    }
	
	// Update is called once per frame
	void Update () {
	    for(int i = 0; i < _heroes.Count; i++)
        {
            string sexe = _heroes[i].IsMale ? "M" : "F";
            _pHeroes[i].GetComponentsInChildren<Image>()[1].sprite = _spritesHeroes.Find(s => s.name == (_heroes[i].CharacterClassName + sexe));
            _pHeroes[i].GetComponentInChildren<Text>().text = _heroes[i].CharacterClassName + _heroes[i].CharacterName;

        }
    }
}
