using UnityEngine;
using System.Collections;

public class FallingTrain : MonoBehaviour
{
    public float fallSpeed;
    public LayerMask platformMask = 0;
    private bool fall;

    // Use this for initialization
    void Start ()
    {
        //fall = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (fall)
            //transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //if (coll.gameObject.tag.Equals("Platform"))
        //{
        //    fall = false;
        //}
    }
}
