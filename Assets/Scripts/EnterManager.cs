using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterManager : MonoBehaviour
{
    public static string name;

    public InputField inputField;
    public void OnClickEnter()
    {
        name = inputField.text;
        Debug.Log($"name : {name}");
        SceneManager.LoadScene("FieldScene");
    }
}
