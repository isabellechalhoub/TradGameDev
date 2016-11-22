using UnityEngine;
using System.Collections;

public class WindController : MonoBehaviour 
{
	public Vector3 endPosition = Vector3.zero;
	public float speed = 2;

    public Rect box;
    public Vector2 direction;
    public bool spotted = false;

    private RaycastHit2D hitpoint;

	private float timer = 0;
	public Vector3 startPosition = Vector3.zero;
	private bool outgoing = true;
	private BoxCollider2D player;
	private GameObject wind;
    private BoxCollider2D windColl;
    private bool touching;
    private Vector2 castSize;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<BoxCollider2D>();
		wind = gameObject;
        windColl = wind.GetComponent<BoxCollider2D>();

		startPosition = this.gameObject.transform.position;
		endPosition += startPosition;

		float distance = Vector3.Distance(startPosition, endPosition);
		if (distance != 0)
		{
			speed /= distance;
		}
        touching = false;

        castSize.x = 7;
        castSize.y = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
        Raycast();
        if (spotted)
        {
            this.transform.position.Set(this.transform.position.x, hitpoint.transform.position.y, this.transform.position.z);
            Behaviour();
        }
        else
        {
            this.transform.position.Set(startPosition.x, startPosition.y, startPosition.z);
        }
	}

    void Raycast()
    {
        spotted = Physics2D.BoxCast(startPosition, box.size, this.transform.eulerAngles.z, direction, endPosition.y - startPosition.y, 1 << LayerMask.NameToLayer("Player"));
        hitpoint = Physics2D.BoxCast(startPosition, box.size, this.transform.eulerAngles.z, direction, endPosition.y - startPosition.y, 1 << LayerMask.NameToLayer("Player"));
    }

    void Behaviour()
    {
        timer += Time.deltaTime * speed;
        this.transform.position = Vector3.Lerp(this.transform.position, endPosition, timer);
        if (timer > 1)
        {
            timer = 0;
        }
    }

    void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
        Gizmos.matrix = Matrix4x4.TRS((Vector2)this.transform.position + box.center, this.transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector2.zero, box.size);
        Gizmos.matrix = Matrix4x4.TRS((Vector2)this.transform.position + box.center + (direction.normalized * (endPosition.y - startPosition.y)), this.transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector2.zero, box.size);
    }
}
