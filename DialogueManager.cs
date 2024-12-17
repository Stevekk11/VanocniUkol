namespace VanocniUkol;

/// <summary>
/// The DialogueManager class implements the logic to handle and navigate through
/// a dialogue system consisting of a series of DialogueNode objects. It controls
/// and manages the flow of dialogue based on the player's input and the structure
/// of interconnected dialogue nodes.
/// </summary>
public class DialogueManager
{
#if UNITY_EDITOR
    public InputField inputField;
#endif
    private DialogueNode currentNode; // Current active node in the dialogue
    private readonly Dictionary<string, DialogueNode> dialogueNodes; // Mapping of node IDs to DialogueNode objects

    /// <summary>
    /// Constructs a DialogueManager with the starting node and the full dialogue graph.
    /// </summary>
    /// <param name="startingNodeId">The ID of the dialogue node where the dialogue should begin.</param>
    /// <param name="dialogueNodes">
    /// A dictionary containing all dialogue nodes in the graph, keyed by their unique IDs.
    /// </param>
    public DialogueManager(string startingNodeId, Dictionary<string, DialogueNode> dialogueNodes)
    {
        this.dialogueNodes = dialogueNodes;

        if (!dialogueNodes.TryGetValue(startingNodeId, out currentNode))
        {
            throw new ArgumentException($"Starting node with ID '{startingNodeId}' not found in dialogue graph.");
        }
    }

    /// <summary>
    /// Begins the dialogue sequence starting from the current dialogue node.
    /// The function handles the progression of dialogue by displaying the
    /// current dialogue text, presenting available player choices, and
    /// transitioning to subsequent dialogue nodes based on player input.
    /// The method continues navigating through the dialogue tree until a
    /// terminal node is reached or the flow is stopped.
    /// </summary>
    public void StartDialogue()
    {
        while (currentNode != null && !currentNode.isTerminal)
        {
            // Display the current dialogue
            currentNode.DisplayDialogue();

            // Display the player's choices
            currentNode.DisplayChoices();

            // Get and validate the user's input
            int choice = GetChoiceFromPlayer();
            if (choice < 1 || choice > currentNode.playerChoices.Count)
            {
#if UNITY_EDITOR
                UnityEngine.Debug.Log("Invalid choice. Please try again.");
#else
                Console.WriteLine("Invalid choice. Please try again.");
#endif
                continue;
            }
//abc
            // Get the choice text corresponding to the user's input
            string selectedKey = currentNode.playerChoices.Keys.ToList()[choice - 1];
            string nextNodeId = currentNode.playerChoices[selectedKey];

            // Transition to the next node (resolve it by ID)
            if (!dialogueNodes.TryGetValue(nextNodeId, out currentNode))
            {
                Console.WriteLine($"Error: Dialogue node with ID '{nextNodeId}' not found!");
                currentNode = null; // Break the loop by setting node to null
                return;
            }
        }

        // End of dialogue handling
        if (currentNode != null && currentNode.isTerminal)
        {
            currentNode.DisplayDialogue();
            Console.WriteLine("Dialogue has ended.");
        }
    }

    /// <summary>
    /// Prompts the player to input a numeric choice corresponding to available dialogue options.
    /// The method reads and parses user input, returning the selected choice as an integer.
    /// If the input is invalid or non-numeric, the method returns 0 as a fallback.
    /// </summary>
    /// <returns>
    /// An integer representing the player's chosen option, or 0 if the input is invalid.
    /// </returns>
    private int GetChoiceFromPlayer()
    {
        
#if UNITY_EDITOR
        UnityEngine.Debug.Log("Enter your choice: ");
        string input = inputField.text;
#else
        Console.Write("Enter your choice: ");
        string input = Console.ReadLine();
#endif
        return int.TryParse(input, out int choice) ? choice : 0;
    }
}