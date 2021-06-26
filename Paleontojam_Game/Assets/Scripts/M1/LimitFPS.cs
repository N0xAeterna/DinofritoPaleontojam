using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitFPS : MonoBehaviour
{
    void Start()
    {
        print("A");
        print("B");
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DialogoManager.Instancia.MostrarSiguienteOracion();
        }
    }
}
