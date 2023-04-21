using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnJewels : MonoBehaviour
{
    [SerializeField] private GameObject[] jewel;// переменная которая хранит в себе объект с драгоценностью, которая вылетает из пушки
    private int countJewels = 350;// Количество драгоценностей которые надо заспавнить на старте
    private List<GameObject> listJewels = new List<GameObject>();// Список который хранит в себе объекты драгоценнстей. Ради оптимизированного спавна
    public WaitForSeconds wait; // частота перезапуска корутины, тоесть скорость стрельбы.
    [SerializeField] private int[] numberJewel;
    [SerializeField] private bool activeRespawn;
    [SerializeField] private Vector3[] spawnJewelsPos;
    public int commonMultiplier;
    public Finish_Fail ff;
    public FailTrigger ft;
    public int order;
    [SerializeField] private bool proverkaTwoOne;
    [SerializeField] private int twoOne;
    [SerializeField] private GameObject[] slots1;
    [SerializeField] private GameObject[] slots2;
    [SerializeField] private GameObject[] slots3;
    [SerializeField] private int[] setka;
    public bool[] razreshenie;
    [SerializeField] private int[] whatX;

    private void Start()
    {
        for (int j = 0; j < 12; j++)
        {
            spawnJewelsPos[j] = jewel[j].transform.position;
            numberJewel[j] = 350;
        }
        InstanceJewels();
        StartCoroutine("ShootingJewels"); // Запуск корутины стрельбы
        wait = new WaitForSeconds(0.5f);
        order = 7;
        commonMultiplier = 1;
    }


    private void Update()
    {
        
        if(activeRespawn == true)
        {
            Respawn();
            activeRespawn = false;
        }

    }

    private void Respawn()// Метод который выключает объекты с драгоценностями и задает им позицию, которая была у них на старте
    {
        for (int j = 0; j < 12; j++)
        {
            for (int i = 0; i < listJewels.Count; i++)
            {
                listJewels[i].SetActive(false);
                listJewels[i].transform.position = spawnJewelsPos[j];
                if (i == listJewels.Count - 1)
                    StartCoroutine("CycleShooting");
            }
        }
    }

    private IEnumerator CycleShooting()
    {
        yield return new WaitForSeconds(3f);

        StartCoroutine("ShootingJewels");
    }

    private void InstanceJewels()// Метод который спавнить драгоценности на старте, чтобы потом их больше не спавнить
    {
        GameObject jewelClone;

        for (int j = 0; j < 12; j++)
        {
            for (int i = 0; i < countJewels; i++)
            {
                jewelClone = Instantiate(jewel[j], jewel[j].transform.position, Quaternion.identity);
                listJewels.Add(jewelClone);// добавляем драгоценность в список
            }
        }
    }

    private IEnumerator ShootingJewels()// Корутина которая осуществляет стрельбу из пушки
    {
        yield return wait;

        listJewels[listJewels.Count - numberJewel[commonMultiplier - 1] - (350*11 - ((commonMultiplier - 1)*350))].SetActive(true);// Активируем объкт драгоценности из списка
        numberJewel[commonMultiplier - 1]--;// Плюсуем переменную, чтобы в следущем запуске корутины взять из списка следущий объект 
        if(numberJewel[commonMultiplier - 1] == 0)
        {
            activeRespawn = true;
            numberJewel[commonMultiplier - 1] = 0;
            StopCoroutine("ShootingJewels");
        }
        else if (ff.proverkaFinish2 == true || ft.proverkaLose == true)
        {
            StopCoroutine("ShootingJewels");
        }
        else
        {
            StartCoroutine("ShootingJewels");// Перезапуск корутины 
        }
    }

    public void Postanovka(int x, int y, int type)
    {
        razreshenie[y + 1] = true;
        whatX[y] = x;
        if (type == 1)
        {
            setka[y] = type;
            if (x == 0)
            {
                slots1[y].gameObject.SetActive(false);
                slots3[y].gameObject.SetActive(false);
                slots2[y].gameObject.SetActive(false);
                if (y > 0)
                {
                    slots1[y - 1].gameObject.SetActive(true);
                    slots2[y - 1].gameObject.SetActive(true);
                }
                if (y < 3)
                {
                    if (setka[y + 1] == 2)
                    {
                        slots3[y].gameObject.SetActive(false);
                    }
                }
            }
            else if (x == 2)
            {
                slots3[y].gameObject.SetActive(false);
                slots1[y].gameObject.SetActive(false);
                slots2[y].gameObject.SetActive(false);
                if (y > 0)
                {
                    slots3[y - 1].gameObject.SetActive(true);
                    slots2[y - 1].gameObject.SetActive(true);
                }
                if (y < 3)
                {
                    if (setka[y + 1] == 2)
                    {
                        slots1[y].gameObject.SetActive(false);
                    }
                }
            }
        }
        if (type == 2)
        {
            setka[y] = type;
            if (x == 1)
            {
                slots2[y].gameObject.SetActive(false);
                slots1[y].gameObject.SetActive(false);
                slots3[y].gameObject.SetActive(false);
                if (y > 0)
                {
                    slots2[y - 1].gameObject.SetActive(true);
                    slots1[y - 1].gameObject.SetActive(true);
                    slots3[y - 1].gameObject.SetActive(true);
                }
            }
        }
        if (type == 3)
        {
            whatX[y-1] = x;
            setka[y] = type;
            if (x == 0)
            {
                slots1[y].gameObject.SetActive(false);
                slots3[y].gameObject.SetActive(false);
                slots2[y].gameObject.SetActive(false);
                if (y > 1)
                {
                    slots1[y - 2].gameObject.SetActive(true);
                    slots2[y - 2].gameObject.SetActive(true);
                }
                if (y < 3)
                {
                    if (setka[y + 1] == 2)
                    {
                        slots3[y].gameObject.SetActive(false);
                    }
                }
            }
            else if (x == 2)
            {
                slots3[y].gameObject.SetActive(false);
                slots1[y].gameObject.SetActive(false);
                slots2[y].gameObject.SetActive(false);
                if (y > 1)
                {
                    slots3[y - 2].gameObject.SetActive(true);
                    slots2[y - 2].gameObject.SetActive(true);
                }
                if (y < 3)
                {
                    if (setka[y + 1] == 2)
                    {
                        slots1[y].gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    public void Uborka(int x, int y, int type)
    {
        if (type == 1 && razreshenie[y] == false)
        {
            razreshenie[y + 1] = false;
            if (x == 0)
            {
                slots1[y].gameObject.SetActive(true);
                slots2[y].gameObject.SetActive(true);
                if (y > 0)
                {
                    slots1[y - 1].gameObject.SetActive(false);
                    slots2[y - 1].gameObject.SetActive(false);
                }
                if (y < 3)
                {
                    if (setka[y + 1] == 2)
                    {
                        slots3[y].gameObject.SetActive(true);
                    }
                }
                if (y == 3)
                {
                    slots3[y].gameObject.SetActive(true);
                }
            }
            else if (x == 2)
            {
                slots3[y].gameObject.SetActive(true);
                slots2[y].gameObject.SetActive(true);
                if (y > 0)
                {
                    slots3[y - 1].gameObject.SetActive(false);
                    slots2[y - 1].gameObject.SetActive(false);
                }
                if (y < 3)
                {
                    if (setka[y + 1] == 2)
                    {
                        slots1[y].gameObject.SetActive(true);
                    }
                }
                if (y == 3)
                {
                    slots1[y].gameObject.SetActive(true);
                }
            }
        }
        if (type == 2 && razreshenie[y] == false)
        {
            razreshenie[y + 1] = false;
            if (x == 1)
            {
                slots2[y].gameObject.SetActive(true);
                if (y < 3)
                {
                    if(whatX[y + 1] == 0)
                    {
                        slots1[y].gameObject.SetActive(true);
                    }
                    if (whatX[y + 1] == 2)
                    {
                        slots3[y].gameObject.SetActive(true);
                    }
                    if (whatX[y + 1] == 1)
                    {
                        slots3[y].gameObject.SetActive(true);
                        slots1[y].gameObject.SetActive(true);
                    }
                }
                if (y > 0)
                {
                    slots2[y - 1].gameObject.SetActive(false);
                    slots1[y - 1].gameObject.SetActive(false);
                    slots3[y - 1].gameObject.SetActive(false);
                }
                if(y == 3)
                {
                    slots3[y].gameObject.SetActive(true);
                    slots1[y].gameObject.SetActive(true);
                }
            }
        }
        if (type == 3 && razreshenie[y-1] == false)
        {
            razreshenie[y + 1] = false;
            if (x == 0)
            {
                slots1[y].gameObject.SetActive(true);
                slots2[y].gameObject.SetActive(true);
                if (y > 1)
                {
                    slots1[y - 2].gameObject.SetActive(false);
                    slots2[y - 2].gameObject.SetActive(false);
                }
                if (y < 3)
                {
                    if (setka[y + 1] == 2)
                    {
                        slots3[y].gameObject.SetActive(true);
                    }
                }
                if (y == 3)
                {
                    slots3[y].gameObject.SetActive(true);
                }
            }
            else if (x == 2)
            {
                slots3[y].gameObject.SetActive(true);
                slots2[y].gameObject.SetActive(true);
                if (y > 1)
                {
                    slots3[y - 2].gameObject.SetActive(false);
                    slots2[y - 2].gameObject.SetActive(false);
                }
                if (y < 3)
                {
                    if (setka[y + 1] == 2)
                    {
                        slots1[y].gameObject.SetActive(true);
                    }
                }
                if (y == 3)
                {
                    slots1[y].gameObject.SetActive(true);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    //Скрипт вешается на мультиплай.
    //Мультиплай - это тот куб, который перетягивается на пушку и на котором написаны математические действия.
    private float mZCoord;
    [SerializeField] private bool flagSlot;
    public SpawnJewels sj;

    private Vector3 mOffset;
    private Vector3 startPos;
    private Vector3 slotPos;
    // Слот - это место, в которое нужно поставить мультиплай
    // Слоты это копии мультиплаев только на них меш отключен, чтобы их не было видно. Нужны только для наметки позиции.
    [SerializeField] private GameObject[] slots;
    [SerializeField] private GameObject[] slots2;
    [SerializeField] private GameObject[] slots3;// Это массив в котором хранятся слоты для пушки. 
    [SerializeField] private GameObject[] OtherSlot;

    private GameObject nowSlot;
    [SerializeField] private int multiplier;
    [SerializeField] private int summator;
    [SerializeField] private bool multi;
    [SerializeField] private bool TwoOne;
    [SerializeField] private int order1;
    [SerializeField] private int x;
    [SerializeField] private int type;
    [SerializeField] private Vector3 startCollider;

    private void Start()
    {
        startPos = transform.position;
        startCollider = gameObject.GetComponent<BoxCollider>().size;
    }

    private void OnMouseDown()//Метод, который срабатывает когда нажимаешь мышью на мультиплай.
    {
        gameObject.GetComponent<BoxCollider>().size = new Vector3(gameObject.GetComponent<BoxCollider>().size.x - 0.7f, gameObject.GetComponent<BoxCollider>().size.y - 0.7f, gameObject.GetComponent<BoxCollider>().size.z); 
        mZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        mOffset = transform.position - GetMouseWorldPos();
        if (multi == true)
        {
            //sj.order++;
            //sj.order++;
            if (type != 3)
            {
                if (sj.razreshenie[order1] == false)
                {
                    sj.commonMultiplier = (sj.commonMultiplier / multiplier) - summator;
                    sj.Uborka(x, order1, type);
                    multi = false;
                }
                if (sj.razreshenie[order1] == true)
                {
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                }
            }
            else if (type == 3 && order1 > 0)
            {
                if (sj.razreshenie[order1 - 1] == false)
                {
                    sj.commonMultiplier = (sj.commonMultiplier / multiplier) - summator;
                    sj.Uborka(x, order1, type);
                    multi = false;
                }
                if (sj.razreshenie[order1 - 1] == true)
                {
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
    }

    private void OnMouseUp()//Метод, который срабатывает когда отпускаешь кнопку мыши на мультиплае.
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<BoxCollider>().size = startCollider;
        if (flagSlot == true)
        {

            GoToSlotPosition();
            //nowSlot.SetActive(false);
            if (sj.razreshenie[order1] == false && type != 3)
            {
                sj.commonMultiplier = (sj.commonMultiplier * multiplier) + summator;
                multi = true;
                sj.Postanovka(x, order1, type);
            }
            if (order1 > 0)
            {
                if (sj.razreshenie[order1 - 1] == false && type == 3 && order1 > 0)
                {
                    sj.commonMultiplier = (sj.commonMultiplier * multiplier) + summator;
                    multi = true;
                    sj.Postanovka(x, order1, type);
                }
            }
            else if (order1 <= 0 && type == 3) BackToStartPosition();         
        }
        else
        {
            if (type != 3)
            {
                if (sj.razreshenie[order1] == false)
                {
                    BackToStartPosition();
                }
                if (sj.razreshenie[order1] == true)
                {
                    //GoToSlotPosition();
                    BackToStartPosition();
                }
            }
            else if (type == 3 && order1 > 0)
            {
                if (sj.razreshenie[order1 - 1] == false)
                {
                    BackToStartPosition();
                }
                if (sj.razreshenie[order1 - 1] == true)
                {
                    //GoToSlotPosition();
                    BackToStartPosition();
                }
            }
            else if (type == 3 && order1 <= 0)
            {
                BackToStartPosition();
            }
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }

    private void BackToStartPosition()// Метод который возвращает мультиплай на стартовую позицию
    {
        transform.position = startPos;
    }

    private void GoToSlotPosition()// Метод который ровно ставит мультиплай на пушку
    {
        transform.position = slotPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "slot")
        {
            CheckSlots1(other.gameObject);
        }
        if (other.tag == "slot2")
        {
            CheckSlots2(other.gameObject);
        }
        if (other.tag == "slot3")
        {
            CheckSlots3(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "slot")
        {
            flagSlot = false;
        }
        if (other.tag == "slot2")
        {
            flagSlot = false;
        }
        if (other.tag == "slot3")
        {
            flagSlot = false;
        }
    }

    private void CheckSlots1(GameObject another)
    {
        if (TwoOne == false)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (another == slots[i])
                {
                    flagSlot = true;
                    nowSlot = slots[i];
                    slotPos = slots[i].transform.position;
                    order1 = i;
                    x = 0;
                }
            }
        }
    }
    private void CheckSlots2(GameObject another) {
        if (TwoOne == true)
        {
            for (int i = 0; i < slots2.Length; i++)
            {
                if (another == slots2[i])
                {
                    flagSlot = true;
                    nowSlot = slots2[i];
                    slotPos = slots2[i].transform.position;
                    order1 = i;
                    x = 1;
                }
            }
        } 
    }
    private void CheckSlots3(GameObject another)
    {
        if (TwoOne == false)
        {
            for (int i = 0; i < slots3.Length; i++)
            {
                if (another == slots3[i])
                {
                    flagSlot = true;
                    nowSlot = slots3[i];
                    slotPos = slots3[i].transform.position;
                    order1 = i;
                    x = 2;
                }
            }
        }
    } 
}
