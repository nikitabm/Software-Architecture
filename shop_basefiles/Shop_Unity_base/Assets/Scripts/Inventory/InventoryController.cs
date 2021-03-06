﻿using System;
using Hobgoblin.Model;

namespace Hobgoblin.InventoryMvc
{
    public class InventoryController
    {
        private InventoryModel _model;
        public InventoryController(InventoryModel pModel)
        {
            _model = pModel;
        }
        //------------------------------------------------------------------------------------------------------------------------
        //                                                  SelectItem()
        //------------------------------------------------------------------------------------------------------------------------
        //attempt to select an item
        public void SelectItem(Item pItem)
        {
            if (pItem != null)
            {
                _model.SelectItem(pItem);
                UpdateData();
                NotifyObservers();

            }
        }
        public int GetItemId(Item pItem)
        {
            return _model.GetItemId(pItem);
        }

        public void SetSelectedItemId(int pId)
        {
            _model.SelectItemByIndex(pId);
        }

        public void RemoveCurrentItem()
        {
            _model.RemoveCurrentItem();
        }
        public void AddGold(int pGold)
        {
            _model.AddGold(pGold);
        }
        public Item GetSelectedItem()
        {
            return _model.GetSelectedItem();
        }

        public void AddMessage(string pMessage)
        {
            _model.AddMessage(pMessage);
            UpdateData();
            NotifyObservers();
        }

        private void NotifyObservers()
        {
            _model.NotifyObservers();
        }

        private void UpdateData()
        {
            _model.UpdateData();
        }

        public void SelectItem(int pIndex)
        {
            _model.SelectItemByIndex(pIndex);
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  Browse()
        //------------------------------------------------------------------------------------------------------------------------
        public void Browse()
        {
            _model.SelectItemByIndex(0);
        }
    }
}
