using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{

	public GameObject player;

	public int cubeDirection = 0;

	Vector2 mouse;

	public void Turn() {
		float direction;
		float prevAngle = transform.eulerAngles.y;

		mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);

		if (mouse.x < Screen.width / 2)//Left
		{
			direction = -90;
			cubeDirection --;
		}
		else//Right
		{
			direction = 90;
			cubeDirection ++;
		}

		if (cubeDirection == -1)
			cubeDirection = 3;
		if (cubeDirection == 4)
			cubeDirection = 0;

		transform.eulerAngles = new Vector3(transform.eulerAngles.x, prevAngle + direction, transform.eulerAngles.z);;

		player.GetComponent<PlayerScript>().MoveWithCube();
	}
}
