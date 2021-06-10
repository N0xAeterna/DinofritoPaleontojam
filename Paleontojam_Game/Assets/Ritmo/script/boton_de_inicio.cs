using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
hace funcionar el boton de inicio
*/

public class boton_de_inicio : MonoBehaviour {

    public Canvas play_, editar_, inicio_;
    public GameObject obj_edita;

    // Start is called before the first frame update
    void Start () {
        inicio_.enabled = true;
    }

    // Update is called once per frame
    void Update () {

    }

    public void play () {
        //manda a dare play
        obj_edita.SetActive (false);
        play_.enabled = true;
        inicio_.enabled = false;
        GetComponent<ctrl_game> ().iniciar_juego ();
    }

    public void editar () {
        //manda a editar
        obj_edita.SetActive (true);
        editar_.enabled = true;
        inicio_.enabled = false;
    }

    public void atras () {
        obj_edita.SetActive (false);
        editar_.enabled = false;
        play_.enabled = false;
        inicio_.enabled = true;
        GetComponent<ctrl_game> ().atras ();
    }

}