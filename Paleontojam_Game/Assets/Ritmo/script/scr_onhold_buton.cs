using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class scr_onhold_buton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    public UnityEvent OnHold, OnHold2;
    bool OnPressed, p_dir;

    public void OnPointerDown (PointerEventData eventData) {
        OnPressed = true;
    }

    public void OnPointerUp (PointerEventData eventData) {
        OnPressed = false;
        p_dir = true;
    }

    void Update () {
        if (OnPressed) {
            OnHold.Invoke ();
        } else {
            if (p_dir) {
                p_dir = false;
                OnHold2.Invoke ();
            }
        }
    }
}