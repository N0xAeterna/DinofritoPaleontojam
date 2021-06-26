using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ctrl_game : MonoBehaviour {
    public Transform marco, b_player, b_point;
    public bool inicia, pulsar, rebote_l, on_play, point_moment, al_cambio_coloco_point, pausa, prueba;
    public float limit_1, limit_2, speed, point_dura, con1_;
    public List<point> points = new List<point> ();
    public List<obj_point> obj_points_ = new List<obj_point> ();
    public Text t_actual, t_total, t_pista_;
    public List<Sprite> ima_bochon_ = new List<Sprite> ();
    public int p_t_actual, p_time_barra, p_actual, p_total, c_obj_point;
    public AudioSource pista;
    public GameObject obj_point_, img_bien, img_mal, ima_bochon; 

    void Start () {
        inicia = false;
        pulsar = true;
        b_point.position = new Vector3 (8f, 5.6f, 1f);
        //Instantiate (obj_point_, b_player.GetComponent<Transform> ().poslition, Quaternion.identity, marco.GetComponent<Transform> ());
        GameObject n_ = Instantiate (obj_point_, b_point.position, Quaternion.identity, marco.GetComponent<Transform> ());
        c_obj_point = 1;
        n_.GetComponent<obj_point> ().control = this;
        obj_points_.Add (n_.GetComponent<obj_point> ());
        p_time_barra = 2;
        mal_ ();
        on_play = true;
        iniciar_juego ();
    }

    // Update is called once per frame
    void Update () {
        if (on_play) {
            //con1_ = con1_ + (1 * Time.deltaTime);
            //t_pista_.text = pista.time.ToString ();
            //si ya se ha iniciado el nivel
            if (inicia) {
                if (!pista.isPlaying && pausa || pista.time > points[1].t_yo) {
                    //esto indica que ha finalizado la musica y que no estamos en pausa
                    //print ("finalizado");
                    on_play = false;
                    inicia = false;
                    pulsar = false;
                    point_moment = false;
                    finalizar_juego ();
                    pista.Stop ();
                    mal_ ();
                }
                rebotar ();
                //print ("estoy");
                if (click ()) {
                    //si hemos presionado
                    if (pulsar) {
                        //si podemos pulsar
                        pulsar = false;
                        point_moment = false;
                        // print (p_actual.ToString ());
                        if (prueba) {
                            img_bien.SetActive (true);
                            img_mal.SetActive (false);

                            AudioManager.PlayOneShot(AudioClipName.MusicalSFXOne, false);
                        } else {
                            //ima_bochon_
                        }
                        //p_time_actual = points[p_time_actual].p_der;
                    } else {
                        //si no podemos pulsar
                        p_actual -= 1;
                        t_actual.text = p_actual.ToString ();
                        //print (p_actual.ToString ());
                        if (prueba) {
                            img_bien.SetActive (false);
                            img_mal.SetActive (true);

                            AudioManager.PlayOneShot(AudioClipName.MusicalSFXFail, false);
                        } else {
                            //ima_bochon_
                        }
                    }
                }
            }
            //si estamos iniciando el nivel
            else {
                //si podemos pulsar, por pruebas
                if (pulsar) {
                    //print ("iniio");
                    //si se ha pulsado
                    if (click ()) {
                        if (prueba) {
                            img_bien.SetActive (true);
                            img_mal.SetActive (false);
                        } else {
                            //ima_bochon_
                        }
                        inicia = true;
                        pulsar = false;
                        point_moment = false;
                        //p_time_actual = points[p_time_actual].p_der;
                        p_time_barra = points[p_time_barra].p_der;
                        colocar_points (limit_2);
                        marco.transform.GetChild (0).gameObject.GetComponent<obj_point> ().llamar_destruir (point_dura);
                        pista.Play ();
                        Invoke ("mal_", 0.3f);
                    }
                }
            }

        }
        //if (Input.GetKeyDown (KeyCode.R)) {
        //    print ("ottro");
        //    Application.LoadLevel (0);
        //}

        if (Input.GetKeyDown (KeyCode.Escape)) {
            print ("pausa");
            //atras ();
        }
    }

    bool click () {
        if (Input.GetKeyDown ("a")) {
            print ("presionas");
            return true;
        }
        return false;
    }

    void rebotar () {
        //se tiene que mover hasta llegar al punto
        float limit = 0f;
        float step = speed * Time.deltaTime;
        Vector3 pos = b_player.position;
        //camabiamos el reebote
        if (b_player.position.x == limit_1) {
            rebote_l = false;
            //cambiamos el limite
            limit = limit_2;
            colocar_points (limit);
            speed += 0.4f;
            //print (con1_ + "");
            //con1_ = 0;
            print ("reboteb");
        } else if (b_player.position.x == limit_2) {
            rebote_l = true;
            //cambiamos el limite 
            limit = limit_1;
            colocar_points (limit);
            speed += 0.4f;
            print ("rebotea");
            //print (con1_ + "");
            //con1_ = 0;
            //print (pista.time + "pista");
        } else {
            print ("no");
        }
        if (rebote_l) {
            limit = limit_1;
        } else {
            limit = limit_2;
        }
        //movemos
        b_player.position = Vector3.MoveTowards (pos, new Vector3 (limit, pos.y, pos.z), step);
        print ("p");
    }

    public void iniciar_juego () {
        on_play = true;
        scr_nivel n_ = GetComponent<f_archivos> ().leer_archivo ();
        points = n_.points;
        t_total.text = points.Count.ToString ();
        t_actual.text = points.Count.ToString ();
        p_actual = points.Count;
        p_total = points.Count;
        p_t_actual = 0;
        p_time_barra = 0;
        points = n_.points;
    }

    public void colocar_points (float limit) {
        /*calculo cuantos points puedo colocar desde una esquina de la barra
            si es 4.6 estamos en la izq y debemos recorrer 9.2m
            //la pos es o 4.6 o -4.6 
            //9.12, esto entre la speed, nos dara cuanto tiempo tardar en recorrer la barra
            //sped 1 =1 m/s , speed 3 = 3m/s
            //9.12/1 = 9.12, 9.12/3 = 3.04 
            //tengo que sacar la distancia atual hasta el punto de rebote
            //puede ser 4.6 o -4.6
            //print (b_player.position.x + "/" + points[p_time_actual].t_yo + "");
            //print (pista.time);
            //print (((9.21 / speed) + pista.time) + ""); //tengo que sacar 
        //*/

        float limit_pista = (16f / speed) + pista.time;
        //print (limit_pista + "pista_time");

        for (var i = 0; i < 10; i++) {
            //print (p_time_barra + "p_time_barra");
            if (points[p_time_barra].t_yo < limit_pista) {
                //entramos
                colocar_point ();
                p_time_barra = points[p_time_barra].p_der;
                //print (limit_pista + "&" + p_time_barra + "");
            } else {
                break;
            }
        }
    }

    public void colocar_point () {

        int dir_ = -1;
        if (rebote_l) {
            dir_ = 1;
        }
        Vector3 pos = b_point.position;
        /*tengo un limit1 y un limit2, esotos son 4.6 y -4.6,
            //el centro es 0, el player tambien es 0,
            //debo de saber donde esta el player
            //y cuanto falta para que aparesca el siguiente point
            //si esta a 4 y falta 2 seria en 6 y eso no es posible
            //por lo cual debo de esperar 0.6 y volver a sacar el calculo
            //12-10=2 ese seria lo que habria que sumarle al player para poner el puntero
        */
        //3 + 2 = 5 se pasa, entonces esperar a 4.6 = 11.6
        pos.x = (b_player.position.x + ((points[p_time_barra].t_yo - pista.time) * dir_) * speed);

        //print (pos.x + "pos-x");
        GameObject n_point = Instantiate (obj_point_, pos, Quaternion.identity, marco.GetComponent<Transform> ());
        n_point.GetComponent<obj_point> ().control = this;
        //n_point.GetComponent<obj_point> ().point = points[p_time_barra].p_yo;
        n_point.GetComponent<obj_point> ().point = obj_points_.Count;
        n_point.name = "p_" + points[p_time_barra].p_yo + "";
        obj_points_.Add (n_point.GetComponent<obj_point> ());

    }

    public void hemos_salido (int _p_) {
        //print ("salimos3");
        obj_points_[_p_].llamar_destruir (0);
        if (pulsar || point_moment) {
            p_actual -= 1;
            point_moment = false;
            pulsar = false;
            t_actual.text = p_actual.ToString ();
            if (prueba) {
                img_bien.SetActive (false);
                img_mal.SetActive (true);

                AudioManager.PlayOneShot(AudioClipName.MusicalSFXFail, false);
            } else {
                //ima_bochon_
            }
        }
        ima_bochon.GetComponent<SpriteRenderer>().sprite = ima_bochon_[p_t_actual];
        p_t_actual += 1;
        Invoke ("mal_", 0.3f);
    }

    public void hemos_entrado (int _p_) {
        pulsar = true;
        point_moment = true;
    }

    public void atras () {
        on_play = false;
        pista.Stop ();
        pulsar = false;
        point_moment = false;
        inicia = false;
        obj_points_.Clear ();
        b_point.position = new Vector3 (6f, 5.5f, 1f);
    }

    void mal_ () {
        if (prueba) {
            //SaveAndLoad.instancia.GuardarNumErrores(SaveAndLoad.instancia.CargarNumErrores().NumOfLose + 1);
            //print(SaveAndLoad.instancia.CargarNumErrores().NumOfLose);
            img_bien.SetActive (false);
            img_mal.SetActive (false);
        } else {
            //SaveAndLoad.instancia.GuardarNumErrores(SaveAndLoad.instancia.CargarNumErrores().NumOfLose + 1);
            //print(SaveAndLoad.instancia.CargarNumErrores().NumOfLose);
            //ima_bochon_
        }
    }

    public void finalizar_juego () {
        if (p_actual < (p_total / 2)) {
            //hemos perdido
            
            print ("perdiste");
        } else {
            //hemos ganado
            print ("has ganado");
        }

        CanvasAnimation.Instancia.AnimarCanvas(SceneManager.GetActiveScene().buildIndex + 1);
    }

}