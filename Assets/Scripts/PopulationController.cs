using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationController : MonoBehaviour
{
    public GameObject beesPrefab;
    public int populationSize = 100;
    public int genomeLenght;
    public float cutoff = 0.3f;
    public Transform spawnPoint;
    public Transform end;

    private List<GeneticPathfinder> population = new List<GeneticPathfinder>();

    private void Start()
    {
        InitPopulation();
    }

    private void Update()
    {
        if (!HasActive())
        {
            NextGeneration();
        }
    }

    private void InitPopulation()
    {
        for (int i = 0; i < populationSize; i++)
        {
            GameObject go = Instantiate(beesPrefab, spawnPoint.position, Quaternion.identity);
            go.GetComponent<GeneticPathfinder>().InitCreature(new DNA(genomeLenght), end.position);
            population.Add(go.GetComponent<GeneticPathfinder>());
        }
    }

    private void NextGeneration()
    {
        int survivorCut = Mathf.RoundToInt(populationSize * cutoff);
        List<GeneticPathfinder> survivors = new List<GeneticPathfinder>();
        for (int i = 0; i < survivorCut; i++)
        {
            survivors.Add(GetFittest());
        }
        for (int i = 0; i < population.Count; i++)
        {
            Destroy(population[i].gameObject);
        }
        population.Clear();

        while (population.Count < populationSize)
        {
            for (int i = 0; i < survivors.Count; i++)
            {
                GameObject go = Instantiate(beesPrefab, spawnPoint.position, Quaternion.identity);
                go.GetComponent<GeneticPathfinder>().InitCreature(new DNA(survivors[i].dna, survivors[Random.Range(0, 10)].dna), end.position);
                population.Add(go.GetComponent<GeneticPathfinder>());
                if (population.Count >= populationSize)
                {
                    break;
                }
            }
        }
        for (int i = 0; i < survivors.Count; i++)
        {
            Destroy(survivors[i].gameObject);
        }
    }

    private GeneticPathfinder GetFittest()
    {
        float maxFitness = float.MinValue;
        int index = 0;
        for (int i = 0; i < population.Count; i++)
        {
            if (population[i].GetFitness > maxFitness)
            {
                maxFitness = population[i].GetFitness;
                index = i;
            }
        }
        GeneticPathfinder fittest = population[index];
        population.Remove(fittest);
        return fittest;
    }
    private bool HasActive()
    {
        for (int i = 0; i < population.Count; i++)
        {
            if (!population[i].hasFinished)
            {
                return true;
            }
        }
        return false;
    }
}
