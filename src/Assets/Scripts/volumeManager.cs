using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeManager : MonoBehaviour {

    public Slider masterSlider;
    public Slider musicSlider;
    public AudioSource musicAudio;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        musicAudio.volume = musicSlider.value;
        AudioListener.volume = masterSlider.value;
	}
}
