using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding : MonoBehaviour
{
    private WorldCellData[,] worldCells;

    public void SetCells(WorldCellData[,] worldCells)
    {
        this.worldCells = worldCells;
    }

    public List<Vector2> FindPath(Vector2 start, Vector2 target)
    {
        List<Vector2> path = new List<Vector2>();

        Vector2Int startInt = new Vector2Int((int)start.x, (int)start.y);
        Vector2Int targetInt = new Vector2Int((int)target.x, (int)target.y);

        Dictionary<Vector2, Vector2> backtrack = new Dictionary<Vector2, Vector2>();

        WorldCellData startCell = worldCells[startInt.x, startInt.y];
        WorldCellData targetCell = worldCells[targetInt.x, targetInt.y];

        if (startCell.CellType == WorldCellType.Wall || targetCell.CellType == WorldCellType.Wall)
        {
            //Debug.LogError("Start or target cell is a wall. Path cannot be found.");
            return path;
        }

        List<Vector2> openList = new List<Vector2>();
        List<Vector2> closedList = new List<Vector2>();

        openList.Add(start);

        while (openList.Count > 0)
        {
            Vector2 currentCell = openList[0];
            openList.RemoveAt(0);
            closedList.Add(currentCell);

            Vector2Int currentInt = new Vector2Int((int)currentCell.x, (int)currentCell.y);

            if (currentInt == targetInt)
            {
                // Путь найден, восстановление пути
                Vector2 backtrackCell = currentCell;
                while (backtrackCell != start)
                {
                    path.Insert(0, backtrackCell);
                    backtrackCell = backtrack[backtrackCell];
                }
                path.Insert(0, start);
                break;
            }

            List<Vector2> neighbors = GetNeighbors(currentInt);
            foreach (Vector2 neighbor in neighbors)
            {
                Vector2Int neighborInt = new Vector2Int((int)neighbor.x, (int)neighbor.y);

                if (closedList.Contains(neighbor) || worldCells[neighborInt.x, neighborInt.y].CellType == WorldCellType.Wall)
                {
                    continue;
                }

                int newCost = startCell.Cost + 1; // Простая стоимость движения к соседней ячейке
                if (newCost < worldCells[neighborInt.x, neighborInt.y].Cost || !openList.Contains(neighbor))
                {
                    worldCells[neighborInt.x, neighborInt.y].Cost = newCost;
                    backtrack[neighbor] = currentCell;

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }

        return path;
    }

    private List<Vector2> GetNeighbors(Vector2Int cell)
    {
        List<Vector2> neighbors = new List<Vector2>();

        // Проверка ячейки выше
        Vector2Int neighborUp = new Vector2Int(cell.x, cell.y + 1);
        if (IsCellValid(neighborUp))
        {
            neighbors.Add(new Vector2(neighborUp.x, neighborUp.y));
        }

        // Проверка ячейки ниже
        Vector2Int neighborDown = new Vector2Int(cell.x, cell.y - 1);
        if (IsCellValid(neighborDown))
        {
            neighbors.Add(new Vector2(neighborDown.x, neighborDown.y));
        }

        // Проверка ячейки слева
        Vector2Int neighborLeft = new Vector2Int(cell.x - 1, cell.y);
        if (IsCellValid(neighborLeft))
        {
            neighbors.Add(new Vector2(neighborLeft.x, neighborLeft.y));
        }

        // Проверка ячейки справа
        Vector2Int neighborRight = new Vector2Int(cell.x + 1, cell.y);
        if (IsCellValid(neighborRight))
        {
            neighbors.Add(new Vector2(neighborRight.x, neighborRight.y));
        }

        return neighbors;
    }

    private bool IsCellValid(Vector2Int cell)
    {
        // Дополнительная логика проверки ячейки, например, в пределах игрового поля и не является стеной
        if (cell.x >= 0 && cell.x < worldCells.GetLength(0) &&
            cell.y >= 0 && cell.y < worldCells.GetLength(1) &&
            worldCells[cell.x, cell.y].CellType != WorldCellType.Wall)
        {
            return true;
        }

        return false;
    }
}
