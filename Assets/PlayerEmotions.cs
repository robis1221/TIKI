using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using Affdex;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;

public class PlayerEmotions : ImageResultsListener
{
    private const string DELIMITER = ",";
    private const string NOT_A_NUMBER = "NaN";

    public float currentMetric;

    public StreamWriter outputFile;
    private GameObject participant;
    public bool calledTheStart = false;

    public void callTheStart()
    {
        
        participant = GameObject.FindWithTag("ParticipantNumberObject");
        if (participant.GetComponent<InitialScript>() != null)
        {
            outputFile = new StreamWriter(participant.GetComponent<InitialScript>().getNR() + "_VAGUE_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "PlayerMetrics.csv");
        }
        else
        {
            outputFile = new StreamWriter("CONCRETE_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "PlayerMetrics.csv");
        }
            outputFile.WriteLine ("Time,Joy,Fear,Disgust,Sadness,Anger,Surprise,Contempt,Valence,Engagement,Smile,InnerBrowRaise,BrowRaise,BrowFurrow,NoseWrinkle," +
            "UpperLipRaise,LipCornerDepressor,ChinRaise,LipPucker,LipPress,LipSuck,MouthOpen,Smirk,EyeClosure,Attention");
        print("calledstartemo");
        calledTheStart = true;
    }

    public override void onFaceFound(float timestamp, int faceId)
    {
        if (calledTheStart)
        {
            if (Debug.isDebugBuild) Debug.Log("Found the face");
        }
    }

    public override void onFaceLost(float timestamp, int faceId)
    {
        if (calledTheStart)
        {
            if (Debug.isDebugBuild) Debug.Log("Lost the face");
        }
    }

    public override void onImageResults(Dictionary<int, Face> faces)
    {
        if (calledTheStart)
        {
            var rowToWrite = new StringBuilder();

            rowToWrite.Append(Time.time + DELIMITER);

            if (faces.Count > 0)
            {
                foreach (Emotions emotion in Enum.GetValues(typeof(Emotions)))
                {
                    faces[0].Emotions.TryGetValue(emotion, out currentMetric);
                    rowToWrite.Append(currentMetric + DELIMITER);
                }

                foreach (Expressions expression in Enum.GetValues(typeof(Expressions)))
                {
                    faces[0].Expressions.TryGetValue(expression, out currentMetric);
                    rowToWrite.Append(currentMetric + DELIMITER);
                }
            }
            else
            {
                for (int x = 0; x < Enum.GetNames(typeof(Emotions)).Length + Enum.GetNames(typeof(Expressions)).Length; x++)
                {
                    if (rowToWrite.Length > 0)
                    {
                        rowToWrite.Append(DELIMITER);
                    }
                    rowToWrite.Append(NOT_A_NUMBER);
                }
            }

            outputFile.WriteLine(rowToWrite.ToString());
        }
    }

    void OnApplicationQuit ()
    {
        if (calledTheStart)
        {
            outputFile.Flush();
            outputFile.Close();
        }
    }
}