using System.Collections;
using System.Collections.Generic;
using Menu;
using UnityEngine;

public class PlayerTwoSet : PlayerSetPrefs
{
    public override void UpdateCouleur(int couleur)
    {
        base.UpdateCouleur(couleur);
        PlayerPrefs.SetInt("couleur2", couleur);
    }

    public override void updateName(string val)
    {
        PlayerPrefs.SetString("name2", val);
    }
}