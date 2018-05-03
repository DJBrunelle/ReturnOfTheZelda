using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public int speed;
	private Animator anim;

	//States associated with each type of movement
	private struct MoveState
	{
		public string name;
		public KeyCode[] keys;
		public Vector2 direction;
		public bool flip;

	}
	private SpriteRenderer sr;
	private float animspeed;
	private enum State { Idle, Moving, Attacking, Frozen };
	private State linkState;

	/*
	 * All possible abbreviated movements
	 * ur = Up Right
	 * ul = Up Left
	 * dr = Down Right
	 * dl = Down Left
	 * rt = Right
	 * lt = Left
	 * up = Up
	 * dn = Down
	 */
	private string[] moveDirs = new string[] { "ur", "ul", "dr", "dl", "rt", "lt", "up", "dn" };

	private Dictionary<string, MoveState> moveDict;


	// Use this for initialization
	void Start()
	{
		linkState = State.Idle;
		anim = GetComponent<Animator>();
		sr = GetComponent<SpriteRenderer>();
		anim.speed = 1;

		//Define states for each type of movement
		moveDict = new Dictionary<string, MoveState>(){
			//Up Right
			{moveDirs[0], new MoveState{
				name = "LinkUpRight",
				keys = new KeyCode[]{KeyCode.W, KeyCode.D},
				direction = new Vector2(1,1).normalized,
				flip = false
			}},
			//Up Left
			{moveDirs[1], new MoveState{
				name = "LinkUpRight",
				keys = new KeyCode[]{KeyCode.W, KeyCode.A},
				direction = new Vector2(-1,1).normalized,
				flip = true
			}},
			//Down Right
			{moveDirs[2], new MoveState{
				name = "LinkDownLeft",
				keys = new KeyCode[]{KeyCode.S, KeyCode.D},
				direction = new Vector2(1,-1).normalized,
				flip = true
			}},
			//Down Left
			{moveDirs[3], new MoveState{
				name = "LinkDownLeft",
				keys = new KeyCode[]{KeyCode.S, KeyCode.A},
				direction = new Vector2(-1,-1).normalized,
				flip = false
			}},
			//Right
			{moveDirs[4], new MoveState{
				name = "Link Right",
				keys = new KeyCode[]{KeyCode.D},
				direction = Vector2.right,
				flip = false
			}},
			//Left
			{moveDirs[5], new MoveState{
				name = "Link Right",
				keys = new KeyCode[]{KeyCode.A},
				direction = Vector2.left,
				flip = true
			}},
			//Up
			{moveDirs[6], new MoveState{
				name = "Link Up",
				keys = new KeyCode[]{KeyCode.W},
				direction = Vector2.up,
				flip = false
			}},
			//Down
			{moveDirs[7], new MoveState{
				name = "Link Down",
				keys = new KeyCode[]{KeyCode.S},
				direction = Vector2.down,
				flip = false
			}},
		};

	}

	// Update is called once per frame
	void Update()
	{
		if (linkState == State.Idle || linkState == State.Moving)
		{
			Movement();
		}
	}

	/*
	 * Moves the player in direction based on WASD keys being held down
	 * Shift makes link move faster
	 */
	void Movement()
	{
		MoveState moveState = new MoveState() { name = "" };
		anim.speed = 1;

		//Check each possible direction
		foreach (string dir in moveDirs)
		{
			bool found = true;
			//Check keys required for each direction
			foreach (KeyCode key in moveDict[dir].keys)
			{
				//keys not being pressed
				if (!Input.GetKey(key))
				{
					found = false;
				}
			}
			//if keys are being pressed for a certain direction
			//set state to this direction and exit loop
			if (found)
			{
				moveState = moveDict[dir];
				break;
			}
		}
		//Set animation if state was found
		if (moveState.name != "")
		{

			anim.Play(moveState.name);
			sr.flipX = moveState.flip;

			//Speed up if shift is being pressed
			int finalSpeed = speed;
			if (Input.GetKey(KeyCode.LeftShift))
			{
				finalSpeed = speed + (speed / 2);
			}

			//Move player in state direction
			transform.position += (Vector3)(moveState.direction * finalSpeed) * Time.deltaTime;
			linkState = State.Moving;
		}
		else // pause on current animation
		{
			anim.Play(moveState.name, 0, 0f);
			anim.speed = 0;
			linkState = State.Idle;
		}
	}
}
