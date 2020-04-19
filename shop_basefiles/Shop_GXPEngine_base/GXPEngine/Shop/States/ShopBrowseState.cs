﻿using System;
using GXPEngine;
using Core;
namespace States
{
    using Model;
    using View;
    using Controller;
    using System.Collections.Generic;

    public class ShopBrowseState : MBGameObject
    {
        private ShopController shopController;
        private ShopView shopView;
        private ShopMessageView shopMessageView;

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  ShopBrowseState()
        //------------------------------------------------------------------------------------------------------------------------
        public ShopBrowseState(List<Item> items)
        {
            //create shop
            ShopModel shop = new ShopModel(items);

            //create controller
            shopController = new ShopController(shop);

            //create shop view
            shopView = new ShopView(shop, shopController);
            AddChild(shopView);
            //shopView.Subscribe(shop);
            Helper.AlignToCenter(shopView, true, true);

            //create message view
            shopMessageView = new ShopMessageView(shop);
            AddChild(shopMessageView);
            //shopMessageView.Subscribe(shop);
            Helper.AlignToCenter(shopMessageView, true, false);

        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  Step()
        //------------------------------------------------------------------------------------------------------------------------
        //update the views
        public void Step()
        {
            shopView.Step();
            shopMessageView.Step();
        }

    }
}
