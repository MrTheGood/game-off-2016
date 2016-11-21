using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {
	public static BoardManager boardManager;
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
}
