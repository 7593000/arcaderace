using TMPro;
using UnityEngine;

public class TableCellScore : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _number;
    [SerializeField]
    private TMP_Text _name;
    [SerializeField]
    private TMP_Text _timeScore;


    public void CreateCell(string numbler, string name, string time)
    {
        _number.text = numbler;
        _name.text = name;
        _timeScore.text = time;
    }
}