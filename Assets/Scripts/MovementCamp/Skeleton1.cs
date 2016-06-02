using UnityEngine;
using System.Collections;

public class Skeleton1 : MonoBehaviour
{

    public float moveTime = 0.1f;           //Time it will take object to move, in seconds.
    private BoxCollider2D boxCollider;      //The BoxCollider2D component attached to this object.
    private Rigidbody2D rb2D;               //The Rigidbody2D component attached to this object.
    private float inverseMoveTime;          //Used to make movement more efficient.
    int x = 0;


    void Start()
    {
        //Get a component reference to this object's BoxCollider2D
        boxCollider = GetComponent<BoxCollider2D>();

        //Get a component reference to this object's Rigidbody2D
        rb2D = GetComponent<Rigidbody2D>();

        //By storing the reciprocal of the move time we can use it by multiplying instead of dividing, this is more efficient.
        inverseMoveTime = 0.1f / moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (x >= 0 && x < 100)
        {
            Vector3 end = new Vector3(-7, 3.4f, 0);
            Vector3 start = transform.position;
            Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            gameObject.transform.position = newPostion;
            x++;
        }
        else if (x >= 100 && x < 200)
        {
            Vector3 end = new Vector3(-8.4f, 3.4f, 0);
            Vector3 start = transform.position;
            Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            gameObject.transform.position = newPostion;
            x++;
        }
        else if (x >= 200 && x < 300)
        {
            Vector3 end = new Vector3(-8.4f, 4.8f, 0);
            Vector3 start = transform.position;
            Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            gameObject.transform.position = newPostion;
            x++;
        }
        else if (x >= 300 && x < 410)
        {
            Vector3 end = new Vector3(-7, 4.8f, 0);
            Vector3 start = transform.position;
            Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            gameObject.transform.position = newPostion;
            x++;
        }
        else
        {
            x = 0;

        }


    }
}