using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour {

    public AudioSource gvrAudioSource;
    public Image soundIcon;
    private bool mute = false;
    private Sprite iconSoundOn;
    private Sprite iconSoundOff;

    private void Start() {
        iconSoundOn = Resources.Load<Sprite>("UI/baseline_volume_up_black_36dp");
        iconSoundOff = Resources.Load<Sprite>("UI/baseline_volume_off_black_36dp");
    }

    public void toggleSound() {
        if (mute) {
            gvrAudioSource.mute = false;
            mute = false;
            soundIcon.sprite = iconSoundOn;
        } else {
            gvrAudioSource.mute = true;
            mute = true;
            soundIcon.sprite = iconSoundOff;
        }
    }


}
