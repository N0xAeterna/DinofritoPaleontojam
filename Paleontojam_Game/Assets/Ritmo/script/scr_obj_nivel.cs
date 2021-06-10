using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "scr_obj_nivel", menuName = "nivel_ritmo")]
[System.Serializable]
public class scr_obj_nivel : ScriptableObject {
    public List<point> points = new List<point> ();
    public bool iniciado;
}

[System.Serializable]
public class scr_nivel {
    public List<point> points = new List<point> ();
}