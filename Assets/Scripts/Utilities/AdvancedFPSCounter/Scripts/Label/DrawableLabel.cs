using System.Text;
using UnityEngine;

namespace CodeStage.AdvanecedFPSCounter.Label
{
	internal class DrawableLabel
	{
		public LabelAnchor anchor = LabelAnchor.UpperLeft;
		public GUIText guiText = null;
		//public string newText = "";
		public StringBuilder newText;
		public bool dirty = false;

		private int fontSize = 0;

		public DrawableLabel(LabelAnchor anchor, int fontSize)
		{
			this.anchor = anchor;
			this.fontSize = fontSize;

			//Debug.Log("DrawableLabel constructor, newText = " + newText);

			newText = new StringBuilder(1000);
		}

		internal void CheckAndUpdate()
		{
			if (newText.Length > 0)
			{
				if (guiText == null)
				{

					GameObject anchorObject = new GameObject(anchor.ToString(), typeof(GUIText));
					guiText = anchorObject.guiText;

					if (anchor == LabelAnchor.UpperLeft)
					{
						anchorObject.transform.position = new Vector3(0f, 1f);
						guiText.anchor = TextAnchor.UpperLeft;
						guiText.alignment = TextAlignment.Left;
						guiText.pixelOffset = new Vector2(5f, -5f);
					}
					else if (anchor == LabelAnchor.UpperRight)
					{
						anchorObject.transform.position = new Vector3(1f, 1f);
						guiText.anchor = TextAnchor.UpperRight;
						guiText.alignment = TextAlignment.Right;
						guiText.pixelOffset = new Vector2(-5f, -5f);
					}
					else if (anchor == LabelAnchor.LowerLeft)
					{
						anchorObject.transform.position = new Vector3(0f, 0f);
						guiText.anchor = TextAnchor.LowerLeft;
						guiText.alignment = TextAlignment.Left;
						guiText.pixelOffset = new Vector2(5f, 5f);
					}
					else if (anchor == LabelAnchor.LowerRight)
					{
						anchorObject.transform.position = new Vector3(1f, 0f);
						guiText.anchor = TextAnchor.LowerRight;
						guiText.alignment = TextAlignment.Right;
						guiText.pixelOffset = new Vector2(-5f, 5f);
					}

					guiText.fontSize = fontSize;
					anchorObject.transform.parent = AFPSCounter.Instance.mySweetNest.transform;
				}

				if (dirty)
				{
					guiText.text = newText.ToString();
					dirty = false;
				}
				newText.Length = 0;
			}
			else if (guiText != null)
			{
				Object.DestroyImmediate(guiText.gameObject);
			}
		}

		internal void Clear()
		{
			newText.Length = 0;
			if (guiText != null) Object.Destroy(guiText.gameObject);
		}

		internal void Dispose()
		{
			Clear();
			newText = null;
		}

		internal void ChangeFontSize(int newSize)
		{
			fontSize = newSize;
			if (guiText != null) guiText.fontSize = fontSize;
		}

		
	}
}