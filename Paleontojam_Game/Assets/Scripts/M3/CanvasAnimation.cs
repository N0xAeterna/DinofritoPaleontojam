using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CanvasAnimation : MonoBehaviour
{
    static CanvasAnimation instancia;

    [SerializeField]
    Animator dientes = null;
    [SerializeField]
    Animator gorgoHada = null;
    [SerializeField]
    Animator titulo = null;

    public static CanvasAnimation Instancia { get { return instancia; } }

    void Start()
    {
        if(instancia == null)
        {
            instancia = this;
        }
    }

    public void AnimarCanvas(int sceneToGo)
    {
        dientes.SetTrigger("NivelFinalizado");
        StartCoroutine(AnimarGorgoHada(dientes, sceneToGo));
    }

    IEnumerator AnimarGorgoHada(Animator currentAnimation, int sceneToGo)
    {
        AudioManager.PlayOneShot(AudioClipName.MusicalSFXGo, true);
        yield return new WaitForSeconds(currentAnimation.GetCurrentAnimatorStateInfo(0).length);
        gorgoHada.SetTrigger("NivelFinalizado");
        yield return new WaitForSeconds(0.5f);
        titulo.SetTrigger("NivelFinalizado");

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneToGo);
    }
}
