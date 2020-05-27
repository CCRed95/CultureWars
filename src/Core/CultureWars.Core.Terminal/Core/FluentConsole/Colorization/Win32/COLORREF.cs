using System.Drawing;
using System.Runtime.InteropServices;

namespace CultureWars.Core.FluentConsole.Colorization.Win32
{
	/// <summary>
	/// A Win32 COLORREF, used to specify an RGB color.  See MSDN for more information:
	/// https://msdn.microsoft.com/en-us/library/windows/desktop/dd183449(v=vs.85).aspx
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct COLORREF
	{
		private readonly uint colorDWORD;


		internal COLORREF(Color color)
		{
			colorDWORD = color.R 
				+ ((uint)color.G << 8) 
				+ ((uint)color.B << 16);
		}

		internal COLORREF(uint r, uint g, uint b)
		{
			colorDWORD = r + (g << 8) + (b << 16);
		}

		public override string ToString()
		{
			return colorDWORD.ToString();
		}
	}
}
