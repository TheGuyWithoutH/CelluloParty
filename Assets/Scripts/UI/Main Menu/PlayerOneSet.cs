using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneSet : MonoBehaviour
{
    public void updateCouleur(int couleur)
    {
        PlayerPrefs.SetInt("couleur1", couleur);
    }

    public void updateName(string val)
    {
        PlayerPrefs.SetString("name1", val);
        Debug.Log(val);
        Debug.Log("works");

    }
}
