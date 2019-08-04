using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DragAndRotate : MonoBehaviour
{
	[SerializeField] private float desiredAngle;
	[SerializeField] private float angle;
	[SerializeField] private float colorChangeSpeed;
	[SerializeField] private bool rotate = true;

	private Camera[] cams;

	public bool RotateObject
	{
		get
		{
			return rotate;
		}
		set
		{
			rotate = value;
		}
	}

	private void Awake()
	{
		cams = FindObjectsOfType<Camera>();
	}

	private void OnMouseDrag()
	{
		if (rotate) Rotate();
	}

	private void Rotate()
	{
		if (Input.GetAxisRaw("Mouse Y") > .1f)
		{
			transform.Rotate(angle * Time.deltaTime * Input.GetAxisRaw("Mouse Y"), 0, 0);
			if (transform.eulerAngles.x >= desiredAngle - 5)
			{
				transform.eulerAngles = new Vector3(Mathf.Lerp(transform.eulerAngles.x, desiredAngle, angle * Time.deltaTime),
					transform.eulerAngles.y, transform.eulerAngles.z);
				rotate = false;
				StartCoroutine(LoadNextLevel());
			}
			ChangeBackgroundColor();
		}
	}

	private IEnumerator LoadNextLevel()
	{
		yield return new WaitForSeconds(.5f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	private void ChangeBackgroundColor()
	{
		foreach(Camera c in cams)
			if(c.backgroundColor != Color.white)
				c.backgroundColor = 
					Color.Lerp(Color.black, Color.white, 
					transform.eulerAngles.x / 90);
	}
}
