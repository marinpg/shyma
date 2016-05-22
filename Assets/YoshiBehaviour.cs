using UnityEngine;
using System.Collections;

public class YoshiBehaviour : MonoBehaviour {

    public Rigidbody2D rb;
    public CircleCollider2D collision;
    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;
    private bool isMoving;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        collision.enabled = false;
        rb.gravityScale = 0;
        isMoving = false;
    }

    // Update is called once per frame
    void Update() {
        if (transform.position.y >= 4)
        {
            collision.enabled = true;
        }
        if (Input.GetMouseButtonDown(0)) 
        {
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0) && !isMoving)
        {
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
            currentSwipe.Normalize();
            rb.gravityScale = 1;
            rb.AddForce(currentSwipe * 700);
            isMoving = true;
        }
    }
}