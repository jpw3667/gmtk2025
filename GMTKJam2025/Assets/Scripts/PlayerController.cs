using System.Runtime.CompilerServices;
using UnityEditorInternal.VersionControl;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject myHook;

    [SerializeField] private Rope currentRope;

    private float horizontal;
    private float acceleration = 20f;
    private float jumpStrength = 6f;
    private float maxSpeed = 6f;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        myHook.transform.position = new Vector2(transform.position.x, transform.position.y + 0.6f);
        horizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocityY = jumpStrength; ;
        }

        if (Input.mouseScrollDelta.y > 0)
        {
            currentRope.addLink();
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            currentRope.removeLink();
        }
    }

    private void FixedUpdate()
    {
        if(horizontal == 0)
        {
            rb.linearVelocityX *= 0.94f;
        }
       // Debug.Log(horizontal);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x + (horizontal * acceleration * Time.deltaTime), rb.linearVelocity.y);
        if(rb.linearVelocity.x > maxSpeed)
        {
            rb.linearVelocityX = maxSpeed;
        }
        else if(rb.linearVelocity.x < maxSpeed * -1)
        {
            rb.linearVelocityX = maxSpeed * -1;
        }

       

        if(rb.position.y < -5)
        {
            rb.position = Vector2.zero;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
