using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject winPanel;

    void Start()
    {
        // Ensure the win panel is initially inactive
        winPanel.SetActive(false);
    }

    public void ShowWinPanel()
    {
        // Activate the win panel when called
        winPanel.SetActive(true);
    }
}
