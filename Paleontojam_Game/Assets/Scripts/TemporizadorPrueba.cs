using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Prueba demostrativa del temporizador
/// </summary>
public class TemporizadorPrueba : MonoBehaviour
{
    // soporte para temporizador
    Temporizador prueba;

    /// <summary>
    /// inicializa el temporizador
    /// </summary>
    void Start()
    {
        // instanciando temporizador
        prueba = gameObject.AddComponent<Temporizador>();
        prueba.Duracion = 1;
        prueba.Iniciar();
    }

    /// <summary>
    /// Resolviendo finalizacion del temporizador
    /// </summary>
    void Update()
    {
        if (prueba.Finalizado)
        {
            print("Se ha finalizado el temporizador");

            // reiniciando
            prueba.Iniciar();
        }
    }
}
