using UnityEngine;
using System.Collections;

public class MoleEnemy : MonoBehaviour {

    #region Vars
    private bool inRange;
    public float speed = 1f;
    public float startingPos;
    public float endingPos;
    private float direction = -1f;
    private int health = 3;
    private float distance;
    private Vector2 walking;
    private bool hiding = true;
    private BoxCollider2D player;
    private BoxCollider2D shield;
    private BoxCollider2D enemy;
    private GameObject me;
    private PolygonCollider2D sword;
	#endregion

    void Start()
    {
		// Set starting/ending positions (left to right movement)
        distance = startingPos - endingPos;
        endingPos = transform.position.x - distance;
        startingPos = transform.position.x;

        // Get colliders for objects
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
        me = gameObject;
        enemy = me.GetComponent<BoxCollider2D>();
        sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //inRange = Physics2D.OverlapCircle(transform.position, range, playerLayer);

        if (inRange)
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

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
        transform.Translate(walking);
    }
}
