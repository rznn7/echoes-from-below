using UnityEngine;

public class DisplayGrid : MonoBehaviour
{
    public int gridSize = 10;
    public float cellSize = 1.0f;
    public Color gridColor = Color.white;
    public float lineWidth = 0.025f;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        float halfCellSize = cellSize / 2;
        
        for (int x = -gridSize; x <= gridSize; x++)
        {
            float xPos = x * cellSize - halfCellSize;
            CreateLine(new Vector3(xPos, 0, -gridSize * cellSize - halfCellSize), new Vector3(xPos, 0, gridSize * cellSize - halfCellSize));
        }

        for (int z = -gridSize; z <= gridSize; z++)
        {
            float zPos = z * cellSize - halfCellSize;
            CreateLine(new Vector3(-gridSize * cellSize - halfCellSize, 0, zPos), new Vector3(gridSize * cellSize - halfCellSize, 0, zPos));
        }
    }

    void CreateLine(Vector3 start, Vector3 end)
    {
        GameObject line = new GameObject("GridLine");
        line.transform.parent = transform;
        LineRenderer lr = line.AddComponent<LineRenderer>();
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = gridColor;
        lr.endColor = gridColor;
        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
}