using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using OpenAI;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Threading;
using System.Linq;
using System.IO;

using static Parser.Parser;
using static Parser.Token;
using Parser;


public class ChatboxController : MonoBehaviour
{
    public TMP_InputField chatboxInputField;
    public TMP_Text chatboxText;
    public GameObject chatboxPanel;

    
   
    void Start()
    {
        // Make sure the chatbox is initially hidden
        chatboxPanel.SetActive(false);

        // Attach the function send GPT to the onValueChanged event
        
        chatboxInputField.onEndEdit.AddListener(run_cmd);
    }
    void OnEnable()
    {
        //Register InputField Events
        //chatboxInputField.onEndEdit.AddListener(SendRequest);
        //chatboxInputField.onValueChanged.AddListener(delegate { SendRequest(); });
    }
    void Update()
    {


        // Check if the "P" key is pressed
        if (Input.GetKeyDown(KeyCode.P) && !chatboxInputField.isFocused)
        {
            // Toggle the chatbox visibility
            ToggleChatbox();
            
        }




                  if (Input.GetKeyDown(KeyCode.P) && Input.GetKeyDown(KeyCode.Q))
                  {
                      chatboxPanel.SetActive(false);
                  }

            }

    


    void ToggleChatbox()
    {
        // Toggle the active state of the chatbox panel
        chatboxPanel.SetActive(!chatboxPanel.activeSelf);

        // Focus on the input field when showing the chatbox
        if (chatboxPanel.activeSelf)
        {
            chatboxInputField.Select();
            chatboxInputField.ActivateInputField();
        }
    }

    void DisplayInChatbox(string message)
    {
        // Append the message to the chatbox text
        chatboxText.text += $"\n{message}";

        // Scroll the chatbox text to the bottom to show the latest messages
        chatboxText.rectTransform.sizeDelta = new Vector2(chatboxText.rectTransform.sizeDelta.x, chatboxText.preferredHeight);
    }



    public void run_cmd(string args)
    {
        if(chatboxInputField.text =="" || chatboxInputField.text =="Recording..." || chatboxInputField.text == "Sending..."){
            return;
        }

        Debug.Log(args);

        System.Diagnostics.ProcessStartInfo start = new System.Diagnostics.ProcessStartInfo();
        /*
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======

>>>>>>> fd9243601247ca37fbd2a09f94610d3147e801ca
>>>>>>> 0129e15aa8eba30d997550e94370363c001466cb
        /*
        //Nom du script Python
        string PythonScriptName = "Assistant_pour_C#.py";
        //Recuperation de l'adresse du script
        string unityProjectPath = Path.GetDirectoryName(Application.dataPath);
        string scriptsFolderPath = Path.Combine(unityProjectPath, "Assets" + Path.DirectorySeparatorChar + "Scripts" + Path.DirectorySeparatorChar + PythonScriptName);
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 0129e15aa8eba30d997550e94370363c001466cb
        start.Arguments = string.Format(start.FileName + " \"{0}\"", args); */
        
        
        start.FileName = "python";
        start.FileName = "C:\\Users\\cloti\\anaconda3\\python";
        start.Arguments = string.Format("C:\\Users\\cloti\\Documents\\Cours\\PSC\\Python\\Assistant_pour_C#.py \"{0}\"", args);

        //start.Arguments = string.Format("C:\\Users\\cloti\\Documents\\Cours\\PSC\\Python\\Requ�te_GPT_pour_C#.py \"{0}\"", args);


        //start.Arguments = string.Format(start.FileName + " \"{0}\"", args); 
        //start.Arguments = string.Format(File + " \"{0}\"", args); 
        
        
 

        start.UseShellExecute = false;
        start.RedirectStandardOutput = true;
        using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(start))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                 string result = reader.ReadToEnd();
                 Debug.Log(result);
                 chatboxText.text = "GPT: " + result;

                //TEST CHATBOX
                //string test = "RIGHT(10); GOTO('BUTTON'); UP(3)";
                //chatboxText.text = "Test: " + test;

                List<Token> tokens = Parser.Parser.Tokenize(result);
                 List<Node> forest = Parser.Parser.Parse(tokens);
                 Parser.Parser.Execute(forest); // Renvoie null 
                


            }
        } 
    }

}
