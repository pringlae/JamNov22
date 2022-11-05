using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "Data/DialogueData", order = 0)]
public class DialogueData : ScriptableObject
{
    public Speech[] speeches;
}

[System.Serializable]
public struct Speech
{
    public bool isPlayer;
    public string text;
}

