using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField] private Transform[] winCheckers;
	[SerializeField] private Camera renderCam;

	private Camera cam;

	private bool[] cameraCorners;

	private void Awake()
	{
		cam = Camera.main;
		cameraCorners = new bool[4];
	}

	private void Update()
	{
		CheckWin();
	}

	// Check Level Win
	/*public void CheckWin()
	{
		if (cameraCorners.All(x => x == true))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
		else
		{
			Bounds bounds = new Bounds();

			bounds.center = Vector2.zero;
			bounds.size = new Vector2(cam.orthographicSize * 3.5f, cam.orthographicSize * 2);

			for (int i = 0; i < cameraCorners.Length; i++)
				if (!bounds.Contains(winCheckers[i].position))
					cameraCorners[i] = true;
		}
	}*/

	public void CheckWin()
	{
		Texture2D tex = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
		Rect rectReadPicture = new Rect(0, 0, 1920, 1080);

		// Initialize and render
		RenderTexture rt = new RenderTexture(1920, 1080, 24);
		renderCam.targetTexture = rt;
		renderCam.Render();
		RenderTexture.active = rt;

		// Read pixels
		tex.ReadPixels(rectReadPicture, 0, 0);

		// Clean up
		renderCam.targetTexture = null;
		RenderTexture.active = null; // added to avoid errors 
		DestroyImmediate(rt);
	}


}
