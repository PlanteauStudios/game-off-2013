using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridManager: MonoBehaviour
{
    public GameObject Cell;
    public GameObject Ground;

    private float cellWidth;
    private float cellHeight;
    private float groundWidth;
    private float groundHeight;
    public CellGrid _cells = new CellGrid();

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
        Debug.Log("GridManager createGrid. GridSize X : " + gridSize.x + " GridSize Y: " + gridSize.y);
        for (float y = 0; y < gridSize.y; y++)
        {
            List<CollabCell > current_list = new List<CollabCell >();
            for (float x = 0; x < gridSize.x; x++)
            {
                GameObject cell = (GameObject)Instantiate(Cell);
                Vector2 gridPos = new Vector2(x, y);
                cell.transform.position = calcWorldCoord(gridPos);
                cell.transform.parent = cellGridGO.transform;
                CellPassthrough cell_pass = cell.GetComponent<CellPassthrough>();
                if (!cell_pass.CanPass()) Debug.Log("FOOO");
                int score = cell_pass.CanPass() ? CollabCell.BASE_SCORE : CollabCell.IMPASS_SCORE;
                // Debug.Log("Gridmanager creategrid current score for x, y {" + x + ","  + y + "} is "+ score);
                CollabCell store_cell = new CollabCell(cell, score, cell_pass.CanPass());
                current_list.Add(store_cell);
            }
            _cells.Add(current_list);
        }
    }

    public void Build()
    {
        setSizes();
        createGrid();

    }

}