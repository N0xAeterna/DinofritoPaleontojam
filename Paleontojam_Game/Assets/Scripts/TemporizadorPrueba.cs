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
    int cambioDeDuracion = 1;

    /// <summary>
    /// inicializa el temporizador
    /// </summary>
    void Start()
    {
        // instanciando temporizador
        prueba = gameObject.AddComponent<Temporizador>();
        prueba.Duracion = cambioDeDuracion++;
        prueba.Iniciar();
    }

    /// <summary>
    /// Resolviendo finalizacion del temporizador
    /// </summary>
    void Update()
    {
        if (prueba.Corriendo)
        {
            print("Temporizador\nDuracion: " + prueba.Duracion + ", Restante: " + prueba.TiempoRestante);
        }

        if (prueba.Finalizado)
        {
            print("Se ha finalizado el temporizador, duracion: " + (cambioDeDuracion - 1));

            // reiniciando
            prueba.Duracion = cambioDeDuracion++;
            prueba.Iniciar();
        }
    }
}
