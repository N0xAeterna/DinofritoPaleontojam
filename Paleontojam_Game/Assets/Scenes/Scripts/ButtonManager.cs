using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instancia;
    [SerializeField]
    private List<GameObject> DesactiveInStart;


    private bool ActiveDesactiveB;
    public List<GameObject> DesactiveInStart1 { get => DesactiveInStart; set => DesactiveInStart = value; }

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
    }
    void Start()
    {
        foreach (GameObject Desactive in DesactiveInStart)
        {
            Desactive.SetActive(false);
        }

        /* iniciando dialogo */
        DialogoTrigger dialogo = FindObjectOfType<DialogoTrigger>();
        if(dialogo != null)
            StartCoroutine(IniciarDialogo(dialogo));
    }

    IEnumerator<WaitForSeconds> IniciarDialogo(DialogoTrigger dialogo)
    {
        yield return new WaitForSeconds(1.5f);
        dialogo.IniciarDialogo();
    }

   
    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SiguienteNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void ActiveDesactive(GameObject Canvas)
    {
        ActiveDesactiveB = !ActiveDesactiveB;
        Canvas.SetActive(ActiveDesactiveB);
    }
}
