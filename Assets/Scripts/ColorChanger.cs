using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField]
    private Light _light;
    [SerializeField]
    private float _fluxDuration = 5f;
    private float _fluxTime = 0;
    private Color _nextColor;

    // Start is called before the first frame update
    void Start()
    {
        if (_light == null)
            _light = this.gameObject.GetComponent<Light>();

        _nextColor = Random.ColorHSV();
    }

    private void Update()
    {
        _light.color = Color.Lerp(_light.color, _nextColor, _fluxTime);
        if (_fluxTime > 1 || (_light.color.r == _nextColor.r && _light.color.g == _nextColor.g && _light.color.b == _nextColor.b))
        {
            _nextColor = Random.ColorHSV();
            _fluxTime = 0;
        }
        else
        {
            _fluxTime += Time.deltaTime / _fluxDuration;
        }
    }
}
