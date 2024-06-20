using Meta.XR.MRUtilityKit;
using UnityEngine;
using UnityEngine.UI;

public class SpawnCubeOnButtonPress : MonoBehaviour
{
    public Button yourButton; // Ссылка на кнопку
    public GameObject cubePrefab; // Префаб куба
    public FindSpawnPositions findSpawnPositions; // Ссылка на FindSpawnPositions

    void Start()
    {
        yourButton.onClick.AddListener(TriggerSpawn); // Подписка на событие нажатия кнопки
    }

    void TriggerSpawn()
    {
        // Замена объекта для спавна на ваш куб
        findSpawnPositions.SpawnObject = cubePrefab;

        // Ограничение спавна только одним объектом
        findSpawnPositions.SpawnAmount = 1;

        // Установка фильтра спавна только на столы
        findSpawnPositions.Labels = MRUKAnchor.SceneLabels.TABLE;

        // Вызов метода спавна
        findSpawnPositions.StartSpawn();
    }

    void OnDestroy()
    {
        // Очищаем подписку на событие нажатия кнопки
        yourButton.onClick.RemoveListener(TriggerSpawn);
    }
}
