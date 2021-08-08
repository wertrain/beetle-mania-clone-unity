using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButton : MonoBehaviour
{
    public void OnButtonPress(GameObject sender)
    {
        switch (sender.name)
        {
            case "RetryButton":
                SceneManager.LoadScene("GameScene");
                break;

            case "EndButton":
                Destroy(GameObject.Find("GameOverMenu(Clone)"));
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
                UnityEngine.Application.Quit();
#endif
                break;
        }
    }
}
