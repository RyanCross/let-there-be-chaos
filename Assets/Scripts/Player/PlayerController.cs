using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera cam;
    public SpriteRenderer spriteRenderer;

    // Player Stats:
    public float moveSpeed = 1f;

    Vector2 movement;
    [SerializeField] Vector3 currentMousePos;
    [SerializeField] Vector3 currentLookDir;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        currentMousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        currentLookDir = (currentMousePos - transform.position).normalized; // normalize the displacement vector between the player and the current mouse position

        if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        if (movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

}
