using UnityEngine;
using System;

/// <summary>
/// This script manages the lighter.
/// </summary>
public class StormLighter : MonoBehaviour
{

    #region Fields

    //The key to open the lighter
    public KeyCode OpenKey;
    //the key to turn on the lighter (particle system)
    public KeyCode TurnOnKey;
    //The animator
    private Animator _animator;
    //The target particel system
    private ParticleSystem _particleSystem;
    //The audio source to play the sound effects
    private AudioSource _audioSource;
    //Contains all sound effects
    private AudioClip[] _audioClips;
    //Determines if the lighter is open or not
    private bool _isOpen;

    #endregion

    #region MonoBehaviour Methods

    //Start
    void Start()
    {
        _isOpen = false;

        try
        {
            _animator = GetComponent<Animator>();
            _particleSystem = GetComponentInChildren<ParticleSystem>();
            _particleSystem.gameObject.SetActive(false);

            _audioClips = new AudioClip[3];
            _audioClips[0] = (AudioClip)Resources.Load("Sounds/Clap_Open", typeof(AudioClip));
            _audioClips[1] = (AudioClip)Resources.Load("Sounds/Clap_Close", typeof(AudioClip));
            _audioClips[2] = (AudioClip)Resources.Load("Sounds/Fire_On", typeof(AudioClip));

            _audioSource = GetComponent<AudioSource>();

        }
        catch (NullReferenceException e)
        {
            Debug.LogError(e.Message);
        }

    }

    //Update method
    void Update()
    {
        if (Input.GetKeyDown(OpenKey))
        {
            if (_isOpen)
            {
                //Close the lighter
                _animator.SetTrigger("Close");
                _particleSystem.gameObject.SetActive(false);
                _isOpen = false;
            }
            else
            {
                if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    //open the lighter
                    _animator.SetTrigger("Open");
                    _isOpen = true;
                }
            }
        }

        if (Input.GetKeyDown(TurnOnKey) && _isOpen && _animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            //turn the lighter on
            _animator.SetTrigger("TurnOn");
            _particleSystem.gameObject.SetActive(true);
        }

    }

    //Plays the "open" sound effect (animation event)
    public void PlayOpenSound()
    {
        _audioSource.clip = _audioClips[0];
        _audioSource.Play();
    }

    //Plays the "close" sound effect (animation event)
    public void PlayCloseSound()
    {
        _audioSource.clip = _audioClips[1];
        _audioSource.Play();
    }

    //Plays the "turn on" sound effect (animation event)
    public void PlayTurnOnSound()
    {
        _audioSource.clip = _audioClips[2];
        _audioSource.Play();
    }

    #endregion
}
