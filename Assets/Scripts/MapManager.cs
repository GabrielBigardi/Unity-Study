using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : Singleton<MapManager>
{
    public Transform warningPrefab;
    public Tilemap map;

    public List<Vector2> availablePositions = new List<Vector2>();

	private void Start()
	{
        PopulateList();
        StartCoroutine(RandomTile_CR());
    }

    private void PopulateList()
	{
		for (int x = -10; x < 10; x++)
		{
			for (int y = -6; y < 6; y++)
			{
                if(Mathf.Abs(x) % 2 == 1 && Mathf.Abs(y) % 2 == 1)
				{
                    availablePositions.Add(new Vector2(x, y));
                }
            }
		}
	}

    IEnumerator RandomTile_CR()
	{
		while (availablePositions.Count > 0)
		{
            var currentPosition = availablePositions[Random.Range(0, availablePositions.Count)];
            Transform GO = Instantiate(warningPrefab, currentPosition, Quaternion.identity);

            yield return new WaitForSeconds(1.5f);

            Destroy(GO.gameObject);
            map.SetTile(map.WorldToCell(currentPosition), null);

            if ((Vector2)FindObjectOfType<PlayerController>().transform.position == currentPosition || FindObjectOfType<PlayerController>().movePos == currentPosition)
			{
                Debug.Log("Game Over");
                Time.timeScale = 0f;
			}

            availablePositions.Remove(currentPosition);
        }
	}
}
