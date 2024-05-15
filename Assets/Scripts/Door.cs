using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;

using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Networking;
using OpenAI;
using Unity.VisualScripting;
using System.Threading;
using System.Linq;
using System.IO;

using static Parser.Parser;
using static Parser.Token;
using Parser;


public class Door : MonoBehaviour
    
{
    string layerName = "Door";

    public void Open()
    {
        TilemapCollider2D tilemapCollider = GetComponent<TilemapCollider2D>();
        TilemapRenderer tilemapRenderer = GetComponent<TilemapRenderer>();

        tilemapCollider.enabled = false;
        tilemapRenderer.enabled = false;
    }

    public void Close()
    {
        TilemapCollider2D tilemapCollider = GetComponent<TilemapCollider2D>();
        TilemapRenderer tilemapRenderer = GetComponent<TilemapRenderer>();

        tilemapCollider.enabled = true;
        tilemapRenderer.enabled = true;
    }


}
