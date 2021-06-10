using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_point : MonoBehaviour {

    public ctrl_game control;
    public int point;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void llamar_destruir (float time_destroy) {
        //print ("destroy");
        Destroy (gameObject, time_destroy);
    }

    public void OnTriggerEnter (Collider other) {
        print ("entramos2");
        try {
            control.hemos_entrado (point);
        } catch (System.Exception ex) {
            //
        }
    }
    public void OnTriggerExit (Collider other) {
        //print ("salimos2");
        control.hemos_salido (point);

    }

}