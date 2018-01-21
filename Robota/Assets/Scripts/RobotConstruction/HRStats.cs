using System;

[Serializable]
public class HRStats : Stats {

    public int decisiveness;
    public int peopleSkills;
    public int compartmentalization;

    public HRStats(int decisiveness, int peopleSkills, int compartmentalization)
    {
        this.decisiveness = decisiveness;
        this.peopleSkills = peopleSkills;
        this.compartmentalization = compartmentalization;
    }

    public static HRStats operator+(HRStats s1, HRStats s2)
    {
        return new HRStats(
            s1.decisiveness + s2.decisiveness,
            s1.peopleSkills + s2.peopleSkills,
            s1.compartmentalization + s2.compartmentalization);
    }

    public static HRStats operator-(HRStats s1, HRStats s2)
    {
        return new HRStats(
            s1.decisiveness - s2.decisiveness,
            s1.peopleSkills - s2.peopleSkills,
            s1.compartmentalization - s2.compartmentalization);
    }

    public Stats AtLeastZero()
    {
        return new HRStats(
            Math.Max(decisiveness, 0),
            Math.Max(peopleSkills, 0),
            Math.Max(compartmentalization, 0));
    }

    public int StatCount()
    {
        return 3;
    }

    public int GetStatByIndex(int index)
    {
        switch (index)
        {
            case 1: return decisiveness;
            case 2: return peopleSkills;
            case 3: return compartmentalization;
            default: throw new ArgumentOutOfRangeException("index", index, "Acceptable values are 1, 2, or 3.");
        }
    }

    public override string ToString()
    {
        return "HRStats(decisiveness = " + decisiveness + ", peopleSkills = " + peopleSkills + ", compartmentalization = " + compartmentalization + ")";
    }

}
