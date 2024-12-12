using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;


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
    private readonly string _apiKey = System.IO.File.ReadAllLines(".env").First(line => line.StartsWith("OPENAI_API_KEY")).Split('=')[1].Trim();
 
    private void Start()
    {
        

    }

    public IEnumerator GetChatCompletion(string profession, string pun)
    {
        var request = new ChatRequest
        {
            model = "gpt-4o-mini",
            messages = new Message[]
            {
                new Message { role = "system", content = $"You are a member of a particular profession. Assess whether a message is funny to you particularly." },
                new Message { role = "user", content = "You are a Living Potato\nReact to this message as only a Living Potato would: strong fertilizer drink good" },
                new Message { role = "assistant", content = "Haha, I take it that you suggest I drink a strong fertilizer because it would be good for me. I understand and it is something I might do. Thanks for making me laugh!" },
                new Message { role = "user", content = "You are a Living Potato\nReact to this message as only a Living Potato would: is they fly better jump" },
                new Message { role = "assistant", content = "Hmm... I don't quite understand what you are trying to say. Could you try to rephrase it perhaps?" },
                new Message { role = "user", content = $"You are {profession}\nReact to this message as only {profession} would: {pun}" }

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
            string response = jsonResponse.choices[0].message.content;
            Debug.Log("Response: " + response);

            GameObject jurdgementCanvas = GameObject.Find("JurdgementCanvas");

            TextMeshProUGUI jurdgementText = jurdgementCanvas.transform.Find("Panel/JurdgementText").GetComponent<TextMeshProUGUI>();
            jurdgementText.text = response;

            var panel = jurdgementCanvas.transform.Find("Panel");
            panel.gameObject.SetActive(true);
        }
    }

}