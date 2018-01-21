using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSettings : MonoBehaviour {

    private const float emailButtonsYOffset = -40f;

    public ReceivedEmail[] receivedEmails;
    public ArchiveEmail[] archiveEmails;

    public Stats baseRobotStats;
    public RobotPart[] robotParts;
    public Question[] questions;

    public GameObject incomingMailObject;
    public GameObject archiveMailObject;
    public GameObject emailButtonPrefab;
    public GameObject emailSubject;
    public GameObject emailText;
    public GameObject blueprintButton;

    private List<GameObject> receivedEmailButtons = new List<GameObject>();
    private List<GameObject> archiveEmailButtons = new List<GameObject>();

    private void Start()
    {
        InitializeEmail();
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
            emailSubject.GetComponent<Text>().text = email.subject;
            emailText.GetComponent<Text>().text = email.text;
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
        }
    }

    private void InitializeEmail()
    {
        InitializeReceivedEmail();
        InitializeArchiveEmail();
    }

}
