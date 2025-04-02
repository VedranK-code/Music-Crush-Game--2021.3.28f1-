using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

public class SaveHighScore : MonoBehaviour
{

    private FirebaseFirestore db;

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
    }

    public void Save(string value)
    {
        /*Debug.Log("In here");
        var final = value.Trim();

        Debug.Log(final);

        if (final.Length > 15 || final.Length < 3)
        {
            return;
        }

        var userScore = PlayerPrefs.GetInt("Score");*/

    Debug.Log("In here");
var final = value.Trim();

Debug.Log(final);

if (final.Length < 3)
{
    return;
}

if (final.Length > 12)
{
    final = final.Substring(0, 12); // Limit the username to a maximum of 12 characters
}

var userScore = PlayerPrefs.GetInt("Score");

Dictionary<string, object> score = new Dictionary<string, object>
{
    { "username", final },
    { "score", userScore },
    { "timestamp", FieldValue.ServerTimestamp }
};

db.Collection("scores").AddAsync(score).ContinueWithOnMainThread(task =>
{
    DocumentReference addedDocRef = task.Result;
});
    }
}