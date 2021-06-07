using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsScript : MonoBehaviour
{
    public static ToolsScript instancia;
    [SerializeField]private float Herramienta;
    [SerializeField]private float Distancia;
    public LayerMask layer;
    public LayerMask layerObject;

    public float Herramienta1 { get => Herramienta; set => Herramienta = value; }
    public float Distancia1 { get => Distancia; set => Distancia = value; }

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
    }

  public void changeValor(float valor)
    {
        Herramienta = valor;
    }
}
