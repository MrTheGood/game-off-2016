using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
	public float speed;
	public int damage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector2.right * speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Robot") 
			return;
		
		Destroy(gameObject);
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Robot") 
			return;
		
		Destroy(gameObject);
	}
}
