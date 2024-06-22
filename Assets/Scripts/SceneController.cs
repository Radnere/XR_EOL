using Meta.XR.MRUtilityKit;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject _bookRandomSpawnerObject; 

    private GameObject _currentEchoBookInstance;

    public static SceneController Instance;

    void Awake()
    {
        Instance = this;
    }

    public void SpawnEchoBook()
    {
        if (_currentEchoBookInstance != null)
        {
            Debug.Log("EchoBook has already been spawned. Only one instance is allowed.");
            return;
        }

        Debug.Log("-> SceneController::SpawnEchoBook()");

        if (_bookRandomSpawnerObject != null)
        {
            _bookRandomSpawnerObject.SetActive(true); 

            FindSpawnPositions spawner = _bookRandomSpawnerObject.GetComponent<FindSpawnPositions>();
            if (spawner != null)
            {
                spawner.StartSpawn(); 
                _currentEchoBookInstance = spawner.SpawnObject; 
                Debug.Log("EchoBook successfully spawned.");
            }
            else
            {
                Debug.LogWarning("FindSpawnPositions component is not found on the spawner object.");
            }
        }
        else
        {
            Debug.LogError("BookRandomSpawnerObject is not assigned in the inspector.");
        }
    }

    public void DestroyEchoBook()
    {
        if (_currentEchoBookInstance != null)
        {
            FindSpawnPositions spawner = _bookRandomSpawnerObject.GetComponent<FindSpawnPositions>();
            Debug.Log("-> SceneController::DestroyEchoBook()");
            Destroy(spawner.SpawnObject);
            _currentEchoBookInstance = null;
            Debug.Log("EchoBook has been destroyed.");
            _bookRandomSpawnerObject.SetActive(false); 
        }
        else
        {
            Debug.LogWarning("No EchoBook instance found to destroy.");
        }
    }
}
