using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
	public bool lockCursor = true;
	public float mouseSensitivity = 2;
	public Transform target;
	public float dstFromTarget = 0;
	public Vector2 pitchMinMax = new Vector2(-35, 90);

	public float rotationSmoothTime = 0;
	Vector3 rotationSmoothVelocity;
	Vector3 currentRotation;

	float yaw;
	float pitch;
	
	private float cameraPitch;

	void Start()
	{
		if (lockCursor)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	void LateUpdate()
	{
		yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
		pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

		currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);

		Quaternion camRotation = Quaternion.Euler(currentRotation.x,currentRotation.y,0);
		Quaternion camTargetRotation = Quaternion.Euler(0,currentRotation.y,0);

		target.transform.rotation = camTargetRotation;
		transform.rotation = camRotation;

	}

}