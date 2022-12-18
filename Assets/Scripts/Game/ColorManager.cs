using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    [SerializeField] private int _numberOfHoles;
    [SerializeField] private GameObject _ball;
    [SerializeField] private Image _coloringImage;
    [SerializeField] private Sprite[] _images;
    [SerializeField] private Material[] _colors;
    [SerializeField] private GameObject[] _coorectHoles;
    [SerializeField] private GameObject[] _allHoles;
    
    private Renderer _ballRenderer;
    public int counter;

    void Awake()
    {
        _ball = GameObject.Find("Ball");
        _ballRenderer = _ball.GetComponent<Renderer>();
        _ballRenderer.enabled = true;
        _numberOfHoles = _coorectHoles.Length;
        Colorize();
    }

    public void Colorize()
    {
        if (counter < _numberOfHoles+1)
        {
            Debug.Log(counter);
            _coloringImage.sprite = _images[counter];
            _ballRenderer.sharedMaterial = _colors[counter];
            _coorectHoles[counter].GetComponent<Renderer>().enabled = true;
            _coorectHoles[counter].GetComponent<Renderer>().material = _colors[counter];
            for (int i = 0; i < _coorectHoles.Length; i++)
            {
                _coorectHoles[i].transform.GetChild(0).gameObject.GetComponent<Renderer>().material = _colors[i];
                _coorectHoles[i].transform.GetChild(1).gameObject.tag = "WrongHole";
            }
            _coorectHoles[counter].transform.GetChild(1).gameObject.tag = "CorrectHole";
            counter++;
        }
    }
}
