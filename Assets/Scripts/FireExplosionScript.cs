using UnityEngine;
using System.Collections;

public class FireExplosionScript : MonoBehaviour {

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
