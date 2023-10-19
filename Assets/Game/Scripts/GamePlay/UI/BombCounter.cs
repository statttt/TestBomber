using Game.Scripts.GamePlay.Setup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using VContainer;
using UnityEngine.WSA;

public class BombCounter : MonoBehaviour
{
    [SerializeField]
    private BombType _type;
    [SerializeField]
    private TMP_Text _text;
    [SerializeField]
    private Image _background;
    [SerializeField]
    private GameObject _outline;

    private int _count;

    public BombType Type => _type;
    public int Count => _count;

    public void Initialize(BombInfo bombInfo)
    {
        _type = bombInfo.Type;
        _background.color = bombInfo.Color;
        UpdateText();
    }

    public void AddBomb()
    {
        _count++;
        UpdateText();
    }

    public void RemoveBomb()
    {
        _count--;
        UpdateText();
    }

    public void UpdateText()
    {
        _text.text = _count.ToString();
    }

    public void Activate()
    {
        _outline.SetActive(true);
    }

    public void Deactivate()
    {
        _outline.SetActive(false);
    }
}
