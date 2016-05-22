using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class shootScript : MonoBehaviour {

    public Rigidbody2D rb;
    //public CircleCollider2D collision;
    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;
    private bool isMoving;

    public GameObject[] trashTypes;
    public GameObject[] trashBins;
    public Text scoreText;
    private int score;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //collision.enabled = false;
        disableBinCollision();
        rb.gravityScale = 0;
        isMoving = false;
        if (scoreText.text == "Score Text")
            score = 0;
        else
        {
            score = int.Parse(scoreText.text);
        }
        setScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreText.text == "Score Text")
            score = 0;
        else
        {
            score = int.Parse(scoreText.text);
        }

        if (transform.position.y >= 3.6)
        {
            //collision.enabled = true;
            enableBinCollision();
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
            rb.AddForce(currentSwipe * 650*5);
            isMoving = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == gameObject.tag)
        {
            score++;
            setScoreText();
            isMoving = false;
            //collision.enabled = false;
            disableBinCollision();
            rb.gravityScale = 0;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
            gameObject.transform.position = new Vector3(0, -4, 0);
            gameObject.transform.rotation = new Quaternion(0, 0, 0,0);
            switchTrashType();
        }
        else if (other.gameObject.CompareTag("Boundaries") || other.gameObject.tag != gameObject.tag)
        {
            score = 0;
            setScoreText();
            isMoving = false;
            //collision.enabled = false;
            disableBinCollision();
            rb.gravityScale = 0;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
            gameObject.transform.position = new Vector3(0, -4, 0);
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    void setScoreText()
    {
        scoreText.text = score.ToString();
    }

    void switchTrashType()
    {
        int type;
        type = Random.Range(0,3);
        Debug.Log(type);
        gameObject.SetActive(false);
        trashTypes[type].SetActive(true);
        //collision.enabled = false;
        disableBinCollision();

    }

    void enableBinCollision()
    {
        foreach (GameObject t in trashBins)
        {
            EdgeCollider2D[] eCol = t.GetComponents<EdgeCollider2D>();
            BoxCollider2D bCol = t.GetComponent<BoxCollider2D>();
            foreach (EdgeCollider2D e in eCol) e.enabled = true;
            bCol.enabled = true;
        }
    }

    void disableBinCollision()
    {
        foreach (GameObject t in trashBins)
        {
            EdgeCollider2D[] eCol = t.GetComponents<EdgeCollider2D>();
            BoxCollider2D bCol = t.GetComponent<BoxCollider2D>();
            foreach (EdgeCollider2D e in eCol) e.enabled = false;
            bCol.enabled = false;
        }
    }

    void OnBecameInvisible()
    {
        //collision.enabled = true;
    }
}
