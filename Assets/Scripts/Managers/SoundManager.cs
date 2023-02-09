using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        shoot,
        hit,
        coin,
        gameOver,
        timeRewind,
        UIButtonWood,
        UIButtonScifi,
        UICloseButton,
        UIClick,
        WaveStart,
        TimeCellCollect

    };

    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;

    private static Dictionary<Sound, float> soundTimerMap;

    public static void Initialize() {
        soundTimerMap = new Dictionary<Sound, float>();
        soundTimerMap[Sound.timeRewind] = 0f;
    }

    public static void PlaySound(Sound sound)
    {
        if(!CanPlaySound(sound)) return; 
        if (oneShotGameObject == null)
        {
            oneShotGameObject = new GameObject("Sound");
            oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
        }

        oneShotAudioSource.PlayOneShot(GetAudioClip(sound));

    }

    private static bool CanPlaySound(Sound sound) {
        switch(sound) {
            default:
                return true;

            case Sound.timeRewind:
                if(soundTimerMap.ContainsKey(sound)) {
                    float lastTimePlayed = soundTimerMap[sound];
                    float rewindTimerMax = 1f;
                    if(lastTimePlayed + rewindTimerMax < Time.time) {   //check if its time to play the sound
                        soundTimerMap[sound] = Time.time;
                        return true;
                    } else {
                        return false;
                    }
                } else {
                    return true;
                }
        }
    }

    //3d sound
    public static void PlaySound(Sound sound, Vector3 position)
    {
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

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.Instance.soundAudioClips)
        {
            if (soundAudioClip.sound == sound)
            {
                //Debug.Log("Found sound");
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }

    //public static void AddButtonSounds(this Button_UI buttonUI)
}
