using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Camera _cam;

    public Grid grid; 
    public Tilemap myTileMap; 

    public Tile tilePrefab1;
    public Tile tilePrefab2;

    public int width = 3;
    public int height = 3;

    void Start()
    {
        GenerateGrid();
    }
    
    void Update()
    {      

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            Vector3Int coordinate = grid.WorldToCell(mouseWorldPos);

            if (coordinate.x < width && coordinate.x > -1 && coordinate.y < height && coordinate.y > -1)
            {
                myTileMap.SetTile(coordinate, tilePrefab2);
                ControlMatch(coordinate.x, coordinate.y);
            }                        
        }
    }

    private void GenerateGrid()
    {
       Vector3Int startCoor;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                startCoor = new Vector3Int(i, j, 0);
                
                myTileMap.SetTile(startCoor, tilePrefab1);              
            }
        }      

        _cam.transform.position = new Vector3((float)width * grid.cellSize.x / 2, (float)height * grid.cellSize.y / 2, -10); // Main camera is centered
        _cam.orthographic = true;
        _cam.orthographicSize = width;
    }

    private void ControlMatch(int tileWidth, int tileHeight)
    {

        int result = 0;

        result += ControlLeft(tileWidth, tileHeight);
        if (result < 2)
        {
            //Debug.Log("Girdi");

            result += ControlRight(tileWidth, tileHeight);

            if (result < 2)
            {
                result += ControlUp(tileWidth, tileHeight);

                if (result < 2)
                {
                    result += ControlDown(tileWidth, tileHeight);
                }
            }           
        }     

        if (result > 1)
        {
            ConvertAll(tileWidth, tileHeight);
        }
    }

    private void ConvertAll(int tileWidth, int tileHeight)
    {
            ConvertFirst(tileWidth, tileHeight);
            ConvertLeft(tileWidth, tileHeight);
            ConvertDown(tileWidth, tileHeight);
            ConvertUp(tileWidth, tileHeight);
            ConvertRight(tileWidth, tileHeight);
    }

    private int ControlRight(int tileWidth, int tileHeight)
    {
        
        int result = 0;

        for (int i = tileWidth + 1; i < width; i++)
        {
            if (myTileMap.GetTile(new Vector3Int(i, tileHeight, 0)).name.ToString().Equals("X Tile"))
            {
                result++;
            }
            
            else break;
        }
         
        return result;
    }

    private int ControlLeft(int tileWidth, int tileHeight)
    {
        int result = 0;
        for (int i = tileWidth - 1 ; i >= 0; i--)
        {
            if (myTileMap.GetTile(new Vector3Int(i, tileHeight, 0)).name.ToString().Equals("X Tile"))
            {
                result++;             
            }
            else break;
        }

        return result;
    }

    private int ControlUp(int tileWidth, int tileHeight)
    {

        int result = 0;
        for (int i = tileHeight + 1; i < height; i++)
        {
            if (myTileMap.GetTile(new Vector3Int(tileWidth, i, 0)).name.ToString().Equals("X Tile"))
            {
                result++;
      
            }
            else break;
        }

        return result;
    }

    private int ControlDown(int tileWidth, int tileHeight)
    {

        int result = 0;
        for (int i = tileHeight - 1; i > 0; i--)
        {            
            if (myTileMap.GetTile(new Vector3Int(tileWidth, i, 0)).name.ToString().Equals("X Tile"))
            {
                result++;
            }
            else break;
        }

        return result;
    }

    private void ConvertFirst(int tileWidth, int tileHeight)
    {
        myTileMap.SetTile(new Vector3Int(tileWidth, tileHeight, 0), tilePrefab1);
    }

    private void ConvertRight( int tileWidth, int tileHeight)
    {
        for (int i = tileWidth + 1; i < width; i++)
        {
            if (myTileMap.GetTile(new Vector3Int(i, tileHeight, 0)).name.ToString().Equals("X Tile"))
            {
                myTileMap.SetTile(new Vector3Int(i, tileHeight, 0), tilePrefab1);
            }
            else break;
        }
    }
    private void ConvertLeft(int tileWidth, int tileHeight)
    {
        for (int i = tileWidth - 1; i >= 0; i--)
        {

            if (myTileMap.GetTile(new Vector3Int(i, tileHeight, 0)).name.ToString().Equals("X Tile"))
            {
                myTileMap.SetTile(new Vector3Int(i, tileHeight, 0), tilePrefab1);
            }
            else break;
        }
    }

    private void ConvertUp(int tileWidth, int tileHeight)
    {
        for (int i = tileHeight + 1; i < height; i++)
        {
            if (myTileMap.GetTile(new Vector3Int(tileWidth, i, 0)).name.ToString().Equals("X Tile"))
            {
                myTileMap.SetTile(new Vector3Int(tileWidth, i, 0), tilePrefab1);
            }
            else break;
        }
    }

    private void ConvertDown(int tileWidth, int tileHeight)
    {
        for (int i = tileHeight - 1; i > 0; i--)
        {
            if (myTileMap.GetTile(new Vector3Int(tileWidth, i, 0)).name.ToString().Equals("X Tile"))
            {
                myTileMap.SetTile(new Vector3Int(tileWidth, i, 0), tilePrefab1);
            }
            else break;
        }
    }
}
