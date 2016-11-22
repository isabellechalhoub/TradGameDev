using UnityEngine;
using System.Collections;

public class FlyingEnemy : MonoBehaviour
{
    private GameObject player;
    public float speed;
    public float range;
    public LayerMask playerLayer;
    public bool inRange;
    //public int health = 1;
    //private BoxCollider2D foot;
    //private BoxCollider2D head;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //foot = player.GetComponent<BoxCollider2D>();
        //head = this.GetComponentInChildren<BoxCollider2D>();
        //Debug.Log(head.gameObject.name);
	}
	
	// Update is called once per frame
	void Update ()
    {
        inRange = Physics2D.OverlapCircle(transform.position, range, playerLayer);

        if (inRange)
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        //if (foot.IsTouching(head))
        //{
        //    //Debug.Log("here");
        //    health--;
        //}
        //if (health == 0)
        //{
        //    this.gameObject.SetActive(false);
        //}
	}

    void OnDrawGizmosSelected ()
    {
        Gizmos.DrawSphere(transform.position, range);
    }
}
