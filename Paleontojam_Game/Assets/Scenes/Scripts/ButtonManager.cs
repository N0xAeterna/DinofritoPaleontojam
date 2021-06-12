using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instancia;
    [SerializeField]private List<GameObject> DesactiveInStart;
 

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
    }

   
 public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SiguienteNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
