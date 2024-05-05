using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    private int WoodCurrent;
    private int StoneCurrent;
    private int FoodCurrent;
    private int SettlersCurrent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddResource(SelectableObjectType ObjectType, int amount)
    {
        switch (ObjectType)
        {
            case SelectableObjectType.Tree:
                WoodCurrent += amount;
                break;
            case SelectableObjectType.CutTree:
                WoodCurrent += amount;
                break;
            case SelectableObjectType.Stone:
                StoneCurrent += amount;
                break;
            case SelectableObjectType.MinedStone:
                StoneCurrent += amount;
                break;
            case SelectableObjectType.Settler:
                SettlersCurrent += amount;
                break;
            case SelectableObjectType.Food:
                FoodCurrent += amount;
                break;
            default:
                Debug.LogWarning($"Unknown resource type: {ObjectType}");
                break;
        }
    }

    public bool TryRemoveResource(SelectableObjectType objectType, int amount)
    {
        switch (objectType)
        {
            case SelectableObjectType.Tree:
                if (WoodCurrent >= amount)
                {
                    WoodCurrent -= amount;
                    return true;
                }
                break;
            case SelectableObjectType.CutTree:
                if (WoodCurrent >= amount)
                {
                    WoodCurrent -= amount;
                    return true;
                }
                break;
            case SelectableObjectType.Stone:
                if (StoneCurrent >= amount)
                {
                    StoneCurrent -= amount;
                    return true;
                }
                break;
            case SelectableObjectType.MinedStone:
                if (StoneCurrent >= amount)
                {
                    StoneCurrent -= amount;
                    return true;
                }
                break;
            case SelectableObjectType.Food:
                if (FoodCurrent >= amount)
                {
                    FoodCurrent -= amount;
                    return true;
                }
                break;
            case SelectableObjectType.Settler:
                if (SettlersCurrent >= amount)
                {
                    SettlersCurrent -= amount;
                    return true;
                }
                break;
            default:
                Debug.LogWarning($"Unknown resource type: {objectType}");
                break;
        }
        return false;
    }
}
