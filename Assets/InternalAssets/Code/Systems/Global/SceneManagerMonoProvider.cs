using UnityEngine;

public class SceneManagerMonoProvider : MonoBehaviour
{
    //private LevelData data;

    //private void OnEnable()
    //{
    //    GameBootstrap.OnDataReceived += SetupData;
    //}
    //private void OnDisable()
    //{
    //    GameBootstrap.OnDataReceived -= SetupData;
    //}

    public void LoadMenuScene()
    {
        SceneHelper.LoadScene(ProjectScene.Menu);
    }

    public void Restart()
    {
        SceneHelper.LoadScene(ProjectScene.Gameplay);
    }

   

    //public void LoadNextLevel()
    //{
    //    if (data.LevelID + 1 >= 11) { SceneHelper.LoadScene(ProjectScene.Menu); return; }
    //    LevelData Newdata = FindObjectOfType<LevelDataContainer>().levelDataArray[data.LevelID + 1];
    //    GameBootstrap.SetLevelData(Newdata);
    //    SceneHelper.LoadScene(ProjectScene.Gameplay);
    //}

    //public void SetupData(LevelData data)
    //{
    //    this.data = data;
    //}
}
