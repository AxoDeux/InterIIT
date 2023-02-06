using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighScoreEntry> highScoreEntryList;
    private List<Transform> highScoreEntryTransformList;

    private void Awake()
    {
        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        highScoreEntryList = new List<HighScoreEntry>()
        {
            new HighScoreEntry("sarvo", 1934),
            new HighScoreEntry("ishan", 3246),
            new HighScoreEntry("rahul", 2245),
            new HighScoreEntry("YOU", PlayerPrefs.GetInt("HIGHSCORE"))
        };
        highScoreEntryTransformList = new List<Transform>();

        //Sort entry list
        for (int i = 0; i < highScoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highScoreEntryList.Count; j++)
            {
                if (highScoreEntryList[j].Score > highScoreEntryList[i].Score)
                {
                    //swap
                    HighScoreEntry tmp = highScoreEntryList[i];
                    highScoreEntryList[i] = highScoreEntryList[j];
                    highScoreEntryList[j] = tmp;
                }
            }
        }


        foreach (HighScoreEntry highScoreEntry in highScoreEntryList)
        {
            CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highScoreEntryTransformList);
        }
        //PlayerPrefs.SetString
    }
    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry,
        Transform container, List<Transform> transformList)
    {
        float templateHeight = 40f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }
        entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().text = rankString;
        int score = highScoreEntry.Score;

        entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();

        string name = highScoreEntry.Name;
        entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().text = name;

        transformList.Add(entryTransform);
    }

    private class HighScoreEntry
    {
        int score;
        string name;

        public int Score
        {
            get
            {
                return score;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
        }
        public HighScoreEntry(string name, int score)
        {
            this.score = score;
            this.name = name;
        }

    }
}
