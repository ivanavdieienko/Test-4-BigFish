using System.Collections.Generic;
using UnityEngine;

public class PinsManager : MonoBehaviour
{
    [SerializeField]
    private float pinsSpacingX = 0.6f;

    [SerializeField]
    private float pinsSpacingY = 0.5f;

    [SerializeField]
    private int rowCount = 12;

    [SerializeField]
    private GameObject pinPrefab;

    private List<GameObject> rowList = new();

    public void CreatePins()
    {
        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            GameObject row = new GameObject($"Row{rowIndex+1}");
            row.transform.SetParent(transform, false);

            float rowY = -rowIndex * pinsSpacingY;
            row.transform.localPosition = new Vector3(0, rowY, 0);

            int elementsInRow = 3 + rowIndex;
            float rowWidth = (elementsInRow - 1) * pinsSpacingX;
            float startX = -rowWidth / 2;

            for (int elementIndex = 0; elementIndex < elementsInRow; elementIndex++)
            {
                GameObject pin = Instantiate(pinPrefab, row.transform);
                pin.transform.localPosition = new Vector3(startX + elementIndex * pinsSpacingX, 0, 0);
            }

            rowList.Add(row);
        }
    }

    public void RemovePins()
    {
        foreach (var row in rowList)
        {
            DestroyImmediate(row);
        }
        rowList.Clear();
    }
}