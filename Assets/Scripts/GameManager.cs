using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool Started;
    public static int BallsToRegister;
    public static float TickPhaseMax = 3;
    public static float PrePlacedObjectsOpacity = 0.5f;
    public static LayerMask GroundLayer;
    public static LayerMask SkyLayer;
    public static List<SpriteRenderer> OnStartSpriteRenderers;

    private static GameManager _thisManager;

    // public GameObject clockArrow;
    public int sceneIndex;
    public SpriteRenderer backgroundFade;
    public LayerMask groundLayer;
    public LayerMask skyLayer;
    public GameObject loseVolume;
    public GameObject winVolume;
    public GameObject pauseVolume;
    public GameObject playVolume;
    public GameObject pauseIcon;
    public GameObject playIcon;
    public GameObject gridOnIcon;
    public GameObject gridOffIcon;
    public GameObject grid;
    public GameObject[] speedUpIcons;

    private bool _isAlternativeTick;
    private bool _losed;
    private bool _completelyStopped = true;
    private bool _justFirstTick;
    private int _tickTimePhase = 1;
    private float _tickTime = 0.25f;
    private float _tickPhase;
    private float _alternativeTickPhase;
    private float _clockArrowToRotationZ;
    private float _clockArrowRotationSpeed = 0.5f;

    public static Action Tick1;
    public static Action Tick2;
    public static Action Tick3;
    public static Action AlternativeTick1;
    public static Action AlternativeTick2;
    public static Action AlternativeTick3;
    public static Action Restart;
    public static Action PreRestart;
    public static Action ResetToStart;
    
    private void Start()
    {
        GroundLayer = groundLayer;
        SkyLayer = skyLayer;
        
        _thisManager = this;
        playVolume.SetActive(false);
        pauseVolume.SetActive(false);
        loseVolume.SetActive(false);;
        winVolume.SetActive(false);

        OnStartSpriteRenderers = new List<SpriteRenderer>();
        foreach (SpriteRenderer sp in GameObject.FindObjectsOfType(typeof(SpriteRenderer)))
        {
            OnStartSpriteRenderers.Add(sp);
        }
        OnStartSpriteRenderers.Remove(backgroundFade);
        
        ChangeGridActive();
        StartCoroutine(Ticker());
    }

    private void Update()
    {
        // clockArrow.transform.rotation = Quaternion.Lerp(clockArrow.transform.rotation,
        //     quaternion.Euler(0, 0, _clockArrowToRotationZ), _clockArrowRotationSpeed);
        
        if (Input.GetKeyUp(KeyCode.Space)) StartPause();
        if (Input.GetKey(KeyCode.LeftAlt)) CompleteStop();
        if (Input.GetKeyUp(KeyCode.RightAlt)) SpeedUp();
        if (Input.GetKeyUp(KeyCode.LeftShift)) ChangeGridActive();
    }

    public void ChangeGridActive()
    {
        grid.SetActive(!grid.activeSelf);

        if (grid.activeSelf)
        {
            gridOnIcon.SetActive(true);
            gridOffIcon.SetActive(false);
        }
        else
        {
            gridOnIcon.SetActive(false);
            gridOffIcon.SetActive(true);
        }
    }
    
    public void SpeedUp()
    {
        _tickTimePhase += 1;
        if (_tickTimePhase > 3) _tickTimePhase = 1;

        for (int i = 0; i < speedUpIcons.Length; i++)
        {
            speedUpIcons[i].SetActive(false);
        }
        speedUpIcons[_tickTimePhase - 1].SetActive(true);

        // Time.timeScale = _tickTimePhase;
    }
    
    public void CompleteStop()
    {
        playIcon.SetActive(true);
        pauseIcon.SetActive(false);
        playVolume.SetActive(false);
        pauseVolume.SetActive(false);
        loseVolume.SetActive(false);
        winVolume.SetActive(false);
        
        ResetToStart?.Invoke();
        Started = false;
        _completelyStopped = true;
        _losed = false;
        _tickPhase = 0;
        _alternativeTickPhase = 0;
        BallsToRegister = 0;
        _isAlternativeTick = false;
    }

    public void StartPause()
    {
        if (!_losed)
        {
            if (Started)
            {
                Pause();
                playIcon.SetActive(true);
                pauseIcon.SetActive(false);
                playVolume.SetActive(true);
                pauseVolume.SetActive(false);
            }
            else
            {
                TickStart();
                playIcon.SetActive(false);
                pauseIcon.SetActive(true);
                playVolume.SetActive(false);
                pauseVolume.SetActive(true);
            }
        }
    }

    private static void Pause()
    {
        Started = false;
    }

    private void TickStart()
    {
        if (!Started && !_losed)
        {
            if (_completelyStopped)
            {
                PreRestart?.Invoke();
                Invoke(nameof(DelayedCompleteStop), 0.5f);
            }
            Started = true;
        }
    }

    private void DelayedCompleteStop()
    {
        Restart?.Invoke();
        _completelyStopped = false;
    }

    public static void RegisterBall()
    {
        BallsToRegister -= 1;
        if (BallsToRegister <= 0)
        {
            Win();
        }
    }

    private static void Win()
    {
        Debug.Log("WIN");
        _thisManager.LocalWin();
    }

    private void LocalWin()
    {
        Started = false;
        playVolume.SetActive(false);
        pauseVolume.SetActive(false);
        winVolume.SetActive(true);

        PlayerPrefs.SetInt("Level" + sceneIndex, 1);
    }
    
    public static void Lose()
    {
        _thisManager.StopTicking();
    }

    private void StopTicking()
    {
        Started = false;
        _losed = true;
        playVolume.SetActive(false);
        pauseVolume.SetActive(false);
        loseVolume.SetActive(true);
    }

    public static bool GetIsCompletelyStopped()
    {
        return _thisManager.GetIsCompletelyStoppedLocal();
    }

    public bool GetIsCompletelyStoppedLocal()
    {
        return _completelyStopped;
    }
    
    private IEnumerator Ticker()
    {
        while (true)
        {
            if (Started)
            {
                if (_isAlternativeTick)
                {
                    if (_alternativeTickPhase >= TickPhaseMax) _alternativeTickPhase = 0;
                    _alternativeTickPhase += 1;

                    switch (_alternativeTickPhase)
                    {
                        case 1:
                            AlternativeTick1?.Invoke();
                            goto case 2;
                        case 2:
                            AlternativeTick2?.Invoke();
                            goto case 3;
                        case 3:
                            AlternativeTick3?.Invoke();
                            break;
                    }
                }
                else
                {
                    if (_tickPhase >= TickPhaseMax) _tickPhase = 0;
                    _tickPhase += 1;

                    switch (_tickPhase)
                    {
                        case 1:
                            Tick1?.Invoke();
                            goto case 2;
                        case 2:
                            Tick2?.Invoke();
                            goto case 3;
                        case 3:
                            Tick3?.Invoke();
                            break;
                    }
                }
                
                _isAlternativeTick = !_isAlternativeTick;
            }
            
            yield return new WaitForSeconds(_tickTime / _tickTimePhase);
        }
    }
}