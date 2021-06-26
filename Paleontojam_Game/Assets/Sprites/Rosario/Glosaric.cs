using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Glosaric : MonoBehaviour
{
    public Image glosario;
    [SerializeField]private GameObject SalirDefinicion;
    [SerializeField] private GameObject SalirDefinicionDDefinicion;
    [SerializeField]private List<GameObject> spritestodesact;
    public void ActivarDefinicion(Sprite DEfinicion)
    {
        for (int i = 0; i < spritestodesact.Count; i++)
        {
            spritestodesact[i].SetActive(false);
        }
        glosario.sprite = DEfinicion;
        SalirDefinicion.SetActive(false);
        SalirDefinicionDDefinicion.SetActive(true);
    }
    public void Salir(Sprite DEfinicion)
    {
        for (int i = 0; i < spritestodesact.Count; i++)
        {
            spritestodesact[i].SetActive(true);
        }
        glosario.sprite = DEfinicion;
        SalirDefinicion.SetActive(true);
        SalirDefinicionDDefinicion.SetActive(false);
    }
}
