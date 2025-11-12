using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEditor.Build.Reporting;

public class PlayerAPIManager : MonoBehaviour
{
    private string baseUrl = "https://68f95b49deff18f212b95420.mockapi.io/Player";

   IEnumerator GetPlayers()
   {
        using (UnityWebRequest www = UnityWebRequest.Get(baseUrl))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
                Debug.Log("Players: " + www.downloadHandler.text);
            else Debug.LogError("Erro: " + www.error);
        }
   }

    IEnumerator AddPlayer()
    {
        string json = "{\"vida\": 100, \"quantidadeItens\": 5, \"posicaoX\": 1, \"posicaoY\": 2,\"posicaoZ\":3}";

        using (UnityWebRequest www = new UnityWebRequest(baseUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();

            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
                Debug.Log("Player adicionado!");
            else Debug.LogError("Erro: " + www.error);
        }
    }
}
