using System.Text.Json.Serialization;

namespace VanocniUkol;

/// <summary>
/// Represents a dialogue node in a dialogue system, including the body text,
/// possible player choices, and whether it is a terminal node.
/// </summary>

public class DialogueNode
{
    //data structure for storing dialogue nodes.
    // Uses graphs.
    [JsonInclude]
    public string bodyText;
    [JsonInclude]
    public Dictionary<string, string> playerChoices;
    [JsonInclude]
    public bool isTerminal;


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="bodyText"></param>
    /// <param name="playerChoices"></param>
    /// <param name="isTerminal"></param>
    public DialogueNode(string bodyText, Dictionary<string, string> playerChoices, bool isTerminal)
    {
        this.bodyText = bodyText;
        this.playerChoices = playerChoices;
        this.isTerminal = isTerminal;
    }

    /// <summary>
    /// Displays the dialogue text.
    /// </summary>
    public void DisplayDialogue()
    {
        Console.WriteLine(bodyText);
#if UNITY_EDITOR
        Debug.log(bodyText);
#endif
    }

    /// <summary>
    /// Displays the available choices.
    /// </summary>
    public void DisplayChoices()
    {
        if (playerChoices.Count > 0)
        {
            Console.WriteLine("Your choices:");
            #if UNITY_EDITOR
            Debug.log("Your choices:");
            #endif
            int i = 1;
            foreach (var choice in playerChoices.Keys)
            {
            #if UNITY_EDITOR
                UnityEngine.Debug.Log($"{i}: {choice}");
            #else
                Console.WriteLine($"{i}: {choice}");
            #endif
                i++;
            }
        }
        else
        {
            #if UNITY_EDITOR
            Debug.log("There are no choices available.");
            #endif
            Console.WriteLine("There are no choices available.");
        }
    }
}