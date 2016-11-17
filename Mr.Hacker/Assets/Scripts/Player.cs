using UnityEngine;
using System.Collections;


// @Author: Grafisch Lyceum Rotterdam, Maarten de Goede
public class Player : MonoBehaviour {
	public int health = 3;
	public float speed = 4;

	private CharacterController characterController;
	private Animator animator;
	private Sprite startSprite;



	void Start () {
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		startSprite = GetComponent<SpriteRenderer>().sprite;
	}
	void Update () {
		//Move the player.
		walkPlayer();
	}


	/// <summary>
	/// Moves the player to the location based on the input, and plays the correct animation.
	/// </summary>
	private void walkPlayer() {
		//Calculate the input/walking speed
		float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		float y = Input.GetAxis("Vertical") * speed * Time.deltaTime;

		//Reset the animator and rotation
		transform.rotation = new Quaternion(0, 0, 0, 0);
		animator.enabled = true;
		//Test the direction and set the animation.
		if (x > 0) {
			//Right
			animator.SetInteger("WalkState", 1);
		} else if (x < 0) {
			//Left
			animator.SetInteger("WalkState", 1);
			//Flip the player horizontally 
			transform.rotation = new Quaternion(0, 180, 0, 0);
		} else if (y > 0) {
			//Up
			animator.SetInteger("WalkState", 2);
		} else if (y < 0) {
			//Down
			animator.SetInteger("WalkState", 0);
		} else {
			//The player isn't walking. Stop the animation and set a standing sprite.
			animator.enabled = false;
			GetComponent<SpriteRenderer>().sprite = startSprite;
		}

		//Move the player
		characterController.Move(new Vector3(x, y, 0));
	}
}
