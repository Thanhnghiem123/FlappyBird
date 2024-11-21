using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public GameObject background1;
    public GameObject background2;
    public GameObject base1;
    public GameObject base2;
    private Camera mainCamera;
    private float widthOfBackground;

    void Start()
    {
        mainCamera = Camera.main;
        // Giả sử background1 và background2 có cùng kích thước
        widthOfBackground = background1.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Dịch chuyển pipe
        transform.Translate(Vector3.left * 5f * Time.deltaTime);

        // Tính toán điểm góc trên bên phải của background1 trong viewport của camera
        Vector3 background1Right = new Vector3(background1.transform.position.x + widthOfBackground/2, background1.transform.position.y, background1.transform.position.z);
        Vector3 viewportPositionBG = mainCamera.WorldToViewportPoint(background1Right);

        Vector3 base1Right = new Vector3(base1.transform.position.x + widthOfBackground/2, base1.transform.position.y, base1.transform.position.z);
        Vector3 viewportPositionBase = mainCamera.WorldToViewportPoint(base1Right);


        // Kiểm tra xem điểm góc trên bên phải của background1 có đi qua điểm (0,0) của viewport camera hay không
        if (viewportPositionBG.x < 0)
        {
            // Chuyển background1 đến sau background2
            background1.transform.position = new Vector3(background2.transform.position.x + widthOfBackground, background1.transform.position.y, background1.transform.position.z);

            // Hoán đổi background1 và background2 để tiếp tục kiểm tra
            SwitchObjects(ref background1, ref background2);

        }
        if (viewportPositionBase.x < 0)
        {
            base1.transform.position = new Vector3(base2.transform.position.x + widthOfBackground, base1.transform.position.y, base1.transform.position.z);
            SwitchObjects(ref base1, ref base2);
        }
    }

    
    void SwitchObjects(ref GameObject obj1, ref GameObject obj2)
    {
        GameObject temp = obj1;
        obj1 = obj2;
        obj2 = temp;

        // Debug thông tin hoán đổi
    }
}
