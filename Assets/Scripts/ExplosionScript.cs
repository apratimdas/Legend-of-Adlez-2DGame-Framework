using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {

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

    //// Use this for initialization
    //void Start () {
    //       Invoke("Die", 1f);
    //}

    //   void Die()
    //   {
    //       Destroy(gameObject);
    //   }
}
