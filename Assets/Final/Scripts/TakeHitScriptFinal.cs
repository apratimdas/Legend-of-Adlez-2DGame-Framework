using UnityEngine;
using System.Collections;

public class TakeHitScriptFinal : MonoBehaviour {

    public float maxhealth = 100f;
    private float currenthealth;

	// Use this for initialization
	void Start () {
        currenthealth = maxhealth;
	}

    public void damage(float dmg)
    {
        currenthealth -= dmg;

        if (currenthealth <= 0)
            Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
