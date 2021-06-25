using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveModels : MonoBehaviour
{
    private bool OneTIme;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
     
        if (!OneTIme)
        {
            if (SaveAndLoad.instancia.CargarNumErrores().NumOfLose < 5)
            {
                ButtonManager.instancia.DesactiveInStart1[0].SetActive(true);
                ButtonManager.instancia.DesactiveInStart1[1].SetActive(true);
                print("0");
                SaveAndLoad.instancia.GuardarNumErrores(0);
                OneTIme = true;
            }
            else if (SaveAndLoad.instancia.CargarNumErrores().NumOfLose < 10)
            {
                ButtonManager.instancia.DesactiveInStart1[2].SetActive(true);
                print("1");
                SaveAndLoad.instancia.GuardarNumErrores(0);
                OneTIme = true;
            }
            else if (SaveAndLoad.instancia.CargarNumErrores().NumOfLose < 15)
            {
                ButtonManager.instancia.DesactiveInStart1[3].SetActive(true);
                print("1");
                SaveAndLoad.instancia.GuardarNumErrores(0);
                OneTIme = true;
            }
            else
            {
                ButtonManager.instancia.DesactiveInStart1[4].SetActive(true);
                print("2");
                SaveAndLoad.instancia.GuardarNumErrores(0);
                OneTIme = true;
            }
        }
      
    }
}
