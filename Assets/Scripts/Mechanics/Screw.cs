using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Screw : MonoBehaviour
{
	[SerializeField] private float scaleAmount;
	[SerializeField] private float scaleDuration;
	[SerializeField] private float angle;
	[SerializeField] private float playSoundThreshold = 0.1f;
    [SerializeField] private AudioSource source;

    private Camera cam;
	private float scaleDesired = 1;

	private void Awake()
	{
		cam = Camera.main;
	}

	private void OnMouseDrag()
	{
		RotateSquare();
	}

	private void RotateSquare()
	{
		if (Input.GetMouseButton(0))
		{
			Vector3 mousePos = Input.mousePosition;
			mousePos = cam.ScreenToWorldPoint(mousePos);
			Vector2 direction = mousePos - transform.position;

			float previousRot = transform.rotation.z;
			transform.up = direction;

			ScaleSquare(previousRot);
		}
        else if (source.isPlaying)
            source.Pause();

    }

	private void ScaleSquare(float previousRot)
	{
		float newRot = transform.rotation.z;


        float mov = Mathf.Abs(newRot - previousRot);
        print(mov);
        if (mov >= playSoundThreshold)
        {
            if (!source.isPlaying)
                source.Play();
        }
        else
        {
            if (source.isPlaying)
                source.Pause();
        }

        if (newRot > 0 && previousRot > 0)
		{

			if (newRot > previousRot)
			{
				scaleDesired += scaleAmount;
				transform.DOScale(scaleDesired, scaleDuration);
			}
			else if (newRot < previousRot)
			{
				scaleDesired -= scaleAmount;
				if (scaleDesired < 1) scaleDesired = 1;
				transform.DOScale(scaleDesired, scaleDuration);
			}
		}
		if (newRot < 0 && previousRot < 0)
		{
			if (newRot > previousRot)
			{
				scaleDesired += scaleAmount;
				transform.DOScale(scaleDesired, scaleDuration);
			}
			else if (newRot < previousRot)
			{
				scaleDesired -= scaleAmount;
				if (scaleDesired < 1) scaleDesired = 1;
				transform.DOScale(scaleDesired, scaleDuration);
			}
		}
	}
}
