using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class BuildingManager : MonoBehaviour
{
	public static BuildingManager Instance;

	public GridLayout GridLayout;
	public Tilemap MainTilemap;
	public Tilemap TempTilemap;

	[Header("Building Tiles")]
	public TileBase whiteTile;
	public TileBase greenTile;
	public TileBase redTile;

	public static Dictionary<TileBuildType, TileBase> tileBases = new Dictionary<TileBuildType, TileBase>();

	private Building temp;
	private Vector3 prevPos;
	private BoundsInt prevArea;

	[Header("Building Controlls")]
	public KeyCode MoveKey = KeyCode.Mouse0;
	public KeyCode BuildKey = KeyCode.Space;
	public KeyCode CancelBuild = KeyCode.Escape;


	#region Unity Methods

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

	private void Start()
	{
		tileBases.Add(TileBuildType.Empty, null);
		tileBases.Add(TileBuildType.White, whiteTile);
		tileBases.Add(TileBuildType.Green, greenTile);
		tileBases.Add(TileBuildType.Red, redTile);
	}

	private void Update()
	{
		if (!temp)
		{
			return;
		}

		if (Input.GetKey(MoveKey))
		{
			if (EventSystem.current.IsPointerOverGameObject(0))
			{
				return;
			}

			if (!temp.Placed)
			{
				Vector2 startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Vector3Int cellPos = GridLayout.LocalToCell(startPos);

				if (prevPos != cellPos)
				{
					temp.transform.localPosition = GridLayout.CellToLocalInterpolated(cellPos
						+ new Vector3(0.5f, 0.5f, 0f));
					prevPos = cellPos;

					FollowBuilding();
				}
			}
		}
		else if (Input.GetKeyDown(BuildKey))
		{
			if (temp.CanBePlaced())
			{
				temp.Place();
			}
			else
			{
				Debug.Log("Cannot place here");
			}
		}
		else if (Input.GetKeyDown(CancelBuild))
		{
			ClearArea();
			Destroy(temp.gameObject);
		}

	}

	#endregion

	#region Tilemap Managment

	private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
	{
		TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
		int counter = 0;

		foreach (var v in area.allPositionsWithin)
		{
			Vector3Int pos = new Vector3Int(v.x, v.y, 0);
			array[counter] = tilemap.GetTile(pos);
			counter++;
		}

		return array;
	}

	private static void SetTilesBlock(BoundsInt area, TileBuildType type, Tilemap tilemap)
	{
		int size = area.size.x * area.size.y * area.size.z;
		TileBase[] tileArray = new TileBase[size];
		FillTilles(tileArray, type);
		tilemap.SetTilesBlock(area, tileArray);
	}

	private static void FillTilles(TileBase[] arr, TileBuildType type)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = tileBases[type];
		}
	}

	#endregion

	#region Building Placement

	public void InitializeWithBuilding(GameObject building)
	{
		temp = Instantiate(building, Vector3.zero, Quaternion.identity).GetComponent<Building>();
		FollowBuilding();
	}

	private void ClearArea()
	{
		TileBase[] toClear = new TileBase[prevArea.size.x * prevArea.size.y * prevArea.size.z];
		FillTilles(toClear, TileBuildType.Empty);
		TempTilemap.SetTilesBlock(prevArea, toClear);
	}

	private void FollowBuilding()
	{
		ClearArea();

		temp.area.position = GridLayout.WorldToCell(temp.gameObject.transform.position);
		BoundsInt buildingArea = temp.area;

		TileBase[] baseArray = GetTilesBlock(buildingArea, MainTilemap);

		int size = baseArray.Length;
		TileBase[] tileArray = new TileBase[size];

		for (int i = 0; i < baseArray.Length; i++)
		{
			if (baseArray[i] == tileBases[TileBuildType.White])
			{
				tileArray[i] = tileBases[TileBuildType.Green];
			}
			else
			{
				FillTilles(tileArray, TileBuildType.Red);
				break;
			}
		}
		TempTilemap.SetTilesBlock(buildingArea, tileArray);
		prevArea = buildingArea;
	}

	public bool CanTakeArea(BoundsInt area)
	{
		TileBase[] baseArray = GetTilesBlock(area, MainTilemap);
		foreach (var i in baseArray)
		{
			if (i != tileBases[TileBuildType.White])
			{
				Debug.Log("Cannot place here");
				return false;
			}
		}
		return true;
	}


	public void TakeArea(BoundsInt area)
	{
		SetTilesBlock(area, TileBuildType.Empty, TempTilemap);
		SetTilesBlock(area, TileBuildType.Green, MainTilemap);
	}

	#endregion
}

public enum TileBuildType
{
	Empty,
	White,
	Green,
	Red
}
