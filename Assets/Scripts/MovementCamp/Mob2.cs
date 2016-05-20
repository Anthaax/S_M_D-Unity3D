using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Mob2 : MonoBehaviour
{

    public float moveTime = 0.1f;           //Time it will take object to move, in seconds.
    private BoxCollider2D boxCollider;      //The BoxCollider2D component attached to this object.
    private Rigidbody2D rb2D;               //The Rigidbody2D component attached to this object.
    private float inverseMoveTime;          //Used to make movement more efficient.
    int x = 0;
    GameObject test;
    GameObject test2;




    void Start()
    {
        //Get a component reference to this object's BoxCollider2D
        boxCollider = GetComponent<BoxCollider2D>();

        //Get a component reference to this object's Rigidbody2D
        rb2D = GetComponent<Rigidbody2D>();

        //By storing the reciprocal of the move time we can use it by multiplying instead of dividing, this is more efficient.
        inverseMoveTime = 0.1f / moveTime;
        test = GameObject.Find("Text");
        test2 = GameObject.Find("Text2");
        test.GetComponent<Text>().enabled = false;
        test2.GetComponent<Text>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (x >= 0 && x < 500)
        {
            Vector3 end = new Vector3(2, 4, 0);
            Vector3 start = transform.position;
            Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            gameObject.transform.position = newPostion;
            x++;

            if (x>200)
            {
                test.GetComponent<Text>().enabled = true;
            }
            
            test2.GetComponent<Text>().enabled = false;

        }
        else if (x >= 500 && x < 1000)
        {
            Vector3 end = new Vector3(4, 4, 0);
            Vector3 start = transform.position;
            Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            gameObject.transform.position = newPostion;
            x++;
            test.GetComponent<Text>().enabled = false;
            if (x >700)
            {
                test2.GetComponent<Text>().enabled = true;
            }
            
        }
        else
        {
            x = 0;

        }


    }
}
