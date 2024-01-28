using UnityEngine;
using TMPro;

public class CanvasScaler : MonoBehaviour
{
    public TextMeshProUGUI textObject;  // Reference to the TextMeshProUGUI element
    public float padding = 10f;  // Optional padding around the text

    private RectTransform canvasRect;

    private void Start()
    {
        // Ensure the reference to the TextMeshProUGUI element is set
        if (textObject == null)
        {
            Debug.LogError("TextMeshProUGUI Object reference not set!");
        }

        // Get the RectTransform of the Canvas
        canvasRect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Adjust the Canvas size based on the Text size
        AdjustCanvasSize();
    }

    // Function to adjust the Canvas size based on the Text size
    private void AdjustCanvasSize()
    {
        // Get the preferred size of the TextMeshProUGUI object
        Vector2 textSize = textObject.GetPreferredValues();

        // Calculate the aspect ratio of the text box
        float textAspectRatio = textSize.x / textSize.y;

        // Calculate the aspect ratio of the Canvas
        float canvasAspectRatio = canvasRect.sizeDelta.x / canvasRect.sizeDelta.y;

        // Adjust the Canvas size to match the TextMeshProUGUI size while maintaining the aspect ratio
        if (textAspectRatio > canvasAspectRatio)
        {
            // If the text is wider, adjust the Canvas width
            canvasRect.sizeDelta = new Vector2(textSize.x + padding, (textSize.x + padding) / canvasAspectRatio);
        }
        else
        {
            // If the text is taller, adjust the Canvas height
            canvasRect.sizeDelta = new Vector2((textSize.y + padding) * canvasAspectRatio, textSize.y + padding);
        }
    }
}
