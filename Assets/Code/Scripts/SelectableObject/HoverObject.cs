using System.Collections;
using UnityEngine;

public class HoverObject : MonoBehaviour
{
	public SelectableObject ObjectToSelect;


	private void OnMouseEnter()
	{
		SelectableObjectManager.Instance.SelectObject(this.gameObject);
	}

	private void OnMouseExit()
	{
		SelectableObjectManager.Instance.RemoveSelectedObject();
	}
}
