using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(BoxCollider2D))]
public class ClickableLight : MonoBehaviour
{
    const float MAX_INTENSITY = 0.75f;
    const float MIN_INTENSITY = 0f;
    [SerializeField] private Light2D clickableLight;

    private void Start() {
        clickableLight.intensity = MAX_INTENSITY;
    }

    private void OnMouseDown() {
        if(!Input.GetMouseButtonDown(0)) { return; }
        if(clickableLight.intensity == MAX_INTENSITY) {
            clickableLight.intensity = MIN_INTENSITY;
        }else {
            clickableLight.intensity = MAX_INTENSITY;
        }
    }
}
