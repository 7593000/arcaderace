using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]
    private TableCellScore _cellPrefabs;
    [SerializeField]
    private InputTableScore _inputFieldPrefabs;
    private InputTableScore _inputFilesInTable;
    [SerializeField]
    private TableResult _tableParents;

    [SerializeField]
    private Button _saveButton;
    [SerializeField]
    private Button _exitButton;
    [SerializeField]
    private Image _imageBackGround;
    private bool _isStatusMenu = false;

    private SceneLoader _sceneLoader;
    /// <summary>
    /// дефолтные данные участников, для стартового заполнения таблицы результатов.
    /// </summary>
    private string _dataScore = "Василий,025143;Колян-Пуля,014210;НемногоТормоз,042351;ВоВаТурбо,020470;NoName,040521;Player777,040232; Termit, 040223; Terminator,013221;EHoT_B_KeDAX,025633; KapaTeJIb,045531 ";

    private List<Tuple<string , int>> _listScore = new();
    [SerializeField]
    private const string PLAYERMARKER = "player000000000000";

    private string _dataPlayerTime = string.Empty;
    private string _playerName = "Player";//дефолтное имя игрока, если не занесено имя
    private int _maxScoureCell = 9;


    public void InstallScoreTable( string time )
    {
        _dataPlayerTime = time;


        ShowTableScore();
        _imageBackGround.GetComponent<Image>().enabled = false;
        GetComponent<Canvas>().enabled = true;
        GetComponent<CanvasGroup>().interactable = true;
        _saveButton.gameObject.SetActive(true);
    }

    private void ShowMenu(bool status )
    {
        _saveButton.gameObject.SetActive(false);
        _imageBackGround.GetComponent<Image>().enabled = status;
        GetComponent<Canvas>().enabled = status;
        GetComponent<CanvasGroup>().interactable = status;
       
    }

    private void ShowTableScore()
    {
        AssemblingTable();
        CheckingLaceRanking();

        int count = 1;

        ScoreSorting( _listScore );
        foreach ( Tuple<string , int> data in _listScore )
        {

            string name = data.Item1;
            string timer = data.Item2.ToString();

            if ( name == PLAYERMARKER )
            {
                _inputFilesInTable = Instantiate( _inputFieldPrefabs , _tableParents.transform );
                _inputFilesInTable.CreateCell( ( count++ ).ToString() , ParceTime( _dataPlayerTime ) );
            }
            else
            {
                TableCellScore cellData = Instantiate( _cellPrefabs , _tableParents.transform );
                cellData.CreateCell( ( count++ ).ToString() , data.Item1 , ParceTime( data.Item2.ToString() ) );
            }





        }

    }
    private void OnValidate()
    {
      
        _sceneLoader = GetComponentInChildren<SceneLoader>();
    }

    private void Start()
    {
     


        if ( !DataPlayerPrefs.CheckedHasKey( SaveDataType.Score ) )
        {
            DataPlayerPrefs.Save( SaveDataType.Score , _dataScore );
        }


    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            _isStatusMenu = !_isStatusMenu;
            ShowMenu(_isStatusMenu);
        }
    }

    /// <summary>
    /// Проверка результата игрока с таблицей результатов , входит ли результат в 5ку
    /// </summary>
    private void CheckingLaceRanking()
    {
        List<Tuple<string , int>> newList = new();
       
        int tempTime = int.Parse( _dataPlayerTime );


        if ( tempTime > _listScore[ ^1 ].Item2 )
        {
            return;// Если время игрока больше времени в последнем элементе списка, игрок не вошел в топ 5
        }




        // Если время игрока меньше времени в первом элементе списка, он занимает первое место


        if ( tempTime < _listScore[ 0 ].Item2 )
        {
            newList.Add( Tuple.Create( PLAYERMARKER , tempTime ) );
            Debug.Log( "Первый" );

            for ( int i = 0; i < Math.Min( _maxScoureCell , _listScore.Count ); i++ )
            {
                newList.Add( _listScore[ i ] );
            }

        }
        else
        {
            bool playerAdded = false;
            Debug.Log( "Поиск места" );
            // Проходимся по списку и находим место, куда вставить результат игрока
            for ( int i = 0; i < Math.Min( _maxScoureCell , _listScore.Count ); i++ )

            {

                if ( tempTime >= _listScore[ i ].Item2 && !playerAdded )
                {
                    newList.Add( Tuple.Create( PLAYERMARKER , tempTime ) );
                    playerAdded = true;
                }

                newList.Add( _listScore[ i ] );
            }
        }

        // Обновляем список результатов
        _listScore = newList;
    }


    private string ParceTime( string timeString )
    {

        if ( timeString.Length < 6 )
        {
            timeString = 0 + timeString;
        }

        string timeParc = string.Empty;
        for ( int i = 0; i < timeString.Length; i += 2 )
        {

            if ( i > timeString.Length )
            {
                return timeParc;
            }

            timeParc += timeString.Substring( i , 2 );

            if ( i != timeString.Length - 2 )
            {
                timeParc += ":";
            }
        }
        return timeParc;
    }


    /// <summary>
    /// Построить таблицу с данными
    /// </summary>
    private void AssemblingTable()
    {
        if ( DataPlayerPrefs.CheckedHasKey( SaveDataType.Score ) )
        {
            _dataScore = DataPlayerPrefs.Load( SaveDataType.Score );
            ParsingDataScore( _dataScore );
        }
        else
        {
            ParsingDataScore( _dataScore );
            DataPlayerPrefs.Save( SaveDataType.Score , _dataScore );
        }

    }

    private void ParsingDataScore( string dataScore )
    {



        string[] tempData = dataScore.Split( ';' );

        foreach ( string data in tempData )
        {

            string[] temp = data.Split( ',' );

            _listScore.Add( Tuple.Create( temp[ 0 ] , int.Parse( temp[ 1 ] ) ) );


        }

        ScoreSorting( _listScore );

    }

    private void ScoreSorting( List<Tuple<string , int>> scoreList )
    {


        scoreList.Sort( ( a , b ) => a.Item2.CompareTo( b.Item2 ) );


    }
    /// <summary>
    /// Внести данные в таблицу
    /// </summary>
    public void WritingDataToTable()
    {
        string tempDataScore = string.Empty;
        string namePlayer = _playerName;

        int count = 0;

        if ( _inputFilesInTable.GetInputText.text.Length > 0 )
        {
            namePlayer = _inputFilesInTable.GetInputText.text;
        }

        foreach ( Tuple<string , int> data in _listScore )
        {
            if ( data.Item1 == PLAYERMARKER )
            {
                tempDataScore += $"{namePlayer},{data.Item2}";
            }
            else
            {
                tempDataScore += $"{data.Item1},{data.Item2}";
            }


            if ( count != _listScore.Count - 1 )
            {
                tempDataScore += ";";
            }
            count++;
        }

        Debug.Log( tempDataScore );




        DataPlayerPrefs.Save( SaveDataType.Score , tempDataScore );
    }

    public void GoToGarage()
    {
        _sceneLoader.LoadSceneAsync( "Garage" );
    }

    public void Restart()
    {
        _sceneLoader.LoadSceneAsync( "Track01" );
    }

    #region Exit Game
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR
    Application.Quit();
#endif
    }
    #endregion

}
