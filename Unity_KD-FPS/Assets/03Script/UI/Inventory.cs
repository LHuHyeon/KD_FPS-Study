using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;  // 인벤토리 실행 시 마우스만 움직이도록 사용할 변수

    // 필요한 컴포넌트 
    [SerializeField]
    private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotParent;

    // 슬롯들
    private Slot[] slots;

    void Start()
    {
        slots = go_SlotParent.GetComponentsInChildren<Slot>();
    }

    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I)){
            inventoryActivated = !inventoryActivated;

            if (inventoryActivated)
                OpenInventory();
            else
                CloseInventory();
        }
    }

    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        // 장비가 아닐 경우
        if (Item.ItemType.Equipment != _item.itemType){
            for (int i = 0; i < slots.Length; i++){
                if (slots[i].item != null){
                    if (slots[i].item.itmeName == _item.itmeName){
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++){
                if (slots[i].item == null){
                    slots[i].AddItem(_item, _count);
                    return;
                }
            }
    }
}
