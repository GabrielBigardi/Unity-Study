using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : Singleton<MapManager>
{
    public Transform warningPrefab;
    public Tilemap map;

	private void Start()
	{
        StartCoroutine(RandomTile_CR());
    }

	public Vector2 RandomTilePosition()
	{
        int oddNumberX = Random.Range(-9, 9);
        int oddNumberY = Random.Range(-5, 5);
        if (oddNumberX % 2 == 0) oddNumberX++;
        if (oddNumberY % 2 == 0) oddNumberY++;
        return new Vector3(oddNumberX, oddNumberY);
	}

    IEnumerator RandomTile_CR()
	{
		while (true)
		{
            Transform GO = Instantiate(warningPrefab, RandomTilePosition(), Quaternion.identity);
            Vector2 lastPos = GO.transform.position;

            yield return new WaitForSeconds(0.5f);

            map.SetTile(map.WorldToCell(GO.position), null);
            Destroy(GO.gameObject);

            if ((Vector2)FindObjectOfType<PlayerController>().transform.position == lastPos || FindObjectOfType<PlayerController>().movePos == lastPos)
			{
                Debug.Log("Game Over");
                Time.timeScale = 0f;
			}
        }
	}
}
