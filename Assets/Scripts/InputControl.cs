using UnityEngine;
using System.Collections;

public class InputControl : MonoBehaviour {

	public GameObject burgerObject;
	public float burgerDelay;
	public float jumpForce;
	private float delayTime;
	public Vector3 burgerHighPosition;
	public Vector3 burgerMidPosition;
	public Vector3 burgerLowPosition;
    private Animator playerAnim;
    private int touchID;

	enum PLAYER_STATE{
		PLAYER_STATE_WALKING,
		PLAYER_STATE_JUMPING,
		PLAYER_STATE_CROUCHING};

	PLAYER_STATE currentState;
    
	// Use this for initialization
	void Start () {
		currentState = PLAYER_STATE.PLAYER_STATE_WALKING;
		delayTime = 0.0f;
        playerAnim = GetComponentInParent<Animator>();
    }
	
	// Update is called once per frame
	void Update () 
	{
		delayTime += Time.deltaTime;
	

	}

	void FixedUpdate()
	{
        //MOBILE/TOUCHSCREEN INPUT
        
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x < (Screen.width / 4)) //left side of screen
                {
                    if(touch.position.y < (Screen.height /2) && currentState != PLAYER_STATE.PLAYER_STATE_JUMPING) //top of screen
                    {
                    
                        currentState = PLAYER_STATE.PLAYER_STATE_JUMPING;
                        playerAnim.SetTrigger("Jump");
                        
                        
                    }
                    else if (touch.position.y >= (Screen.height / 2) && (currentState == PLAYER_STATE.PLAYER_STATE_WALKING))//can't crouch from a jump
                    {
                        touchID = touch.fingerId;
                        currentState = PLAYER_STATE.PLAYER_STATE_CROUCHING;
                        playerAnim.SetBool("Crouch", true);
                    }
                }
                else if (touch.position.x >= (Screen.width / 4) && (delayTime > burgerDelay)) //right side of screen
                {
                    ShootBurger();

                    delayTime = 0.0f;
                }
            }
            else if (touch.fingerId == touchID && touch.phase == TouchPhase.Ended && currentState == PLAYER_STATE.PLAYER_STATE_CROUCHING)
            {
                if(touch.position.x < (Screen.height / 2) && touch.position.y < (Screen.width / 4))
                {
                    touchID = -1;
                    currentState = PLAYER_STATE.PLAYER_STATE_WALKING;
                    playerAnim.SetBool("Crouch", false);
                }
            }
        }

        //KEYBOARD INPUT

		if ((Input.GetAxis("Horizontal") > 0) && (delayTime > burgerDelay))
		{
            ShootBurger();
			
			delayTime = 0.0f;
		}
		else if ((Input.GetAxis("Vertical") > 0) && currentState != PLAYER_STATE.PLAYER_STATE_JUMPING)//can't jump while jumping
		{
			print ("Jump!");
			
			currentState = PLAYER_STATE.PLAYER_STATE_JUMPING;
			playerAnim.SetTrigger("Jump");
			
		}
		else if ((Input.GetAxis("Vertical") < 0) && (currentState == PLAYER_STATE.PLAYER_STATE_WALKING))//can't crouch from a jump
		{
            currentState = PLAYER_STATE.PLAYER_STATE_CROUCHING;
            playerAnim.SetBool("Crouch", true);
        }
		else if ((Input.GetAxis("Vertical") == 0) && currentState == PLAYER_STATE.PLAYER_STATE_CROUCHING)//stop crouching
		{
			currentState = PLAYER_STATE.PLAYER_STATE_WALKING;
            playerAnim.SetBool("Crouch", false);
        }
        //Debug.Log("Current y position: " + transform.position.y);
    }

	public void JumpFinished()
	{
		currentState = PLAYER_STATE.PLAYER_STATE_WALKING;
	}

    void ShootBurger()
    {
        switch (currentState)
        {
            case PLAYER_STATE.PLAYER_STATE_CROUCHING:
                Instantiate(burgerObject, burgerLowPosition, Quaternion.identity);
                break;
            case PLAYER_STATE.PLAYER_STATE_WALKING:
                Instantiate(burgerObject, burgerMidPosition, Quaternion.identity);
                break;
            case PLAYER_STATE.PLAYER_STATE_JUMPING:
                Instantiate(burgerObject, burgerHighPosition, Quaternion.identity);
                break;
        }

    }
}
