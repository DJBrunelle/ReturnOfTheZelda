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


	// Use this for initialization
	void Start()
	{
		anim = GetComponent<Animator>();
		sr = GetComponent<SpriteRenderer>();
		anim.speed = 1;

		directions = new Dictionary<string, Vector2>()
		{
			{"up", Vector2.up},
			{"down", Vector2.down},
			{"left", Vector2.left},
			{"right", Vector2.right},
			{"upRight", new Vector2(1,1)},
			{"upLeft", new Vector2(-1,1)},
			{"downRight", new Vector2(1,-1)},
			{"downLeft", new Vector2(-1,-1)},
		};

	}

	// Update is called once per frame
	void Update()
	{
		Movement();
	}

	void Movement()
	{
		Vector2 direction = Vector2.zero;

		if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
		{
			anim.Play("LinkUpRight");
			sr.flipX = false;
			direction = directions["upRight"];
		}
		else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
		{
			anim.Play("LinkUpRight");
			sr.flipX = true;
			direction = directions["upLeft"];
		}
		else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
		{
			anim.Play("LinkDownLeft");
			sr.flipX = true;
			direction = directions["downRight"];
		}
		else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
		{
			anim.Play("LinkDownLeft");
			sr.flipX = false;
			direction = directions["downLeft"];
		}
		else if (Input.GetKey(KeyCode.W))
		{
			anim.Play("Link Up");
			direction = directions["up"];
		}
		else if (Input.GetKey(KeyCode.D))
		{
			sr.flipX = false;
			anim.Play("Link Right");
			direction = directions["right"];
		}
		else if (Input.GetKey(KeyCode.S))
		{
			anim.Play("Link Down");
			direction = directions["down"];
		}
		else if (Input.GetKey(KeyCode.A))
		{
			sr.flipX = true;
			anim.Play("Link Right");
			direction = directions["left"];
		}

		int finalSpeed = speed;
		if (Input.GetKey(KeyCode.LeftShift))
		{
			finalSpeed = speed + (speed / 2);
		}

		transform.position += (Vector3)(direction * finalSpeed) * Time.deltaTime;
	}
}
