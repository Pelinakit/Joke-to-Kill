using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject winPanel;
    public WordJurdge wordJurdge;

    void Start()
    {
        // Ensure the win panel is initially inactive
        winPanel.SetActive(false);
    }

    public void ShowWinPanel()
    {
        // Activate the win panel when called
        // winPanel.SetActive(true);
        SubmitPun();
    }

    public void SubmitPun()
    {
        GameObject mainCanvas = GameObject.Find("MainCanvas");
        Debug.Log(mainCanvas);
        TextMeshProUGUI jokeText = mainCanvas.transform.Find("JokeContainer/Imagebg/JokeText").GetComponent<TextMeshProUGUI>();
        Debug.Log(jokeText);
        StartCoroutine(wordJurdge.GetChatCompletion("Anthropomorphic Potato who is also a Farmer", jokeText.text));
    }
}
