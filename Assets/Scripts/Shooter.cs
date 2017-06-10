using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour
{

    enum attackmode { single, stream };
    private attackmode test = attackmode.stream;
    [SerializeField]
    private Transform player;
    public GameObject puff;
    private bool reloaded = true;
    public int poolSize = 30;
    public bool poolCanGrow = true;
    public float spawnTime = 0.5f;
    private int currLength;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("AttackMode");
        //currLength = spawnPoints.Length;
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        //InvokeRepeating("Spawnstream", spawnTime, spawnTime);
        //InvokeRepeating("Spawnsingle", spawnTime*10, spawnTime*10);
    }

    IEnumerator AttackMode()
    {
        while(true)
        {
            yield return new WaitForSeconds(6f);
            test = test == attackmode.stream ? attackmode.single : attackmode.stream;
            reloaded = false;
        }
    }

    void Spawn()
    {
        if (test == attackmode.stream)
            Spawnstream();
        else
            Spawnsingle();
    }
    void Spawnstream()
    {
        GameObject obj = Instantiate(puff);
        obj.transform.position = transform.position;
        obj.transform.SetParent(transform);
        Vector3 direction = player.transform.position - transform.position;
        obj.GetComponent<Rigidbody2D>().AddForce(direction.normalized * 500);
    }
    void Spawnsingle()
    {
        if (reloaded)
        {
            GameObject obj = Instantiate(puff);
            obj.transform.position = transform.position;
            obj.transform.SetParent(transform);
            Vector3 direction = player.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(direction.normalized * 1000);
            reloaded = false;
        }
        else
            StartCoroutine("Reload");
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1f);
        reloaded = true;
    }
    // Update is called once per frame
    void Update()
    {


    }
}
