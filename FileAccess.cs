using System.Text.Json;

namespace VanocniUkol;

/// <summary>
/// Provides functionality for saving and loading dialogue nodes in JSON format.
/// This class allows serializing dialogue data to a file and deserializing it from a file.
/// </summary>
public class FileAccess
{
    /// <summary>
    /// Saves a dialogue node to a JSON file at a predefined location.
    /// The dialogue includes the main body text, player choices, and terminal state.
    /// </summary>
    /// <param name="dialogueNode">The dialogue node to be serialized and saved to a file.</param>
    /// <param name="filePath">The path to the file where the dialogue will be saved.</param>
    public static void SaveDialogueToFileJson<T>(T dialogueNode, string filePath)
    {
        try
        {
            var json = JsonSerializer.Serialize(dialogueNode, new JsonSerializerOptions { WriteIndented = true });
            // Save the JSON content to the file
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                streamWriter.Write(json);
            }

            Console.WriteLine($"Dialogue has been saved to file: {filePath}");
#if UNITY_EDITOR
            Debug.log("Dialogue has been saved to file: " + filePath);
#endif
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occured while saving the dialogue: -> {e.Message}");
#if UNITY_EDITOR
            Debug.log("An error occured while saving the dialogue: -> " + e.Message);
#endif
        }
    }

    /// <summary>
    /// Loads a dialogue node from a JSON file at the specified location.
    /// The method deserializes the JSON content into a DialogueNode object.
    /// </summary>
    /// <param name="filePath">The path to the file from which the dialogue will be loaded.</param>
    /// <returns>A DialogueNode object deserialized from the JSON file, or null if the file does not exist or an error occurs during deserialization.</returns>
    public static T? LoadDialogueFromFileJson<T>(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
#if UNITY_EDITOR
            Debug.log("File not found: " + filePath);
#endif
        }
        var json = File.ReadAllText(filePath);
        var dialogueNode = JsonSerializer.Deserialize<T>(json);
        Console.WriteLine($"Dialogue has been loaded from file: {filePath}");
        return dialogueNode;
    }
}