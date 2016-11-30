using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour {
	public static BoardManager boardManager;
	public static int score = 0;
	public GameObject[] levels;

	private int currentLevel = 0;
	private GameObject level;


	void Awake () {
		if (boardManager == null)
			boardManager = this;
		else if (boardManager != this)
			Destroy(this);
		
		loadLevel();
	}
	
	public void loadLevel() {
		Destroy(level);
		level = (GameObject)Instantiate(levels[currentLevel]);

		currentLevel++;

		if (currentLevel >= levels.Length)
			currentLevel = 0;
	}

	void OnGUI() {
		int health = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().health;
		string scoreText = "";
		if (score < 100) {
			scoreText = "0";
		} 
		if (score < 10) {
			scoreText += "0";
		}
		scoreText += score + " ";

		GameObject.Find("scoreText").GetComponent<Text>().text = "Score: " + scoreText;
		GameObject.Find("lifeText").GetComponent<Text>().text = " Lives: " + health;
	}
}
