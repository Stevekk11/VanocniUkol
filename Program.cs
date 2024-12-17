using System;
using System.Collections.Generic;

namespace VanocniUkol;

class Program
{
    static void Main(string[] args)
    {

        // Define a sample dialogue graph
        var dialogueGraph = new Dictionary<string, DialogueNode>
        {
            {
                "Node1", new DialogueNode(
                    "Welcome to the dialogue! What would you like to do?",
                    new Dictionary<string, string>
                    {
                        { "Go to Middle", "Node2" },
                        { "End the Conversation", "Node3" }
                    },
                    false
                )
            },
            {
                "Node2", new DialogueNode(
                    "You are in the middle of the conversation. Where would you like to go next?",
                    new Dictionary<string, string>
                    {
                        { "Go back to Start", "Node1" },
                        { "End the Conversation", "Node3" }
                    },
                    false
                )
            },
            {
                "Node3", new DialogueNode(
                    "The conversation ends here.",
                    new Dictionary<string, string>(),
                    true
                )
            }
        };

        // Create a DialogueManager and start the dialogue
        var dialogueManager = new DialogueManager("Node1", dialogueGraph);
       // dialogueManager.StartDialogue();

    }
}