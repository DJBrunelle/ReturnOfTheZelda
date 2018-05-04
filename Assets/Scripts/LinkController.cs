using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkController : MonoBehaviour
{

	public int speed;

	private Animator anim;
	private SpriteRenderer sr;
	private float animspeed;
	private enum State { Idle, Moving, Attacking, Frozen };
	private State linkState;

	// Use this for initialization
	void Start()
	{
		linkState = State.Idle;
		anim = GetComponent<Animator>();
		sr = GetComponent<SpriteRenderer>();
		anim.speed = 1;
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
		LinkData.MoveState moveState = new LinkData.MoveState() { name = "" };

		//Check each possible direction
		foreach (string dir in LinkData.moveDirs)
		{
			bool found = true;
			//Check keys required for each direction
			foreach (KeyCode key in LinkData.moveDict[dir].keys)
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
				moveState = LinkData.moveDict[dir];
				break;
			}
		}
		//Set animation if state was found
		if (moveState.name != "")
		{
			anim.Play(moveState.name);
			anim.SetInteger("Movement", moveState.paramMove);
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
		else // pause on current animation if keys not being pressed 
		{
			anim.SetInteger("Movement", -1);
			linkState = State.Idle;
		}
	}
}