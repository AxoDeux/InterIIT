using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RewindButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public delegate void RewindEventHandler();
    public static event RewindEventHandler RewindEvent;

    public void OnPointerDown(PointerEventData eventData) {
        RewindEvent.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData) {
        RewindEvent.Invoke();
    }

}
