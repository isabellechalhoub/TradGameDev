using UnityEngine;
using System.Collections;

public class WindForce : MonoBehaviour
{
    public float thrust;
    public Rigidbody2D rb;

	// Use this for initialization
	void Start ()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        rb.AddForce(transform.up * thrust, ForceMode2D.Impulse);
	}
}
