using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class f_archivos : MonoBehaviour {
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void escribir_archivo (scr_nivel file_) {
        string file_path = Application.dataPath + "/editable/nivel.json";
        string json_string = JsonUtility.ToJson (file_);
        File.WriteAllText (file_path, json_string);
    }

    public scr_nivel leer_archivo () {
        string file_path = Application.dataPath + "/editable/nivel.json";
        string json_string = File.ReadAllText (file_path);
        scr_nivel nivel = JsonUtility.FromJson<scr_nivel> (json_string);
        //print ("a");
        return nivel;
    }
}