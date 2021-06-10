using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class level_editor : MonoBehaviour {

    public AudioSource pista;
    public float duracion_pista, tiempo_de_pista, limit_izq, limit_der, speed, dis_m_pista_, mas_dis_pista, dur_barra;
    public int minuto, segundo, milisegundo, p_total, p_actual, c_dir_izq_audio, ran_c_dir_audio;
    public GameObject b_audio, b_larga, puntero, b_pista, caja_punteros;
    public Text t_actual, t_total, b_play;
    public bool play;
    public Transform tra_b_pista, tra_b_audio;
    public List<point> points = new List<point> ();
    public List<GameObject> obj_points = new List<GameObject> ();
    public scr_obj_nivel n_editable;

    void Start () { }

    void Update () {
        if (play) {
            mover_pista (pista.time * -1f);
        }
        objtener_tiempo (pista.time);
        t_actual.text = minuto.ToString ("00") + ":" + segundo.ToString ("00") + ":" + milisegundo.ToString ("0000");
        //print (pista.time.ToString () + "/" + tra_b_pista.position.x.ToString ());
    }

    public void iniciar () {
        points.Clear ();
        duracion_pista = pista.clip.length;
        b_audio.GetComponent<Transform> ().localScale = new Vector3 (duracion_pista, 1, 1);
        objtener_tiempo (duracion_pista);
        tiempo_de_pista = 0f;
        p_actual = 0;
        t_total.text = "/  " + minuto.ToString ("00") + ":" + segundo.ToString ("00") + ":" + milisegundo.ToString ("0000");
        tra_b_pista = b_pista.GetComponent<Transform> ();
        tra_b_audio = b_audio.GetComponent<Transform> ();
        c_dir_izq_audio = 1;
        mas_dis_pista = 1f;
        if (!n_editable.iniciado) {
            poner_puntero (true);
            poner_puntero (true);
            n_editable.iniciado = true;
            n_editable.points.Clear ();
            n_editable.points = points;
        } else {
            int conta = n_editable.points.Count;
            poner_puntero (true);
            poner_puntero (true);
            for (int i = 2; i < conta; i++) {
                poner_puntero (false, n_editable.points[i].t_yo);
            }
            n_editable.points.Clear ();
            n_editable.points = points;
        }
    }

    void objtener_tiempo (float duracion) {
        minuto = (int) duracion / 60;
        segundo = (int) (duracion % 60f);
        milisegundo = (int) ((duracion - ((float) (minuto * 60) + segundo)) * 1000f);
    }

    public void dar_play () {
        if (!play) {
            pista.Play ();
            b_play.text = "pausa";
            play = true;
        } else {
            pista.Pause ();
            b_play.text = "play";
            play = false;
        }
    }

    public void rebobina () {
        pista.Stop ();
        t_actual.text = "00:00:0000";
        b_play.text = "play";
        play = false;
        tra_b_pista.position = Vector3.zero;
    }

    public void boton_puntero () {
        poner_puntero ();
        print ("p1");
    }

    public void poner_puntero (bool predefinido = false, float time_ = 0f) {
        //creara un puntero en la posicion que tiene la barra de audio
        GameObject p_ = Instantiate (puntero, new Vector3 (0, 0.5f, 0f), Quaternion.identity, caja_punteros.GetComponent<Transform> ());
        //obtengo los punteros
        point point_ = new point ();
        scr_puntero scr_p = p_.GetComponent<scr_puntero> ();
        //algunos datos generales
        scr_p.p_yo = points.Count;
        point_.p_yo = points.Count;
        print ("a");
        //si es predefinido
        if (predefinido) {
            //agregar_datos_predefinidos (p_);
            scr_p.borrar = true;
            if (points.Count == 0) {
                //es el inicio
                //son las el point objeto
                scr_p.p_der = 1;
                scr_p.p_izq = 0;
                //scr_p.p_yo = 0;
                scr_p.t_der = pista.clip.length;
                scr_p.t_izq = 0f;
                scr_p.t_yo = 0f;
                scr_p.inicio = true;

                //son las del point clase
                point_.p_der = 1;
                point_.p_izq = 0;
                //point_.p_yo = 0;
                point_.t_der = pista.clip.length;
                point_.t_izq = 0f;
                point_.t_yo = 0f;
            } else if (points.Count == 1) {
                //es el final
                //son las del point objeto
                scr_p.p_der = 1;
                scr_p.p_izq = 0;
                //scr_p.p_yo = 1;
                scr_p.t_der = pista.clip.length;
                scr_p.t_izq = 0f;
                scr_p.t_yo = pista.clip.length;
                scr_p.final = true;
                //son las del point clase
                point_.p_der = 1;
                point_.p_izq = 0;
                //point_.p_yo = 1;
                point_.t_der = pista.clip.length;
                point_.t_izq = 0f;
                point_.t_yo = pista.clip.length;
            }
        } else {
            print ("b");
            //le doy su posicion en x
            Vector3 pos = p_.GetComponent<Transform> ().position;
            //colocamos los datos generales
            if (time_ == 0f) {
                scr_p.t_yo = pista.time;
                point_.t_yo = pista.time;
                p_.GetComponent<Transform> ().position = new Vector3 (0f, pos.y, pos.z);
            } else {
                scr_p.t_yo = time_;
                point_.t_yo = time_;
                p_.GetComponent<Transform> ().position = new Vector3 (point_.t_yo, pos.y, pos.z);
            }

            //buscamos los punteros izq y der
            int c_a = 0, c_b = 1, p_izq_ = 0, p_der_ = 1;
            //los punteros no estan ordenados
            //porlo cual debemos iniciar el inicio por el 0 y el final por el 1
            //e ir buscando entre sus nodos hasta llegar al que queremos
            for (int i = 0; i < points.Count; i++) {
                //tengo que verificar el ultimo primero
                //revisamos que nuestro time sea menos que los de los extremos
                //print (c_b + "//" + points[c_b].t_yo + "!!" + scr_p.t_yo + "");
                if (points[c_b].t_yo > scr_p.t_yo) {
                    //si es mayor puede ser nuestro p_der
                    p_der_ = c_b;
                    scr_p.p_der = p_der_;
                    point_.p_der = p_der_;
                    c_b = points[c_b].p_izq;
                    print ("d");
                } else {
                    //sino es correcto entonces nos hemos pasado de reversa
                    //debemos cambiar los valores de el nodo der
                    points[p_der_].p_izq = scr_p.p_yo;
                    obj_points[p_der_].GetComponent<scr_puntero> ().p_izq = scr_p.p_yo;
                    print ("e");
                    break;
                }
            }
            for (int i = 0; i < points.Count; i++) {
                //tengo que verificar el ultimo primero
                //revisamos que nustro time sea menos que los de los extremos 
                if (points[c_a].t_yo < scr_p.t_yo) {
                    p_izq_ = c_a;
                    scr_p.p_izq = p_izq_;
                    point_.p_izq = p_izq_;
                    c_a = points[c_a].p_der;
                    print ("f");
                } else {
                    //sino nos hemos pasado 
                    points[p_izq_].p_der = scr_p.p_yo;
                    obj_points[p_izq_].GetComponent<scr_puntero> ().p_der = scr_p.p_yo;
                    print ("g");
                    break;
                }
            }
            print ("c");
        }

        //print (scr_p.p_izq.ToString () + "/" + scr_p.p_der.ToString ());
        //creo lo que que guardare despues 
        //le coloco como editor este script
        //le doy sus primeros datos 
        scr_p.editor = this;
        scr_p.speed = 1f;
        point_.speed = 1f;
        point_.dura = dur_barra;
        //p_.name = "Point" + scr_p.p_yo.ToString ();
        //n_editable.points.Add (point_);
        obj_points.Add (p_);
        points.Add (point_);

    }

    public void agregar_datos_predefinidos (GameObject p_) {
        point point_ = new point ();
        scr_puntero scr_p = p_.GetComponent<scr_puntero> ();
        scr_p.borrar = true;
        if (points.Count == 0) {
            //es el inicio
            //son las el point objeto
            scr_p.p_der = 0;
            scr_p.p_izq = 1;
            scr_p.p_yo = 0;
            scr_p.t_der = 0f;
            scr_p.t_izq = pista.clip.length;
            scr_p.t_yo = 0f;
            scr_p.inicio = true;
            //son las del point clase
            point_.p_der = 0;
            point_.p_izq = 1;
            point_.p_yo = 0;
            point_.t_der = 0f;
            point_.t_izq = pista.clip.length;
            point_.t_yo = 0f;
        } else if (points.Count == 1) {
            //es el final
            //son las del point objeto
            scr_p.p_der = 0;
            scr_p.p_izq = 1;
            scr_p.p_yo = 1;
            scr_p.t_der = 0f;
            scr_p.t_izq = pista.clip.length;
            scr_p.t_yo = pista.clip.length;
            scr_p.final = true;
            //son las del point clase
            point_.p_der = 0;
            point_.p_izq = 1;
            point_.p_yo = 1;
            point_.t_der = 0f;
            point_.t_izq = pista.clip.length;
            point_.t_yo = pista.clip.length;
            //le doy su posicion
            Vector3 pos = p_.GetComponent<Transform> ().position;
            p_.GetComponent<Transform> ().position = new Vector3 (pista.clip.length, pos.y, pos.z);
        }

        scr_p.dura = 0.2f;
        scr_p.speed = 1f;
        point_.dura = 0.2f;
        point_.speed = 1f;

        obj_points.Add (p_);
        points.Add (point_);

    }

    public bool quitar_puntero (int p_yo) {
        //quitara un puntero, de la lista, actualizara los demas,
        //mandara un bool si se logro borrar, y lo demas y se borrara el gameobject

        return true;
    }

    public void mover_pista (float limit) {
        //tiene que mover el b_pista dependiendo del tiempo de la musica
        //float step = speed * Time.deltaTime;
        Vector3 pos = tra_b_pista.position;
        //movemos
        //tra_b_pista.position = Vector3.MoveTowards (pos, new Vector3 (limit, pos.y, pos.z), step);
        tra_b_pista.position = new Vector3 (limit, pos.y, pos.z);
    }

    public void der_izq_pista (float dir_) {
        Vector3 pos = tra_b_pista.position;
        //el contador de la barra de audio aumenta y cuando llega a 10 la dis se duplica, y el contador a 0
        c_dir_izq_audio += 1;
        if (c_dir_izq_audio > ran_c_dir_audio) {
            mas_dis_pista *= 2;
            c_dir_izq_audio = 0;
        }
        float n_pos = pos.x + ((dis_m_pista_ * dir_) * mas_dis_pista);
        if (n_pos < (tra_b_audio.localScale.x * -1)) {
            n_pos = tra_b_audio.localScale.x * -1;
            c_dir_izq_audio = 0;
        } else if (n_pos > 0) {
            n_pos = 0f;
            c_dir_izq_audio = 0;
        }
        pista.time = n_pos * -1;
        tra_b_pista.position = new Vector3 (n_pos, pos.y, pos.z);
    }

    public void p_dir_iz_audio_ () {
        //pone a 0 el contador de movimiento de la barra de audio
        c_dir_izq_audio = 0;
        mas_dis_pista = 1;
    }

    public void guardar () {
        //se guardara en un archivo json
        scr_nivel n_ = new scr_nivel ();
        n_.points = n_editable.points;
        GetComponent<f_archivos> ().escribir_archivo (n_);
        print ("fin guardado");
    }

    public void cargar () {
        scr_nivel n_ = GetComponent<f_archivos> ().leer_archivo ();
        n_editable.points = n_.points;
        iniciar ();
        print ("fin iniciado");
    }

}