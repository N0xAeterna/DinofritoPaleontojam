using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]private float magnitude;
    [SerializeField]private float duration;
    void Start()
    {
        
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(Shake());
        }
    }
    // Update is called once per frame
    IEnumerator Shake()
    {
        Vector3 poscicionInicial = transform.localPosition;
        float count = 0;
        while (count < duration)
        {
            float x = Random.Range(transform.localPosition.x -1,transform.localPosition.x +1)*magnitude;
                float y = Random.Range(transform.localPosition.y -1,transform.localPosition.y+ 1) * magnitude;
            transform.localPosition = new Vector3(x, y, poscicionInicial.z);
            count += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = poscicionInicial;
    }
}
