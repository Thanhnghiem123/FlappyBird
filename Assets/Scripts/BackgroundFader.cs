using UnityEngine;

public class BackgroundFader : MonoBehaviour
{
    public SpriteRenderer backgroundRenderer;
    public float fadeDuration = 0.5f; // Thời gian để chuyển đổi giữa mờ và đậm
    private float targetAlpha = 1f; // Alpha mục tiêu
    private float currentAlpha = 1f; // Alpha hiện tại
    private bool fadingOut = true; // Đang mờ dần

    void Start()
    {
        if (backgroundRenderer == null)
        {
            backgroundRenderer = GetComponent<SpriteRenderer>();
        }
        Time.timeScale = 0;
    }

    void Update()
    {
        // Tính toán alpha hiện tại dựa trên thời gian
        currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, Time.unscaledDeltaTime/ fadeDuration);

        // Thiết lập màu của sprite renderer với alpha mới
        Color color = backgroundRenderer.color;
        color.a = currentAlpha;
        backgroundRenderer.color = color;

        // Khi alpha đã đạt đến mục tiêu, thay đổi mục tiêu
        if (Mathf.Approximately(currentAlpha, targetAlpha))
        {
            if (fadingOut)
            {
                targetAlpha = 0f; // Mờ dần
            }
            else
            {
                targetAlpha = 1f; // Đậm dần
            }
            fadingOut = !fadingOut; // Đảo ngược trạng thái mờ/đậm
        }


        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || IsTouchingScreen())
        {
            Time.timeScale = 1;
            Destroy(gameObject);
            GameObject scoreCanvas = GameObject.FindGameObjectWithTag("ScoreCanvas");
            Canvas canvas = scoreCanvas.GetComponent<Canvas>();
            canvas.sortingOrder = 1;
        }
    }
    private bool IsTouchingScreen()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                return true;
            }
        }
        return false;
    }
}
