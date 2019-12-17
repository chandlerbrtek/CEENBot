using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**  
* volumeManager adjusts in game volume based on UI sliders  
*/

public class volumeManager : MonoBehaviour {

    public Slider masterSlider;
    public Slider musicSlider;
    public AudioSource musicAudio;
    public float startMusicOffset;
    /**
     * Called on program start
     */
    void Start () 
    {
        masterSlider.value = 1;
        musicSlider.value = 1f;
        startMusicOffset = 0.1f;
        musicAudio.volume = musicSlider.value * masterSlider.value*startMusicOffset;
        
    }

    /**
     * updates the volume for the menu music
     */
    public void updateVol()
    {
        musicAudio.volume = musicSlider.value * masterSlider.value*startMusicOffset;
    }
}
