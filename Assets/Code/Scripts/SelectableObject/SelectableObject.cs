using UnityEngine;

[CreateAssetMenu(fileName = "New SelectableObject", menuName = "Selection/SelectableObject")]
public class SelectableObject : ScriptableObject
{
	[Header("Selectable Object Settings")]
	public string ObjectName = "New Object";
	public SelectableObjectType ObjectType;
	public int WoodCapacity;
	public int MaxWoodCapacity;
	public int StoneCapacity;
	public int MaxStoneCapacity;
	public int FoodCapacity;
	public int MaxFoodCapacity;
	public int SettlerCapacity;
	public int MaxSettlerCapacity;
	public GameObject ObjectPrefab;

	//[TextArea]
	//public string Description = "Description placeholder";
}

public enum SelectableObjectType
{
	Tree,
	CutedTree,
	Stone,
	MinedStone,
	Settler,
	Food,
	Structure
}