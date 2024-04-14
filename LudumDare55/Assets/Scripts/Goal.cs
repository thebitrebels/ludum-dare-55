using UnityEngine;

public class Goal : MonoBehaviour
{
    public string nextSceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")  
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }

}
