using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Un temporizador que puede tener cualquier duracion para manejar eventos tiempo-dependientes
/// </summary>
public class Temporizador : MonoBehaviour
{
    #region Campos

    // soporte de inicializacion
    float tiempoTranscurrido;
    float tiempoObjetivo;

    // soporte de estados del temporizador
    bool iniciado = false;
    bool corriendo = false;

    #endregion

    #region Propiedades

    /// <summary>
    /// Duracion del temporizador, puede ser cambiada mientras el temporizador corre
    /// </summary>
    public float Duracion
    {
        get { return tiempoObjetivo; }
        set { tiempoObjetivo = value; }
    }

    /// <summary>
    /// Permite conocer si el temporizador se ha finalizado
    /// </summary>
    /// <value>Retorna verdadero si ya se finalizó, falso si no</value>
    public bool Finalizado
    {
        get
        {
            return iniciado && !corriendo;
        }
    }

    /// <summary>
    /// Muestra si el temporizador se esta corriendo.
    /// </summary>
    /// <value>Retorna verdadero si está corriendo, falso si no</value>
    public bool Corriendo
    {
        get { return corriendo; }
    }

    /// <summary>
    /// Tiempo restante en el temporizador
    /// </summary>
    public float TiempoRestante
    {
        get { return tiempoObjetivo - tiempoTranscurrido; }
    }
    #endregion

    #region Metodos

    /// <summary>
    /// Inicia el temporizador si la duracion es mayor que 0, 
    /// sirve tambien para reiniciar el temporizador
    /// </summary>
    public void Iniciar()
    {
        if (!corriendo)
        {
            if (tiempoObjetivo > 0)
            {
                iniciado = true;
                corriendo = true;
                tiempoTranscurrido = 0;
            }
        }
    }


    /// <summary>
    /// Actualiza el tiempo transcurrido en el temporizador
    /// </summary>
    void Update()
    {
        if (corriendo)
        {
            tiempoTranscurrido += Time.deltaTime;
            if (tiempoTranscurrido > tiempoObjetivo)
            {
                corriendo = false;
            }
        }
    }

    /// <summary>
    /// Reinicia el temporizar al estado inicial (como si nunca se hubiera iniciado)
    /// </summary>
    public void ReiniciarEstado()
    {
        iniciado = false;
        corriendo = false;
    }

    #endregion
}
