using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenStageButton : MonoBehaviour
{
    public int StageNumber;

    public void OpenStage()
    {
        SceneManager.LoadScene($"Stage{StageNumber}");
    }
}