
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{


    private Image _image;
    [SerializeField]
    private Color _normalColor;
    [SerializeField]
    private Texture _carTexture;
    private GarageEngine _engine;

    public bool StatusClicked = true;

    public string GetTextureName => _carTexture.name;

    public void ColorDefault(float opacity, bool statusSelect, GarageEngine engine)
    {

        _engine = engine;
        _normalColor = new Color(1f, 1f, 1f, opacity);
        StatusClicked = statusSelect;
    }



    private void Start()
    {
        _image = GetComponent<Image>();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!StatusClicked)
            _image.color = Color.white;

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!StatusClicked)
            _image.color = _normalColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        foreach (var image in _engine.GetImagesList)
        {
            if (image != this)
            {
                image.ResetClickState();
            }
        }

        StatusClicked = true;
        _image.color = Color.white;


        _engine.SetTexture(_carTexture);
    }

    public void ResetClickState()
    {
        StatusClicked = false;
        _image.color = _normalColor;
    }
}

