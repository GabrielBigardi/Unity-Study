using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MapManager : Singleton<MapManager>
{
    public Transform warningPrefab;
    public Transform explosionParticlesPrefab;

    public Transform gameOverPanel;
    public TMPro.TMP_Text gameOverText;

    public Tilemap map;

    public List<Vector2> availablePositions = new List<Vector2>();
    public List<Transform> currentWarnings = new List<Transform>();

	private void Start()
	{
        PopulateList();
        StartCoroutine(RandomTile_CR());
    }

	private void Update()
	{
		if (Input.anyKeyDown && gameOverPanel.gameObject.activeSelf)
		{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
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
            currentWarnings.Add(GO);

            yield return new WaitForSeconds(1.5f);

            map.SetTile(map.WorldToCell(currentPosition), null);
            Instantiate(explosionParticlesPrefab, currentPosition, Quaternion.identity);
            CameraShakeManager.Instance.Shake(0.5f, 0.4f);
            SFXManager.Instance.PlaySFX(0);
            availablePositions.Remove(currentPosition);

            if ((Vector2)PlayerController.Instance.transform.position == currentPosition || PlayerController.Instance.movePos == currentPosition)
			{
                Debug.Log("Game Over");
                Time.timeScale = 1f;
                StartCoroutine(PlayerGameOver_CR());
                yield break;
			}

            currentWarnings.Remove(GO);
            Destroy(GO.gameObject);
            ScoreManager.Instance.AddScore();
            if(Time.timeScale < 2.5f) Time.timeScale += 0.1f;
        }
	}

    IEnumerator PlayerGameOver_CR()
	{
        PlayerController.Instance.enabled = false;

        //Clear warnings instantly
        foreach (var currentWarning in currentWarnings)
        {
            Destroy(currentWarning.gameObject);
        }

        //Fancy animations on player death until it disappears
        Sequence s = DOTween.Sequence();
        s.Append(PlayerController.Instance.transform.DOScale(0f, 1f));
        s.Join(PlayerController.Instance.transform.DORotate(new Vector3(0f, 0f, -180f), 0.75f).SetLoops(-1));
        s.OnComplete(() => StartCoroutine(MapGameOver_CR()));
        s.Play();
        yield return null;
    }

    IEnumerator MapGameOver_CR()
	{
		//Destroy all map tiles in sequence
		foreach (var availablePosition in availablePositions)
		{
            map.SetTile(map.WorldToCell(availablePosition), null);
            Instantiate(explosionParticlesPrefab, availablePosition, Quaternion.identity);
            SFXManager.Instance.PlaySFX(0);
            yield return new WaitForSeconds(2f / availablePositions.Count);
        }

        gameOverPanel.gameObject.SetActive(true);
        gameOverText.SetText($"You scored {ScoreManager.Instance.CurrentScore}, press any key to retry");
        yield break;
	}
}
