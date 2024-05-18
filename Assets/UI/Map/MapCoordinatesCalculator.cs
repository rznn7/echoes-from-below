using System.Collections;
using TMPro;
using UnityEngine;

public class MapCoordinatesCalculator : MonoBehaviour
{
    [SerializeField]
    PlayerMovement playerMovement;

    [SerializeField]
    string changingString = "";

    TextMeshProUGUI[] _xCoordinateLabels;
    TextMeshProUGUI[] _yCoordinateLabels;

    const int CoordinateCount = 9;
    const float HideDuration = 2f;

    void Start()
    {
        InitializeCoordinateLabels();
        UpdateCoordinates();
    }

    void InitializeCoordinateLabels()
    {
        _xCoordinateLabels = new TextMeshProUGUI[CoordinateCount];
        _yCoordinateLabels = new TextMeshProUGUI[CoordinateCount];

        for (var i = 0; i < CoordinateCount; i++)
        {
            var xLabelName = "x" + i;
            var yLabelName = "y" + i;

            var xLabelObject = GameObject.Find(xLabelName);
            var yLabelObject = GameObject.Find(yLabelName);

            _xCoordinateLabels[i] = xLabelObject.GetComponent<TextMeshProUGUI>();
            _yCoordinateLabels[i] = yLabelObject.GetComponent<TextMeshProUGUI>();
        }
    }

    public void UpdateCoordinates()
    {
        StartCoroutine(UpdateCoordinatesCoroutine());
    }

    IEnumerator UpdateCoordinatesCoroutine()
    {
        SetCoordinateLabelsToChangingString();

        yield return new WaitForSeconds(HideDuration);

        var playerPosition = playerMovement.endpos;

        for (var i = 0; i < CoordinateCount; i++)
        {
            _xCoordinateLabels[i].text = $"{playerPosition.x + (i - 4)}";
            _yCoordinateLabels[i].text = $"{playerPosition.z + (4 - i)}";
        }
    }

    void SetCoordinateLabelsToChangingString()
    {
        for (var i = 0; i < CoordinateCount; i++)
        {
            _xCoordinateLabels[i].text = changingString;
            _yCoordinateLabels[i].text = changingString;
        }
    }
}
