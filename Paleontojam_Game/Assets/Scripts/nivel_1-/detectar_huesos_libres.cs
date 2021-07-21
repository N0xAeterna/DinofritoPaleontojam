using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectar_huesos_libres : MonoBehaviour {

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void OnTriggerEnter (Collider other) {
        print ("entro  "+other.name);
    }

    public void OnTriggerExit (Collider other) {
        print ("salio  ");
        if (other.gameObject.layer == 12) {
            print (other.name);
        }

    }

}