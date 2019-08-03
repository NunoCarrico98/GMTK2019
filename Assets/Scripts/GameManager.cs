using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField] private Camera renderCam;
	[SerializeField] private Color colorToFill;

	private Camera cam;

	private void Awake()
	{
		cam = Camera.main;
	}

	private void Update()
	{
		CheckWin();
	}

	private void CheckWin()
	{
		int width = 256;
		int height = 144;

		Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		Rect rectReadPicture = new Rect(0, 0, width, height);

		RenderTexture rt = new RenderTexture(width, height, 24);
		renderCam.targetTexture = rt;
		renderCam.Render();
		RenderTexture.active = rt;

		tex.ReadPixels(rectReadPicture, 0, 0);
		tex.Apply();

		List<Color> colors = new List<Color>();

		for (int i = 0; i < width; i++)
			for (int j = 0; j < height; j++)
			{
				Color color = tex.GetPixel(i, j);
				colors.Add(color);
			}

		if (colors.All(c => c == colorToFill))
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
