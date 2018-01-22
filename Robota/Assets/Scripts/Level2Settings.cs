using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level2Settings : MonoBehaviour {

    private const float emailButtonsYOffset = -40f;

    public int balanceThreshold; // if the difference of stats between the most and second most dominant stat is less than or equal to this, the robot is considered balanced

    public ReceivedEmail[] receivedEmails;
    public ArchiveEmail[] archiveEmails;

    public HRStats baseRobotStats;
    public RobotPart[] robotParts;
    public Question[] questions;

    public GameObject incomingMailObject;
    public GameObject archiveMailObject;
    public GameObject emailButtonPrefab;
    public GameObject emailSubject;
    public GameObject emailText;
    public GameObject archiveEmailSubject;
    public GameObject archiveEmailText;
    public GameObject blueprintButton;

    
    public NewsArticle[] newsArticles;
    public GameObject newsNameObject;
    public GameObject newsHeadlineObject;
    public GameObject newsTextObject;
    public GameObject newsRobotHead;
    public GameObject newsRobotTorso;
    public GameObject newsRobotArms;
    public GameObject newsRobotLegs;

    private List<GameObject> receivedEmailButtons = new List<GameObject>();
    private List<GameObject> archiveEmailButtons = new List<GameObject>();

    private void Start()
    {
        InitializeNews();
        InitializeEmail();
    }

    private static bool CurrentLevelIsNamed(string name)
    {
        return SceneManager.GetActiveScene().name == name;
    }

    private void InitializeNews()
    {
        if (CurrentLevelIsNamed("Level1"))
        {
            return;
        }

        newsRobotHead.GetComponent<Image>().sprite = PreviousLevel.headSprite;
        newsRobotTorso.GetComponent<Image>().sprite = PreviousLevel.torsoSprite;
        newsRobotArms.GetComponent<Image>().sprite = PreviousLevel.armsSprite;
        newsRobotLegs.GetComponent<Image>().sprite = PreviousLevel.legsSprite;

        NewsArticle article = newsArticles[PreviousLevel.outcome];
        newsNameObject.GetComponent<Text>().text = article.newsName;
        newsHeadlineObject.GetComponent<Text>().text = article.newsHeadline;
        newsTextObject.GetComponent<Text>().text = article.newsText;
    }

    private void CreateReceivedEmailButtonFor(ReceivedEmail email)
    {
        GameObject emailButton = Instantiate(emailButtonPrefab, incomingMailObject.transform, false);
        emailButton.transform.Translate(new Vector3(0, emailButtonsYOffset * receivedEmailButtons.Count, 0));
        receivedEmailButtons.Add(emailButton);
        emailButton.GetComponentInChildren<Text>().text = "Message " + receivedEmailButtons.Count;
        emailButton.SetActive(true);

        emailButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            emailSubject.GetComponent<Text>().text = email.subject;
            emailText.GetComponent<Text>().text = email.text;
        });
        if (email.contract)
        {
            emailButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                blueprintButton.GetComponent<Button>().interactable = true;
            });
        }
    }

    private void InitializeReceivedEmail()
    {
        receivedEmailButtons.ForEach<GameObject>(Destroy);
        receivedEmailButtons.Clear();

        foreach (ReceivedEmail email in receivedEmails)
        {
            CreateReceivedEmailButtonFor(email);
        }

        receivedEmailButtons[0].GetComponent<Button>().onClick.Invoke();
    }

    private void CreateArchiveEmailButtonFor(ArchiveEmail email)
    {
        GameObject emailButton = Instantiate(emailButtonPrefab, archiveMailObject.transform, false);
        emailButton.transform.Translate(new Vector3(0, emailButtonsYOffset * archiveEmailButtons.Count, 0));
        archiveEmailButtons.Add(emailButton);
        emailButton.GetComponentInChildren<Text>().text = "Message " + archiveEmailButtons.Count;
        emailButton.SetActive(true);

        emailButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            archiveEmailSubject.GetComponent<Text>().text = email.subject;
            archiveEmailText.GetComponent<Text>().text = email.text;
        });
    }

    private void InitializeArchiveEmail()
    {
        archiveEmailButtons.ForEach<GameObject>(Destroy);
        archiveEmailButtons.Clear();

        foreach (ArchiveEmail email in archiveEmails)
        {
            CreateArchiveEmailButtonFor(email);
        } 

        if (archiveEmails.Length == 0)
        {
            GameObject emailButton = Instantiate(emailButtonPrefab, archiveMailObject.transform, false);
            emailButton.SetActive(true);
            emailButton.GetComponentInChildren<Text>().text = "No messages";
        } else
        {
            archiveEmailButtons[0].GetComponent<Button>().onClick.Invoke();
        }
    }

    private void InitializeEmail()
    {
        InitializeReceivedEmail();
        InitializeArchiveEmail();
    }

    public int EvaluateRobot(Stats stats)
    {
        stats = stats.AtLeastZero();
        List<KeyValuePair<int, int>> orderedStats = new List<KeyValuePair<int, int>>();

        for (int i = 1; i <= stats.StatCount(); i++)
        {
            orderedStats.Add(new KeyValuePair<int, int>(stats.GetStatByIndex(i), i));
        }
        orderedStats.Sort((x, y) => x.Key.CompareTo(y.Key));
        orderedStats.Reverse();

        if (orderedStats[0].Key <= orderedStats[1].Key + balanceThreshold)
        {
            return 0;
        }

        return orderedStats[0].Value;
    }

}
