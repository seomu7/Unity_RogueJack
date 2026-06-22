using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SocialPlatforms.Impl;

[Serializable]
public class ScoreData
{
    public string player_name;
    public int score;
}

[Serializable]
public class RankingResponse
{
    public ScoreData[] rankings;
}

[Serializable]
public class PartialRankingResponse
{
    public string status;
    public int rank;
}

public class MyJetsonNano : MonoBehaviour
{
    private const string SERVER_URL = "http://seomu7.duckdns.org:8000";

    private static MyJetsonNano _instance;
    public static MyJetsonNano Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MyJetsonNano>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (_instance == this)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public IEnumerator GetRanking()
    {
        string url = SERVER_URL + "/ranking";

        Debug.Log("Request : " + url);

        using UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
            yield break;
        }

        string json = request.downloadHandler.text;

        Debug.Log("Response:");
        Debug.Log(json);

        RankingResponse response = JsonUtility.FromJson<RankingResponse>(json);

        foreach (ScoreData score in response.rankings)
        {
            Debug.Log(
                $"{score.player_name} : {score.score}"
            );
        }
    }

    public IEnumerator GetRanking(LeaderBoardController leaderBoardController)
    {
        string url = SERVER_URL + "/ranking";

        Debug.Log("Request : " + url);

        using UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
            GameManager.Instance.resultViewController.ShowNetworkErrorText();
            yield break;
        }

        string json = request.downloadHandler.text;

        Debug.Log("Response:");
        Debug.Log(json);

        RankingResponse response = JsonUtility.FromJson<RankingResponse>(json);

        List<string> texts = new List<string>();

        foreach (ScoreData score in response.rankings)
        {
            Debug.Log(
                $"{score.player_name} : {score.score}"
            );

            texts.Add(score.player_name+"\n"+score.score.ToString());
        }

        leaderBoardController.ChangeFirstText(texts[0]);
        leaderBoardController.ChangeSecondText(texts[1]);
        leaderBoardController.ChangeThirdText(texts[2]);

        leaderBoardController.ToggleActive();

        yield return null;
    }

    public IEnumerator Submit()
    {
        ScoreData sumbitData = new ScoreData()
        {
            score = 120,
            player_name = "SeomuJin"
        };

        string url = SERVER_URL + "/submit";

        string json = JsonUtility.ToJson(sumbitData);

        byte[] body = System.Text.Encoding.UTF8.GetBytes(json);

        UnityWebRequest req = new UnityWebRequest(url, "POST");

        req.uploadHandler = new UploadHandlerRaw(body);

        req.downloadHandler = new DownloadHandlerBuffer();

        req.SetRequestHeader(
            "Content-Type",
            "application/json"
        );

        yield return req.SendWebRequest();

        Debug.Log(req.downloadHandler.text);
    }

    public IEnumerator Submit(ScoreData scoreData)
    {
        string url = SERVER_URL + "/submit";

        string json = JsonUtility.ToJson(scoreData);

        byte[] body = System.Text.Encoding.UTF8.GetBytes(json);

        UnityWebRequest req = new UnityWebRequest(url, "POST");

        req.uploadHandler = new UploadHandlerRaw(body);

        req.downloadHandler = new DownloadHandlerBuffer();

        req.SetRequestHeader(
            "Content-Type",
            "application/json"
        );

        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(req.error);
            yield break;
        }

        string responseJson = req.downloadHandler.text;

        Debug.Log(responseJson);

        PartialRankingResponse response = JsonUtility.FromJson<PartialRankingResponse>(responseJson);
        
        Debug.Log(
            $"업로드 성공! 현재 순위 : {response.rank}"
        );
    }

    public IEnumerator Submit(ScoreData scoreData, UploadController uploadController)
    {
        string url = SERVER_URL + "/submit";

        string json = JsonUtility.ToJson(scoreData);

        byte[] body = System.Text.Encoding.UTF8.GetBytes(json);

        UnityWebRequest req = new UnityWebRequest(url, "POST");

        req.uploadHandler = new UploadHandlerRaw(body);

        req.downloadHandler = new DownloadHandlerBuffer();

        req.SetRequestHeader(
            "Content-Type",
            "application/json"
        );

        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(req.error);
            yield break;
        }

        string responseJson = req.downloadHandler.text;

        Debug.Log(responseJson);

        PartialRankingResponse response = JsonUtility.FromJson<PartialRankingResponse>(responseJson);

        Debug.Log(
            $"업로드 성공! 현재 순위 : {response.rank}"
        );
    }

    public IEnumerator Submit(ScoreData scoreData, Action<int> responseAction)
    {
        string url = SERVER_URL + "/submit";

        string json = JsonUtility.ToJson(scoreData);

        byte[] body = System.Text.Encoding.UTF8.GetBytes(json);

        UnityWebRequest req = new UnityWebRequest(url, "POST");

        req.uploadHandler = new UploadHandlerRaw(body);

        req.downloadHandler = new DownloadHandlerBuffer();

        req.SetRequestHeader(
            "Content-Type",
            "application/json"
        );

        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(req.error);
            yield break;
        }

        string responseJson = req.downloadHandler.text;

        Debug.Log(responseJson);

        PartialRankingResponse response = JsonUtility.FromJson<PartialRankingResponse>(responseJson);

        responseAction.Invoke(response.rank);

        yield break;
    }

    public IEnumerator Partial_Ranking()
    {
        int current_score = 100;

        string url = $"http://seomu7.duckdns.org:8000/partial_ranking?score={current_score}";

        using UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if(request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
            yield break;
        }

        string json = request.downloadHandler.text;

        Debug.Log(json);

        PartialRankingResponse response =JsonUtility.FromJson<PartialRankingResponse>(json);

        Debug.Log($"예상 순위 : {response.rank}");
    }

    public IEnumerator Partial_Ranking(int current_score)
    {
        string url = $"http://seomu7.duckdns.org:8000/partial_ranking?score={current_score}";

        using UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
            yield break;
        }

        string json = request.downloadHandler.text;

        Debug.Log(json);

        PartialRankingResponse response = JsonUtility.FromJson<PartialRankingResponse>(json);

        Debug.Log($"예상 순위 : {response.rank}");
    }

    public void StartPartial_Ranking(int current_score)
    {
        StartCoroutine(Partial_Ranking(current_score));
    }

    public void StartSubmit()
    {
        StartCoroutine(Submit());
    }

    public void StartGetRanking(LeaderBoardController leaderBoardController)
    {
        StartCoroutine (GetRanking(leaderBoardController));
    }
}
