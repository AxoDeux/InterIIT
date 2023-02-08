using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager {
    public enum Sound {
        shoot,
        hit
    };

    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;

    public static void PlaySound(Sound sound) {
        if(oneShotGameObject == null) {
           oneShotGameObject = new GameObject("Sound");
           oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
        }
        
        oneShotAudioSource.PlayOneShot(GetAudioClip(sound));

    }

    //3d sound
    public static void PlaySound(Sound sound, Vector3 position) {
        GameObject soundGameObject = new GameObject("Sound");
        soundGameObject.transform.position = position;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound);
        audioSource.maxDistance = 100f;
        audioSource.spatialBlend = 1f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.dopplerLevel = 0f;
        audioSource.Play();

        Object.Destroy(soundGameObject, audioSource.clip.length);
    }

    private static AudioClip GetAudioClip(Sound sound) {
        foreach(GameAssets.SoundAudioClip soundAudioClip in GameAssets.Instance.soundAudioClips) {
            if(soundAudioClip.sound == sound) {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }

    //public static void AddButtonSounds(this Button_UI buttonUI)
}
