using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundComponent : MonoBehaviour
{
    [SerializeField] private float _transitionSpeed;
    [SerializeField] private AlarmDeterminer _alarm;
    [SerializeField] private AudioSource _audio;

    private Coroutine _coroutine;

    private float _minVolumeAlarm = 0.0f;
    private float _maxVolumeAlarm = 1.0f;

    private void OnEnable()
    {
        _alarm.Registered += StartPlayAudio;
        _alarm.Disarmed += StopPlayAudio;
    }

    private void OnDisable()
    {
        _alarm.Registered -= StartPlayAudio;
        _alarm.Disarmed -= StopPlayAudio;
    }

    private void StartPlayAudio()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeVolume(_maxVolumeAlarm));
    }

    private void StopPlayAudio()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeVolume(_minVolumeAlarm));
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        while (_audio.volume != targetVolume)
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, targetVolume, _transitionSpeed * Time.deltaTime);
            yield return null;
        }
    }
}