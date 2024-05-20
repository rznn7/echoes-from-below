using UnityEngine;

public class DisplayGrid : MonoBehaviour
{
    public int gridSize = 10;
    public float cellSize = 1.0f;
    public Color gridColor = Color.white;
    public float lineWidth = 0.025f;
    public PlayerMovement pm;
    public GameObject gridParent;
    public GameObject gridl;
    void Start()
    {
        GenerateGrid();
        pm.OnPlayerMove += zeroGrid;
    }

    void zeroGrid() {
        gridParent.transform.position = pm.endpos;
    }
    void GenerateGrid()
    {
        var halfCellSize = cellSize / 2;

        for (var x = -gridSize; x <= gridSize; x++)
        {
            var xPosition = x * cellSize - halfCellSize;
            CreateLine(new Vector3(xPosition, 0, -gridSize * cellSize - halfCellSize),
                new Vector3(xPosition, 0, gridSize * cellSize - halfCellSize));
        }

        for (var z = -gridSize; z <= gridSize; z++)
        {
            var zPosition = z * cellSize - halfCellSize;
            CreateLine(new Vector3(-gridSize * cellSize - halfCellSize, 0, zPosition),
                new Vector3(gridSize * cellSize - halfCellSize, 0, zPosition));
        }
    }

    void CreateLine(Vector3 start, Vector3 end)
    {
        var line = Instantiate(gridl);
        line.transform.parent = gridParent.transform;

        var lr = line.GetComponent<LineRenderer>();
        lr.useWorldSpace = false;
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = gridColor;
        lr.endColor = gridColor;
        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.sortingLayerName = "BG";
    }
}
