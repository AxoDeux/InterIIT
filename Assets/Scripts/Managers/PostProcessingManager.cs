using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{
    public static PostProcessingManager Instance { get; private set; }
    private Volume volume;


    private ChromaticAberration chromatic;
    private Vignette vignette;
    private LensDistortion lensDistortion;

    private bool isHurting;
    private bool isRewinding;

    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }
    void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet<ChromaticAberration>(out chromatic);
        volume.profile.TryGet<Vignette>(out vignette);
        volume.profile.TryGet<LensDistortion>(out lensDistortion);
        
    }

    private void FixedUpdate() {
        if(chromatic && isHurting) {
            chromatic.intensity.value = Mathf.PingPong(Time.time * 6, 0.7f);
        }

        if(vignette && lensDistortion && isRewinding) {
            lensDistortion.intensity.value = Mathf.Lerp(-0.5f, 0.5f, Mathf.PingPong(Time.time, 1));
        }
    }

    public void PlayerHurting() {
        if(isHurting) return;
        isHurting = true;
        StartCoroutine(HurtForAWhile());
    }

    private IEnumerator HurtForAWhile() {
        yield return new WaitForSeconds(0.8f);
        isHurting = false;
        chromatic.intensity.value = Mathf.Lerp(chromatic.intensity.value, 0f, 1);
    }

    public void TimeRewinding() {
        if(isRewinding) return;
        isRewinding = true;
        vignette.intensity.value = Mathf.Lerp(0f, 0.5f, 1);
        StartCoroutine(RewindTime());
    }

    private IEnumerator RewindTime() {
        yield return new WaitForSeconds(0.8f);
        isRewinding = false;
        lensDistortion.intensity.value = Mathf.Lerp(lensDistortion.intensity.value, 0f, 1f);
    }

    public void ResetVignette() {
        vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0f, 1);
    }
}
