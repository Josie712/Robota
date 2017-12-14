using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSettings : MonoBehaviour {

    public ReceivedEmail[] receivedEmails;
    public SentEmail[] sentEmails;

    public Stats baseRobotStats;
    public RobotPart[] robotParts;
    public Question[] questions;

    public GameObject incomingMailObject;
    public GameObject sentMailObject;
    public GameObject decisivenessBar;
    public GameObject peopleSkillsBar;
    public GameObject compartmentalizationBar;

    private void Start()
    {
        // InitializeEmail();
        InitializeStatBars();
    }

    private void InitializeEmail()
    {
        throw new NotImplementedException();
    }

    private void InitializeStatBars()
    {
        decisivenessBar.GetComponent<Slider>().value = baseRobotStats.decisiveness;
        peopleSkillsBar.GetComponent<Slider>().value = baseRobotStats.peopleSkills;
        compartmentalizationBar.GetComponent<Slider>().value = baseRobotStats.compartmentalization;
    }

}
