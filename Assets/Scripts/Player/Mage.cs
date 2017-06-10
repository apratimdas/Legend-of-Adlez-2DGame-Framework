using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Mage : MonoBehaviour
{
    enum ClickDirection { Right, Left, Up, Down };

    [SerializeField]
    private float speed;

    private Animator myAnimator;
    private Animator swordAnimator;
    private GameObject myCanvas;
    private GameObject mySword;

    public float proj_speed;
    public float health = 30;

    bool rotationlock = false;

    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "blue" || collision.tag == "enemy") return;
    //    health -= 10;
    //    Debug.Log("Collided");
    //    collision.gameObject.SetActive(false);
    //    if (health <= 0)
    //        gameObject.SetActive(false);
    //}

    public void Awake()
    {
        myAnimator = GetComponent<Animator>();
        swordAnimator = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
        myCanvas = gameObject.transform.GetChild(1).gameObject;
        mySword = gameObject.transform.GetChild(0).GetChild(0).gameObject;
    }

    // Use this for initialization
    void Start()
    {
        myCanvas.SetActive(false);
        mySword.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ClickDirection dir = ClickDirection.Down;


        float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        bool left, right, up, down;
        bool walking;

        left = horiz < 0 ? true : false;
        right = horiz > 0 ? true : false;
        up = vert > 0 ? true : false;
        down = vert < 0 ? true : false;
        walking = left || right || up || down;

        transform.parent.position += ((new Vector3(horiz, vert))) * Time.deltaTime * speed;

        //Clamping player position
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);

        Vector3 mousepos = Input.mousePosition;
        Vector3 objpos = Camera.main.WorldToScreenPoint(transform.position);
        mousepos.x -= objpos.x;
        mousepos.y -= objpos.y;

        rotationlock = swordAnimator.GetCurrentAnimatorStateInfo(0).IsName("StaffSwing") ? true : false;

        if (rotationlock)
        {
            myCanvas.transform.GetChild(1).GetComponent<Text>().text = "Fireball!";
            myCanvas.SetActive(true);
            mySword.SetActive(true);
        }
        else
        {
            mySword.SetActive(false);
            myCanvas.SetActive(false);
        }

        float angle = Mathf.Atan2(mousepos.y, mousepos.x) * Mathf.Rad2Deg;
        //Debug.Log(angle);
        if (angle < 45 && angle > -45)
            dir = ClickDirection.Right;
        else if (angle > 45 && angle < 135)
            dir = ClickDirection.Up;
        else if (angle > 135 || angle < -135)
            dir = ClickDirection.Left;
        else if (angle > -135 && angle < -45)
            dir = ClickDirection.Down;


        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        if (!rotationlock) gameObject.transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        myAnimator.SetBool("FacingLeft", false);
        myAnimator.SetBool("FacingRight", false);
        myAnimator.SetBool("FacingUp", false);
        myAnimator.SetBool("FacingDown", false);


        myAnimator.SetBool("Right", right);
        myAnimator.SetBool("Left", left);
        myAnimator.SetBool("Up", up);
        myAnimator.SetBool("Down", down);

        myAnimator.SetBool("Walking", walking);

        switch (dir)
        {
            case ClickDirection.Right:
                myAnimator.SetBool("FacingRight", true);
                break;
            case ClickDirection.Left:
                myAnimator.SetBool("FacingLeft", true);
                break;
            case ClickDirection.Up:
                myAnimator.SetBool("FacingUp", true);
                break;
            case ClickDirection.Down:
                myAnimator.SetBool("FacingDown", true);
                break;
            default:
                break;
        }


        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
            Shoot();


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Teleport();
        }
    }

    private void Shoot()
    {
        mySword.SetActive(true);
        swordAnimator.SetTrigger("Attack");
    }

    private void Teleport()
    {
        //transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousepos = Input.mousePosition;

        mousepos.z = 10f;
        transform.position = Camera.main.ScreenToWorldPoint(mousepos);
    }
}
