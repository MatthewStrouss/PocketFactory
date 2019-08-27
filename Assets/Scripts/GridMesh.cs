using UnityEngine;

//using System.Collections;



public class GridMesh : MonoBehaviour
{

    [SerializeField] private Transform _transform;

    [SerializeField] private Material _material;

    [SerializeField] private Vector2 _gridSize;

    [SerializeField] private int _rows;

    [SerializeField] private int _columns;



    void Start()
    {

        UpdateGrid();

    }



    public void UpdateGrid()
    {

        _transform.localScale = new Vector3(_gridSize.x, _gridSize.y, -9f);

        //_material.SetTextureScale("_MainTex", new Vector2(_columns, _rows));

    }

}