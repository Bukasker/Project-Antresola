using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildPanelTabsButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[Header("Position Settings")]
	public RectTransform PanelPosition;
	public float moveDistance = 30f;

	[Header("Image Settings")]
	public Image panelImage;
	public Sprite hoveredSprite;
	public Sprite originalSprite;


	public void OnPointerEnter(PointerEventData eventData)
	{
		panelImage.sprite = hoveredSprite;
		MovePanel(-moveDistance);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		panelImage.sprite = originalSprite;
		MovePanel(moveDistance);
	}

	void MovePanel(float distance)
	{
		Vector3 newPosition = new Vector3 (PanelPosition.localPosition.x + distance, PanelPosition.localPosition.y, PanelPosition.localPosition.z);
		PanelPosition.localPosition = newPosition;
	}
}
