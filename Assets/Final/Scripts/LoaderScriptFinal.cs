using UnityEngine;
using System.Collections;

public class LoaderScriptFinal : MonoBehaviour
{
    public GameObject gameManager;

    void Awake()
    {
        if (GameManager.Instance == null)
        {
            Instantiate(gameManager);
        }
    }
}