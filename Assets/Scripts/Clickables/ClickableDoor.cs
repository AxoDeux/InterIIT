using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ClickableDoor : MonoBehaviour {

    [SerializeField] private Sprite open;
    [SerializeField] private Sprite close;

    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnMouseDown() {
    
        if(!Input.GetMouseButtonDown(0)) { return; }
        if(spriteRenderer.sprite == open) {
            spriteRenderer.sprite = close;
        } else {
            spriteRenderer.sprite = open; 
        }
    }
}
