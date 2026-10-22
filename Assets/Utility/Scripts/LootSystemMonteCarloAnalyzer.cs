using UnityEngine;

public class LootSystemMonteCarloAnalyzer : MonoBehaviour
{
    [Header("Simulation Settings")]
    [SerializeField] int numPlayers = 100000;
    [SerializeField] int packsRippedPerPlayer = 200;
    [SerializeField] int randomSeed = 1995;

    [Header("References")]
    [SerializeField] PityLootSystem cardLootSystem;

    [Header("Latest Results")]
    [SerializeField] SimulationResults latestResults;

    void Start()
    {
        latestResults = RunBasicSimulation();
        LogSingleSystemResult("BASIC CARD SYSTEM", latestResults);
    }

    void LogSingleSystemResult(string title, SimulationResults results)
    {
        Debug.Log($"==== {title} ====\n" +
                  $"Average Prototypes: {results.prototypeAverage:F4}\n" +
                  $"Percent of Players with 0 Prototypes: {results.percentWithZeroPrototypes:F4}%\n" +
                  $"One in X Players: {results.oneInXPlayers:F4}\n" +
                  $"Min Prototypes: {results.minPrototypes}\n" +
                  $"Max Prototypes: {results.maxPrototypes}\n");
    }

    static SimulationResults BuildResult(int[] prototypeCounts)
    {
        var totalPrototypes = 0;
        var zeroPrototypesPlayers = 0;
        var maxPrototypes = int.MinValue;
        var minPrototypes = int.MaxValue;

        for (var i = 0; i < prototypeCounts.Length; i++)
        {
            var count = prototypeCounts[i];
            totalPrototypes += count;

            if (count == 0) zeroPrototypesPlayers++;
            if (count > maxPrototypes) maxPrototypes = count;
            if (count < minPrototypes) minPrototypes = count;
        }

        var average = (float)totalPrototypes / prototypeCounts.Length;
        var zeroPercent = (float)zeroPrototypesPlayers / prototypeCounts.Length * 100f;
        var oneInX = zeroPercent > 0f ? 0f : 100f / zeroPercent;
        //var histogram = BuildHistogramPrototype(prototypeCounts, 12);

        return new SimulationResults(average, zeroPercent, oneInX, minPrototypes, maxPrototypes);
    }

    SimulationResults RunBasicSimulation()
    {
        var random = new System.Random(randomSeed);
        var prototypeCounts = new int[numPlayers];

        for (var player = 0; player < numPlayers; player++)
        {
            var prototypeCount = 0;
            for (var pack = 0; pack < packsRippedPerPlayer; pack++)
            {
                if (cardLootSystem.Roll((float)random.NextDouble()) == LootRarity.Prototype)
                {
                    prototypeCount++;
                }
            }
            prototypeCounts[player] = prototypeCount;
        }
        return BuildResult(prototypeCounts);

    }
}

[System.Serializable]
public struct SimulationResults
{
    public float prototypeAverage;
    public float percentWithZeroPrototypes;
    public float oneInXPlayers;
    public int minPrototypes;
    public int maxPrototypes;
    //[TextArea(4, 24)] public string histogram;

    public SimulationResults(float prototypeAverage, float percentWithZeroPrototypes, float oneInXPlayers, int minPrototypes, int maxPrototypes)
    {
        this.prototypeAverage = prototypeAverage;
        this.percentWithZeroPrototypes = percentWithZeroPrototypes;
        this.oneInXPlayers = oneInXPlayers;
        this.minPrototypes = minPrototypes;
        this.maxPrototypes = maxPrototypes;
        //this.histogram = histogram;
    }
}
