using UnityEngine;
using System.Collections;
using Prime31;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour 
{
#region Vars
	public GameObject gameCamera;
	public GameObject healthBar;
	public GameObject gameOverPanel;
    public GameObject pausePanel;
    public GameObject winPanel;
	public float walkSpeed = 3;
	public float gravity = -35;
	public float jumpHeight = 2;
	public int health = 100;
	public BoxCollider2D coll;
	public BoxCollider2D enemy;
	public GameObject healthNum;
    public GameObject shield;
    public GameObject sword;
    private bool windy;
    private bool topwind;
    private Vector3 swordStartPos;
    private Vector3 swordEndPos;
	private bool shieldin = false;
	private bool floatin = false;
	private bool playerControl = true;
	private int currHealth = 0;
	public CharacterController2D _controller;
	private AnimationController2D _animator;
    private bool swinging = false;
    private bool pause = false;
    private bool wind;
    public GameObject deathMusic;
    private AudioSource clip3;
    private AudioSource clip2;
    public GameObject levelMusic;

    private GameObject journal;
    private GameObject jar;
    private GameObject lunchbox;
    private GameObject photo;
    private GameObject plushie;
    private GameObject gameboy;
    private GameObject shell;
    private GameObject journalUI;
    private GameObject jarUI;
    private GameObject lunchboxUI;
    private GameObject photoUI;
    private GameObject plushieUI;
    private GameObject gameboyUI;
    private GameObject shellUI;

    #endregion

    void Start ()
    {
        shield = GameObject.FindGameObjectWithTag("Shield");
        sword = GameObject.FindGameObjectWithTag("Sword");
		coll = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D> ();
		enemy = GameObject.FindGameObjectWithTag ("Enemy").GetComponent<BoxCollider2D> ();
		_controller = gameObject.GetComponent<CharacterController2D>();
		_animator = gameObject.GetComponent<AnimationController2D>();

        journal = GameObject.Find("Journal");
        jar = GameObject.Find("Jar");
        lunchbox = GameObject.Find("Lunchbox");
        photo = GameObject.Find("Photo");
        plushie = GameObject.Find("Plushie");
        gameboy = GameObject.Find("GameBoy");
        shell = GameObject.Find("Shell");

        journalUI = GameObject.Find("JournalUI");
        jarUI = GameObject.Find("JarUI");
        lunchboxUI = GameObject.Find("LunchboxUI");
        photoUI = GameObject.Find("PhotoUI");
        plushieUI = GameObject.Find("PlushieUI");
        gameboyUI = GameObject.Find("GBUI");
        shellUI = GameObject.Find("ShellUI");

        journalUI.SetActive(false);
        jarUI.SetActive(false);
        lunchboxUI.SetActive(false);
        photoUI.SetActive(false);
        plushieUI.SetActive(false);
        gameboyUI.SetActive(false);
        shellUI.SetActive(false);

        gameCamera.GetComponent<CameraFollow2D> ().startCameraFollow (this.gameObject);
		winPanel.SetActive(false);
		gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
		currHealth = health;
        _animator.setAnimation("Fall");
        windy = false;
        topwind = false;

        clip3 = deathMusic.GetComponent<AudioSource>();
        clip3.Stop();
        clip3.time = 5.0f;
        clip2 = levelMusic.GetComponent<AudioSource>();
        clip2.Play();
    }

	void Update ()
    {
		if (playerControl) 
		{
			Vector3 velocity = PlayerInput ();
			_controller.move (velocity * Time.deltaTime);
		}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
            if (pause)
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
                playerControl = false;
                _animator.setAnimation("Idle");
            }
            else
            {
                Time.timeScale = 1;
                pausePanel.SetActive(false);
                playerControl = true;
            }
        }
	}
		
