using UnityEngine;
using System.Collections;

public class Player_Testing : MonoBehaviour {

    public float speed = 30f;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetTrigger("Attack");
        }
    }
    
    void FixedUpdate()
    {
        MouseMovement();
    }

    void MouseMovement()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);

        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        GetComponent<Rigidbody2D>().angularVelocity = 0;

        float input = Input.GetAxis("Vertical");

        GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * speed * input);

        if (Input.GetMouseButton(0))
        {
            GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * speed);
        }
    }
}
