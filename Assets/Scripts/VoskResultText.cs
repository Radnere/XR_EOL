using UnityEngine;
using UnityEngine.UI;

public class VoskResultText : MonoBehaviour 
{
    public VoskSpeechToText VoskSpeechToText;
    public Text ResultText;

    void Awake()
    {
        VoskSpeechToText.OnTranscriptionResult += OnTranscriptionResult;
    }
    void ClearText()
    {
        ResultText.text = ""; // Очистка текста
    }    private void OnTranscriptionResult(string obj)
    {
        Debug.Log(obj);
        var result = new RecognitionResult(obj);
        ClearText();
        for (int i = 0; i < result.Phrases.Length; i++)
        {
            if (i > 0)
            {
                ResultText.text += ", ";
            }

            ResultText.text += result.Phrases[i].Text;
        }
    	ResultText.text += "\n";
    }
}
