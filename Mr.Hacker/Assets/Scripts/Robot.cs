using UnityEngine;
using System.Collections;


// @Author: Grafisch Lyceum Rotterdam, Maarten de Goede
public class Robot : MonoBehaviour {
	public int health = 5;
	public int damage = 1;
	public float speed = 3;
	public bool hackable = true;				//If the robot is hackable or not
	public Vector2[] path;						//The path the robot should use

	private int targetWaypoint;					//The next point of the path
	private bool isHacked = false;				//If the robot is hacked
	private Animator animator;


	void Start () {
		targetWaypoint = 0;						//The start target waypoint is 0
		animator = GetComponent<Animator>();
	}
	void Update () {
		//Move the robot
		walkRobot(speed * Time.deltaTime);
	}

	/// <summary>
	/// Fires a laser.
	/// </summary>
	private void fireLaser() {
		//Star the animation
		animator.SetTrigger("Fire");
	}

	/// <summary>
	/// Walks the robot.
	/// </summary>
	/// <param name="speed">Moving speed.</param>
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
}
