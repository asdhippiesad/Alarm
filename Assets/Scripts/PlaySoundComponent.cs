using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundComponent : MonoBehaviour
{
    [SerializeField] private float _transitionSpeed;
    [SerializeField] private AlarmSystem _alarm;
    [SerializeField] private AudioSource _audio;

    public AlarmSystem Alarm => _alarm;

    private float _minVolumeAlarm = 0.0f;
    private float _maxVolumeAlarm = 1.0f;

    private void OnEnable()
    {
        Alarm.Registered += StartPlayAudio;
    }

    private void OnDisable()
    {
        Alarm.Disarmed -= StopPlayAudio;;
    }

    private void StartPlayAudio()
    {
        _audio.Play();
        StartCoroutine(SoundOn(_maxVolumeAlarm));
    }

    private void StopPlayAudio()
    {
        StartCoroutine(SoundOff(_minVolumeAlarm));
    }

    private IEnumerator SoundOn(float currentVolume)
    {
        while (_audio.volume != currentVolume)
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, currentVolume, _transitionSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator SoundOff(float targetVolume)
    {
        while (_audio.volume > 0)
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, targetVolume, _transitionSpeed * Time.deltaTime);
            yield return null;
        }

        if (_audio.volume == 0)
        {
            _audio.Stop();
        }
    }
}