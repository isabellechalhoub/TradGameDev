using UnityEngine;
using System.Collections;
using Prime31;

public class PatrolEnemy : MonoBehaviour {

	public float speed = 1f;
	public float startingPos;
	public float endingPos;
    private int health = 3;
    private float distance;
	private Vector2 walking;
	public BoxCollider2D player;
	public PolygonCollider2D shield;
    public BoxCollider2D enemy;
    public GameObject me;
    public PolygonCollider2D sword;

    void Start () 
	{
        distance = startingPos - endingPos;
		endingPos = transform.position.x - distance;
		startingPos = transform.position.x;
        player = GameObject.FindGameObjectWithTag ("Player").GetComponent<BoxCollider2D> ();
        shield = GameObject.FindGameObjectWithTag ("Shield").GetComponent<PolygonCollider2D> ();
        me = gameObject;
        enemy = me.GetComponent<BoxCollider2D>();
        sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<PolygonCollider2D>();
    }

	// Update is called once per frame
	void Update () 
	{
        if (health == 0)
        {
            me.SetActive(false);
        }

        if (transform.position.x > startingPos || transform.position.x < endingPos || enemy.IsTouching(player) || enemy.IsTouching(sword) || enemy.IsTouching(shield))
        {
            if (enemy.IsTouching(sword))
                health--;
            this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
        walking.x = -1 * speed * Time.deltaTime;
        transform.Translate (walking);
	}
}
