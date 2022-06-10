using System.Collections;
using System.Collections.Generic;
using Menu;
using UnityEngine;

public class PlayerOneSet : PlayerSetPrefs
{
    public override void UpdateCouleur(int couleur)
    {
        base.UpdateCouleur(couleur);
        PlayerPrefs.SetInt("couleur1", couleur);
        
    }

    public override void updateName(string val)
    {
        PlayerPrefs.SetString("name1", val);

    }
}
