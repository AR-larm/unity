using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textInput_script : MonoBehaviour
{
    private Text InputString;
    public InputField inputtext;

    // Start is called before the first frame update
    void Start()
    {
        inputtext = gameObject.GetComponent<InputField>();
        inputtext.onEndEdit.AddListener(delegate { LockInput(inputtext); });
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LockInput(InputField input)
    {
        if (input.text.Equals(RandomNumberGen_script.RanString.text))
        {
            Application.Quit();
        }
    }
}
