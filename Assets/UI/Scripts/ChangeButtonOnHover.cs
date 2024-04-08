using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeButtonOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[Header("Image Settings")]
	public Image panelImage;
	public Sprite hoveredSprite;
	public Sprite originalSprite;

	public void OnPointerEnter(PointerEventData eventData)
	{
		panelImage.sprite = hoveredSprite;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		panelImage.sprite = originalSprite;
	}
}
