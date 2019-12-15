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

	void Start () {
        masterSlider.value = 1;
        musicSlider.value = 1f;
        startMusicOffset = 0.1f;
        musicAudio.volume = musicSlider.value * masterSlider.value*startMusicOffset;
        
    }

    /**  
    * Update is called once per frame   
    */ 
    void Update () {
        
        //AudioListener.volume = masterSlider.value *musicAudio.volume;
	}

    public void updateVol()
    {
        musicAudio.volume = musicSlider.value * masterSlider.value*startMusicOffset;
    }
}
