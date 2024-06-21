using Meta.XR.MRUtilityKit;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic; // Для использования List<>

public class CanvasTrigger : MonoBehaviour
{
    public GameObject canvas; // Ссылка на объект Canvas
    public Transform playerCamera; // Ссылка на камеру или "голову" игрока
    public List<Transform> activators; // Список активаторов триггера

    public float canvasDistance = 2.0f; // Расстояние от камеры, на котором должен появляться Canvas
    public Vector3 canvasOffset = new Vector3(0, 0, 0); // Смещение относительно направления взгляда

        void Start()
    {
    }
 private void OnTriggerEnter(Collider other)
    {
        foreach (var activator in activators)
        {
            if (other.transform == activator) // Проверяем, входит ли объект в список активаторов
            {
                PositionCanvasInFrontOfPlayer();
                canvas.SetActive(true); // Активируем Canvas
                return; // Завершаем цикл, если нашли активатор
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (var activator in activators)
        {
            if (other.transform == activator) // Проверяем, выходит ли объект из списка активаторов
            {
                canvas.SetActive(false); // Деактивируем Canvas
                return; // Завершаем цикл, если нашли активатор
            }
        }
    }

    private void PositionCanvasInFrontOfPlayer()
    {
        // Предполагаем, что камера игрока — это первый активатор в списке
        if (activators.Count > 0 && canvas != null)
        {
            Transform playerCamera = activators[0];
            canvas.transform.position = playerCamera.position + playerCamera.forward * canvasDistance + canvasOffset;
            canvas.transform.rotation = Quaternion.LookRotation(canvas.transform.position - playerCamera.position);
        }
    }
}
