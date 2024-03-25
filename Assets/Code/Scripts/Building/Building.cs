using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
	public bool Placed { get; private set; }
	public BoundsInt area;

	#region BuildMethods
	
	public bool CanBePlaced()
	{
		Vector3Int positionInt = BuildingManager.Instance.GridLayout.LocalToCell(transform.position);
		BoundsInt areaTemp = area;
		areaTemp.position = positionInt;

		if (BuildingManager.Instance.CanTakeArea(areaTemp))
		{ 
			return true;
		}
		return false;
	}

	public void Place()
	{
		Vector3Int positionInt = BuildingManager.Instance.GridLayout.LocalToCell(transform.position);
		BoundsInt areaTemp = area;
		areaTemp.position = positionInt;
		Placed = true;
		BuildingManager.Instance.TakeArea(areaTemp);
	}
	#endregion
}