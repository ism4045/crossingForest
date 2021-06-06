using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject MainCamera;

    public Player player;
    public Player player2;
    public int distance;
    public int gems;
    public int getin;
    public GameObject menuPanel;
    public GameObject gamePanel;
    public Text maxScoreTxt;
    public Text scoreTxt;
    public Text gemsTxt;
    public Text maxGemText;
    public int maxGem;
    public Transform[] animalZones;
    public GameObject[] animals;
    public List<int> animalList;
    public Text curGemText;
    public GameObject overPanel;
    public Text curScoreText;
    public Text bestText;
    public Text bestText2;
    public GameObject cat;

    public bool isStart;

    public AudioSource menuSound;
    

    void Awake()
    {
        maxScoreTxt.text = string.Format("{0:n0}",PlayerPrefs.GetInt("MaxScore")) + "m";
        maxGemText.text = string.Format("{0:n0}", PlayerPrefs.GetInt("MaxGem")) + "개";
        menuSound.Play();
    }

    public void GameStart()
    {
        isStart = true;
        StartCoroutine(startAnimal());
        //Debug.Log("?");
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        player.gameObject.SetActive(true);
        player2.gameObject.SetActive(true);
        MainCamera.transform.position = new Vector3(0, 3.6f, -7.6f);
    }

    public void GameOver()
    {
        gamePanel.SetActive(false);
        overPanel.SetActive(true);
        player.gameObject.SetActive(false);
        player2.gameObject.SetActive(false);
        curScoreText.text = scoreTxt.text;
        curGemText.text = gemsTxt.text+"개";
        int maxScore = PlayerPrefs.GetInt("MaxScore");
        int maxGem = PlayerPrefs.GetInt("MaxGem");
        Debug.Log("maxGem "+maxGem);
        Debug.Log("gems " + gems);
        if (gems > maxGem)
        {
            PlayerPrefs.SetInt("MaxGem", gems);
        }
        if (distance > maxScore)
        {
            bestText.gameObject.SetActive(true);
            PlayerPrefs.SetInt("MaxScore", distance);
        }
        else
        {
            bestText.gameObject.SetActive(false);
            bestText2.gameObject.SetActive(false);
        }
    }

    void LateUpdate()
    {
        scoreTxt.text = string.Format("{0:n0}",distance) + "m";
        gemsTxt.text = string.Format("{0:n0}",gems);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator startAnimal()
    {
        for(int i = 0; i<animalZones.Length;i++)
        {
            StartCoroutine(plusAnimals(i));
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator plusAnimals(int k)
    {
        int ran = UnityEngine.Random.Range(0, 4);

        for (int i = 0;i<30;i++)
        {
            GameObject animal = Instantiate(animals[ran], animalZones[k].position, animalZones[k].rotation);
            yield return new WaitForSeconds(1f);
        }
    }
}
