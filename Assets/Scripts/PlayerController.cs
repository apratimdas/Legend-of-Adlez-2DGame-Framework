using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    enum ClickDirection { Right, Left, Up, Down};

    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private ParticleSystem myParticle;
    [SerializeField]
    private GameObject[] dashImage;
    [SerializeField]
    private GameObject boostedImage;

    private AudioSource mySound;
    private Animator myAnimator;
    private Animator swordAnimator;
    private GameObject myCanvas;
    private GameObject mySword;
    private Rigidbody2D body;

    public int dash_charge = 3;
    public float proj_speed;
    public float health = 30;
    private bool recharge = false;

    bool rotationlock = false;


    public void Awake()
    {
        myAnimator = GetComponent<Animator>();
        mySound = GetComponent<AudioSource>();
        swordAnimator = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
        myCanvas = gameObject.transform.GetChild(1).gameObject;
        mySword = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        body = GetComponent<Rigidbody2D>();
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

        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene("2D_Tim");
        }

        float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        bool left, right, up, down;
        bool walking;

        left = horiz < 0;
        right = horiz > 0;
        up = vert > 0;
        down = vert < 0;
        walking = left || right || up || down;

        transform.position += ((new Vector3(horiz, vert))) * Time.deltaTime * speed;


        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
        Vector3 mousepos = Input.mousePosition;
        Vector3 objpos = Camera.main.WorldToScreenPoint(transform.position);
        mousepos.x -= objpos.x;
        mousepos.y -= objpos.y;

        rotationlock = swordAnimator.GetCurrentAnimatorStateInfo(0).IsName("Sword Slash") ? true : false;

        if (rotationlock)
        {
            myCanvas.transform.GetChild(1).GetComponent<Text>().text = "DIE!!";
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
        if (!rotationlock) gameObject.transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 10));

        SetAnimator(left, right, up, down, walking, dir);


        if (Input.GetKeyDown(KeyCode.Mouse0))
            Attack();
        if (Input.GetKeyDown(KeyCode.Space) && dash_charge > 0)
            Dash(left, right, up, down, mousepos.x, mousepos.y);


    }

    public void boosted()
    {
        StartCoroutine("boost_timer");
    }

    IEnumerator boost_timer()
    {
        boostedImage.SetActive(true);
        dash_charge = 999;
        foreach (GameObject item in dashImage)
        {
            item.SetActive(true);
        }
        yield return new WaitForSeconds(5f);
        dash_charge = 3;
        boostedImage.SetActive(false);
    }

    private void Dash(bool left, bool right, bool up, bool down, float mousex , float mousey)
    {
        
        dash_charge--;
        if(dash_charge<3) dashImage[dash_charge].SetActive(false);
        //recharge = true;
        if (!recharge) StartCoroutine("start_recharge");
        //myParticle.Clear();
        myParticle.Play();
        int x = 0, y = 0;
        if (left) x--;
        if (right) x++;
        if (up) y++;
        if (down) y--;
        if(x==0&&y==0)
            body.AddForce(new Vector2(mousex, mousey).normalized * 2000);
        else body.AddForce(new Vector2(x, y).normalized * 2000);
        
    }

    IEnumerator start_recharge()
    {
        if (dash_charge < 3) recharge = true;
        while (recharge)
        {
            yield return new WaitForSeconds(3f);
            if(dash_charge<3) dashImage[dash_charge].SetActive(true);
            dash_charge++;
            if (dash_charge >= 3) recharge = false;
        }
    }

    private void SetAnimator(bool left, bool right, bool up, bool down, bool walking, ClickDirection dir)
    {
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

    }

    private void Attack()
    {
        mySound.Play();
        mySword.SetActive(true);
        swordAnimator.SetTrigger("Attack");
    }
}
