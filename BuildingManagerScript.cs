using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManagerScript : MonoBehaviour
{
    [SerializeField] private BuildingClass _buildingList;
    [SerializeField] private CellClass _cell;
    public GameObject Building;
    public int SelectedBuilding;
    public int Width, Height;
    private int _roomIndex = 3;

    void Start()
    {
        BuildingSwitcher(SelectedBuilding);
    }

    public void BuildingSwitcher(int index)
    {
        _cell.StartChangingBuilding = true;
        Building = Instantiate(_buildingList.buildings[index].BuildingPrefab, transform.position, Quaternion.identity);
        Width = _buildingList.buildings[index].width;
        Height = _buildingList.buildings[index].height;
        _cell.SelectedBuilding = index;
        if (index == _roomIndex) 
        {
            Building.tag = "Room";
        }
        _cell.ChangeBuilding();
        _cell.StartChangingBuilding = false;
    }
}
