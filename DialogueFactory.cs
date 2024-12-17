namespace VanocniUkol;

public class DialogueFactory
{
    /// <summary>
    /// Creates a new dialogue node with the specified body text, player choices, and terminal state.
    /// </summary>
    /// <param name="bodyText">The main text content of the dialogue.</param>
    /// <param name="playerChoices">A dictionary of player choices, where keys are choice descriptions and values are subsequent dialogue nodes.</param>
    /// <param name="isTerminal">Indicates whether the dialogue node represents a terminal point in the dialogue.</param>
    /// <returns>Returns a constructed instance of <see cref="DialogueNode"/> with the provided body text, choices, and terminal state.</returns>
    public static DialogueNode CreateDialogue(string bodyText, Dictionary<string, string>? playerChoices,
        bool isTerminal = false)
    {
        //if no choices are made, return an empty dictionary
        playerChoices ??= new Dictionary<string, string>();

        return new DialogueNode(bodyText, playerChoices, isTerminal);
    }
}