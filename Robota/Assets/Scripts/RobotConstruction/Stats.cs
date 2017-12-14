using System;

[System.Serializable]
public class Stats {

    public int decisiveness;
    public int peopleSkills;
    public int compartmentalization;

    public Stats(int decisiveness, int peopleSkills, int compartmentalization)
    {
        this.decisiveness = decisiveness;
        this.peopleSkills = peopleSkills;
        this.compartmentalization = compartmentalization;
    }

    public static Stats operator+(Stats s1, Stats s2)
    {
        return new Stats(
            s1.decisiveness + s2.decisiveness,
            s1.peopleSkills + s2.peopleSkills,
            s1.compartmentalization + s2.compartmentalization);
    }

    public static Stats operator-(Stats s1, Stats s2)
    {
        return new Stats(
            s1.decisiveness - s2.decisiveness,
            s1.peopleSkills - s2.peopleSkills,
            s1.compartmentalization - s2.compartmentalization);
    }

    public Stats AtLeastZero()
    {
        return new Stats(
            Math.Max(decisiveness, 0),
            Math.Max(peopleSkills, 0),
            Math.Max(compartmentalization, 0));
    }

}
