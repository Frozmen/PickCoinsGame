using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMoving : MonoBehaviour {
	private static float RATATION_SPEED = 8;
	private static float POSIBLE_MOVE_ERROR = 0.5f;

	public Transform obj;
	public Vector3 maxValues;
    private GameController gameController;

    // Use this for initialization
    void Start () {
        gameController = GameController.instance;
        generateNewTarget ();
	}

	void Update()
	{
		var look_dir = targetPositionVector - obj.position;
		look_dir.y = 0;
		obj.rotation = Quaternion.Slerp(obj.rotation,Quaternion.LookRotation(look_dir),RATATION_SPEED*Time.deltaTime);
		obj.position += obj.forward * gameController.getStarMovingSpeed() * Time.deltaTime;
		if (Mathf.Abs(targetPositionVector.x - obj.position.x) <= POSIBLE_MOVE_ERROR &&
			Mathf.Abs(targetPositionVector.z - obj.position.z) <= POSIBLE_MOVE_ERROR) 
		{
			generateNewTarget();
		}
			

	}

	private Vector3 targetPositionVector;
	private void generateNewTarget()
	{
		targetPositionVector = new Vector3 (Random.Range (-maxValues.x, maxValues.x), maxValues.y, Random.Range (-maxValues.z, maxValues.z));
	}
		
}
