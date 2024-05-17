using UnityEngine;
using System.Collections;

public class WaveEffect : MonoBehaviour
{
    public Transform startTransform;
    public float waveDuration = 2.0f;
    public float maxRadius = 5.0f;
    public float waveSpeed = 1.0f;
    public int segments = 100;
    public Color waveColor = Color.white;
    public GameObject waveColliderPrefab;

    LineRenderer _lineRenderer;
    Coroutine _waveCoroutine;
    GameObject _currentWaveCollider;

    void Start()
    {
        _lineRenderer = gameObject.AddComponent<LineRenderer>();
        _lineRenderer.positionCount = 0;
        _lineRenderer.startWidth = 0.1f;
        _lineRenderer.endWidth = 0.1f;
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.startColor = waveColor;
        _lineRenderer.endColor = waveColor;
        _lineRenderer.useWorldSpace = true;
        _lineRenderer.loop = true;
    }

    public void StartWave()
    {
        if (_waveCoroutine != null)
        {
            StopCoroutine(_waveCoroutine);
            _lineRenderer.positionCount = 0;
        }
        _waveCoroutine = StartCoroutine(WaveAnimation());
    }

    IEnumerator WaveAnimation()
    {
        var elapsedTime = 0f;
        var startPosition = startTransform.position;

        _currentWaveCollider = Instantiate(waveColliderPrefab, startPosition, Quaternion.identity);
        var waveCollider = _currentWaveCollider.GetComponent<SphereCollider>();
        waveCollider.radius = 0;

        while (elapsedTime < waveDuration)
        {
            var radius = Mathf.Lerp(0, maxRadius, elapsedTime / waveDuration) * waveSpeed;
            DrawCircle(radius, startPosition);
            waveCollider.radius = radius;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _lineRenderer.positionCount = 0;
        Destroy(_currentWaveCollider);
    }

    void DrawCircle(float radius, Vector3 center)
    {
        var angle = 2 * Mathf.PI / segments;
        _lineRenderer.positionCount = segments + 1;
        for (var i = 0; i <= segments; i++)
        {
            var x = Mathf.Cos(i * angle) * radius + center.x;
            var z = Mathf.Sin(i * angle) * radius + center.z;
            _lineRenderer.SetPosition(i, new Vector3(x, center.y, z));
        }
    }
}
