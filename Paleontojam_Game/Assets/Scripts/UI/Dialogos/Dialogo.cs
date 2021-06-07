using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Para mostrar dialogos en la UI
/// </summary>
public class Dialogo : MonoBehaviour
{
    #region Campos

    // soporte para dialogo
    [SerializeField]
    string nombre = null;

    [SerializeField]
    [TextArea(3, 10)]
    string[] oraciones = null;
    Queue<string> dialogo;

    float velocidadDeEscrituraPorCaracter = .02f;

    // soporte para UI
    [SerializeField]
    Text uiTextoNombre = null;
    [SerializeField]
    Text uiTextoDialogo = null;

    // soporte para estado de dialogo
    bool iniciado = false;
    bool corriendo = false;

    #endregion

    #region Propiedades

    /// <summary>
    /// Estado de progreso del dialogo
    /// </summary>
    /// <value>Retorna verdadero si se finaliza el dialogo, de otra manera, falso</value>
    public bool Finalizado
    {
        get { return iniciado && !corriendo; }
    }

    /// <summary>
    /// Estado de inicio del dialogo
    /// </summary>
    /// <value>Verdadero si el dialogo ha sido inciado, de otra manera, falso</value>
    public bool Iniciado
    {
        get { return iniciado; }
    }

    /// <summary>
    /// Estado de la presentacion actual del dialogo
    /// </summary>
    /// <value>Verdadero si el dialogo se esta mostrando en pantalla, de otra manera, falso</value>
    public bool Corriendo
    {
        get { return corriendo; }
    }

    /// <summary>
    /// Nombre de personaje al que pertence el dialogo
    /// </summary>
    public string Nombre
    {
        get { return nombre; }
    }

    #endregion

    #region Metodos

    /// <summary>
    /// Inicializa la cola de dialogo
    /// </summary>
    void Start()
    {
        dialogo = new Queue<string>();
    }

    /// <summary>
    /// Inicia el dialogo en cuestion si no se ha iniciado aun
    /// </summary>
    public void IniciarDialogo()
    {
        if (!corriendo)
        {
            corriendo = true;
            iniciado = true;
            uiTextoNombre.text = nombre;

            dialogo.Clear();

            // guardando todas las oraciones en cola
            foreach(string e in oraciones)
            {
                dialogo.Enqueue(e);
            }

            SiguienteOracion();
        }
    }

    /// <summary>
    /// Muestra la siguiente oracion en la cola de dialogo
    /// </summary>
    public void SiguienteOracion()
    {
        if (corriendo)
        {
            if(dialogo.Count != 0)
            {
                string nuevaOracion = dialogo.Dequeue();
                uiTextoDialogo.text = "";

                StopAllCoroutines();
                StartCoroutine(EscribirOracion(nuevaOracion));
            }
            else
            {
                corriendo = false;
            }
        }    
    }

    /// <summary>
    /// Escribe la oracion del dialogo en la caja de dialogo caracter por caracter
    /// </summary>
    /// <param name="oracion">Oracion a escribir</param>
    IEnumerator<WaitForSeconds> EscribirOracion(string oracion)
    {
        foreach(char c in oracion)
        {
            uiTextoDialogo.text += c;
            yield return new WaitForSeconds(velocidadDeEscrituraPorCaracter);
        }
    }

    #endregion
}
