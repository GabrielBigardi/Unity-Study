using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBD.SaveSystem;

public class PlayerSaveHandler : MonoBehaviour
{
    private void OnEnable()
    {
        SaveSystem.LoadedGameData += OnLoadGame;
    }

    private void OnDisable()
    {
        SaveSystem.LoadedGameData -= OnLoadGame;
    }

    private void Start()
    {
        SaveSystem.LoadGame<SaveData>("TestSave");
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SaveGame("TestSave", new SaveData("teste", GetComponent<PlayerCore>().PlayerHealth.CurrentLifes));
    }

    private void OnLoadGame(string loadedJson)
    {
        var loadedData = JsonUtility.FromJson<SaveData>(loadedJson);
    }
}
