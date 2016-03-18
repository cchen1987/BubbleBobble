/**
 * Image.cs - To hide SDL image handling
 * 
 * Changes:
 * 0.01, 20-11-2015: Basic skeleton
 */



using System;
using Tao.Sdl;


public class Image
{
    private IntPtr internalPointer;

    public Image(string fileName)
    {
        Load(fileName);
    }

    public void Load(string fileName)
    {
        internalPointer = SdlImage.IMG_Load(fileName);
        if (internalPointer == IntPtr.Zero)
            Hardware.FatalError("Image not found: " + fileName);
    }


    public IntPtr GetPointer()
    {
        return internalPointer;
    }
}
