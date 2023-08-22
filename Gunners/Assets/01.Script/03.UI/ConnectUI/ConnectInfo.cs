using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ConnectInfo : MonoBehaviour
{
    public static ConnectInfo info = null;

    public bool IsServer;
    public string RoomNumber;
    private void Awake() {
        if(info != null) Destroy(gameObject);
        info = this;
        DontDestroyOnLoad(gameObject);
    }
    public void BeHost(){
        IsServer = true;
        SceneManager.LoadScene(1);
    }
    public void BeClient(){
        IsServer = false;
        RoomNumber = FindFirstObjectByType<TMP_InputField>().text;
        SceneManager.LoadScene(1);
    }
}
