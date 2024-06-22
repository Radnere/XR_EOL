using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class VoskDialogText : MonoBehaviour 
{
    public VoskSpeechToText VoskSpeechToText;
    public Text DialogText;
    Regex bookAppear_regex = new Regex("add book|echo Book|diploma book");
    Regex bookDisAppear_regex = new Regex("book zero|book minus|book disapear");

    private bool isBookSpawned = false;

    void Awake()
    {
        VoskSpeechToText.OnTranscriptionResult += OnTranscriptionResult;
        ResetState();
    }

    void ResetState()
    {
        isBookSpawned = false;
    }

    void AddResponse(string response)
    {
        ClearText();
        DialogText.text += response + "\n";
    }

    void ClearText()
    {
        DialogText.text = ""; 
    }

    void Say(string response)
    {
        System.Diagnostics.Process.Start("/usr/bin/say", response); 
    }

    private void OnTranscriptionResult(string obj)
    {
        Debug.Log(obj);
        var result = new RecognitionResult(obj);
        foreach (RecognizedPhrase p in result.Phrases)
        {
            if (bookAppear_regex.IsMatch(p.Text))
            {
                if (!isBookSpawned)
                {
                    AddResponse("Summoning the ALMIGHTY, OH BOOK, APPEAR! I COMMAND YOU!");
                    SceneController.Instance.SpawnEchoBook(); 
                    AddResponse("BOOM BAM, THE BOOK HAS APPEARED");
                    isBookSpawned = true;  
                    return;
                }
                else
                {
                    AddResponse("The book has already appeared. Cannot create another one.");
                    return;
                }
            }
            if (bookDisAppear_regex.IsMatch(p.Text))
            {
                if (isBookSpawned)
                {
                    AddResponse("OH, GODS, I HEARD YOU, WE SHALL DESTROY THE BOOK!");
                    SceneController.Instance.DestroyEchoBook();
                    AddResponse("OH, we sent a prayer for the book to disappear.");
                    isBookSpawned = false;  
                    return;
                }
                else
                {
                    AddResponse("There's no book to remove.");
                    return;
                }
            }
        }
        if (result.Phrases.Length > 0 && result.Phrases[0].Text != "") {
            AddResponse("I don't understand you, speak properly, in English.");
        }
    }
}
