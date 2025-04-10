using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;   

    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("audioVolume"))
        {
            PlayerPrefs.SetFloat("audioVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    // Update is called once per frame
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("audioVolume", volumeSlider.value);
    }

    private void Load()
    {
        PlayerPrefs.GetFloat("audioVolume");
    }
}
