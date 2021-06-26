using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialog : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IniciarDialogo());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!DialogoManager.Instancia.Finalizado)
                DialogoManager.Instancia.MostrarSiguienteOracion();
        }
    }

    IEnumerator<WaitForSeconds> IniciarDialogo()
    {
        yield return new WaitForSeconds(1.5f);
        DialogoTrigger dialogo = FindObjectOfType<DialogoTrigger>();
        dialogo.IniciarDialogo();
    }
}
