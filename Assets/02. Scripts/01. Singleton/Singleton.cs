using UnityEngine;

// MonoBehaviour�� ��ӹ��� Ŭ������ ��� �����ϵ��� ���׸� �������� ����
public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
    protected static T instance = null;

    // �ʱ⿡ ������ �Լ�
    protected virtual void SingletonInit() { }

    // Instance������Ƽ
    public static T Instance
    {
        get
        {
            // ���� instance�� ����Ȱ� ���� ��
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;    // TŸ�� ��ü ã��

                // instance�� Singleton<T>Ÿ������ ����ȯ�� �������� üũ
                Singleton<T> tempinst = instance as Singleton<T>;

                // ����ȯ�� �ȉ��� �� ����
                if (instance == null) 
                {
                    return null;
                }

                // ����ȯ ���� �� instance�� �ִ� SingletonInit�Լ� ����
                tempinst.SingletonInit();
            }
            return instance;
        }
    }
}
