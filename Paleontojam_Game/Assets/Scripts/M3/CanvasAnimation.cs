using UnityEngine;
using System.Collections;

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

    public void AnimarCanvas()
    {
        dientes.SetTrigger("NivelFinalizado");
        StartCoroutine(AnimarGorgoHada(dientes));
    }

    IEnumerator AnimarGorgoHada(Animator currentAnimation)
    {
        AudioManager.PlayOneShot(AudioClipName.MusicalSFXGo, true);
        yield return new WaitForSeconds(currentAnimation.GetCurrentAnimatorStateInfo(0).length);
        gorgoHada.SetTrigger("NivelFinalizado");
        yield return new WaitForSeconds(0.5f);
        titulo.SetTrigger("NivelFinalizado");
    }
}
