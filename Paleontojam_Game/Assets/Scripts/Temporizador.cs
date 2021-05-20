using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Un temporizador que puede tener cualquier duracion
/// </summary>
public class Temporizador : MonoBehaviour
{
    #region Campos

    // soporte de inicializacion
    float segundosTranscurridos;
    float segundosTotales;

    // soporte de estados del temporizador
    bool iniciado = false;
    bool corriendo = false;

    #endregion

    #region Propiedades

    /// <summary>
    /// Permite definir la duracion del temporizador mientras no esté corriendo
    /// </summary>
    public float Duracion
    {
        set
        {
            if (!corriendo)
            {
                segundosTotales = value;
            }
        }
    }

    /// <summary>
    /// Permite conocer si el temporizador se ha finalizado
    /// </summary>
    public bool Finalizado
    {
        get
        {
            return iniciado && !corriendo;
        }
    }
    #endregion

    #region Metodos

    /// <summary>
    /// Inicia el temporizador si la duracion es mayor que 0, 
    /// sirve tambien para reiniciar el temporizador
    /// </summary>
    public void Iniciar()
    {
        if(segundosTotales > 0)
        {
            iniciado = true;
            corriendo = true;
            segundosTranscurridos = 0;
        }
    }


    /// <summary>
    /// Actualiza el tiempo transcurrido en el temporizador
    /// </summary>
    void Update()
    {
        if (corriendo)
        {
            segundosTranscurridos += Time.deltaTime;
            if (segundosTranscurridos > segundosTotales)
            {
                corriendo = false;
            }
        }
    }

    #endregion
}
