using GXPEngine;
using States;

public class MyGame : Game
{

    private ShopBrowseState shopBrowseState;

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  MyGame()
    //------------------------------------------------------------------------------------------------------------------------        
    public MyGame() : base(800, 600, false)
	{
        shopBrowseState = new ShopBrowseState();
        AddChild(shopBrowseState);
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Update()
    //------------------------------------------------------------------------------------------------------------------------        
    void Update()
    {
        shopBrowseState.Step();
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Main()
    //------------------------------------------------------------------------------------------------------------------------        
    static void Main()
	{
        new MyGame().Start();
	}
}