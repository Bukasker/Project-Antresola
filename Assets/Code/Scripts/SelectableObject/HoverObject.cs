using System.Collections;
using UnityEngine;

public class HoverObject : MonoBehaviour
{
	public SelectableObject ObjectToSelect;
	public float hoverTimeThreshold = 0.5f; // Czas w sekundach, po którym obiekt opuszcza obszar

	private bool isHovering = false;

	private void OnMouseEnter()
	{
		isHovering = true;
		StartCoroutine(HoverTimer());
	}

	private void OnMouseExit()
	{
		SelectableObjectManager.Instance.RemoveSelectedObject();
		isHovering = false;
	}

	IEnumerator HoverTimer()
	{
		float timer = 0f;
		while (timer < hoverTimeThreshold && isHovering)
		{
			timer += Time.deltaTime;
			yield return null;
		}

		if (isHovering && ObjectToSelect != null)
			SelectableObjectManager.Instance.SelectObject(gameObject);
	}
}
