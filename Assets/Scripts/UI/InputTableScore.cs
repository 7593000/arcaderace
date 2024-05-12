using TMPro;
using UnityEngine;

public class InputTableScore  : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _number;
    [SerializeField]
    private TMP_InputField _inputText;
    [SerializeField]
    private TMP_Text _timeScore;


    public void CreateCell(string numbler, string time)
    {
        _number.text = numbler;
       
        _timeScore.text = time;
    }

    public TMP_InputField GetInputText { get => _inputText; }

}
