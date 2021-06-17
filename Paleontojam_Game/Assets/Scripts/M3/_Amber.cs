using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Amber : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Rail")
        {
            _GameHandler.Instancia.Mensaje("El fosil se ha golpeado demasiado, ya no se puede entregar en este estado");
            _GameHandler.Instancia.IniciarTemporizador();
        }
    }
}
