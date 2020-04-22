﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAndLook : MonoBehaviour {

	[Header("Camera")]
	public Camera mainCamera;


	[Header("Movement")]
	public Rigidbody playerRigidbody;
	public float speed = 4.5f;
	public float rotateSpeed = 4.5f;
	private Vector3 inputDirection;
	private Vector3 movement;


	//Rotation

	private Plane playerMovementPlane;

	private RaycastHit floorRaycastHit;

	private Vector3 playerToMouse;


	[Header("Animation")]
	public Animator playerAnimator;


	void Awake()
	{
		CreatePlayerMovementPlane();
	}

	void CreatePlayerMovementPlane()
	{
		playerMovementPlane = new Plane (transform.up, transform.position + transform.up);
	}

	void FixedUpdate()
	{

		//Arrow Key Input
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		inputDirection = new Vector3(h, 0, v);

		//Camera Direction
		var cameraForward = mainCamera.transform.forward;
		var cameraRight = mainCamera.transform.right;

		cameraForward.y = 0f;
		cameraRight.y = 0f;

		//Try not to use var for roadshows or learning code
		Vector3 desiredDirection = cameraForward * inputDirection.z + cameraRight * inputDirection.x;
		
		//Why not just pass the vector instead of breaking it up only to remake it on the other side?
		MoveThePlayer(desiredDirection);
		TurnThePlayer();
		AnimateThePlayer(desiredDirection);
		
	}

	void MoveThePlayer(Vector3 desiredDirection)
	{
		movement.Set(desiredDirection.x, 0f, desiredDirection.z);

		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidbody.MovePosition(transform.position + movement);

	}

	void TurnThePlayer()
	{

		Vector3 rotateInput = new Vector3(0, Input.GetAxis("Rotate"), 0);

		if(rotateInput.sqrMagnitude > 0.05f)
			transform.Rotate(rotateInput * rotateSpeed * Time.deltaTime);

        #region Comentado
        /*Vector3 cursorScreenPosition = Input.mousePosition;

		Vector3 cursorWorldPosition = ScreenPointToWorldPointOnPlane(cursorScreenPosition, playerMovementPlane, mainCamera);

		playerToMouse = cursorWorldPosition - transform.position;

		playerToMouse.y = 0f;

		playerToMouse.Normalize();

		Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

		playerRigidbody.MoveRotation(newRotation);
		*/
        #endregion


    }

    Vector3 PlaneRayIntersection(Plane plane, Ray ray)
	{
		float dist = 0.0f;
		plane.Raycast(ray, out dist);
		return ray.GetPoint(dist);
	}

	Vector3 ScreenPointToWorldPointOnPlane(Vector3 screenPoint, Plane plane, Camera camera)
	{	
		Ray ray = camera.ScreenPointToRay(screenPoint);
		return PlaneRayIntersection(plane, ray);
	}


	void AnimateThePlayer(Vector3 desiredDirection)
	{
		if(!playerAnimator)
			return;

		playerAnimator.SetFloat("Forward", Input.GetAxis("Vertical"));
		playerAnimator.SetFloat("Strafe", Input.GetAxis("Horizontal"));

	}
}
