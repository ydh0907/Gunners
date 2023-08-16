using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ConnectUI : MonoBehaviour
{
    public void BeHost(){
        TcpInterface.handler.IsServer = true;
        SceneManager.LoadScene(1);
    }
    public void BeClient(){
        TcpInterface.handler.IsServer = false;
        TcpInterface.handler.HostIPAddress = FindFirstObjectByType<TMP_InputField>().text;
        SceneManager.LoadScene(1);
    }
}
