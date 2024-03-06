#if UNITY_EDITOR
using LazyFramework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SolutionGenerator : MonoBehaviour
{
    [SerializeField] LevelMap levelMap;
    [SerializeField] public List<string> ListDeadSolution;
    [SerializeField] public List<string> ListSolutions = new List<string>();
    [SerializeField] public List<string> ListWinSolution;
    [SerializeField] int maxMove = 9;
    [SerializeField] int maxConsecutive = 3;
    private List<string> baseListSolution = new List<string>();
    private void Start()
    {
        GenerateStrings(maxMove , maxConsecutive);
    }
    public void Regenerate()
    {
        ListDeadSolution.Clear();
        ListSolutions.Clear();
        ListWinSolution.Clear();

        //cache data from base
        ListSolutions = new List<string>(baseListSolution);
        Bug.Log("regenerate solution");
    }
    void GenerateStrings(int maxLength , int maxConsecutive)
    {
        char[] digits = { '1' , '2' , '3' , '4' };

        GenerateStringsRecursive(baseListSolution , "" , maxLength , maxConsecutive , digits , 0 , 0);
        Bug.Log($"{baseListSolution.Count} solutions created");
        Regenerate();

        //post event
        GameServices.OnAllSolutionGenerated();
    }

    void GenerateStringsRecursive(List<string> result , string currentString , int maxLength , int maxConsecutive , char[] digits , int currentLength , int consecutiveCount)
    {
        if (currentLength==maxLength)
        {
            result.Add(currentString);
            return;
        }

        for (int i = 0; i<digits.Length; i++)
        {
            if (consecutiveCount<maxConsecutive||currentString.Length==0||currentString[currentString.Length-1]!=digits[i])
            {
                GenerateStringsRecursive(result , currentString+digits[i] , maxLength , maxConsecutive , digits , currentLength+1 , (currentString.Length>0&&currentString[currentString.Length-1]==digits[i]) ? consecutiveCount+1 : 1);
            }
        }
    }
    void RemoveStringsStartingWith(string prefix)
    {
        ListSolutions.RemoveAll(s => s.StartsWith(prefix));
    }
    public void CleanSolution()
    {
        foreach (var prefix in ListDeadSolution)
        {
            RemoveStringsStartingWith(prefix);
        }

        foreach (var prefix in ListWinSolution)
        {
            RemoveStringsStartingWith(prefix);
        }

        //if has 1 win solution, clear all solution base on that
        if(GetBestSolution() != "")
        {
            int bestSolutionLength = GetBestSolution().Length;
            foreach (var solution in ListDeadSolution)
            {
                RemoveStringsStartingWith (solution.Substring(0, bestSolutionLength));
            }
        }

    }
    private string GetBestSolution()
    {
        var minLength = 100;
        string bestSolution = "";
        foreach (var solution in ListWinSolution)
        {
            if (solution.Length<minLength)
            {
                bestSolution=solution;
                minLength=solution.Length;
            }
        }
        return bestSolution;
    }

    [CustomEditor(typeof(SolutionGenerator))]
    public class SolutionGeneratorGUI : Editor
    {

        public SolutionGenerator SolutionGenerator;

        private void OnEnable()
        {
            SolutionGenerator=(SolutionGenerator)target;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();
            if (GUILayout.Button("Generate solutions"))
            {
                SolutionGenerator.ListSolutions.Clear();
                SolutionGenerator.Regenerate();
            }
        }
    }
}
#endif