using UnityEngine;
using System.Collections;
using S_M_D.Character;
using S_M_D.Camp.Class;
using System.Collections.Generic;
using UnityEngine.UI;
using S_M_D.Spell;
using System;

public class CasernBoard : MonoBehaviour {


    public static void SetBoard()
    {
        Casern casern = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Casern) as Casern;
        BaseHeros hero = casern.Hero;
  
        List<GameObject> spellsBox = Start.CasernSpells;

        if(hero != null)
        {
            for (int i = 0; i < spellsBox.Count; i++)
            {
                if (hero.Spells[i] != null)
                {
                    spellsBox[i].GetComponent<Button>().image.sprite = Resources.Load<Sprite>("Sprites/Combat/Characters/Spells/" + hero.Spells[i].Name);
                    spellsBox[i].GetComponentsInChildren<Text>()[0].text = hero.Spells[i].Name + ". Nv:" + hero.Spells[i].Lvl;
                    spellsBox[i].GetComponentsInChildren<Text>()[1].text = hero.Spells[i].Price.ToString();

                    if (hero.Spells[i].IsBuy)
                    {
                        SetToActiveButton(spellsBox[i].GetComponentsInChildren<Button>()[0]);
                        for (int j = 0; j < spellsBox[i].GetComponentsInChildren<Button>().Length; j++)
                        {
                            if (spellsBox[i].GetComponentsInChildren<Button>()[j].name == "BuySpell")
                                SetToInactiveButton(spellsBox[i].GetComponentsInChildren<Button>()[j]);
                            if (spellsBox[i].GetComponentsInChildren<Button>()[j].name == "UpSpell")
                                SetToActiveButton(spellsBox[i].GetComponentsInChildren<Button>()[j]);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < spellsBox[i].GetComponentsInChildren<Button>().Length; j++)
                        {
                            if (spellsBox[i].GetComponentsInChildren<Button>()[j].name == "BuySpell")
                                SetToActiveButton(spellsBox[i].GetComponentsInChildren<Button>()[j]);
                            if (spellsBox[i].GetComponentsInChildren<Button>()[j].name == "UpSpell")
                                SetToInactiveButton(spellsBox[i].GetComponentsInChildren<Button>()[j]);

                        }
                    }
                    CheckEquiped(i, hero.Spells[i]);
                }
                else
                {
                    spellsBox[i].GetComponent<Button>().image.sprite = Resources.Load<Sprite>("Sprites/Combat/Characters/NoSpell");
                    spellsBox[i].GetComponentsInChildren<Text>()[0].text = "NoSpell";
                    spellsBox[i].GetComponentsInChildren<Text>()[1].text = "";
                    SetToInactiveButton(spellsBox[i].GetComponentsInChildren<Button>()[0]);
                    SetToInactiveButton(spellsBox[i].GetComponentsInChildren<Button>()[1]);
                    SetToInactiveButton(spellsBox[i].GetComponentsInChildren<Button>()[2]);
                }
                
            }


        }

        else
        {
            foreach(Button b in Start.MenuBGCasern.GetComponentsInChildren<Button>())
            {
                if(b.name != "Close")
                    SetToInactiveButton(b);
            }
        }

       
    }

    private static void CheckEquiped(int i, Spells spell)
    {
        List<GameObject> spellsBox = Start.CasernSpells;

        if (spell.IsEquiped)
            spellsBox[i].GetComponent<Button>().image.color = Color.green;
        else
            spellsBox[i].GetComponent<Button>().image.color = Color.red;
    }

    public void BuySpell()
    {
        string spellName = gameObject.GetComponentsInChildren<Text>()[0].text.Split('.')[0];
        Casern casern = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Casern) as Casern;
        casern.BuySpellToHero(FindSpellByName(spellName));
        SetBoard();
    }
    public void UpgSpell()
    {
        string spellName = gameObject.GetComponentsInChildren<Text>()[0].text.Split('.')[0];
        if(FindSpellByName(spellName).IsBuy)
        {
            Casern casern = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Casern) as Casern;
            casern.UpgradeSpellToHero(FindSpellByName(spellName));
            SetBoard();
        }
        
    }

    private Spells FindSpellByName(string spellName)
    {
        Casern casern = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Casern) as Casern;
        BaseHeros hero = casern.Hero;
        foreach (Spells s in hero.Spells)
        {
            if (s.Name == spellName)
                return s;
        }
        return null;
    }
    public void setSpellEquipedOrNot()
    {
        Casern casern = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Casern) as Casern;
        BaseHeros hero = casern.Hero;

        string spellName = gameObject.GetComponentsInChildren<Text>()[0].text.Split('.')[0];
        for(int i = 0; i < hero.Spells.Length; i++)
        {
            if(hero.Spells[i] != null && spellName == hero.Spells[i].Name)
            {
                if(hero.Spells[i].IsBuy)
                {
                    if (hero.Spells[i].IsEquiped)
                    {
                        hero.Spells[i].IsEquiped = false;
                        CheckEquiped(i, hero.Spells[i]);
                        SpellComparer compare = new SpellComparer();
                        Array.Sort( hero.Spells, compare );
                        SetBoard();
                    }
                    else
                    {
                        if (NumberOfSpellsEquiped() < 4)
                        {
                            hero.Spells[i].IsEquiped = true;
                            CheckEquiped(i, hero.Spells[i]);
                            SpellComparer compare = new SpellComparer();
                            Array.Sort( hero.Spells, compare );
                            SetBoard();
                        }
                    }
                }
                SetBoard();
                break;
            }
        }

    }

    private int NumberOfSpellsEquiped()
    {
        Casern casern = Start.Gtx.PlayerInfo.GetBuilding(BuildingNameEnum.Casern) as Casern;
        BaseHeros hero = casern.Hero;
        int nb = 0;
        foreach(Spells spell in hero.Spells)
        {
            if(spell != null)
            {
                if (spell.IsEquiped)
                    nb++;
            }
        }

        return nb;
    }

    public static void SetToInactiveButton(Button button)
    {
        button.GetComponent<Image>().color = Color.gray;
        button.enabled = false;
    }
    public static void SetToActiveButton(Button button)
    {
        button.GetComponent<Image>().color = Color.white;
        button.enabled = true;
    }

}
