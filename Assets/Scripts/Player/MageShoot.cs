using UnityEngine;
using System.Collections;

public class MageShoot : MonoBehaviour {
    
    [Tooltip("Put the fireball prefab here")]
    [SerializeField]
    private GameObject fireball;

    public GameObject staff;

    private bool canShoot = true;

    void Update()
    {
        if (canShoot && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)))
        {
            Invoke("ShootFireball", 0.1f);
            canShoot = false;
        }
        if (!staff.activeInHierarchy)
        {
            canShoot = true;
        }
    }

    void ShootFireball()
    {
        GameObject obj = (GameObject)Instantiate(fireball);

        obj.transform.position = staff.transform.position + staff.transform.parent.transform.right * 0.5f;
        obj.transform.rotation = staff.transform.parent.transform.rotation;
        obj.SetActive(true);

        print(obj.name);
    }
}
