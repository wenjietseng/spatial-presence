 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Questionnaire : MonoBehaviour
{
    public class QuestionnaireItem {
        public string questionStatement;
        public string lowerAnchor;
        public string higherAnchor;
        public int scalesCount = 5;
        // procedure I guess
        public bool isRespond = false;
        public int likertScale = 0;

        public QuestionnaireItem(string q, string l = "I do not agree at all", string h  = "I fully agree", int scale = 5)
        {
            questionStatement = q;
            lowerAnchor = l;
            higherAnchor = h;
            scalesCount = scale;
        }
    }

    // SUS Presence: 1-7
    public static QuestionnaireItem sus01 = new QuestionnaireItem("I had a sense of 'being there' in the virtual environment:", "not at all", "very much", 7);
    public static QuestionnaireItem sus02 = new QuestionnaireItem("There were times during the experience when the virtual environment was the reality for me...", "at no time", "almost all the time", 7);
    public static QuestionnaireItem sus03 = new QuestionnaireItem("The virtual environment seems to me to be more like...", "images I saw", "somewhere I visited", 7);
    public static QuestionnaireItem sus04 = new QuestionnaireItem("I had a stronger sense of...", "being elsewhere", "being in the VE", 7);
    public static QuestionnaireItem sus05 = new QuestionnaireItem("I think of the virtual environment as a place in a way similar to other places that I've been today...", "not at all", "very much so", 7);
    public static QuestionnaireItem sus06 = new QuestionnaireItem("During the experience I often thought that I was really standing in the virtual environment...", "not very often", "very much so", 7);
    
    // SPES: All items were designed to be answered on a 5-point Likert scale ranging from 1 (= I do not agree at all) to 5 (= I fully agree).
    public static QuestionnaireItem sl01  = new QuestionnaireItem("I felt like I was actually there in the environment of the presentation.");
    public static QuestionnaireItem sl02  = new QuestionnaireItem("It seemed as though I actually took part in the action of the presentation.");
    public static QuestionnaireItem sl03  = new QuestionnaireItem("It was as though my true location had shifted into the environment in the presentation.");
    public static QuestionnaireItem sl04  = new QuestionnaireItem("I felt as though I was physically present in the environment of the presentation.");
    public static QuestionnaireItem pa01  = new QuestionnaireItem("The objects in the presentation gave me the feeling that I could do things with them.");
    public static QuestionnaireItem pa02  = new QuestionnaireItem("I had the impression that I could be active in the environment of the presentation.");
    public static QuestionnaireItem pa05  = new QuestionnaireItem("I felt like I could move around among the objects in the presentation.");
    public static QuestionnaireItem pa08  = new QuestionnaireItem("It seemed to me that I could do whatever I wanted in the environment of the presentation.");

    // MEC_SPQ_SSM: 5-point Likert scale ranging from 1 (‘I do not agree at all’) to 5 (‘I fully agree’)
    public static QuestionnaireItem ssm01 = new QuestionnaireItem("I was able to imagine the arrangement of the spaces presented in VR very well.");
    public static QuestionnaireItem ssm02 = new QuestionnaireItem("I had a precise idea of the spatial surroundings presented in the VR.");
    public static QuestionnaireItem ssm03 = new QuestionnaireItem("I was able to make a good estimate of the size of the presented space.");
    public static QuestionnaireItem ssm04 = new QuestionnaireItem("Even now, I still have a concrete mental image of the spatial environment.");

    public static List<QuestionnaireItem> spatialPresenceItems;
    public static List<QuestionnaireItem> susItems;

    void Start()
    {
        spatialPresenceItems = new List<QuestionnaireItem>();
        spatialPresenceItems.Add(sl01);
        spatialPresenceItems.Add(sl02);
        spatialPresenceItems.Add(sl03);
        spatialPresenceItems.Add(sl04);
        spatialPresenceItems.Add(pa01);
        spatialPresenceItems.Add(pa02);
        spatialPresenceItems.Add(pa05);
        spatialPresenceItems.Add(pa08);
        spatialPresenceItems.Add(ssm01);
        spatialPresenceItems.Add(ssm02);
        spatialPresenceItems.Add(ssm03);
        spatialPresenceItems.Add(ssm04);
        // shuffle items

        susItems = new List<QuestionnaireItem>();
        susItems.Add(sus01);
        susItems.Add(sus02);
        susItems.Add(sus03);
        susItems.Add(sus04);
        susItems.Add(sus05);
        susItems.Add(sus06);
        // shuffle items
    }
}
