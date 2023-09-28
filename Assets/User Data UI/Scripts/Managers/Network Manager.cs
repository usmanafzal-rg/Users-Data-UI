using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Task = UnityEditor.VersionControl.Task;
using System.Text;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour, IService
{
    public async Task<TProps> Get<TProps>(string url)
    {
        using HttpClient httpClient = new HttpClient();
        HttpResponseMessage response = await httpClient.GetAsync(url);
        TProps result = await FromJson<TProps>(response);
        return result;
    }

    public async Task<TProps> Post<TProps, TDProps>(string url, TDProps data)
    {
        HttpContent content = ToJson<TDProps>(data);
        using HttpClient httpClient = new HttpClient();
        HttpResponseMessage response = await httpClient.PostAsync(url, content);
        TProps result = await FromJson<TProps>(response);
        return result;
    }

    public async Task<TProps> Put<TProps, TDProps>(string url, TDProps data)
    {
        HttpContent content = ToJson<TDProps>(data);
        using HttpClient httpClient = new HttpClient();
        HttpResponseMessage response = await httpClient.PutAsync(url, content);
        TProps result = await FromJson<TProps>(response);
        return result;
    }

    public async Task<TProps> Delete<TProps>(string url)
    {
        using HttpClient httpClient = new HttpClient();
        HttpResponseMessage response = await httpClient.DeleteAsync(url);
        TProps result = await FromJson<TProps>(response);
        return result;
    }

    public void GetSprite<TProps>(string url, TProps data, Action<TProps> callback) where TProps : ISpriteProperties, new()
    {
        Debug.Log("download from api");
        StartCoroutine(GetSpriteCoroutine<TProps>(url, data ,callback));
    }
    
    private IEnumerator GetSpriteCoroutine<TProps>(string url, TProps data ,Action<TProps> callback) where TProps : ISpriteProperties, new()
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        Sprite sprite = null;
        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
            sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            
        }
        
        data.Sprite = sprite;
        callback(data);
    }
    
    private HttpContent ToJson<TDProps>(TDProps data)
    {
        string sendData = JsonUtility.ToJson(data);
        HttpContent content = new StringContent(sendData, Encoding.UTF8, "application/json");
        return content;
    }
    private async Task<TProps> FromJson<TProps>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            TProps data = JsonUtility.FromJson<TProps>(content);
            return data;
        }
        return default(TProps);
    }
    
}
