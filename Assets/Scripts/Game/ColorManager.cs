using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    [SerializeField] private int _numberOfHoles;
    [SerializeField] private int _numberOfShots;
    [SerializeField] private TextMeshProUGUI _numberOfShotsText;
    [SerializeField] private GameObject _ball;
    [SerializeField] private Image _coloringImage;
    [SerializeField] private Sprite[] _images;
    [SerializeField] private Material[] _colors;
    [SerializeField] private GameObject[] _coorectHoles;
    [SerializeField] private Animator _niceTextAnim;
    [SerializeField] private Animator _wrongTextAnim;
    [SerializeField] private Animator _goTextAnim;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;
    
    private Renderer _ballRenderer;
    public int counter;

    void Awake()
    {
        counter = -1;
        _ball = GameObject.Find("Ball");
        _ballRenderer = _ball.GetComponent<Renderer>();
        _ballRenderer.enabled = true;
        _numberOfHoles = _coorectHoles.Length;
        Colorize();
    }

    public void Colorize()
    {
        counter++;
        _coloringImage.sprite = _images[counter];
        if (counter < _numberOfHoles)
        {
            Debug.Log(counter);
            StartCoroutine(AnimationsNumerator());
            _ballRenderer.sharedMaterial = _colors[counter];
            _coorectHoles[counter].GetComponent<Renderer>().enabled = true;
            _coorectHoles[counter].GetComponent<Renderer>().material = _colors[counter];
            for (int i = 0; i < _coorectHoles.Length; i++)
            {
                _coorectHoles[i].transform.GetChild(0).gameObject.GetComponent<Renderer>().material = _colors[i];
                _coorectHoles[i].transform.GetChild(1).gameObject.tag = "WrongHole";
            }
            _coorectHoles[counter].transform.GetChild(1).gameObject.tag = "CorrectHole";
        }
        if (counter >= _numberOfHoles)
        {
            Debug.Log("kazandin");
            _winPanel.SetActive(true);
        }
    }

    IEnumerator AnimationsNumerator()
    {
        if (counter == 0)
        {
            _goTextAnim.SetBool("go",true);
        }

        if (counter > 0)
        {
            _niceTextAnim.SetBool("gol",true);
        }

        yield return new WaitForSeconds(1f);
        _goTextAnim.SetBool("go",false);
        _niceTextAnim.SetBool("gol",false);
    }

    public void WrongHole(){StartCoroutine(WrongHoleNumerator());}

    IEnumerator WrongHoleNumerator()
    {
        _wrongTextAnim.SetBool("wrong",true);
        yield return new WaitForSeconds(1);
        _wrongTextAnim.SetBool("wrong",false);
    }

    public void Shooted()
    {
        _numberOfShots--;
        _numberOfShotsText.text = "SHOTS: " + _numberOfShots.ToString();
        if (_numberOfShots <= 0)
        {
            _losePanel.SetActive(true);
        }
    }
}
