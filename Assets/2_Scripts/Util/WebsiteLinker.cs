using UnityEngine;

public class WebsiteLinker : MonoBehaviour
{
    // 인자로 url 주소를 받습니다.
    public void WebsiteLink(string url)
    {
        Application.OpenURL(url);
    }
}