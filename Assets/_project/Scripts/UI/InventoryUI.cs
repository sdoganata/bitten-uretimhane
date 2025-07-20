using DG.Tweening;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private bool _isInventoryOpen;

    public CanvasGroup inventoryObjectsUI;

    public List<WeaponType> availableWeapons;
    public List<Button> weaponButtons;

    private bool _isShotgunCollected;

    private WeaponType _activeWeapon;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        availableWeapons.Add(WeaponType.MachineGun);
        MachineGunButtonPressed();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryButtonPressed();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && _activeWeapon!=WeaponType.MachineGun)
        {
            MachineGunButtonPressed();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && _isShotgunCollected && _activeWeapon != WeaponType.Shotgun)
        {
            ShotgunButtonPressed();
        }
    }

    public void UpdateInventory()
    {
        foreach (var b in weaponButtons) 
        { 
            //b.interactable = false;
            b.gameObject.SetActive(false);
        }
        for (int i = 0; i < availableWeapons.Count; i++)
        {
            //weaponButtons[i].interactable = true;
            weaponButtons[i].gameObject.SetActive(true);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(1, .2f);
        CloseInventory();
        UpdateInventory();

    }
    public void Hide()
    {
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(0, .2f).OnComplete(() => gameObject.SetActive(false)).SetUpdate(true);
    }

    public void InventoryButtonPressed()
    {
        if (!_isInventoryOpen)
        {
            OpenInventory();
        }
        else
        {
            CloseInventory();
        }
    }


    private void OpenInventory()
    {
        _isInventoryOpen = true;
        inventoryObjectsUI.gameObject.SetActive(true);
        inventoryObjectsUI.DOKill();
        inventoryObjectsUI.DOFade(1, .2f);
    }

    private void CloseInventory()
    {
        _isInventoryOpen = false;
        inventoryObjectsUI.DOKill();
        inventoryObjectsUI.DOFade(1, .2f).OnComplete(() => inventoryObjectsUI.gameObject.SetActive(false));
    }

    public void MachineGunButtonPressed()
    {
        if (_activeWeapon != WeaponType.MachineGun)
        {
            GameDirector.instance.player.weapon.WeaponButtonPressed(WeaponType.MachineGun);
            CloseInventory();
            _activeWeapon = WeaponType.MachineGun;
        }
    }

    public void ShotgunButtonPressed()
    {
        if (_activeWeapon != WeaponType.Shotgun)
        {
            GameDirector.instance.player.weapon.WeaponButtonPressed(WeaponType.Shotgun);
            CloseInventory();
            _activeWeapon = WeaponType.Shotgun;
        }
    }

    internal void WeaponCollected(WeaponType weaponType)
    {
        availableWeapons.Add(weaponType);
        UpdateInventory();
        if (weaponType == WeaponType.Shotgun)
        {
            _isShotgunCollected = true;
        }
    }
}
