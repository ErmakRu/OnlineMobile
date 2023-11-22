using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerTest : MonoBehaviourPun
{
    public float dirX, dirY;
    public float speed;
    PhotonView view;

    private Rigidbody2D rb;
    public Joystick joystick;

    public GameObject destructionFX;
    public static PlayerTest instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        view = GetComponent<PhotonView>();
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = joystick.Horizontal * speed;
        dirY = joystick.Vertical * speed;
        if (view.IsMine) 
        {
            rb.velocity = new Vector2(dirX, dirY);
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 moveAmount = moveInput.normalized * speed * Time.deltaTime;
            transform.position += (Vector3)moveAmount;
        }
    }

    public void GetDamage(int damage)
    {
        Destruction();
    }

    public void Destruction()
    {
        Instantiate(destructionFX, transform.position, Quaternion.identity);
        PhotonNetwork.Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        
    }
}
