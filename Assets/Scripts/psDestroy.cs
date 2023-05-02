using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psDestroy : MonoBehaviour
{


    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {

        StartCoroutine(destroyPS());
    }

    [System.Obsolete]
    IEnumerator destroyPS()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();


        yield return new WaitForSeconds(ps.startLifetime); // set the delay the same as the partica
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
