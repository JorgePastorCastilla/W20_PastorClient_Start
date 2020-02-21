using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1.0f;
    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI playerNameText;
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public bool isGameActive;
    public Text textBoard;

    public Player player;

    public GameObject leftColumn;

    // Start is called before the first frame update
    void Start()
    {
        //player = FindObjectOfType<Player>();
        //playerNameText.text = player.Name;
        scoreText.text = score.ToString();
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        StartCoroutine(ShowLoggedPlayers());
    }

    IEnumerator ShowLoggedPlayers()
    {
        while (true)
        {
            yield return StartCoroutine(GetPlayers());
             
        }

    }

    internal static IEnumerator GetPlayers()
    {
        Player player = FindObjectOfType<Player>();
        UnityWebRequest httpClient = new UnityWebRequest("http://localhost:64497/api/Player/Players", "GET");

        httpClient.SetRequestHeader("Accept", "application/json");

        httpClient.downloadHandler = new DownloadHandlerBuffer();

        yield return httpClient.SendWebRequest();

        if (httpClient.isNetworkError || httpClient.isHttpError)
        {
            throw new Exception("Helper > GetPlayerInfo: " + httpClient.error);
        }
        else
        {
            //meter mas campos
            var texto = "{\"myStrings\":" +httpClient.downloadHandler.text+"}";
            ListOfPlayers lista = JsonUtility.FromJson<ListOfPlayers>(texto);
            textBoard.text += "\nJsonTests > ReceiveJsonListOfString: ";
            foreach (PlayerSerializable playerInList in lista.players)
            {
                textBoard.text += "\n\t" + playerInList.Name;
                textBoard.text += "\n\t" + playerInList.BirthDay;
                textBoard.text += "\n\t" + playerInList.LastLog;
                textBoard.text += "\n\t";
            }
            Task.Delay(3);


            //PlayerSerializable playerSerializable = JsonUtility.FromJson<PlayerSerializable>(httpClient.downloadHandler.text);
            //player.Id = playerSerializable.Id;
            //player.Name = playerSerializable.Name;
            //player.Email = playerSerializable.Email;
            //player.BirthDay = DateTime.Parse(playerSerializable.BirthDay);
        }

        httpClient.Dispose();
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int randomIndex = UnityEngine.Random.Range(0, 4);
            Instantiate(targets[randomIndex]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverScreen.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
