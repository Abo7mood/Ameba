using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	float moveSpeed = 5f;

	Rigidbody2D rb;
	Vector3 mousePosition;
	Touch touch;
	Vector3 touchPosition, whereToMove;
	bool isMoving = false;

	float previousDistanceToTouchPos, currentDistanceToTouchPos;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		PlayerInput();
		Movement();
	}
	private void Movement()
	{
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
		if (isMoving)
			currentDistanceToTouchPos = (mousePosition - transform.position).magnitude;
		previousDistanceToTouchPos = (mousePosition - transform.position).magnitude;

#else
if (isMoving)
			currentDistanceToTouchPos = (touchPosition - transform.position).magnitude;
			previousDistanceToTouchPos = (touchPosition - transform.position).magnitude;
#endif


	}

	private void PlayerInput()
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
		 mousePosition = Input.mousePosition;
		if (Input.GetMouseButtonDown(0))
        {
			PlayerMove(ref mousePosition);
		}

#else
if (Input.touchCount > 0)
		{
			touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Began)
			{
				PlayerMove(ref touchPosition);
			}
		}
#endif


	}
	private  void  PlayerMove(ref Vector3 pos)
	{
		previousDistanceToTouchPos = 0;
		currentDistanceToTouchPos = 0;
		isMoving = true;
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
		pos = Camera.main.ScreenToWorldPoint(mousePosition);
#else
pos = Camera.main.ScreenToWorldPoint(touch.position);
#endif

		pos.z = 0;
		whereToMove = (pos - transform.position).normalized;
		rb.velocity = new Vector2(whereToMove.x * moveSpeed, whereToMove.y * moveSpeed);
	}
}
