using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    [Header("Prefabs:")]
    [SerializeField] private GameObject arrowTrapPrefab;
    [SerializeField] private GameObject sawShooterPrefab;
    [SerializeField] private GameObject laserPrefab;

    [Header("Trap Properties:")]
    [SerializeField] private int numberOfTraps = 12;
    [SerializeField] private SharedInt trapLevel;

    [Header("Spawning Up/Down Wall Coordinates")]
    [SerializeField] private float yStaticCoordinates = 1;
    [SerializeField] private float xUpDownStartingCoordinates = -10;
    [SerializeField] private float zUpperStartingCoordinates = -2;
    [SerializeField] private float zLowerStartingCoordinates = 16;

    [Header("Spawning Left/Right Wall Coordinates")]
    [SerializeField] private float xLeftStartingCoordinates = -10;
    [SerializeField] private float xRightStartingCoordinates = 10;
    [SerializeField] private float zLeftRightStartingCoordinates = -2;

    private List<Vector3[]> availableCoordinates = new List<Vector3[]>();
    private List<GameObject> availableTrapPrefabs = new List<GameObject>();
    private int currentTrapCount;

    private void Awake() => FindAvailableCoordinates();

    private void Start()
    {
        currentTrapCount = trapLevel.Amount;
        FillPrefabList();
        SpawnTrap();
    }

    private void Update()
    {
        if (availableCoordinates.Count != 1 && trapLevel.Amount > currentTrapCount)
        {
            SpawnTrap();
        }
    }

    private void SpawnTrap()
    {
        int trapId = Random.Range(1, 4);
        int coordinateId = Random.Range(1, availableCoordinates.Count);
        Vector3[] coordinateVectorArray = availableCoordinates[coordinateId];
        currentTrapCount++;

        switch (trapId)
        {
            case 1:
                GameObject arrowTrap = Instantiate(arrowTrapPrefab, transform.position, Quaternion.identity);
                ArrowTrap arrowTrapComponent = arrowTrap.GetComponent<ArrowTrap>();

                arrowTrapComponent.SetVectorA(coordinateVectorArray[0]);
                arrowTrapComponent.SetVectorB(coordinateVectorArray[1]);

                Vector3 oldArrowTrapPos = arrowTrapComponent.GetVectorA();
                float arrowXAxis = oldArrowTrapPos.x;
                float arrowZAxis = oldArrowTrapPos.z;

                if (arrowZAxis == zLowerStartingCoordinates)
                {
                    arrowTrap.transform.rotation *= Quaternion.Euler(0, 180, 0);
                }
                else if (arrowXAxis == xLeftStartingCoordinates)
                {
                    arrowTrap.transform.rotation *= Quaternion.Euler(0, -90, 0);
                }
                else if (arrowXAxis == xRightStartingCoordinates)
                {
                    arrowTrap.transform.rotation *= Quaternion.Euler(0, 90, 0);
                }

                availableCoordinates.RemoveAt(coordinateId);
                break;
            case 2:
                GameObject sawShooter = Instantiate(sawShooterPrefab, transform.position, Quaternion.identity);
                SawShootingTrap sawShootingTrapComponent = sawShooter.GetComponent<SawShootingTrap>();

                sawShootingTrapComponent.SetVectorA(coordinateVectorArray[0]);
                sawShootingTrapComponent.SetVectorB(coordinateVectorArray[1]);

                Vector3 oldSawPos = sawShootingTrapComponent.GetVectorA();
                float sawXAxis = oldSawPos.x;
                float sawZAxis = oldSawPos.z;

                if (sawZAxis == zLowerStartingCoordinates)
                {
                    sawShootingTrapComponent.SetVectorA(coordinateVectorArray[0] + new Vector3(0f, 0f, -1.7f));
                    sawShootingTrapComponent.SetVectorB(coordinateVectorArray[1] + new Vector3(0f, 0f, -1.7f));
                    sawShooter.transform.rotation *= Quaternion.Euler(0, 270, 0);
                }
                else if (sawZAxis == zUpperStartingCoordinates)
                {
                    sawShootingTrapComponent.SetVectorA(coordinateVectorArray[0] + new Vector3(0f, 0f, 1.7f));
                    sawShootingTrapComponent.SetVectorB(coordinateVectorArray[1] + new Vector3(0f, 0f, 1.7f));
                    sawShooter.transform.rotation *= Quaternion.Euler(0, 90, 0);
                }
                else if (sawXAxis == xLeftStartingCoordinates)
                {
                    sawShootingTrapComponent.SetVectorA(coordinateVectorArray[0] + new Vector3(-1.7f, 0f, 0f));
                    sawShootingTrapComponent.SetVectorB(coordinateVectorArray[1] + new Vector3(-1.7f, 0f, 0f));
                    sawShooter.transform.rotation *= Quaternion.Euler(0, -360, 0);
                }
                else if (sawXAxis == xRightStartingCoordinates)
                {
                    sawShootingTrapComponent.SetVectorA(coordinateVectorArray[0] + new Vector3(1.7f, 0f, 0f));
                    sawShootingTrapComponent.SetVectorB(coordinateVectorArray[1] + new Vector3(1.7f, 0f, 0f));
                    sawShooter.transform.rotation *= Quaternion.Euler(0, 180, 0);
                }

                availableCoordinates.RemoveAt(coordinateId);
                break;
            case 3:
                GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
                LaserTrap laserTrapComponent = laser.GetComponent<LaserTrap>();

                laserTrapComponent.SetVectorA(coordinateVectorArray[0]);
                laserTrapComponent.SetVectorB(coordinateVectorArray[1]);

                Vector3 oldLaserPos = laserTrapComponent.GetVectorA();
                float laserXAxis = oldLaserPos.x;
                float laserZAxis = oldLaserPos.z;

                if (laserZAxis == zUpperStartingCoordinates)
                {
                    laserTrapComponent.SetVectorA(coordinateVectorArray[0] + new Vector3(0f, 0f, 0.5f));
                    laserTrapComponent.SetVectorB(coordinateVectorArray[1] + new Vector3(0f, 0f, 0.5f));
                    laser.transform.rotation *= Quaternion.Euler(0, 180, 0);
                }
                else if (laserXAxis == zLowerStartingCoordinates)
                {
                    laserTrapComponent.SetVectorA(coordinateVectorArray[0] + new Vector3(0f, 0f, -0.5f));
                    laserTrapComponent.SetVectorB(coordinateVectorArray[1] + new Vector3(0f, 0f, -0.5f));
                }
                else if (laserXAxis == xLeftStartingCoordinates)
                {
                    laserTrapComponent.SetVectorA(coordinateVectorArray[0] + new Vector3(-0.5f, 0f, 0f));
                    laserTrapComponent.SetVectorB(coordinateVectorArray[1] + new Vector3(-0.5f, 0f, 0f));
                    laser.transform.rotation *= Quaternion.Euler(0, 90, 0);
                }
                else if (laserXAxis == xRightStartingCoordinates)
                {
                    laserTrapComponent.SetVectorA(coordinateVectorArray[0] + new Vector3(0.5f, 0f, 0f));
                    laserTrapComponent.SetVectorB(coordinateVectorArray[1] + new Vector3(0.5f, 0f, 0f));
                    laser.transform.rotation *= Quaternion.Euler(0, -90, 0);
                }

                availableCoordinates.RemoveAt(coordinateId);
                break;
        }
    }

    private void FindAvailableCoordinates()
    {
        float xUpDownCoordinate = xUpDownStartingCoordinates;
        float zUpperCoordinate = zUpperStartingCoordinates;
        float zLowerCoordinate = zLowerStartingCoordinates;

        for (int i = 0; i <= numberOfTraps / 4; i++)
        {
            Vector3[] startToEndUpperCoordinatesArray;
            Vector3[] startToEndLowerCoordinatesArray;

            startToEndUpperCoordinatesArray = new Vector3[] { new Vector3(xUpDownCoordinate, yStaticCoordinates, zUpperCoordinate),  
                    new Vector3(xUpDownCoordinate + 4.5f, yStaticCoordinates, zUpperCoordinate) };
            availableCoordinates.Add(startToEndUpperCoordinatesArray);

            startToEndLowerCoordinatesArray = new Vector3[] { new Vector3(xUpDownCoordinate, yStaticCoordinates, zLowerCoordinate),
                    new Vector3(xUpDownCoordinate + 4.5f, yStaticCoordinates, zLowerCoordinate) };
            availableCoordinates.Add(startToEndLowerCoordinatesArray);

            xUpDownCoordinate += 5f;
        }

        float xLeftCoordinate = xLeftStartingCoordinates;
        float xRightCoordinate = xRightStartingCoordinates;
        float zLeftRightCoordinate = zLeftRightStartingCoordinates;

        for (int i = 0; i <= numberOfTraps / 4; i++)
        {
            Vector3[] startToEndLeftCoordinatesArray;
            Vector3[] startToEndRightCoordinatesArray;

            startToEndLeftCoordinatesArray = new Vector3[] { new Vector3(xLeftCoordinate, yStaticCoordinates, zLeftRightCoordinate),
                    new Vector3(xLeftCoordinate, yStaticCoordinates, zLeftRightCoordinate + 2.5f) };
            availableCoordinates.Add(startToEndLeftCoordinatesArray);

            startToEndRightCoordinatesArray = new Vector3[] { new Vector3(xRightCoordinate, yStaticCoordinates, zLeftRightCoordinate),
                    new Vector3(xRightCoordinate, yStaticCoordinates, zLeftRightCoordinate + 2.5f)};
            availableCoordinates.Add(startToEndRightCoordinatesArray);

            zLeftRightCoordinate += 3f;
        }
    }

    private void FillPrefabList()
    {
        availableTrapPrefabs.Add(arrowTrapPrefab);
        availableTrapPrefabs.Add(sawShooterPrefab);
        availableTrapPrefabs.Add(laserPrefab);
    }
}
