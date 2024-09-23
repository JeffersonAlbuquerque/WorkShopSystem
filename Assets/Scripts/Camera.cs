using UnityEngine;
using UnityEngine.UI;

public class Camera : MonoBehaviour
{
    public Transform target;            // O alvo que a câmera vai orbitar
    public float distance = 5.0f;       // Distância da câmera ao alvo
    public float minDistance = 2.0f;    // Distância mínima de zoom
    public float maxDistance = 10.0f;   // Distância máxima de zoom
    public float zoomSpeed = 0.1f;      // Velocidade de zoom
    public float rotationSpeed = 100.0f;// Velocidade de rotação

    private TouchAreaCamera touchArea;

    private float currentDistance;      // Distância atual da câmera ao alvo
    private Vector2 lastTouchPosition;  // Posição do toque anterior para rotação
    private bool isRotating = false;    // A câmera está sendo rotacionada?
    private bool isZooming = false;     // A câmera está sendo aproximada/distanciada?

    void Start()
    {
        currentDistance = distance;

        if (touchArea.touchAreaImage != null)
        {
            touchArea.touchAreaRect = touchArea.touchAreaImage.GetComponent<RectTransform>();
        }
        else
        {
            Debug.LogError("Nenhuma imagem de área de toque foi atribuída.");
        }
    }

    void Update()
    {
        touchArea = FindObjectOfType<TouchAreaCamera>();

        if (target == null || touchArea.touchAreaRect == null) return;

        // Controle de rotação com um toque
        if (Input.touchCount == 1 && !isZooming)
        {
            Touch touch = Input.GetTouch(0);

            // Verifica se o toque está dentro da área de toque da imagem
            if (IsTouchWithinUI(touch))
            {
                // Início do toque
                if (touch.phase == TouchPhase.Began)
                {
                    lastTouchPosition = touch.position;
                    isRotating = false;
                }
                // Movendo o dedo (rotacionando)
                else if (touch.phase == TouchPhase.Moved)
                {
                    // Se o movimento for significativo, considera como rotação
                    if (!isRotating && (touch.position - lastTouchPosition).magnitude > 5f)
                    {
                        isRotating = true;
                    }

                    if (isRotating)
                    {
                        Vector2 deltaTouch = touch.position - lastTouchPosition;
                        lastTouchPosition = touch.position;

                        float horizontal = deltaTouch.x * rotationSpeed * Time.deltaTime;
                        float vertical = -deltaTouch.y * rotationSpeed * Time.deltaTime;

                        // Rotaciona ao redor do alvo
                        transform.RotateAround(target.position, Vector3.up, horizontal);
                        transform.RotateAround(target.position, transform.right, vertical);
                    }
                }
            }
        }

        // Controle de zoom com dois toques
        if (Input.touchCount == 2)
        {
            isZooming = true;  // Está fazendo zoom

            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            // Verifica se os toques estão dentro da área de toque
            if (IsTouchWithinUI(touch1) && IsTouchWithinUI(touch2))
            {
                // Distância entre os dois toques
                float currentPinchDistance = Vector2.Distance(touch1.position, touch2.position);
                float previousPinchDistance = Vector2.Distance(touch1.position - touch1.deltaPosition, touch2.position - touch2.deltaPosition);

                // Diferença entre as distâncias atual e anterior (para zoom)
                float pinchDifference = previousPinchDistance - currentPinchDistance;

                // Ajusta a distância da câmera
                currentDistance += pinchDifference * zoomSpeed;
                currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
            }
        }
        else
        {
            isZooming = false;  // Não está mais fazendo zoom
        }

        // Atualiza a posição da câmera de acordo com a nova distância
        Vector3 direction = (transform.position - target.position).normalized;
        transform.position = target.position + direction * currentDistance;

        // Garante que a câmera sempre olha para o alvo
        transform.LookAt(target);
    }

    // Função que verifica se o toque está dentro da área da imagem de toque
    private bool IsTouchWithinUI(Touch touch)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(touchArea.touchAreaRect, touch.position, null, out localPoint);
        return touchArea.touchAreaRect.rect.Contains(localPoint);
    }
}
