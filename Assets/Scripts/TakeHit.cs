using UnityEngine;
using System.Collections;

public class TakeHit : MonoBehaviour {

    public float maxhealth = 100f;
    private float currenthealth;
    [SerializeField]
    private Canvas canvas;

    private bool canvasoff = true;

	// Use this for initialization
	void Start () {
        currenthealth = maxhealth;
	}

    public void damage(float dmg)
    {
        currenthealth -= dmg;
        if (currenthealth <= 0)
            Destroy(gameObject);

        if (canvasoff)
            StartCoroutine("Ouch");
    }

    IEnumerator Ouch()
    {
        canvasoff = false;
        canvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        canvas.gameObject.SetActive(false);
        canvasoff = true;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
