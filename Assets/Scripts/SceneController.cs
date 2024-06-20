using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject _EchoBook;

    [SerializeField]
    private GameObject _bookRandomSpawnerObject; // Объект с компонентом BookRandomSpawner

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

        // Включаем объект с BookRandomSpawner
        if (_bookRandomSpawnerObject != null)
        {
            _bookRandomSpawnerObject.SetActive(true);
            // Предполагаем, что включение объекта автоматически инициирует спавн книги
            _currentEchoBookInstance = GameObject.Find(_EchoBook.name + "(Clone)");
            if (_currentEchoBookInstance != null)
            {
                Debug.Log("EchoBook successfully spawned.");
            }
            else
            {
                Debug.LogWarning("EchoBook spawn failed.");
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
            Debug.Log("-> SceneController::DestroyEchoBook()");
            Destroy(_currentEchoBookInstance);
            _currentEchoBookInstance = null;
            Debug.Log("EchoBook has been destroyed.");
        }
        else
        {
            Debug.LogWarning("No EchoBook instance found to destroy.");
        }
    }
}
