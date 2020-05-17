﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class RandomNumberGen_script : MonoBehaviour
{
    private const string NonCapitilizedLetters = "abcdefghijklmnopqrstuvwxyz";
    private const string Numbers = "0123456789z";
    private System.Random random;

    public static Text RanString;

    // Start is called before the first frame update
    void Start()
    {
        RanString = GameObject.Find("randomnum").GetComponent<Text>();

        random = new System.Random();

        List<string> charSets = new List<string>();
        charSets.Add(NonCapitilizedLetters);
        charSets.Add(Numbers);

        int length;
        bool canConvert;

        canConvert = int.TryParse("6", out length);

        // length = int.Parse(txtLength.Text); // 포맷 에러 가능성

        if (canConvert == false || length <= 0)
        {
            return;
        }

        StringBuilder sb = new StringBuilder();

        while (length-- > 0)
        {
            int charSet = random.Next(charSets.Count);
            int index = random.Next(charSets[charSet].Length);
            sb.Append(charSets[charSet][index]);
        }

        RanString.text = sb.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}