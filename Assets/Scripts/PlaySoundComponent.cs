using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundComponent : MonoBehaviour
{
    [SerializeField] private float _transitionSpeed;
    [SerializeField] private AlarmDeterminer _alarm;
    [SerializeField] private AudioSource _audio;

    public AlarmDeterminer Alarm => _alarm;
    private Coroutine _coroutine;

    private float _minVolumeAlarm = 0.2f;
    private float _maxVolumeAlarm = 1.0f;

    private void OnEnable()
    {
        Alarm.Registered += StartPlayAudio;
        Alarm.Disarmed += StopPlayAudio;
    }

    private void OnDisable()
    {
        Alarm.Registered -= StartPlayAudio;
        Alarm.Disarmed -= StopPlayAudio;
    }

    private void StartPlayAudio()
    {
        _coroutine = StartCoroutine(SoundOn());
    }

    private void StopPlayAudio()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SoundOff());
    }

    private IEnumerator SoundOn()
    {
        while (_audio.volume < _maxVolumeAlarm)
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, _maxVolumeAlarm, _transitionSpeed * Time.deltaTime);
            yield return null;
        }

        _audio.Play();
    }

    private IEnumerator SoundOff()
    {
        while (_audio.volume > _minVolumeAlarm)
        {
            _audio.volume -= Time.deltaTime * _transitionSpeed;
            yield return null;
        }

        if (_audio.volume != _minVolumeAlarm)
            _audio.Stop();
    }
}