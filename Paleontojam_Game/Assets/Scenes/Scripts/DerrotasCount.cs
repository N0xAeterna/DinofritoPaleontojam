using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DerrotasCount : MonoBehaviour
{
    public static DerrotasCount instancia; 
    private int vecesperdidas;

    public int Vecesperdidas { get => vecesperdidas; set => vecesperdidas = value; }

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
    }
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
