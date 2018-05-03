using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


	public int speed;
	private Animator anim;
	private SpriteRenderer sr;
	float animspeed;

	Dictionary<string, Vector2> directions;
	Dictionary<string, int> movement;


	// Use this for initialization
	void Start()
	{
		anim = GetComponent<Animator>();
		sr = GetComponent<SpriteRenderer>();
		anim.speed = 1;

		//Associate direction with its vector value
		directions = new Dictionary<string, Vector2>()
		{
			{"up", Vector2.up},
			{"down", Vector2.down},
			{"left", Vector2.left},
			{"right", Vector2.right},
			{"upRight", new Vector2(1,1).normalized},
			{"upLeft", new Vector2(-1,1).normalized},
			{"downRight", new Vector2(1,-1).normalized},
			{"downLeft", new Vector2(-1,-1).normalized},
		};

		movement = new Dictionary<string, int>()
		{
			{"up", 1},
			{"down", 2},
			{"left", 0},
			{"right", 0},
			{"upRight", 3},
			{"upLeft", 3},
			{"downRight",4},
			{"downLeft", 4},
		};

	}

	// Update is called once per frame
	void Update()
	{
		Movement();
	}

	/*
	 * Moves the player in direction based on WASD keys being held down
	 * Shift makes link move faster
	 */
	void Movement()
	{
		Vector2 direction = Vector2.zero;
		string currentAnimation = "";

		anim.speed = 1;
		if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
		{
			anim.Play("LinkUpRight");
			sr.flipX = false;
			direction = directions["upRight"];
			currentAnimation = "LinkUpRight";
		}
		else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
		{
			anim.Play("LinkUpRight");
			sr.flipX = true;
			direction = directions["upLeft"];
			currentAnimation = "LinkUpRight";
		}
		else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
		{
			anim.Play("LinkDownLeft");
			sr.flipX = true;
			direction = directions["downRight"];
			currentAnimation = "LinkDownLeft";
		}
		else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
		{
			anim.Play("LinkDownLeft");
			sr.flipX = false;
			direction = directions["downLeft"];
			currentAnimation = "LinkDownLeft";
		}
		else if (Input.GetKey(KeyCode.W))
		{
			anim.Play("Link Up");
			direction = directions["up"];
			currentAnimation = "Link Up";
		}
		else if (Input.GetKey(KeyCode.D))
		{
			sr.flipX = false;
			anim.Play("Link Right");
			direction = directions["right"];
			currentAnimation = "Link Right";
		}
		else if (Input.GetKey(KeyCode.S))
		{
			anim.Play("Link Down");
			direction = directions["down"];
			currentAnimation = "Link Down";
		}
		else if (Input.GetKey(KeyCode.A))
		{
			sr.flipX = true;
			anim.Play("Link Right");
			direction = directions["left"];
			currentAnimation = "Link Right";
		}
		else
		{
			anim.Play(currentAnimation, -1, 0f);
			anim.speed = 0;
		}

		int finalSpeed = speed;
		if (Input.GetKey(KeyCode.LeftShift))
		{
			finalSpeed = speed + (speed / 2);
		}

		transform.position += (Vector3)(direction * finalSpeed) * Time.deltaTime;
	}
}
