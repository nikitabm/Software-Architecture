﻿namespace Hobgoblin.Model
{
    using System;
    using System.Collections.Generic;
    using GXPEngine;
    using Hobgoblin.Utils;

    public class ShopModel : IObservable<ShopData>
    {
        private List<IObserver<ShopData>> _observers;
        private ShopData _data;

        private const int MaxMessageQueueCount = 4; //it caches the last four messages
        private List<string> _messages = new List<string>();

        private List<Item> _items; //items in the store
        private int _selectedItemIndex = 0; //selected item index

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  ShopModel()
        //------------------------------------------------------------------------------------------------------------------------        
        public ShopModel(List<Item> pItems)
        {
            _items = new List<Item>(pItems);

            _data = new ShopData();
            UpdateShopData();

            _observers = new List<IObserver<ShopData>>();
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  GetSelectedItem()
        //------------------------------------------------------------------------------------------------------------------------        
        //returns the selected item
        public Item GetSelectedItem()
        {
            if (_selectedItemIndex >= 0 && _selectedItemIndex < _items.Count)
            {
                return _items[_selectedItemIndex];
            }
            else
            {
                return null;
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  SelectItem()
        //------------------------------------------------------------------------------------------------------------------------
        //attempts to select the given item, fails silently
        public void SelectItem(Item item)
        {
            if (item != null)
            {
                int index = _items.IndexOf(item);
                if (index >= 0)
                {
                    _selectedItemIndex = index;
                    UpdateShopData();
                    NotifyObservers();
                }
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  SelectItemByIndex()
        //------------------------------------------------------------------------------------------------------------------------        
        //attempts to select the item, specified by 'index', fails silently
        public void SelectItemByIndex(int index)
        {
            if (index >= 0 && index < _items.Count)
            {
                _selectedItemIndex = index;
                UpdateShopData();
                NotifyObservers();
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  GetSelectedItemIndex()
        //------------------------------------------------------------------------------------------------------------------------
        //returns the index of the current selected item
        public int GetSelectedItemIndex()
        {
            return _selectedItemIndex;
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  GetItems()
        //------------------------------------------------------------------------------------------------------------------------        
        //returns a deepcopied list with all current items in the shop.
        public List<Item> GetItems()
        {
            return HUtils.DeepCopyList(_items);
        }
        //------------------------------------------------------------------------------------------------------------------------
        //                                                  GetItemCount()
        //------------------------------------------------------------------------------------------------------------------------        
        //returns the number of items
        private int GetItemCount()
        {
            return _items.Count;
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  GetItemByIndex()
        //------------------------------------------------------------------------------------------------------------------------        
        //tries to get an item, specified by index. returns null if unsuccessful
        public Item GetItemByIndex(int index)
        {
            if (index >= 0 && index < _items.Count)
            {
                return _items[index];
            }
            else
            {
                return null;
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  GetMessage()
        //------------------------------------------------------------------------------------------------------------------------        
        //returns the cached list of messages
        public string[] GetMessages()
        {
            return _messages.ToArray();
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  AddMessage()
        //------------------------------------------------------------------------------------------------------------------------
        //adds a message to the cache, cleaning it up if the limit is exceeded
        private void AddMessage(string message)
        {
            _messages.Add(message);
            while (_messages.Count > MaxMessageQueueCount)
            {
                _messages.RemoveAt(0);
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  Buy()
        //------------------------------------------------------------------------------------------------------------------------        
        //not implemented yet TODO
        public bool Buy()
        {
            Item selectedItem = GetSelectedItem();
            if (selectedItem == null)
            {
                AddMessage("You can't buy this item! Come back with more gold or give me your laptop!");
                return false;
            }
            selectedItem.Amount--;
            AddMessage($" Whispering something rude in orc, Hobgoblin (shopKeeper) sells you {selectedItem.name} for {selectedItem.price} gold.");
            if (selectedItem.Amount <= 0)
            {
                AddMessage($" It was the last {selectedItem.name} in the shop! come back later for more.");
                AddMessage(" If you are not scared of goblins or course, ha-ha!");
                _items.Remove(selectedItem);
                _selectedItemIndex = (int)Mathf.Clamp(_selectedItemIndex, 0, _items.Count - 1);
            }
            UpdateShopData();
            NotifyObservers();

            return true;
        }


        //------------------------------------------------------------------------------------------------------------------------
        //                                                  Sell()
        //------------------------------------------------------------------------------------------------------------------------        
        //not implemented yet TODO
        public void Sell()
        {
            if (GetSelectedItem() != null)
            {
                GetSelectedItem().Amount++;
                UpdateShopData();
                NotifyObservers();
            }
        }

        private void UpdateShopData()
        {
            _data.items = _items;
            _data.itemCount = _items.Count;
            if (_items.Count > 0)
            {
                _data.selectedItemIndex = _selectedItemIndex;
                _data.selectedItem = (Item)_items[_selectedItemIndex].Clone();
            }
            else
            {
                _data.selectedItemIndex = 0;
                _data.selectedItem = null;
                AddMessage("There are no items in the shop, come back later");
            }

            _data.messages = DeepCopyMessages();

        }

        private List<string> DeepCopyMessages()
        {
            var deepCopyList = new List<string>();
            foreach (var msg in _messages)
            {
                deepCopyList.Add((string)msg.Clone());
            }
            return deepCopyList;
        }

        private void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(_data);
            }
        }

        public IDisposable Subscribe(IObserver<ShopData> observer)
        {
            // Check whether observer is already registered. If not, add it
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
                // Provide observer with existing data.
                observer.OnNext(_data);
            }
            return new Unsubscriber<ShopData>(_observers, observer);
        }
    }
}
