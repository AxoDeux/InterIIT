using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighScoreEntry> highScoreEntryList;
    private List<Transform> highScoreEntryTransformList;


    //stores highscores
    public static Dictionary<string, int> ScoreTable
        = new Dictionary<string, int>
        {

        };

    private void Awake()
    {
        //PlayerPrefs.DeleteKey("highscoreTable");
        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        //AddHighScoreEntry(10000, "sarvo");
        //AddHighScoreEntry(100, "sarvo");

        Debug.Log(PlayerPrefs.GetString("highscoreTable"));
        highScoreEntryList = new List<HighScoreEntry>()
        {
        //new HighScoreEntry{ name = "sarvo", score = 1000},
        new HighScoreEntry{ name = "ishan", score = 3000},
        new HighScoreEntry{ name = "rahul", score = 2000},


        };


        //MY CODE
        //string jsonString = 

        //

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        if (jsonString == "")
        {
            Highscores highscore = new Highscores { highscoreEntryList = highScoreEntryList };
            string json = JsonUtility.ToJson(highscore);
            Debug.Log($"json is {json}");
            PlayerPrefs.SetString("highscoreTable", json);
            PlayerPrefs.Save();
        }
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);


        //PlayerPrefs.GetString("highscoreTable");

        //Sort entry list
        if (jsonString != "" || highscores != null)
        {
            for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
                {
                    if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                    {
                        //swap
                        HighScoreEntry tmp = highscores.highscoreEntryList[i];
                        highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                        highscores.highscoreEntryList[j] = tmp;
                    }
                }
            }

        }


        highScoreEntryTransformList = new List<Transform>();
        foreach (HighScoreEntry highScoreEntry in highscores.highscoreEntryList)
        {
            CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highScoreEntryTransformList);
        }



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
        int score = highScoreEntry.score;

        entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();

        string name = highScoreEntry.name;
        entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().text = name;

        transformList.Add(entryTransform);
    }
    public static void AddHighScoreEntry(int score, string name)
    {
        //Create highscore entry
        HighScoreEntry highscoreEntry = new HighScoreEntry { score = score, name = name };

        //load saved highscore
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        bool entryPresent = false;
        //add new entry
        foreach(var item in highscores.highscoreEntryList) {
            //if name is present update it
            if(item.name == name) {
                if(score > item.score)
                    item.score = score;
                entryPresent = true;
                break;
            }
        }

        if (!entryPresent) highscores.highscoreEntryList.Add(highscoreEntry);
        //highscores.highscoreEntryList.Add(highscoreEntry);

        //save updated
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }
    private class Highscores
    {
        public List<HighScoreEntry> highscoreEntryList;
    }
    [System.Serializable]
    public class HighScoreEntry
    {
        public int score;
        public string name;

        //public int Score
        //{
        //    get
        //    {
        //        return score;
        //    }
        //}
        //public string Name
        //{
        //    get
        //    {
        //        return name;
        //    }
        //}
        //public HighScoreEntry(string name, int score)
        //{
        //    this.score = score;
        //    this.name = name;
        //}

    }
}
