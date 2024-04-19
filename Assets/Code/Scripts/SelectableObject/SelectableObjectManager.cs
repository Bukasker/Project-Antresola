using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SelectableObjectEvent : UnityEvent<SelectableObject> { }

public class SelectableObjectManager : MonoBehaviour
{
	public static SelectableObjectManager Instance;

	private GameObject selectedGameObject;
	private SelectableObject selectedObject;

	[Header("Select Keys")]
	public KeyCode SelectKey = KeyCode.Mouse0;

	[Header("Tree pivot")]
	public float TreePivoitOffset = 0.8f;

	[Header("Display Events")]
	public SelectableObjectEvent StartDisplay; // Zmiana typu zdarzenia
	public UnityEvent StopDisplay;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

	private void Update()
	{
		if(selectedObject == null || selectedObject.ObjectType == SelectableObjectType.Structure)
		{
			return;
		}
		if (Input.GetKey(SelectKey))
		{
			if (selectedGameObject != null)
			{
				MoveObjectToCursor();
				StopDisplay.Invoke();
			}
		}
	}

	private void MoveObjectToCursor()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z = 0f;
		mousePosition.y -= TreePivoitOffset;
		selectedGameObject.transform.position = mousePosition;
	}

	public void SelectObject(GameObject gameObj)
	{
		selectedGameObject = gameObj;
		var hoverObject = selectedGameObject.GetComponent<HoverObject>();
		selectedObject = hoverObject.ObjectToSelect;
		if (!Input.GetKey(SelectKey))
		{
			StartDisplay?.Invoke(selectedObject);
		}
	}

	public void RemoveSelectedObject()
	{
		selectedGameObject = null;
		selectedObject = null;

		StopDisplay.Invoke();
	}
}
