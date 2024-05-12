using System.Collections;
using TMPro;
using UnityEngine;

 
    public class Speedometer : MonoBehaviour
    {
        private const float c_convertMeterInSecFromKmInH = 3.6f; // 60 * 60 / 1000f;
        [SerializeField]
        private Transform _player;

        [SerializeField]
        private float _maxSpeed = 300f;
        [SerializeField]
        private Color _minColor = Color.yellow;
        [SerializeField]
        private Color _maxColor = Color.red;

        [Space]
        [SerializeField]
        private float _delay = 0.3f;
        [SerializeField]
        private TMP_Text _text;

        private void Start()
        {
            StartCoroutine( Speed() );

        }

        private IEnumerator Speed()
        {
            var prevPos = _player.position;
            while ( true )
            {
                var distance = Vector3.Distance( prevPos , _player.position );
                float speed = Mathf.Round( distance / _delay * c_convertMeterInSecFromKmInH );//Возвращает значение f, округленное до ближайшего целого числа.
                _text.color = Color.Lerp( _minColor , _maxColor , speed / _maxSpeed );
                _text.text = speed.ToString();
                prevPos = _player.position;
                yield return new WaitForSeconds( _delay );
            }
        }
    }
 