#region Movement
	private Vector3 PlayerInput()
	{
		Vector3 velocity = _controller.velocity;
		velocity.x = 0;

        #region moving platform parenting
        if (_controller.isGrounded && _controller.ground != null && (_controller.ground.tag.Equals("MovingPlatform") || _controller.ground.tag.Equals("Rotating Platform")) ) 
		{
			this.transform.parent = _controller.ground.transform;
		}
		else 
		{
            if (this.transform.parent != null)
                this.transform.parent = null;
		}
		#endregion

		#region running left/right
		// Left arrow key
		if (Input.GetAxis ("Horizontal") < 0 && !shieldin && !swinging)
		{
			velocity.x = -walkSpeed;
			if (_controller.isGrounded && !floatin) 
			{
				_animator.setAnimation ("Walk");
				_animator.setFacing ("Left");
			}
		}
        else if (Input.GetKeyUp(KeyCode.LeftArrow) && !shieldin && !swinging)
        {
            velocity.x = 0;
            if (!floatin)
                _animator.setAnimation("Idle");
        }

        // Right arrow key
        else if (Input.GetAxis ("Horizontal") > 0 && !shieldin && !swinging) 
		{
			velocity.x = walkSpeed;
			if (_controller.isGrounded && !floatin) 
			{
				_animator.setAnimation ("Walk");
				_animator.setFacing ("Right");
			}
		}
        else if (Input.GetKeyUp(KeyCode.RightArrow) && !shieldin && !swinging)
        {
            velocity.x = 0;
            if (!floatin)
                _animator.setAnimation("Idle");
        }
		#endregion

		#region idle
		//Idle
		else 
		{
			if (_controller.isGrounded && currHealth != 0 && !shieldin && !swinging) 
			{
                velocity.x = 0;
                _animator.setAnimation("Idle");
			}
		}
		#endregion

		#region Jump/Float
		// Space bar - Jump
		if (Input.GetKeyDown (KeyCode.Space) && !shieldin && _controller.isGrounded && !swinging && !floatin) 
		{
			_animator.setAnimation("Jump");
			velocity.y = Mathf.Sqrt (2f * jumpHeight * -gravity);
		} 
		else if ((Input.GetKeyDown (KeyCode.Space) && !_controller.isGrounded) || floatin) 
		{
			if (!floatin)
            	_animator.setAnimation("Deploy");
            if (topwind) { }
            else if (!windy)
                velocity.y = -2;
			floatin = true;
		}
		if (_controller.isGrounded || Input.GetKeyUp (KeyCode.Space))
		{
			if (!_controller.isGrounded)
			{
				_animator.setAnimation("Fall");
                wind = false;
			}
            else
            {
                //_animator.setAnimation("Land");
            }
            if (!wind)
            {
                floatin = false;
                gravity = -35;
            }
		}

        if (!_controller.isGrounded)
        {
            wind = false;
        }
        #endregion

        #region shield
        //Shield up and down
        //if (Input.GetAxis("Fire1") > 0) {
        //	shieldin = true;
        //} else
        //	shieldin = false;

        if (Input.GetKey(KeyCode.X) && !swinging)
        {
            _animator.setAnimation("Preblock");
            shieldin = true;
            shield.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.X) && shieldin)
        {
            _animator.setAnimation("Unblock");
            shieldin = false;
            shield.SetActive(false);
        }
        else
        {
            shieldin = false;
            shield.SetActive(false);
        }
        #endregion

        #region sword swing
        // swing dat sword bb
        if (Input.GetKey(KeyCode.C) && !shieldin)
        {
            _animator.setAnimation("Slash");
            swinging = true;
            sword.SetActive(true);
            //Transform pos = sword.GetComponent<Transform>();
            //swordStartPos = pos.localPosition;
            //Vector3 axis = new Vector3(pos.localPosition.x, pos.localPosition.y - pos.localPosition.sqrMagnitude, 0);
            //pos.RotateAround(axis, axis, 20 * Time.deltaTime);
        }
        else
        {
            swinging = false;
            sword.SetActive(false);
        }
        #endregion

        // Change velocity.
        velocity.y += gravity * Time.deltaTime;
		return velocity;
	}

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag.Equals("Rotating Platform"))
        {
            transform.rotation = Quaternion.identity;
        }
        if (coll.tag.Equals("Wind"))
        {
            windy = false;
            gravity = -35;
        }
        else if (coll.tag.Equals("TopWind"))
        {
            topwind = false;
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag.Equals("Wind") && floatin)
        {
            gravity = 35;
            windy = true;
        }
        else if(coll.tag.Equals("TopWind"))
        {
            topwind = true;
        }
    }

#endregion

#region Damage/Death/Winning
	// When the player collides with the death collider
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "KillZ")
        {
            PlayerFallDeath();
            clip2.Stop();
            clip3.Play();
        }
        else if (col.tag == "Damaging")
			PlayerDamage (1);
		else if (col.tag == "YouWin") 
            Winning();
		else if (col.tag == "Enemy" && (Input.GetKey (KeyCode.X) || Input.GetKey(KeyCode.C))) {}
		else if(col.tag == "Enemy")
			PlayerDamage (1);

        #region Collectables
        else if (col.tag.Equals("Collectable"))
        {
            if(col.name.Equals("Journal"))
            {
                journal.SetActive(false);
                journalUI.SetActive(true);
            }
            else if (col.name.Equals("Jar"))
            {
                jar.SetActive(false);
                jarUI.SetActive(true);
            }
            else if (col.name.Equals("Lunchbox"))
            {
                lunchbox.SetActive(false);
                lunchboxUI.SetActive(true);
            }
            else if (col.name.Equals("Photo"))
            {
                photo.SetActive(false);
                photoUI.SetActive(true);
            }
            else if (col.name.Equals("Plushie"))
            {
                plushie.SetActive(false);
                plushieUI.SetActive(true);
            }
            else if (col.name.Equals("GameBoy"))
            {
                gameboy.SetActive(false);
                gameboyUI.SetActive(true);
            }
            else if (col.name.Equals("Shell"))
            {
                shell.SetActive(false);
                shellUI.SetActive(true);
            }
            #endregion
        }
    }
		
	private void Winning()
	{
		playerControl = false;
		_animator.setAnimation("Idle");
		winPanel.SetActive(true);
	}

	// Changes player health when damage is taken. checks for death
	private void PlayerDamage(int damage)
	{
		currHealth -= damage;
		float normHealth = (float)currHealth/(float)health;

        if (currHealth == 4)
        {
            GameObject.Find("Heart3").SetActive(false);
        }
        if (currHealth == 2)
        {
            GameObject.Find("Heart2").SetActive(false);
        }
        if (currHealth <= 0)
        {
            GameObject.Find("Heart1").SetActive(false);
            PlayerDeath();
        }
	}

	// Play death animation
	private void PlayerDeath()
	{
        _animator.setAnimation("Idle");
		playerControl = false;
		gameOverPanel.SetActive(true);
        clip2.Stop();
        clip3.Play();
	}

	// Stops the camera follow and reduces health
	private void PlayerFallDeath()
	{
		currHealth = 0;
        //playerControl = false;
		gameCamera.GetComponent<CameraFollow2D>().stopCameraFollow();
		gameOverPanel.SetActive(true);
        clip2.Stop();
        clip3.Play();
	}
#endregion
}
