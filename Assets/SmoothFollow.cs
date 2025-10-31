using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [Header("Target to follow")]
    public Transform target;

    [Header("Follow settings")]
    public float distance = 7.0f;       // Distance derrière le target
    public float height = 5.0f;         // Hauteur par rapport au target
    public float heightDamping = 2.0f;  // Lissage hauteur
    public float rotationDamping = 3.0f;

    void LateUpdate()
    {
        if (!target) return;

        // Calcul de la rotation souhaitée
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Lissage
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Calcul position
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
        Vector3 pos = target.position - currentRotation * Vector3.forward * distance;
        pos.y = currentHeight;

        // Appliquer position et rotation
        transform.position = pos;
        transform.LookAt(target);
    }
}
