using UnityEngine;
using UnityEngine.UI;
public class InfiniteScrollLoop : MonoBehaviour
{
	[SerializeField] private ScrollRect scrollRect;
	[SerializeField] private RectTransform viewPortTransform;
	[SerializeField] private RectTransform contentPanelTransform;
	[SerializeField] private HorizontalLayoutGroup horizontalLayoutGroup;
	[SerializeField] private RectTransform[] ItemList;

	[SerializeField] private int numberOfItemsToDisplay = 3;

	[SerializeField] private Vector2 OldVelocity;
	bool isUpdated;

	void Start()
	{
		if(ItemList.Length > numberOfItemsToDisplay) 
		{
			isUpdated = false;
			OldVelocity = Vector2.zero;
			int ItemsToAdd = Mathf.CeilToInt(viewPortTransform.rect.width / (ItemList[0].rect.width + horizontalLayoutGroup.spacing));

			for (int i = 0; i < ItemsToAdd; i++)
			{
				RectTransform RT = Instantiate(ItemList[i % ItemList.Length], contentPanelTransform);
				RT.SetAsLastSibling();
			}
			for (int i = 0; i < ItemsToAdd; i++)
			{
				int num = ItemList.Length - i - 1;
				while (num < 0)
				{
					num += ItemList.Length;
				}
				RectTransform RT = Instantiate(ItemList[num], contentPanelTransform);
				RT.SetAsFirstSibling();
			}

			contentPanelTransform.localPosition = new Vector3(0 - (ItemList[0].rect.width + horizontalLayoutGroup.spacing) * ItemsToAdd,
				contentPanelTransform.localPosition.y,
				contentPanelTransform.localPosition.z);
		}
	}

	void Update()
	{
		if (ItemList.Length > numberOfItemsToDisplay)
		{
			if (isUpdated)
			{
				isUpdated = false;
				scrollRect.velocity = OldVelocity;
			}

			if (contentPanelTransform.localPosition.x > 0)
			{
				Canvas.ForceUpdateCanvases();
				OldVelocity = scrollRect.velocity;
				contentPanelTransform.localPosition -= new Vector3(ItemList.Length * (ItemList[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
				isUpdated = true;
			}
			if (contentPanelTransform.localPosition.x < 0 - (ItemList.Length * (ItemList[0].rect.width + horizontalLayoutGroup.spacing)))
			{
				Canvas.ForceUpdateCanvases();
				OldVelocity = scrollRect.velocity;
				contentPanelTransform.localPosition += new Vector3(ItemList.Length * (ItemList[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
				isUpdated = true;
			}
		}
	}
}
