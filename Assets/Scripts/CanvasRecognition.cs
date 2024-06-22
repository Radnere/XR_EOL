using UnityEngine;
using System.Text.RegularExpressions;

public class CanvasTrigger : MonoBehaviour
{
    public AudioSource audioSource; // Аудио источник
    public AudioClip enterSound; // Звук при активации
    public AudioClip exitSound; // Звук при деактивации
    public VoskSpeechToText speechToText; // Ссылка на компонент Vosk Speech to Text
    public VoiceProcessor voiceProcessor; // Ссылка на компонент Vosk Speech to Text

    private Regex bookAppearRegex = new Regex("add book|book plus|book one|diploma book");
    private Regex bookDisappearRegex = new Regex("book zero|book minus|book disapear");
    private bool isBookSpawned = false;
    private bool _isRecognitionActive = false;

    private void Awake()
    {
            speechToText.OnTranscriptionResult += HandleSpeechResult;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsActivatorOrChild(other.transform))
        {
            audioSource.PlayOneShot(enterSound);
            StartRecognitionComponents();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsActivatorOrChild(other.transform))
        {
            audioSource.PlayOneShot(exitSound);
            StopRecognitionComponents();
        }
    }

    private void StartRecognitionComponents()
    {
        if (!voiceProcessor.IsRecording) // Проверяем, не запущено ли уже распознавание
        {
            speechToText.ToggleRecording();
            _isRecognitionActive = true; // Устанавливаем флаг в true
        }
        else
        {
            Debug.Log("Recognition is already active.");
        }
    }

    private void StopRecognitionComponents()
    {
        if (voiceProcessor.IsRecording) // Проверяем, активно ли распознавание
        {
            speechToText.ToggleRecording();
            _isRecognitionActive = false; // Устанавливаем флаг в false
        }
        else
        {
            Debug.Log("Recognition is not active.");
        }
    }


    private void HandleSpeechResult(string transcription)
    {
        Debug.Log("Transcription: " + transcription);
        if (bookAppearRegex.IsMatch(transcription) && !isBookSpawned)
        {
            SceneController.Instance.SpawnEchoBook();
            audioSource.PlayOneShot(enterSound);
            isBookSpawned = true;
        }
        else if (bookDisappearRegex.IsMatch(transcription) && isBookSpawned)
        {
            SceneController.Instance.DestroyEchoBook();
            audioSource.PlayOneShot(exitSound);
            isBookSpawned = false;
        }
    }

    private bool IsActivatorOrChild(Transform transform)
    {
        Transform current = transform;
        while (current != null)
        {
            if (current.CompareTag("Activator"))
            {
                return true;
            }
            current = current.parent;
        }
        return false;
    }
}
