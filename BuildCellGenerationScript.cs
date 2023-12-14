using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCellGenerationScript : MonoBehaviour
{
    [SerializeField] private GameObject cell;
    private Transform _cellGeneratorTransform;
    private Transform _startingPointOfGenerator;
    private int _amountOfCells = 56;
    private const float _distanceBetweenCells = 3.5f;

    private void Awake()
    {
        _cellGeneratorTransform = GetComponent<Transform>();
        _startingPointOfGenerator = _cellGeneratorTransform;
    }

    void Start()
    {
        for (int i = 0; i < _amountOfCells; i++)
        {
            Vector3 startingPosition = transform.position;

            for (int j = 0; j < _amountOfCells;  j++)
            {
                Instantiate(cell, new Vector3(transform.position.x, 0.12f, transform.position.z), Quaternion.identity);
                transform.Translate(_distanceBetweenCells, 0, 0);
            }
            transform.position = startingPosition;
            transform.Translate(0, 0, -_distanceBetweenCells);
            startingPosition = transform.position;
        }
    }
}
