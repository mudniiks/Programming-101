using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

	[System.Serializable]
	public class Controls{
		public string horizontalAxis;
		[SerializeField]
		public string verticalAxis;
		[SerializeField]
		public KeyCode aimButton;

	}

	Rigidbody _playerRigidbody;
	Rigidbody playerRigidbody {
		get {
			if (_playerRigidbody == null)
				_playerRigidbody = GetComponent<Rigidbody> ();
			return _playerRigidbody;
		}
	}

	public Controls playerControls;

	public float _speed;
	public float speed {
		get {
			if (Input.GetKey (playerControls.aimButton))
				return _speed / 2;
			else
				return _speed;
		}
	}
		
	// Update is called once per frame
	void Update () {
		Vector2 direction = new Vector2(Input.GetAxis(playerControls.horizontalAxis), Input.GetAxis(playerControls.verticalAxis));
		Move (direction);
		//makes player aim at mouse position
		Aim();
	}
	void Move(Vector2 movement) {
		playerRigidbody.velocity = new Vector3 (movement.x * speed, playerRigidbody.velocity.y, movement.y * speed);
		}
	void Aim() {
		//position of the mouse on the screen (in screen coordinates)
		Vector3 mouseScreenPosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y);
		Debug.Log (mouseScreenPosition);
		// position of the mouse in the game world
		Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint (mouseScreenPosition);
		// make the player look at the mouse game position
		transform.LookAt(new Vector3(mouseWorldPosition.x, transform.position.y, mouseWorldPosition.z));
	}
}

