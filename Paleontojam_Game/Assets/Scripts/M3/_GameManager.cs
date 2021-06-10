using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class _GameManager : MonoBehaviour
{
    public static _GameManager Instance { get; private set; }

    [SerializeField]
    Text mensaje = null;

    Temporizador restartTimer;

    private void Start()
    {
        restartTimer = gameObject.AddComponent<Temporizador>();
        restartTimer.Duracion = 3f;
    }

    private void Update()
    {
        if (restartTimer.Finalizado)
        {
            Message("");
            LoadLevel3();

            restartTimer.ReiniciarEstado();
        }
    }

    public static void RunRestartTimer()
    {
        Instance.restartTimer.Iniciar();
    }

    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Escena1");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Minigame3");
    }

    public static void LoadLevel2()
    {
        SceneManager.LoadScene("minigame2");
    }

    static public void Message(string msg)
    {
        Instance.mensaje.text = msg;
    }
}
