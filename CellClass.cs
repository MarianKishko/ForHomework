using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellClass : MonoBehaviour
{
    class RayDrawer
    {
        public RaycastHit[] hits;

        public void Draw(Vector3 position, Vector3 direction, float magnitude)
        {
            Ray ray = new Ray(position, direction);

            float magnitudeOfRays = 4 * magnitude;
            Debug.DrawRay(ray.origin, ray.direction * magnitudeOfRays, Color.blue, _fuckingTwo);

            hits = Physics.RaycastAll(ray, magnitudeOfRays);
        }

        public void CheckIsNeighborBusy(GameObject cell)
        {
            if (cell.GetComponent<CellClass>()._isThereSomethingOnIt)
                cell.GetComponent<CellClass>().SettingNeighborBusy();
        }

        public void SetNeighborBusy(int index)
        {
            hits[index].transform.gameObject.GetComponent<CellClass>().SwitchBool();
        }
    }
    
    [SerializeField] private bool _isThereSomethingOnIt = false;
    private static bool _isTheNeighborBusy = false;
    [SerializeField] private BuildingClass _buildingList;
    [SerializeField] private BuildingManagerScript _buildingManager;
    private static GameObject _building; // static makes this variable global for all objects with the script
    public int SelectedBuilding;
    public bool StartChangingBuilding;
    private float _distanceBetweenBuilding = 1.7f;
    private const float _yCoordinateOfSpawnBuilding = 1.14f;
    private const float _fuckingTwo = 2f;
    private const int _roomIndex = 3;

    public void ChangeBuilding()
    {
        if (_building == null)
            _building = _buildingManager.Building;
        else
        {
            Destroy(_building);
            _building = _buildingManager.Building;
        }
    }

    void OnMouseOver()
    {
        if (StartChangingBuilding)
        {
            ChangeBuilding();
            Invoke("OnMouseOver", 1);
            CancelInvoke();
        }

        StartChangingBuilding = false;

        if (_building.tag == _buildingList.buildings[_roomIndex].BuildingPrefab.tag)
        {
            if (Input.GetMouseButton(0))
            {
                _building.transform.position = new Vector3(transform.position.x, _yCoordinateOfSpawnBuilding, transform.position.z);
                _building.GetComponent<RoomScript>().SetStartPoint(transform.position);
                _building.GetComponent<RoomScript>().AllowToBuild(true);
            }
            else
            {
                if (_building.GetComponent<RoomScript>().IsAllowedToBuild)
                {
                    _building.GetComponent<RoomScript>().CreateRoom(/*_building.transform.position.x, _building.transform.position.z, _buildingManager*/ _building.transform);
                    _building.GetComponent<RoomScript>().AllowToBuild(false);
                }
            }
        }
        else
        {
            _building.transform.position = new Vector3(transform.position.x, _yCoordinateOfSpawnBuilding, transform.position.z);
            SetSize(_buildingManager.Width, _buildingManager.Height, _distanceBetweenBuilding);

            if (Input.GetMouseButtonDown(0))
            {
                if (_buildingManager.Width != 0)
                    CheckForBuildingAroundInHorizontalWay();
                else
                    CheckForBuildingAroundInVerticalWay();

                BuildBuilding(_building, _isThereSomethingOnIt);
            }
        }
    }

    void DrawVerticalsRays()
    {
        RayDrawer rays = new RayDrawer();

        rays.Draw(transform.position, transform.forward, _buildingManager.Height);

        for (int i = 0; i < rays.hits.Length; i++)
            rays.SetNeighborBusy(i);
    }

    void DrawHorizontalRays()
    {
        RayDrawer rays = new RayDrawer();

        rays.Draw(transform.position, transform.right, _buildingManager.Width);

        for (int i = 0; i < rays.hits.Length; i++)
        {
            rays.hits[i].transform.gameObject.GetComponent<CellClass>().DrawVerticalsRays();
            rays.SetNeighborBusy(i);
        }
    }

    void CheckForBuildingAroundInVerticalWay()
    {
        RayDrawer rays = new RayDrawer();

        rays.Draw(transform.position, transform.forward, _buildingManager.Height);

        for (int i = 0; i < rays.hits.Length; i++)
            rays.CheckIsNeighborBusy(rays.hits[i].transform.gameObject);
    }

    void CheckForBuildingAroundInHorizontalWay()
    {
        RayDrawer rays = new RayDrawer();

        rays.Draw(transform.position, transform.right, _buildingManager.Width);

        for (int i = 0; i < rays.hits.Length; i++)
        {
            transform.gameObject.GetComponent<CellClass>().CheckForBuildingAroundInVerticalWay();
            rays.hits[i].transform.gameObject.GetComponent<CellClass>().CheckForBuildingAroundInVerticalWay();
            rays.CheckIsNeighborBusy(rays.hits[i].transform.gameObject);
        }
    }

    void SetSize(float width, float height, float distance)
    {
        for (int i = 0; i < width; i++)
            _building.transform.Translate(distance, 0, 0);
        for (int j = 0; j < height; j++)
            _building.transform.Translate(0, 0, distance);
    }

    public void SwitchBool()
    {
        _isThereSomethingOnIt = true;
    }

    void BuildBuilding(GameObject building, bool isThereSomethingOnIt)
    {
        if (isThereSomethingOnIt)
            Debug.Log("You can't build building on the cell!");
        else
        {
            if (!_isTheNeighborBusy)
            {
                if (_buildingManager.Width != 0)
                    DrawHorizontalRays();
                else 
                    DrawVerticalsRays();
                
                Instantiate(building);
                SwitchBool();
            }
        }
        _isTheNeighborBusy = false;
    }

    void SettingNeighborBusy()
    {
        _isTheNeighborBusy = true;
    }
}
