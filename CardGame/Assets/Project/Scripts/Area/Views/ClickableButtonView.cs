using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickableButtonView : MonoBehaviour
{
    public void Replay()
    {
        SceneManager.LoadScene("GameScene");
    }
}