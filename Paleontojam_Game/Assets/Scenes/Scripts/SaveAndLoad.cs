using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class SaveAndLoad : MonoBehaviour
{
    public static SaveAndLoad instancia;

    private string path;
    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
    }
    void Start()
    {
        path = Application.dataPath + "/editable/Errors.json";
        print(path);
    }

    
    public void GuardarNumErrores(int Error)
    {
        Datos d = new Datos(Error);
        string Json = JsonUtility.ToJson(d);
        File.WriteAllText(path,Json);
     }
    public Datos CargarNumErrores()
    {
        if (File.Exists(path))
        {
            string text = File.ReadAllText(path);
            Datos d = JsonUtility.FromJson<Datos>(text);
            return d;
        }
        return null;

    }

}
