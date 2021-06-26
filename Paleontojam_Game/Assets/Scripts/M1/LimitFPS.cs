using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitFPS : MonoBehaviour
{
    public DialogoTrigger dialogoTrigger;//esto no deberia ir aca pero estoy apurado y quier que el texto salga
    // Start is called before the first frame update
    void Start()
    {
        print("A");
        ButtonManager.instancia.DesactiveInStart1[2].SetActive(true);
        dialogoTrigger.IniciarDialogo();
        print("B");
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DialogoManager.Instancia.MostrarSiguienteOracion();
            if (DialogoManager.Instancia.Finalizado)
            {
                Destroy(ButtonManager.instancia.DesactiveInStart1[2]);
            }
        }
    }
}
