using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    
    public int speed;
    private Animator anim;
    private SpriteRenderer sr;
    float animspeed;

    private left D;


	// Use this for initialization
	void Start () 
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    
    
	}
	
	// Update is called once per frame
	void Update () {
        //if(Input.GetKeyDown(KeyCode.D)) anim.Play("Link Right");
        //if(Input.GetKeyDown(KeyCode.A)) anim.Play("Link Right");
        //if(Input.GetKeyDown(KeyCode.W)) anim.Play("Link Up");
        //if(Input.GetKeyDown(KeyCode.S)) anim.Play("Link Down");
        //if(Input.GetKeyUp(KeyCode.D)) anim.Play("Link move left"); anim.speed = 0;
        //if(Input.GetKeyUp(KeyCode.A)) anim.Play("Link move left"); anim.speed = 0;
        //if(Input.GetKeyUp(KeyCode.W)) anim.Play("Link stop up"); anim.speed = 0;
        //if(Input.GetKeyUp(KeyCode.S)) anim.Play("Link move down"); anim.speed = 0;
        
        

    
		if(Input.GetKey(KeyCode.W))
        {   
            anim.Play("Link Up");
            anim.speed = 1;
            transform.position += (Vector3)(Vector2.up * speed) * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.D))
        {
        
            sr.flipX = false;
            anim.Play("Link Right");
            anim.speed = 1;
            transform.position += (Vector3)(Vector2.right * speed) * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.S))
        {
            anim.Play("Link Down");
            anim.speed = 1;
            transform.position += (Vector3)(Vector2.down * speed) * Time.deltaTime;
            

        }
        if (Input.GetKey(KeyCode.A))
        {
            sr.flipX = true;
            anim.Play("Link Right");
            anim.speed = 1;
            transform.position += (Vector3)(Vector2.left * speed) * Time.deltaTime;


        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            
   

        }
        GUI.Label(new Rect(0, 25, 40, 60), "Speed");

        animspeed = GUI.HorizontalSlider(new Rect(45, 25, 200, 60), animspeed, 0.0F, 1.0F);
        anim.speed = animspeed;
  
    }
}
