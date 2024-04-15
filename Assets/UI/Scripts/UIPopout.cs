using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIPopout : MonoBehaviour
{
	public RectTransform uiElement;
	public Vector2 targetPosition;
	public float jumpHeight = 50f;
	public float jumpDuration = 0.5f;

	private Vector2 originalAnchoredPosition;
	private bool isAnimating = false;

	void Start()
	{
		originalAnchoredPosition = uiElement.anchoredPosition;
	}

	public void TriggerJumpAnimation(bool forward = true)
	{
		if (!isAnimating)
		{
			StartCoroutine(AnimateJump(forward));
		}
	}

	IEnumerator AnimateJump(bool forward)
	{
		isAnimating = true;

		Vector2 startPosition = forward ? originalAnchoredPosition : new Vector2(targetPosition.x, -targetPosition.y);
		Vector2 endPosition = forward ? targetPosition : new Vector2(targetPosition.x, -targetPosition.y*20);
		float heightOffset = forward ? jumpHeight : -jumpHeight;

		float elapsedTime = 0;
		while (elapsedTime < jumpDuration)
		{
			float t = elapsedTime / jumpDuration;
			float yOffset = Mathf.Sin(t * Mathf.PI) * jumpHeight;
			uiElement.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t) + new Vector2(0f, heightOffset - yOffset);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		uiElement.anchoredPosition = endPosition;

		isAnimating = false;
	}
}
