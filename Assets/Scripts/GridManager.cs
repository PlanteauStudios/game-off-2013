using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class GridManager: MonoBehaviour
{
    public GameObject Cell;
    //This time instead of specifying the number of cells you should just drop your ground game object on this public variable
    public GameObject Ground;
 
    private float cellWidth;
    private float cellHeight;
    private float groundWidth;
    private float groundHeight;
    public List<List<GameObject> > _cells = new List<List<GameObject> >();
    void setSizes()
    {
        cellWidth = Cell.renderer.bounds.size.x;
        cellHeight = Cell.renderer.bounds.size.z;
        groundWidth = Ground.renderer.bounds.size.x;
        groundHeight = Ground.renderer.bounds.size.z;
    }
 
    //The method used to calculate the number cells in a row and number of rows
    //Vector2.x is gridWidthInCells and Vector2.y is gridHeightInCells
    Vector2 calcGridSize()
    {
        float sideLength = cellHeight;
        //the number of whole cell sides that fit inside inside ground height
        int nrOfSides = (int)(groundHeight / sideLength);
        int gridHeightInCells = nrOfSides;
        return new Vector2((int)(groundWidth / cellWidth), gridHeightInCells);
    }
    //Method to calculate the position of the first tile
    //The center of the cell grid is (0,0,0)
    Vector3 calcInitPos()
    {
        Vector3 initPos;
        initPos = new Vector3(-groundWidth / 2 + cellWidth / 2, 0,
            groundHeight / 2 - cellWidth / 2);
 
        return initPos;
    }
 
    Vector3 calcWorldCoord(Vector2 gridPos)
    {
        Vector3 initPos = calcInitPos();
        float offset = 0;
 
        float x =  initPos.x + offset + gridPos.x * cellWidth;
        float z = initPos.z - gridPos.y * cellHeight;
        //If your ground is not a plane but a cube you might set the y coordinate to sth like groundDepth/2 + hexDepth/2
        return new Vector3(x, 0, z);
    }
 
    void createGrid()
    {
        Vector2 gridSize = calcGridSize();
        GameObject cellGridGO = new GameObject("CellGrid");
 
        for (float y = 0; y < gridSize.y; y++)
        {
            List<GameObject> current_list = new List<GameObject>();
            float sizeX = gridSize.x;
            for (float x = 0; x < sizeX; x++)
            {
                GameObject cell = (GameObject)Instantiate(Cell);
                Vector2 gridPos = new Vector2(x, y);
                cell.transform.position = calcWorldCoord(gridPos);
                cell.transform.parent = cellGridGO.transform;
                current_list.Add(cell);
            }
            _cells.Add(current_list);
        }
    }
 
    void Start()
    {
        setSizes();
        createGrid();
    }
}