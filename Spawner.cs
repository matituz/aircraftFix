using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
using TMPro;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private IntVariable score;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float startTimeToSpawn = 3;
    private int generateNumberOfPlane;
    private float timeToSpawn = 3f;
    [SerializeField] private List<GameObject> avaiblePlanes = new List<GameObject>();

    private void Start ()
	{
        Invoke("Finished", timeToSpawn);
        score.Value = 0;
	}
    public void Finished()
    {
        SpawnPlane();
        timeToSpawn = startTimeToSpawn * 0.99f;
        Invoke("Finished", timeToSpawn);
    }
        private void SpawnPlane()
    {
        if (avaiblePlanes[0] != null)
        {
            generateNumberOfPlane = (int)Random.Range(0, avaiblePlanes.Count);
            avaiblePlanes[generateNumberOfPlane].SetActive(true);
            avaiblePlanes[generateNumberOfPlane].transform.position = spawnPoints[(int)Random.Range(0, spawnPoints.Length)].transform.position;
            avaiblePlanes[generateNumberOfPlane].GetComponent<PlaneController>().ResetPlaneData();
            avaiblePlanes.RemoveAt(generateNumberOfPlane);
        }
        else
        {
            timeToSpawn = startTimeToSpawn;
        }
    }
    public void CheckAvaiblePlanes(GameObject addPlane)
    {
        avaiblePlanes.Add(addPlane);
    }
    public void ChangeScore()
    {
        score.Value++;
        scoreText.text = score.Value.ToString();
    }
}
