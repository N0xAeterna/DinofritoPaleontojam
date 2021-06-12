using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Administrador dialogos
/// </summary>
public class DialogoManager : MonoBehaviour
{

    #region Campos

    // soporte para UI, aqui se pone el texto para el nombre
    // y el texto donde se escribiran los dialogos
    [SerializeField]
    Text txtNombre = null;
    [SerializeField]
    Text txtOracion = null;

    // soporte para dialogo
    Queue<string> oraciones;

    // singleton
    static DialogoManager instancia;

    // soporte de estado
    bool iniciado = false;
    bool corriendo = false;

    // velocidad de escritura
    const float TiempoDeEsperaPorCaracter = .01f;

    #endregion

    #region Propiedades
    /// <summary>
    /// Instancia del singleton
    /// </summary>
    public static DialogoManager Instancia
    {
        get { return instancia; }
    }

    /// <summary>
    /// Estado de finalizacion de dialogo
    /// </summary>
    /// <value>Verdadero si ya termino</value>
    public bool Finalizado
    {
        get { return iniciado && !corriendo; }
    }

    #endregion

    #region Metodos

    /// <summary>
    /// Singleton y inicializacion de oraciones
    /// </summary>
    private void Start()
    {
        if(instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        oraciones = new Queue<string>();
    }
    
    /// <summary>
    /// Inicia el dialogo
    /// </summary>
    /// <param name="dialogo">Dialogo a mostrar</param>
    public void IniciarDialogo(Dialogo dialogo)
    {
        if (!corriendo)
        {
            oraciones.Clear();
            foreach (string oracion in dialogo.Oraciones)
            {
                oraciones.Enqueue(oracion);
            }

            txtNombre.text = dialogo.NombreDePersonaje;

            iniciado = true;
            corriendo = true;

            MostrarSiguienteOracion();
        }
    }

    /// <summary>
    /// Escribe la siguiente oracion del dialogo
    /// </summary>
    public void MostrarSiguienteOracion()
    {
        if (corriendo)
        {
            if (oraciones.Count == 0)
            {
                FinalizarDialogo();
                return;
            }

            string oracion = oraciones.Dequeue();
            StopAllCoroutines();
            StartCoroutine(EscribirOracion(oracion));
        }
    }

    /// <summary>
    /// Escribe la oracion caracter por caracter
    /// </summary>
    /// <param name="oracion">Oracion a escribir</param>
    IEnumerator<WaitForSeconds> EscribirOracion(string oracion)
    {
        txtOracion.text = "";

        foreach(char letra in oracion.ToCharArray())
        {
            if(txtOracion != null)
                txtOracion.text += letra;

            yield return new WaitForSeconds(TiempoDeEsperaPorCaracter);
        }
    }

    /// <summary>
    /// Actualiza estado de dialogo
    /// </summary>
    void FinalizarDialogo()
    {
        corriendo = false;
    }

    #endregion
}
