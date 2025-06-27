using System;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    [SerializeField] bool isSelected = false;
    Players_Controller _playerControl;
    public static event Action<Selectable> OnSelectableClicked;



    void Awake()
    {
        _playerControl = GetComponent<Players_Controller>();
        UpdateControlState();
    }

    void OnMouseDown()
    {
        Select();
        OnSelectableClicked?.Invoke(this); // notifica il CameraController
    }


    public void Select()
    {
        if (isSelected) return;

        Selectable[] allSelectables = FindObjectsByType<Selectable>(FindObjectsSortMode.None);

        foreach (Selectable sel in allSelectables)
        {
            if (sel != this)
            {
                sel.Deselect();
            }
        }

        isSelected = true;
        UpdateControlState();
    }
    public void Deselect()
    {
        isSelected = false;
        UpdateControlState();
    }

    void UpdateControlState()
    {
        if (_playerControl != null)
            _playerControl.enabled = isSelected;

        GetComponent<SpriteRenderer>().color = isSelected ? Color.white : new Color(0.8f, 0.8f, 0.8f);
    }

    public bool IsSelected() => isSelected;
}
