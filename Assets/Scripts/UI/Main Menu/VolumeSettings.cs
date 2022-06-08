using UnityEngine;

namespace Menu
{
    public class VolumeSettings : MonoBehaviour
    {

        public void ChangeVolume(float newVolume)
        {
            Debug.Log(newVolume);
            PlayerPrefs.SetFloat("volume", newVolume);
            AudioListener.volume = PlayerPrefs.GetFloat("volume");
        }

        public void MuteVolume()
        {
            AudioListener.volume = 0;
        }

        public void RestoreVolume()
        {
            if (PlayerPrefs.HasKey("volume"))
            {
                AudioListener.volume = PlayerPrefs.GetFloat("volume");
            }
            else
            {
                AudioListener.volume = 0.8f;
            }
        }
    }
}