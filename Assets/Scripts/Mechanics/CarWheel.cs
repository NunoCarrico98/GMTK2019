using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarWheel : MonoBehaviour
{
	[SerializeField] private float maxRotationSpeed;
	[SerializeField] private float accelerationMultiplier;

	[Header("Circle and Cylinder")]
	[SerializeField] private SpriteRenderer circleSpriteRenderer;
	[SerializeField] private float fadeDuration;
	[SerializeField] private float timeToChangeSprite;
	[SerializeField] private GameObject cylinder;

	private float rotationSpeed = 0;
	private float countTimeToChange;
	private bool speeding = false;
	private bool increaseTimer = false;
	private bool decreaseTimer = false;
	private SpriteRenderer squareSpriteRenderer;

	private void Awake()
	{
		squareSpriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void OnMouseDown()
	{
		rotationSpeed += accelerationMultiplier;
		speeding = true;
	}

	private void OnMouseUp()
	{
		speeding = false;
	}

	private void Update()
	{
		if (speeding) Accelerate();
		else Deaccelerate();

		if (increaseTimer) StartCoroutine(IncreaseTimer());
		if (decreaseTimer) StartCoroutine(DecreaseTimer());
	}

	private void Accelerate()
	{
		rotationSpeed += accelerationMultiplier;
		if (rotationSpeed > maxRotationSpeed) rotationSpeed = maxRotationSpeed;
		if (rotationSpeed == maxRotationSpeed && !increaseTimer)
		{
			StopAllCoroutines();
			increaseTimer = true;
		}

		transform.Rotate(0, 0, -rotationSpeed);
	}

	private void Deaccelerate()
	{
		rotationSpeed -= accelerationMultiplier;
		if (rotationSpeed < 0) rotationSpeed = 0;
		if (rotationSpeed != maxRotationSpeed && !decreaseTimer)
		{
			StopAllCoroutines();
			decreaseTimer = true;
		}

		transform.Rotate(0, 0, -rotationSpeed);
	}

	private void Countdown()
	{
		while (countTimeToChange < timeToChangeSprite)
			countTimeToChange += Time.deltaTime;
	}

	private IEnumerator IncreaseTimer()
	{
		increaseTimer = false;
		decreaseTimer = false;

		while (countTimeToChange < timeToChangeSprite)
		{
			countTimeToChange += Time.deltaTime;
			yield return null;
		}
		ChangeObjects();
	}

	private IEnumerator DecreaseTimer()
	{
		decreaseTimer = false;
		increaseTimer = false;

		while(countTimeToChange > 0)
		{
			countTimeToChange -= Time.deltaTime;
			if (countTimeToChange < 0) countTimeToChange = 0;
			yield return null;
		}
		
	}

	private void ChangeObjects()
	{
		if (countTimeToChange >= timeToChangeSprite)
		{
			rotationSpeed = maxRotationSpeed;

			circleSpriteRenderer.DOFade(1, fadeDuration);
			squareSpriteRenderer.DOFade(0, fadeDuration);

			/*
			 * 
			 * FIX THIS SHIT
			 * 
			 * 
			if (circleSpriteRenderer.color.a == 1)
			{
				cylinder.SetActive(true);
				circleSpriteRenderer.gameObject.SetActive(false);
				gameObject.SetActive(false);
			}*/
		}
	}
}
