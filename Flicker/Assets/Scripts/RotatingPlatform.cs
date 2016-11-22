using UnityEngine;
using System.Collections;

public class RotatingPlatform : MonoBehaviour
{
    public Rigidbody2D rbody;
    public float speed;

	// Use this for initialization
	void Start ()
    {
        rbody = this.GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void FixedUpdate ()
    {
        rbody.MoveRotation(rbody.rotation + speed * Time.fixedDeltaTime);
	}
}
