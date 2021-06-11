using UnityEngine;

/// <summary>
/// Elemento que puede iniciar un dialogo
/// </summary>
public class DialogoTrigger : MonoBehaviour
{
    #region Campos

    // soporte para dialogo
    [SerializeField]
    Dialogo dialogo = null;

    #endregion

    #region Metodos
    /// <summary>
    /// Inicia el dialogo con el administrador de dialogo
    /// </summary>
    public void IniciarDialogo()
    {
        DialogoManager.Instancia.IniciarDialogo(dialogo);
    }

    #endregion
}
