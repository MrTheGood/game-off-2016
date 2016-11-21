using UnityEngine;
using System.Collections;


// @Author: Grafisch Lyceum Rotterdam, Maarten de Goede
public class Robot : MonoBehaviour {
	public int health = 5;
	public int damage = 1;
	public float speed = 3;
	public bool hackable = true;				//If the robot is hackable or not
	public Vector2[] path;						//The path the robot should use
	public GameObject laser;

	private int targetWaypoint;					//The next point of the path
	private bool isHacked = false;				//If the robot is hacked
	private bool isShooting = false;			//If the robot is shooting
	private Animator animator;
	private Sprite startSprite;					//The starting sprite


	void Start () {
		targetWaypoint = 0;						//The start target waypoint is 0
		animator = GetComponent<Animator>();
		startSprite = GetComponent<SpriteRenderer>().sprite;
	}
	void Update () {
		if (!isHacked) {
			if (!isShooting) {
				//Move the robot
				walkRobot(speed * Time.deltaTime);
				//Look for the player
				lookForPlayer();
			}
			//Set the walk animation.
			setWalkAnimation();
		}
	}


	private Vector2 getWalkingDirection() {
		Vector2 targetLocation = path[targetWaypoint];

		if (transform.position.x > targetLocation.x) {
			return Vector2.right;
		}
		if (transform.position.x < targetLocation.x) {
			return Vector2.left;
		}
		if (transform.position.y > targetLocation.y) {
			return Vector2.up;
		}
		return Vector2.down;
	}



	private IEnumerator fireLaser(Vector2 direction) {
		//Star the animation
		isShooting = true;
		animator.SetTrigger("Fire");
		yield return new WaitForSeconds(0.6f);

		//Instantiate the laser
		GameObject instance = (GameObject)Instantiate(laser, transform.position, new Quaternion());;

		//Send the laser to the correct direction
		if (getWalkingDirection() == Vector2.left) {
			instance.transform.position -= new Vector3(0, 0.1f, 0);
			instance.transform.rotation = new Quaternion();
		}

		if (getWalkingDirection() == Vector2.right) {
			instance.transform.position -= new Vector3(0.2f, 0.1f, 0);
			instance.transform.rotation = new Quaternion(0, 0, 180, 0);
		}

		if (getWalkingDirection() == Vector2.down) {
			instance.transform.position -= new Vector3();
			instance.transform.rotation = new Quaternion(0, 0, 90, 90);
		}
		
		if (getWalkingDirection() == Vector2.up) {
			instance.transform.position -= new Vector3();
			instance.transform.rotation = new Quaternion(0, 0, 90, -90);
		}

		//Set the laser damage
		instance.GetComponent<Laser>().damage = damage;

		//
		yield return new WaitForSeconds(1.2f);
		isShooting = false;
	}


	private void walkRobot(float speed) {
		//Get the location where the robot should walk to.
		Vector2 targetLocation = path[targetWaypoint];

		//If the robot hasn't yet arrived at the target location,
		if ((Vector2)transform.position != targetLocation) {
			//Move towards the target location.
			transform.position = Vector2.MoveTowards(transform.position, targetLocation, speed);
			return;
		}

		//The robot has arrived. Let's go to the next waypoint.
		targetWaypoint++;
		//If the next waypoint doesn't exist,
		if (targetWaypoint >= path.Length)
			//Go to the first waypoint.
			targetWaypoint = 0;
	}


	private void lookForPlayer() {
		RaycastHit hit;

		if (Physics.Raycast(transform.position, -getWalkingDirection(), out hit)) {
			if (hit.collider.gameObject.tag == "Player") {
				StartCoroutine(fireLaser(getWalkingDirection()));
			}
		}
	}


	private void setWalkAnimation() {
		Vector2 targetLocation = path[targetWaypoint];
		
		//Reset the animator and rotation
		transform.rotation = new Quaternion(0, 0, 0, 0);
		animator.enabled = true;

		//Test the direction and set the animation.
		if (targetLocation.x > transform.position.x) {
			//Right
			animator.SetInteger("WalkState", 2);
		} else if (targetLocation.x < transform.position.x) {
			//Left
			animator.SetInteger("WalkState", 2);
			//Flip the robot horizontally 
			transform.rotation = new Quaternion(0, 180, 0, 0);
		} else if (targetLocation.y > transform.position.y) {
			//Up
			animator.SetInteger("WalkState", 1);
		} else if (targetLocation.y < transform.position.y) {
			//Down
			animator.SetInteger("WalkState", 0);
		} else {
			//The Robot isn't walking. Stop the animation and set a standing sprite.
			animator.enabled = false;
			GetComponent<SpriteRenderer>().sprite = startSprite;
		}
	}
}
