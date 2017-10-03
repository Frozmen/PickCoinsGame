using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance = null;

    private static int COMPLEXITY_UP_COUNT = 5;
	private static int START_STARS_VALUE = 10;
    private static int WIN_STARS = 25;

    private static float STAR_MOVE_SPEED = 5;

    private int count;
    public AudioSource backgroundAudio;
    public AudioSource loseAudio;
    public AudioSource pickPointAudio;

    public Text countText;
	public Text winText;
	public Vector3 spawnValues;
	public GameObject star;
	public GameObject movingStar;

	private bool gameOwer;
    
	void Start () {
        instance = this;
        count = 0;
		SetCountText ();
		winText.text = "";
		gameOwer = false;
    }

    void Update ()
	{
		if (gameOwer)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
				gameOwer = false;
			}
		}
	}

	void SetCountText ()
	{
		countText.text = "Count: " + count + " from " + WIN_STARS;
	}

	public void pickPoint()
	{
		count++;
        pickPointAudio.Play();
        SetCountText ();
		if (count == WIN_STARS) 
		{
			gameOwer = true;
			winText.text = "You Win! To play again press R";
			return;
		}

		if (count >= START_STARS_VALUE + COMPLEXITY_UP_COUNT) 
		{
			spawnMovingStar ();
			return;
		}

		if (count >= START_STARS_VALUE) 
		{
			spawnStaticStar ();
			return;
		}

	}

	public void userLose ()
	{
		winText.text = "You Lose! To play again press R";
		gameOwer = true;
        backgroundAudio.Stop();
        loseAudio.Play();

    }

	public bool isGameOwer()
	{
		return gameOwer;
	}

	private void spawnStaticStar(){
		Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, Random.Range (-spawnValues.z, spawnValues.z));
		Quaternion spawnRotation = Quaternion.identity;
		Instantiate (star, spawnPosition, spawnRotation);
	}

	private void spawnMovingStar(){
		Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, Random.Range (-spawnValues.z, spawnValues.z));
		Quaternion spawnRotation = Quaternion.identity;
		Instantiate (movingStar, spawnPosition, spawnRotation);
	}

    public float getStarMovingSpeed()
    {
        if (count < 20) return STAR_MOVE_SPEED;
        return STAR_MOVE_SPEED*2;
    }
}
