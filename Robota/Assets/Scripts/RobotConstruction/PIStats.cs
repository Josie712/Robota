using System;

[Serializable]
public class PIStats : Stats
{

    private readonly string[] statNames = { "Wit", "Mindfulness", "Resistance to Injury" };

    public int wit;
    public int mindfulness;
    public int resistance;

    public PIStats(int wit, int mindfulness, int resistance)
    {
        this.wit = wit;
        this.mindfulness = mindfulness;
        this.resistance = resistance;
    }

    public static PIStats operator +(PIStats s1, PIStats s2)
    {
        return new PIStats(
            s1.wit + s2.wit,
            s1.mindfulness + s2.mindfulness,
            s1.resistance + s2.resistance);
    }

    public static PIStats operator -(PIStats s1, PIStats s2)
    {
        return new PIStats(
            s1.wit - s2.wit,
            s1.mindfulness - s2.mindfulness,
            s1.resistance - s2.resistance);
    }

    public Stats AtLeastZero()
    {
        return new PIStats(
            Math.Max(wit, 0),
            Math.Max(mindfulness, 0),
            Math.Max(resistance, 0));
    }

    public int StatCount()
    {
        return 3;
    }

    public int GetStatByIndex(int index)
    {
        switch (index)
        {
            case 1: return wit;
            case 2: return mindfulness;
            case 3: return resistance;
            default: throw new ArgumentOutOfRangeException("index", index, "Acceptable values are 1, 2, or 3.");
        }
    }

    public string GetStatNameByIndex(int index)
    {
        return statNames[index - 1];
    }

    public override string ToString()
    {
        return "HRStats(decisiveness = " + wit + ", peopleSkills = " + mindfulness + ", compartmentalization = " + resistance + ")";
    }

}
