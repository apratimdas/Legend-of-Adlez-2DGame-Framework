using UnityEngine;
using System.Collections;

public class BloodScriptFinal : MonoBehaviour {

    public void OnEnable()
    {
        Invoke("Die", 0.5f);
    }

    void Die()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

    // to counter double invokes
    public void OnDisable()
    {
        CancelInvoke();
    }
}
