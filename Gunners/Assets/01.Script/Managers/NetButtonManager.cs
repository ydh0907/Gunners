using UnityEngine;

public class NetButtonManager : MonoBehaviour
{
    [SerializeField] private Transform Loading;

    public void Matching()
    {
        Loading.gameObject.SetActive(true);
        GameManager.Instance.StartMatching();
    }

    public void LoadScene(int num)
    {
        LoadSceneManager.Instance.LoadSceneAsync(num, null);
    }
}
