using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArmyDisplay : MonoBehaviour
{

    public static ArmyDisplay Instance { get; private set; }

    [SerializeField] private GameObject armyPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DrawArmy()
    {
        // Draw the army on the map
        for (int x = 0; x < MapManager.Instance.GetWidth(); x++)
        {
            for (int y = 0; y < MapManager.Instance.GetHeight(); y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                Vector3 cellWorldPos = MapManager.Instance.GetWorldPosWithCellPos(cellPosition);
                Cell cell = MapManager.Instance.GetCell(new Vector3Int(x, y, 0));
                if (cell.ArmySize == 0)
                {
                    continue;
                }
                GameObject armyText = Instantiate(armyPrefab, cellWorldPos, Quaternion.identity);
                armyText.GetComponent<TextMeshProUGUI>().text = cell.ArmySize.ToString();
            
                armyText.transform.SetParent(GameObject.Find("ArmyUI").transform);

                // Set the army text position
                Vector3 screenPos = Camera.main.WorldToScreenPoint(cellWorldPos);
                armyText.transform.position = screenPos;
            }
        }
    }

    public void ClearArmy()
    {
        // Clear the army on the map
        foreach (Transform child in GameObject.Find("ArmyUI").transform)
        {
            Destroy(child.gameObject);
        }
    }
}
