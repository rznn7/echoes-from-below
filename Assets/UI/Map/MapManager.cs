using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    MapCoordinatesCalculator mapCoordinatesCalculator;

    [SerializeField]
    MapVisibility mapVisibility;

    void Start()
    {
        GlobalTimekeeper.inst.dotick.AddListener(OnTick);
        mapVisibility.Hide();
    }

    void OnTick()
    {
        mapCoordinatesCalculator.UpdateCoordinates();
    }
}
