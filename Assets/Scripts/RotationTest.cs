using TMPro;
using UnityEngine;

public class RotationTest : MonoBehaviour
{
    [SerializeField] private Transform _rotatedObject;
    [SerializeField] private TMP_Text _rotationText;
    [SerializeField] private TMP_InputField[] _multiplyQuaternionInputFields;
    [SerializeField] private TMP_InputField[] _rotateInputFields;

    private void Update() => _rotationText.text = 
        string.Format($"Quaternion: {_rotatedObject.rotation.ToString()}" + 
                      $"\nVector3: {_rotatedObject.rotation.eulerAngles.ToString()}");

    public void Multiply()
    {
        try
        {
            float a = float.Parse(_multiplyQuaternionInputFields[0].text);
            float b = float.Parse(_multiplyQuaternionInputFields[1].text);
            float c = float.Parse(_multiplyQuaternionInputFields[2].text);
            float d = float.Parse(_multiplyQuaternionInputFields[3].text);

            Multiply(new Quaternion(b, c, d, a));
        }
        catch
        {
            Debug.LogError("Введіть усі коефіцієнти кватерніону!");
        }
    }
    
    private void Multiply(Quaternion quaternion)
    {
        _rotatedObject.rotation *= quaternion;
    }

    public void Rotate()
    {
        try
        {
            float x = float.Parse(_rotateInputFields[0].text);
            float y = float.Parse(_rotateInputFields[1].text);
            float z = float.Parse(_rotateInputFields[2].text);
            float angle = float.Parse(_rotateInputFields[3].text);
            Rotate(new Vector3(x, y, z), angle);
        }
        catch
        {
            Debug.LogError("Введіть усі коефіцієнти вектору і кут!");
        }
    }

    private void Rotate(Vector3 axis, float angle)
    {
        Vector3 normalizedAxis = axis.normalized;

        float halfAngle = Mathf.Deg2Rad * angle * 0.5f;

        float sinHalfAngle = Mathf.Sin(halfAngle);
        float cosHalfAngle = Mathf.Cos(halfAngle);

        Quaternion q = new Quaternion(
            sinHalfAngle * normalizedAxis.x,
            sinHalfAngle * normalizedAxis.y,
            sinHalfAngle * normalizedAxis.z,
            cosHalfAngle);

        Multiply(q);
    }

    public void ResetRotation()
    {
        _rotatedObject.rotation = Quaternion.identity;
    }
}
