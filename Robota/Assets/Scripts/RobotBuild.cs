using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RobotBuild : MonoBehaviour {

    public LevelSettings levelSettings;

    public GameObject headObject;
    public GameObject torsoObject;
    public GameObject armsObject;
    public GameObject legsObject;

    public GameObject nextButton;
    public GameObject decisivenessBar;
    public GameObject peopleSkillsBar;
    public GameObject compertmentalizationBar;
    public GameObject questionHeaderObject;
    public GameObject questionTextObject;

    public Sprite blankSprite;

    private int currentQuestion;
    private Stats currentStats;

    public void UserClickPart() {
        RecalculateStats();

        if (RobotFullyBuilt())
        {
            nextButton.GetComponent<Button>().interactable = true;
        }
    }

    public void ResetQuestions()
    {
        currentQuestion = -1;
        DisplayNextQuestion();
        RecalculateStats();
    }

    private bool PartObjectSelected(GameObject part) {
        return part.GetComponent<Image>().sprite != blankSprite;
    }

    private bool RobotFullyBuilt() {
        GameObject[] partObjects = { headObject, torsoObject, armsObject, legsObject };
        return partObjects.All(PartObjectSelected);
    }

    private void RecalculateStats() {
        GameObject[] partObjects = { headObject, torsoObject, armsObject, legsObject };

        currentStats = levelSettings.baseRobotStats;
        (from part in partObjects
         select part.GetComponent<Image>().sprite)
            .ForEach(AddStatsOfSprite);

        UpdateStatBars(currentStats.AtLeastZero());
    }

    private void AddStatsOfSprite(Sprite sprite) {
        Stats statsToAdd = (from part in levelSettings.robotParts where part.sprite == sprite select part.stats).FirstOrDefault();
        if (statsToAdd == default(Stats)) {
            statsToAdd = new Stats(0, 0, 0);
        }
        currentStats += statsToAdd;
    }

    private void UpdateStatBars(Stats stats) {
        decisivenessBar.GetComponent<Slider>().value = stats.decisiveness;
        peopleSkillsBar.GetComponent<Slider>().value = stats.peopleSkills;
        compertmentalizationBar.GetComponent<Slider>().value = stats.compartmentalization;
    }

    private void DisplayNextQuestion() {
        currentQuestion++;
        
    }

}
