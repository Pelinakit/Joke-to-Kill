using System;
using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class ChatRequest
{
    public string model;
    public Message[] messages;
}

[Serializable]
public class Message
{
    public string role;
    public string content;
}

[Serializable]
public class Root
{
    public string id;
    public string object_;
    public int created;
    public string model;
    public Usage usage;
    public Choice[] choices;
}

[Serializable]
public class Usage
{
    public int prompt_tokens;
    public int completion_tokens;
    public int total_tokens;
}

[Serializable]
public class Choice
{
    public Message message;
    public string finish_reason;
    public int index;
}
public class WordJurdge : MonoBehaviour
{
    private readonly string _apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

    private void Start()
    {

    }

    public IEnumerator GetChatCompletion(string profession, string pun)
    {
        var request = new ChatRequest
        {
            model = "gpt-4",
            messages = new Message[]
            {
                new Message { role = "system", content = "You are a pun judge. Assess whether the pun fills the following two requirements: 1. The pun is funny. 2. The pun is relevant to the profession." },
                new Message { role = "user", content = "Profession: Astronomer\nPun: Why do astronomers make terrible soccer players? Because they always shoot for the stars!" },
                new Message { role = "assistant", content = "Haha, that's quite funny! As an astronomer, I find this pun amusing because it plays on the phrase 'shooting for the stars,' which is both a literal part of my work in studying celestial bodies and a metaphor for aiming high in goals or ambitions. It humorously suggests that astronomers, focused on the stars, might not be the best at keeping their shots on the ground in soccer. This pun is not only funny but also relevant to my profession, making it a great fit for both criteria." },
                new Message { role = "user", content = $"Profession: {profession}\nPun: {pun}" }
            }
        };

        var json = JsonUtility.ToJson(request);
        Debug.Log("Request JSON: " + json); // Log the full JSON request
        var data = new System.Text.UTF8Encoding().GetBytes(json);

        UnityWebRequest www = new UnityWebRequest("https://api.openai.com/v1/chat/completions", "POST");
        www.uploadHandler = new UploadHandlerRaw(data);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Authorization", $"Bearer {_apiKey}");

        Debug.Log($"Full request is {www.url} {www.method} {www.GetRequestHeader("Content-Type")} {www.GetRequestHeader("Authorization")} {www.uploadHandler.data}");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            var jsonResponse = JsonUtility.FromJson<Root>(www.downloadHandler.text);
            Debug.Log("Response: " + jsonResponse.choices[0].message.content);
        }
    }

}