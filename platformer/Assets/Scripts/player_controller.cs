using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    public Rigidbody2D body;
    public Transform foot;
    public LayerMask jumpable;

    public float speed;
    public float responsiveness;

    public float jumpVelocity;
    public float jumpTime;

    private float x_input_raw;
    private float y_input_raw;

    private float x_input;
    private float y_input;

    private bool grounded;
    private bool jumping;
    private float timeToFall;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x_input_raw = Input.GetAxisRaw("Horizontal");
        y_input_raw = Input.GetAxisRaw("Vertical");

        if( Physics2D.OverlapCircle(foot.position, 0.1f, jumpable) && !jumping)
        {
            grounded = true;
        }

    }

    private void FixedUpdate()
    {
        x_input = Mathf.Lerp(x_input, x_input_raw * speed, responsiveness);

        body.velocity = x_input * Vector2.right + Vector2.up * body.velocity.y;

        if(y_input_raw > 0 && grounded)
        {
            grounded = false;
            jumping = true;
            timeToFall = Time.realtimeSinceStartup + jumpTime;
        }

        if( Time.realtimeSinceStartup > timeToFall || y_input_raw != 1 )
        {
            jumping = false;
        }

        if (jumping)
        {
            body.velocity = body.velocity.x * Vector2.right + (Vector2.up * jumpVelocity);
        }


    }
}
