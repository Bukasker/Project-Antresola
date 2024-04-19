using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildPanelSwipe : MonoBehaviour
{
	public ScrollRect scrollRect;

	public void Swipe(float SwipeValue)
	{
		scrollRect.horizontalNormalizedPosition += SwipeValue;
	}
}
