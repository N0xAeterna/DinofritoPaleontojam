using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Amber : MonoBehaviour
{
    bool destroyed = false;

    public bool Destroyed { get { return destroyed; } }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Rail")
        {
            _GameManager.Message("Oh no! has destruido el fosil");
            _GameManager.RunRestartTimer();
            destroyed = true;
        }
    }
}
