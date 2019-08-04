using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarWheel : MonoBehaviour
{
	[SerializeField] private float maxRotationSpeed;
	[SerializeField] private float accelerationMultiplier;
    [SerializeField] private float maxPitch = 1.5f;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip decelerate;

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
        source = GetComponent<AudioSource>();
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

		ChangeObjects();
	}

	private void Accelerate()
	{
		rotationSpeed += accelerationMultiplier;
		if (rotationSpeed > maxRotationSpeed) rotationSpeed = maxRotationSpeed;

        float lerp = Mathf.InverseLerp(0, maxRotationSpeed, rotationSpeed);
        float pitch = Mathf.Lerp(1, maxPitch, lerp);
        source.pitch = pitch;

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

        float lerp = Mathf.InverseLerp(0, maxRotationSpeed, rotationSpeed);
        float pitch = Mathf.Lerp(1, maxPitch, lerp);
        source.pitch = pitch;

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
		ChangeSprites();
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

	private void ChangeSprites()
	{
		if (countTimeToChange >= timeToChangeSprite)
		{
			rotationSpeed = maxRotationSpeed;

			circleSpriteRenderer.DOFade(1, fadeDuration);
			squareSpriteRenderer.DOFade(0, fadeDuration);

            source.pitch = 1;
            source.clip = decelerate;
            source.loop = false;
            source.time = 0;
            source.Play();
		}
	}

	private void ChangeObjects()
	{
		if (circleSpriteRenderer.color.a >= .95f)
		{
			cylinder.SetActive(true);
			circleSpriteRenderer.gameObject.SetActive(false);
			gameObject.SetActive(false);
		}
	}
}
