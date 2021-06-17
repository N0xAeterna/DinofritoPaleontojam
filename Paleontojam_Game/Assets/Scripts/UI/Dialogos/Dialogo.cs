using UnityEngine;

/// <summary>
/// Contienen un nombre y una colecion de oraciones
/// </summary>
[System.Serializable]
public class Dialogo
{
    #region Campos
    // soporte para dialogo
    [SerializeField]
    string nombreDePersonaje = null;

    [SerializeField]
    [TextArea(6, 25)]
    string[] oraciones = null;
    #endregion

    #region Propiedades
    public string NombreDePersonaje
    {
        get { return nombreDePersonaje; }
    }

    public string[] Oraciones
    {
        get { return oraciones; }
    }
    #endregion
}
