using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISound : MonoBehaviour
{


    [SerializeField] Image SoundImage;
    [SerializeField] Sprite soundOn;
    [SerializeField] Sprite soundOff;

    public void ClickSound()
    {
        if (AudioListener.volume == 1)
        {
            SoundImage.sprite = soundOff;
            AudioListener.volume = 0;
            Debug.Log("À½¾Ç²¨");
        }
        else
        {
            SoundImage.sprite = soundOn;
            AudioListener.volume = 1;
            Debug.Log("À½¾Ç³ª¿Â´Ù");
        }
    }
}
