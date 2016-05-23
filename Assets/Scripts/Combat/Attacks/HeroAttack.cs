using UnityEngine;
using System.Collections;
using S_M_D.Character;
using S_M_D.Combat;
using S_M_D;
using System.Collections.Generic;
using System.Threading;

public class HeroAttack : MonoBehaviour {

    public float moveTime = 0.1f;           //Time it will take object to move, in seconds.
    private float inverseMoveTime;          //Used to make movement more efficient.
    

    public void Attack (int x)
    {
       
        inverseMoveTime = 0.1f / moveTime;
        
        int times = 200;
        while (times > 0)
        {
            Thread.Sleep(1);
            Rigidbody2D rb2D = GameObject.Find("Hero" + x).GetComponent<Rigidbody2D>();
            Vector3 end = new Vector3(50, 50, 0);
            Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            GameObject.Find("Hero" + x).transform.position = newPostion;
            
            times--;
        }

    }

    public void Attacked (int x)
    {

    }
}
