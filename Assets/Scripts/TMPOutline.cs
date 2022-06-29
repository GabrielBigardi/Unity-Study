using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

internal class Outline
{
    public RectTransform RectTransform;
    public Vector2 Offset;
    public TMP_Text TextMeshPro;

    public Outline(Transform baseObject, Vector2 offset)
    {
        this.RectTransform = GameObject.Instantiate(baseObject, Vector3.zero, Quaternion.identity) as RectTransform;
        this.Offset = offset;
        this.TextMeshPro = RectTransform.GetComponent<TMP_Text>();
        this.RectTransform.anchorMin = Vector2.one / 2f;
        this.RectTransform.anchorMax = Vector2.one / 2f;
        this.RectTransform.anchoredPosition = offset;
        this.RectTransform.name = GetOutlineGameObjectName(offset);
    }

    private string GetOutlineGameObjectName(Vector2 position)
    {
        if (position == new Vector2(-1f, 1f))
            return "Top Left";
        if (position == new Vector2(0f, 1f))
            return "Top Center";
        if (position == new Vector2(1f, 1f))
            return "Top Right";
        if (position == new Vector2(-1f, 0f))
            return "Middle Left";
        if (position == new Vector2(0f, 0f))
            return "Middle Center";
        if (position == new Vector2(1f, 0f))
            return "Middle Right";
        if (position == new Vector2(-1f, -1f))
            return "Bottom Left";
        if (position == new Vector2(0f, -1f))
            return "Bottom Center";
        if (position == new Vector2(1f, -1f))
            return "Bottom Right";

        return "";
    }
}

public class TMPOutline : MonoBehaviour
{
    public Color OutlineColor = Color.white;
    public Color MainTextColor = Color.black;

    public List<TMP_Text> _outlines = new();
    public TMP_Text _mainText;

    [ContextMenu("Remove Outline")]
    void RemoveOutline()
    {
        ClearChildrens();

        GetComponent<TMP_Text>().color = Color.white;
    }

    [ContextMenu("Outline - No Corners")]
    private void SetupOutlineNoCorners()
    {
        List<Vector2> outlineCorners = new List<Vector2>()
        {
            new Vector2(0f, 1f), // top center
            new Vector2(-1f, 0f), // middle left
            new Vector2(0f, 0f), // middle center
            new Vector2(1f, 0f), // middle right
            new Vector2(0f, -1), // bottom center
        };

        SetupOutline(outlineCorners);
    }

    [ContextMenu("Outline - Corners")]
    private void SetupOutlineCorners()
    {
        List<Vector2> outlineCorners = new List<Vector2>()
        {
            new Vector2(-1f, 1f), // top left
            new Vector2(0f, 1f), // top center
            new Vector2(1f, 1f), // top right
            new Vector2(-1f, 0f), // middle left
            new Vector2(0f, 0f), // middle center
            new Vector2(1f, 0f), // middle right
            new Vector2(-1f, -1f), // bottom left
            new Vector2(0f, -1f), // bottom center
            new Vector2(1f, -1f) // bottomright
        };

        SetupOutline(outlineCorners);
    }

    private void SetupOutline(List<Vector2> corners)
    {
        Debug.Log($"Setting up Outline with {corners.Count}");

        _mainText = null;
        _outlines.Clear();

        ClearChildrens();

        var objectToDuplicate = transform;

        RectTransform objectToDuplicatePrefab = Instantiate(objectToDuplicate, transform.position, Quaternion.identity) as RectTransform;
        objectToDuplicatePrefab.GetComponent<TMP_Text>().color = Color.black;
        objectToDuplicate.GetComponent<TMP_Text>().color = Color.clear;
        DestroyImmediate(objectToDuplicatePrefab.GetComponent<TMPOutline>());

        foreach (var outlineCorner in corners)
        {
            Outline outlineCornerTransform = new(objectToDuplicatePrefab, outlineCorner);
            outlineCornerTransform.RectTransform.SetParent(this.transform, false);

            if (outlineCornerTransform.RectTransform.anchoredPosition == Vector2.zero)
            {
                outlineCornerTransform.TextMeshPro.color = Color.white;
                _mainText = outlineCornerTransform.TextMeshPro;
            }
            else
            {
                _outlines.Add(outlineCornerTransform.TextMeshPro);
            }
        }

        _mainText.transform.SetAsLastSibling();

        DestroyImmediate(objectToDuplicatePrefab.gameObject);
    }

    private void ClearChildrens()
    {
        var children = new List<GameObject>();
        foreach (Transform child in transform) children.Add(child.gameObject);
        children.ForEach(child => DestroyImmediate(child));
    }

    private void OnValidate()
    {
        RefreshColors();
    }

    private void RefreshColors()
    {
        foreach (var outline in _outlines)
        {
            outline.color = OutlineColor;
        }

        _mainText.color = MainTextColor;
    }
}
