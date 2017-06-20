using UnityEngine;
using System.Collections;

public class SpawnerLookupScriptFinal : MonoBehaviour {

    public string spawnerName = "WarriorSpawnerParent";

	// Use this for initialization
	void Start () {
	    if (!transform.parent || transform.parent.name != spawnerName)
        {
            GameObject parent = GameObject.Find(spawnerName);

            if (!parent)
            {
                return;
            }

            transform.parent = parent.transform;
            parent.GetComponent<EnemySpawnerScriptFinal>().spawnPoints.Add(transform);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
