using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoSet : MonoBehaviour
{
    public void updateCouleur(int couleur)
    {
        PlayerPrefs.SetInt("couleur2", couleur);
    }

    public void updateName(string val)
    {
        PlayerPrefs.SetString("name2", val);

    }
}