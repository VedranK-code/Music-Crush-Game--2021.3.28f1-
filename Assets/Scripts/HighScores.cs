using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Firestore;
using Firebase.Extensions;

public class HighScores : MonoBehaviour
{

    public GameObject scorePrefab;
    public GameObject inputPrefab;
    public Transform scoresWrapper;


    private FirebaseFirestore db;
    private List<GameObject> scoreUiItems = new List<GameObject>();

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;

        var userScore = PlayerPrefs.GetInt("Score");
        var invoked = false;

        db.Collection("scores")
            .OrderByDescending("score")
            .Limit(10)
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                QuerySnapshot snapshot = task.Result;

                var index = 1;
                Debug.Log(userScore);

                foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                {
                    Dictionary<string, object> score = documentSnapshot.ToDictionary();

                    Debug.Log(int.Parse(score["score"].ToString()));

                    if (userScore > int.Parse(score["score"].ToString()) && index < 10 && !invoked)
                    {
                        Debug.Log("Called");
                        invoked = true;

                        var inst = Instantiate(inputPrefab, Vector3.zero, Quaternion.identity);
                        inst.transform.SetParent(scoresWrapper, false);
                        var texts = inst.GetComponentsInChildren<TextMeshProUGUI>();

                        texts[0].text = index.ToString();
                        texts[3].text = userScore.ToString();

                        if (index < 10)
                        {
                            index++;
                            RenderScore(score, index);
                        }
                    }
                    else if (index < 11)
                    {
                        RenderScore(score, index);
                    }

                    index++;
                }

                if (index < 11 && !invoked)
                {
                        Debug.Log("Called In Last");
                        invoked = true;

                        var inst = Instantiate(inputPrefab, Vector3.zero, Quaternion.identity);
                        inst.transform.SetParent(scoresWrapper, false);
                        var texts = inst.GetComponentsInChildren<TextMeshProUGUI>();

                        texts[0].text = index.ToString();
                        texts[3].text = userScore.ToString();
                }
            });
    }

    void OnDestroy()
    {
        PlayerPrefs.SetInt("Score", 0);
    }

    private void RenderScore(Dictionary<string, object> score, int index)
    {
        var inst = Instantiate(scorePrefab, Vector3.zero, Quaternion.identity);
        inst.transform.SetParent(scoresWrapper, false);
        var texts = inst.GetComponentsInChildren<TextMeshProUGUI>();

        texts[0].text = index.ToString();
        texts[1].text = score["username"].ToString();
        texts[2].text = score["score"].ToString();
    }
}
