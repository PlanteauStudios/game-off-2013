using UnityEngine;
using System.Collections;
 
public class GridManager: MonoBehaviour
{
    public GameObject Cell;
    //This time instead of specifying the number of cells you should just drop your ground game object on this public variable
    public GameObject Ground;
 
    private float cellWidth;
    private float cellHeight;
    private float groundWidth;
    private float groundHeight;
 
    void setSizes()
    {
        cellWidth = Cell.renderer.bounds.size.x;
        cellHeight = Cell.renderer.bounds.size.z;
        groundWidth = Ground.renderer.bounds.size.x;
        groundHeight = Ground.renderer.bounds.size.z;
    }
 
    //The method used to calculate the number cells in a row and number of rows
    //Vector2.x is gridWidthInHexes and Vector2.y is gridHeightInCells
    Vector2 calcGridSize()
    {
        float sideLength = cellHeight;
        //the number of whole cell sides that fit inside inside ground height
        int nrOfSides = (int)(groundHeight / sideLength);
        //I will not try to explain the following caculation because I made some assumptions, which might not be correct in all cases, to come up with the formula. So you'll have to trust me or figure it out yourselves.
        int gridHeightInCells = nrOfSides;//(int)( nrOfSides * 2 / 3);
        //When the number of cells is even the tip of the last cell in the offset column might stick up.
        //The number of cells in that case is reduced.
        if (gridHeightInCells % 2 == 0
            && (nrOfSides + 0.5f) * sideLength > groundHeight)
            gridHeightInCells--;
        //gridWidth in cells is calculated by simply dividing ground width by cell width
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
        if (gridPos.y % 2 != 0)
            offset = cellWidth / 2;
 
        float x =  initPos.x + offset + gridPos.x * cellWidth;
        float z = initPos.z - gridPos.y * cellHeight * 0.75f;
        //If your ground is not a plane but a cube you might set the y coordinate to sth like groundDepth/2 + hexDepth/2
        return new Vector3(x, 0, z);
    }
 
    void createGrid()
    {
        Vector2 gridSize = calcGridSize();
        GameObject cellGridGO = new GameObject("CellGrid");
 
        for (float y = 0; y < gridSize.y; y++)
        {
            float sizeX = gridSize.x;
            //if the offset row sticks up, reduce the number of cells in a row
            if (y % 2 != 0 && (gridSize.x + 0.5) * cellWidth > groundWidth)
                sizeX--;
            for (float x = 0; x < sizeX; x++)
            {
                GameObject cell = (GameObject)Instantiate(Cell);
                Vector2 gridPos = new Vector2(x, y);
                cell.transform.position = calcWorldCoord(gridPos);
                cell.transform.parent = cellGridGO.transform;
            }
        }
    }
 
    void Start()
    {
        setSizes();
        createGrid();
    }
}