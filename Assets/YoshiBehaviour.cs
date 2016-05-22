using UnityEngine;
using System.Collections;

public class YoshiBehaviour : MonoBehaviour {

    public Rigidbody2D rb;
    public CircleCollider2D collision;


    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        collision.enabled = true;
        rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update() {
        if (transform.position.y >= 4)
        {
            collision.enabled = true;
        }
    }

    void OnMouseDrag()
    {
        float axisX = Input.GetAxis("Mouse X");
        float axisY = Input.GetAxis("Mouse Y");
        Vector2 dir = new Vector2(axisX, axisY);
        dir = dir.normalized;
        rb.gravityScale = 1;
        rb.AddForce(dir * 70);
        collision.enabled = false;
    }
}