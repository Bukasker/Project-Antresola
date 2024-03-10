using UnityEngine;

[CreateAssetMenu(fileName = "New SelectableObject", menuName = "Selection/StructureObject")]
public class StructureClass : SelectableObject
{
	[Space]
	[Header("Structure  Settings")]
	public string StructureName = "New Structure";
	public StructureType ObjectType;

	public enum StructureType
	{
		House,
		Field,
		Irigation,
		Walls,
		Granary
	}
}
