using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public abstract class PlayerSetPrefs : MonoBehaviour
    {
        public Image celluloImage;
        public Sprite redSprite;
        public Sprite blueSprite;
        public Sprite greenSprite;
        public TMP_Dropdown dropdown;
        public PlayerSetPrefs otherPrefs;

        private List<TMP_Dropdown.OptionData> dropdownOptions = new List<TMP_Dropdown.OptionData>()
        {
            new TMP_Dropdown.OptionData("Red"),
            new TMP_Dropdown.OptionData("Blue"),
            new TMP_Dropdown.OptionData("green")
        };

        public virtual void UpdateCouleur(int couleur)
        {
            
            switch (couleur)
            {
                case 0: 
                    celluloImage.sprite = redSprite;
                    break;
                case 1: 
                    celluloImage.sprite = blueSprite;
                    break;
                case 2: 
                    celluloImage.sprite = greenSprite;
                    break;
                default: 
                    celluloImage.sprite = redSprite;
                    break;
            }
        }

        public abstract void updateName(string val);

        /*public void DisableOption(int index)
        {
            dropdown.ClearOptions();
            List<TMP_Dropdown.OptionData> newOptions = new List<TMP_Dropdown.OptionData>(dropdownOptions);
            newOptions.RemoveAt(index);
            dropdown.options = newOptions;
        }*/
    }
}