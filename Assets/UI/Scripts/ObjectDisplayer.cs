using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ObjectDisplayer : MonoBehaviour
{
	[Header("Display Object Settings")]
	public SelectableObject ObjectToDisplay;
	public GameObject PanelPrefab;
	private GameObject CreatedPrefab;
	private Vector3 mousePosition;

	[Header("Canvas Settings")]
	[SerializeField] private RectTransform canvasRectTransform;
	[SerializeField] private Vector2 border = new Vector2(0.85f, 5f);
	[SerializeField] private float displayDelay = 1.5f;
	[SerializeField] private float heightOfText = 80f;
	private float activeTexts;

	[Header("Object Names")]
	[SerializeField] private string childUpperPanelStrings;
	[SerializeField] private string childDownPanelStrings;
	[SerializeField] private string objectNameString;
	[SerializeField] private string woodCapacityString;
	[SerializeField] private string stoneCapacityString;
	[SerializeField] private string foodCapacityString;
	[SerializeField] private string settlerCapacityString;

	private TextMeshProUGUI objectNameText;
	private TextMeshProUGUI woodCapacityText;
	private TextMeshProUGUI stoneCapacityText;
	private TextMeshProUGUI foodCapacityText;
	private TextMeshProUGUI settlerCapacityText;

	private RectTransform mainPanelBasicRect;
	private RectTransform bottomPanelBasicRect;

	public bool isHovering = false;
	public float hoverTimeThreshold = 0.0f;
	public void Displayer()
	{
		if (ObjectToDisplay != null)
		{
			Vector2 localPoint;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Input.mousePosition, null, out localPoint);

			CreatedPrefab = Instantiate(PanelPrefab, mousePosition, Quaternion.identity);
			CreatedPrefab.transform.SetParent(transform, false);

			var upperPanel = CreatedPrefab.transform.Find(childUpperPanelStrings)?.gameObject;
			objectNameText = upperPanel.transform.Find(objectNameString).GetComponent<TextMeshProUGUI>();

			var downPanel = CreatedPrefab.transform.Find(childDownPanelStrings)?.gameObject;
			woodCapacityText = downPanel.transform.Find(woodCapacityString).GetComponent<TextMeshProUGUI>();
			stoneCapacityText = downPanel.transform.Find(stoneCapacityString).GetComponent<TextMeshProUGUI>();
			foodCapacityText = downPanel.transform.Find(foodCapacityString).GetComponent<TextMeshProUGUI>();
			settlerCapacityText = downPanel.transform.Find(settlerCapacityString).GetComponent<TextMeshProUGUI>();

			objectNameText.text = ObjectToDisplay.ObjectName;

			var verticalLayoutGroup = downPanel.GetComponentInChildren<VerticalLayoutGroup>();

			SetTextAndSetActive(woodCapacityText, ObjectToDisplay.WoodCapacity,
				ObjectToDisplay.MaxWoodCapacity, "Wood : ", verticalLayoutGroup.transform);
			SetTextAndSetActive(stoneCapacityText, ObjectToDisplay.StoneCapacity,
				ObjectToDisplay.MaxStoneCapacity, "Stone : ", verticalLayoutGroup.transform);
			SetTextAndSetActive(foodCapacityText, ObjectToDisplay.FoodCapacity,
				ObjectToDisplay.MaxFoodCapacity, "Food : ", verticalLayoutGroup.transform);
			SetTextAndSetActive(settlerCapacityText, ObjectToDisplay.SettlerCapacity,
				ObjectToDisplay.MaxSettlerCapacity, "Settler : ", verticalLayoutGroup.transform);

			var RectTransform = downPanel.GetComponentInChildren<RectTransform>();
			mainPanelBasicRect = RectTransform;
			RectTransform.sizeDelta = new Vector2(RectTransform.sizeDelta.x, RectTransform.sizeDelta.y + ((activeTexts - 1) * heightOfText));
			var RectTransformObject = CreatedPrefab.GetComponentInChildren<RectTransform>();
			bottomPanelBasicRect = RectTransformObject;
			RectTransformObject.sizeDelta = new Vector2(RectTransformObject.sizeDelta.x, RectTransformObject.sizeDelta.y + ((activeTexts - 1) * heightOfText));

		}
	}


	private void UpdateMousePosition()
	{
		if (CreatedPrefab != null)
		{
			Vector2 localPoint;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Input.mousePosition, null, out localPoint);

			if (Input.mousePosition.y >= (canvasRectTransform.rect.height / border.y) && Input.mousePosition.x <= (canvasRectTransform.rect.width * border.x))
			{
				SetAnchor(CreatedPrefab, new Vector2(0f, 1f));
			}
			else if (Input.mousePosition.y <= (canvasRectTransform.rect.height / border.y) && Input.mousePosition.x >= (canvasRectTransform.rect.width * border.x))
			{
				SetAnchor(CreatedPrefab, new Vector2(1f, 0f));
			}
			else if (Input.mousePosition.y >= (canvasRectTransform.rect.height / border.y) && Input.mousePosition.x >= (canvasRectTransform.rect.width * border.x))
			{
				SetAnchor(CreatedPrefab, new Vector2(1f, 1f));
			}
			else
			{
				SetAnchor(CreatedPrefab, new Vector2(0f, 0f));
			}

			CreatedPrefab.transform.localPosition = localPoint;
		}


	}

	private void SetAnchor(GameObject obj, Vector2 anchor)
	{
		RectTransform rectTransform = obj.GetComponent<RectTransform>();
		if (rectTransform != null)
		{
			rectTransform.anchorMin = anchor;
			rectTransform.anchorMax = anchor;
			rectTransform.pivot = anchor;
		}
	}
	private void SetTextAndSetActive(TextMeshProUGUI textComponent, int capacity, int maxCapacity, string label, Transform parentTransform)
	{
		if (capacity > 0)
		{
			textComponent.text = label + capacity + " / " + maxCapacity;
			textComponent.gameObject.SetActive(true);

			textComponent.transform.SetParent(parentTransform, false);
			activeTexts++;
		}
		else
		{
			textComponent.gameObject.SetActive(false);
		}
	}


	private IEnumerator StartCoroutineUpdatePostion()
	{
		yield return new WaitForSeconds(0.01f);
		UpdateMousePosition();
		StartCoroutine(StartCoroutineUpdatePostion());
	}

	IEnumerator HoverTimer()
	{
		float timer = 0f;
		var currentObject = ObjectToDisplay; 

		while (timer < hoverTimeThreshold)
		{
			timer += Time.deltaTime;

			if (ObjectToDisplay == null && currentObject != null)
			{
				yield break;
			}

			yield return null;
		}

		if (ObjectToDisplay != null)
		{
			StartCoroutine(StartCoroutineUpdatePostion());
			Displayer();
		}
	}
	public void StartDisplay(SelectableObject objectToDisplay)
	{
		UpdateMousePosition();
		ObjectToDisplay = objectToDisplay;

		StartCoroutine(HoverTimer());
	}
	public void StopDisplay()
	{
		activeTexts = 0;
		ObjectToDisplay = null;
		StopCoroutine(StartCoroutineUpdatePostion());
		Destroy(CreatedPrefab);
	}
}
