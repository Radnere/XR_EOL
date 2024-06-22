using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject canvas; // Объект Canvas
    public Transform cameraTransform; // Трансформ камеры

    private const float spawnDistance = 2.0f; // Расстояние перед камерой, где будет появляться Canvas

    private void Awake()
    {
        // Настройка ссылки на трансформ камеры, если это необходимо
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.Start))
        {
            ToggleMenu(!canvas.activeInHierarchy);
        }
    }

    private void ToggleMenu(bool active)
    {
        Debug.Log($"Toggling canvas to: {active}");
        if (active)
        {
            PositionCanvasInFrontOfCamera();
        }
        canvas.SetActive(active);

        if (active)
        {
            Debug.Log("Activating components...");
            StartRecognitionComponents();
        }
        else
        {
            Debug.Log("Deactivating components...");
            StopRecognitionComponents();
        }
    }

    // Позиционирование Canvas перед камерой
    private void PositionCanvasInFrontOfCamera()
    {
        Vector3 spawnPosition = cameraTransform.position + cameraTransform.forward * spawnDistance;
        Quaternion spawnRotation = Quaternion.LookRotation(cameraTransform.forward);

        canvas.transform.position = spawnPosition;
        canvas.transform.rotation = spawnRotation;
    }

    private void StartRecognitionComponents()
    {
        VoiceProcessor voiceProcessor = canvas.GetComponentInChildren<VoiceProcessor>(true);
        VoskSpeechToText voskSpeechToText = canvas.GetComponentInChildren<VoskSpeechToText>(true);

        if (voiceProcessor != null)
        {
            voskSpeechToText.ToggleRecording();// Запуск записи
            Debug.Log("VoiceProcessor recording started.");
        }
        if (voskSpeechToText != null)
        {
            voskSpeechToText.ToggleRecording(); // Запуск распознавания
            Debug.Log("VoskSpeechToText recognition started.");
        }
    }

    private void StopRecognitionComponents()
    {
        VoiceProcessor voiceProcessor = canvas.GetComponentInChildren<VoiceProcessor>(true);
        VoskSpeechToText voskSpeechToText = canvas.GetComponentInChildren<VoskSpeechToText>(true);

        if (voiceProcessor != null)
        {
            voiceProcessor.StopRecording(); // Остановка записи
            Debug.Log("VoiceProcessor recording stopped.");
        }
        if (voskSpeechToText != null)
        {
            voskSpeechToText.ToggleRecording(); // Остановка распознавания
            Debug.Log("VoskSpeechToText recognition stopped.");
        }
    }
}
