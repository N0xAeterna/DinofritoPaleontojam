using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class _GameHandler : MonoBehaviour
{
    static _GameHandler instancia;
    [SerializeField]
    Text txtEstado = null;

    Temporizador reinicioTemporizador;

    bool iniciado = false;

    private void Start()
    {
        reinicioTemporizador = gameObject.AddComponent<Temporizador>();
        reinicioTemporizador.Duracion = 3f;

        if(instancia == null)
        {
            instancia = this;
        }
    }

    private void Update()
    {
        if(reinicioTemporizador.Finalizado)
        {
            Reiniciar();
        }
    }

    public static _GameHandler Instancia
    {
        get { return instancia; }
    }

    public void Mensaje(string msj)
    {
        if (!iniciado)
        {
            txtEstado.text = msj;
        }
    }

    public void IniciarTemporizador()
    {
        if (!iniciado)
        {
            reinicioTemporizador.Iniciar();
            iniciado = true;
        }
    }

    void Reiniciar()
    {
        string name = SceneManager.GetActiveScene().name;
        txtEstado.text = "";
        SceneManager.LoadScene(name);
        iniciado = false;
    }
}
