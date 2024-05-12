using UnityEngine;

/// <summary>
/// Визуализация изменения настроек авто 
/// </summary>
public class CarGarage : MonoBehaviour
{
    [SerializeField]
    private Transform _body;
    private Material _carMaterial;
    [SerializeField]
    private Transform[] _wheels;

    public Transform GetBody => _body;

    public void SetClearance(float value)
    {
        Vector3 newPosition = _body.localPosition;
        newPosition.y = value;
        _body.localPosition = newPosition;
    }

    public void SetCollapse(float value)
    {
        foreach (var _wheel in _wheels)
        {
            var angles = _wheel.rotation.eulerAngles;
            angles.y = value;
            _wheel.transform.localRotation = Quaternion.Euler(angles);
        }
    }

    public void SetTexture( string texture )
    {
        _carMaterial = _body.GetComponent<MeshRenderer>().material;

       
        if ( _carMaterial == null )
            return;

        Texture textureCar = Resources.Load<Texture>( "Textures/" + texture );
       
        if ( textureCar != null )
        {
            _carMaterial.mainTexture = textureCar;
        }
        


    }

    


}
