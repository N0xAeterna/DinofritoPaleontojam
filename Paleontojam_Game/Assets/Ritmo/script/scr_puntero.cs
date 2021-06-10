using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_puntero : MonoBehaviour {

    public level_editor editor;
    public bool borrar;
    public int p_der, p_izq, p_yo;
    public float t_der, t_izq, t_yo, dura, speed;
    public bool inicio, final;

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    void OnMouseDown () {
        if (!borrar && editor.quitar_puntero (0)) {
            Destroy (this.gameObject, 0f);
        }
    }

}