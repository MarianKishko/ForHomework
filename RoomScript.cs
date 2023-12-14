using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    private bool _isAllowedToBuild = false;
    public bool IsAllowedToBuild => _isAllowedToBuild;
    private Vector3 _startingPosition;
    private Vector3 _currentPosition;
    private float _distanceBetweenCells = 3.5f;
    private Transform _cellTransform;
    private float _previousXPosition;

    public void SetStartPoint(Vector3 position)
    {
        _startingPosition = position;
    }

    public void Calculate(float currentWidth, float currentHeight, BuildingManagerScript buildingManager)
    {
        float width = currentWidth;
        float height = currentHeight;

        for (int i = 0; width <= _startingPosition.x; i++)
        {
            currentWidth -= _distanceBetweenCells;
            buildingManager.Width = i;
        }
        for (int i = 0; height <= _startingPosition.z; i++)
        {
            currentHeight -= _distanceBetweenCells;
            buildingManager.Height = i;
        }

        Debug.Log($"Width - {buildingManager.Width}");
        Debug.Log($"Height - {buildingManager.Height}");
    }

    public void CreateRoom(Transform cell)
    {
        _cellTransform = cell;
        Update();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance = Vector3.Distance(Camera.main.transform.position, _cellTransform.transform.position);

        Debug.DrawRay(ray.origin, ray.direction * 450, Color.yellow);

        _previousXPosition = Input.mousePosition.x;

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.name == _cellTransform.transform.gameObject.name) 
            { 
                if (_previousXPosition < Input.mousePosition.x)
                {
                    Debug.Log("Ich bin hier!");
                }
            }
        }
    }

    public void AllowToBuild(bool makeLegal)
    {
        _isAllowedToBuild = makeLegal;
    }
}
