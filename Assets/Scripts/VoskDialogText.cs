using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class VoskDialogText : MonoBehaviour 
{
    public VoskSpeechToText VoskSpeechToText;
    public Text DialogText;
    Regex bookAppear_regex = new Regex("книга плюс|книга появись|появись книга|книга утеряных знаний|книга утеряных знаний появись");
    Regex bookDisAppear_regex = new Regex("книга пропади|исчезни книга|книга ноль|книга минус");
    
    void Awake()
    {
        VoskSpeechToText.OnTranscriptionResult += OnTranscriptionResult;
        ResetState();
    }

    void ResetState()
    {
        // Сброс состояний или логики здесь
    }

    void AddResponse(string response)
    {
        DialogText.text += response + "\n";
    }
      void ClearText()
    {
        DialogText.text = ""; // Очистка текста
    }
    void Say(string response)
    {
        System.Diagnostics.Process.Start("/usr/bin/say", response); 
    }

    private void OnTranscriptionResult(string obj)
    {
        Debug.Log(obj);
        var result = new RecognitionResult(obj);
        ClearText();
        foreach (RecognizedPhrase p in result.Phrases)
        {
            if (bookAppear_regex.IsMatch(p.Text))
            {
				AddResponse("Вызываю ВСЕВЫШНЕГО,, ООО КНИГА, ПОЯВИСЬ ЖЕ! Я ВЕДАЮ!");
                SceneController.Instance.SpawnEchoBook(); // Используем правильное имя класса
				AddResponse("БУМ БАМ, КНИГА ПОЯВИЛАСЬ");
                return;
            }
            if (bookDisAppear_regex.IsMatch(p.Text))
            {
				AddResponse("ООО, БОГИ, Я УСЛШАЛ ТЕБЯ, УНИЧТОЖИМ КНИГУУУУ");
                SceneController.Instance.DestroyEchoBook(); // Используем правильное имя класса
				AddResponse("ООО, мы отправили молитву на исчезновение книги.");
                return;
            }
        }
        if (result.Phrases.Length > 0 && result.Phrases[0].Text != "") {
            AddResponse("я тебя не понимаю, говори нормально, ПО РУСКИ");
        }
    }
}
