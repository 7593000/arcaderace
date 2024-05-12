
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : CarComponent
{

    private void OnEnable()
    {
        FinishEngine.OnTimerWork += OnStatusMovement;
        StartCountdown.OnStartRace += OnStatusMovement;
    }
    private void OnDisable()
    {
        FinishEngine.OnTimerWork -= OnStatusMovement;
        StartCountdown.OnStartRace -= OnStatusMovement;
    }

    protected override void SetupCarSettings()
    {
        if ( DataPlayerPrefs.CheckedHasKey( SaveDataType.SettingsPlayer ) )
        {
            Dictionary<string , string> dictionary = DataPlayerPrefs.ParceHasKey();


            float collapse = float.IsNaN( DataPlayerPrefs.ParceFloat( dictionary , DataSettings.Collapse ) ) ? _settings.GetCollapse : DataPlayerPrefs.ParceFloat( dictionary , DataSettings.Collapse );
            float stiffness = float.IsNaN( DataPlayerPrefs.ParceFloat( dictionary , DataSettings.Stiffness ) ) ? _settings.GetStiffness : DataPlayerPrefs.ParceFloat( dictionary , DataSettings.Stiffness );
            float clearance = float.IsNaN( DataPlayerPrefs.ParceFloat( dictionary , DataSettings.Clearance ) ) ? _settings.GetClearance : DataPlayerPrefs.ParceFloat( dictionary , DataSettings.Clearance );
            float damper = float.IsNaN( DataPlayerPrefs.ParceFloat( dictionary , DataSettings.Damper ) ) ? _settings.GetDamper : DataPlayerPrefs.ParceFloat( dictionary , DataSettings.Damper );
            if ( dictionary.TryGetValue( DataSettings.Texture.ToString() , out string texture ) )
            {
                SetTexture( texture );

            }
            else
            {
                SetTexture( _settings.GetTexture.name );

            }


            SetClearance( clearance );
            SetStiffness( stiffness );
            SetDamper( damper );
            SetCollapse( collapse );

        }
        else
        {
            base.SetupCarSettings();

        }

    }
    private float AngleCalculation()
    {

        float input = Input.GetAxis( "Horizontal" );
        return input * GetMaxAngle;

    }


    private void Turn()

    {
        float angle = AngleCalculation();
        SetSteerInput( angle );

    }
    public void UseBrake()
    {
        if ( Input.GetKey( KeyCode.Space ) )
        {

            UseBrake( true );
        }
        else
        {
            UseBrake( false );

        }
    }
 
    protected override void Start()
    {
        base.Start();
        _movement = new Movement( this );
    }

    private void OnStatusMovement( bool status )
    {

        _movement.OnStarRace( status );
    }

    protected override void Update()
    {
        base.Update();
        Turn();
        UseBrake();
    }

    protected override void GetInput()
    {
        SetMoveInput( Input.GetAxis( "Vertical" ) );
    }





}

