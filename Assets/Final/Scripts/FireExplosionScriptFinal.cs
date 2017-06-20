using UnityEngine;
using System.Collections;

public class FireExplosionScriptFinal : MonoBehaviour {

    public void OnEnable()
    {
        Invoke("Die", 1f);
    }

    void Die()
    {
        gameObject.SetActive(false);
    }

    // to counter double invokes
    public void OnDisable()
    {
        CancelInvoke();
    }
}
