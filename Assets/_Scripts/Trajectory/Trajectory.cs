using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [Header("Trajectory Info")]
    [SerializeField] private GameObject[] trajectoryPointPrefabs;
    [SerializeField] private int howManyTrajectory = 30;
    private readonly List<GameObject> trajectoryPoints = new();

    private void Start()
    {
        for (int i = 0; i < howManyTrajectory; i++)
        {
            GameObject trajectoryPoint = Instantiate(trajectoryPointPrefabs[0], transform.position, Quaternion.identity);
            trajectoryPoint.SetActive(false);
            trajectoryPoint.transform.SetParent(transform);
            trajectoryPoints.Add(trajectoryPoint);
        }
    }

    public void TrajectoryPlacement(Transform birdTransform)
    {
        foreach (GameObject trajectoryPoint in trajectoryPoints)
        {
            if (trajectoryPoint.activeSelf) continue;

            trajectoryPoint.transform.position = birdTransform.position;
            trajectoryPoint.SetActive(true);
            return;
        }
    }

    public void DisableAllTrajectoryPoints()
    {
        foreach(GameObject trajectoryPoint in trajectoryPoints)
        {
            trajectoryPoint.SetActive(false);
        }
    }
}
