using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Ccr.Std.Core.Extensions;
using CultureWars.Core.FluentConsole.Colorization.Win32;
using CultureWars.Core.FluentConsole.Exceptions;
using CultureWars.Core.FluentConsole.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Core.FluentConsole.Colorization
{
	/// <summary>
	///	Exposes methods used for mapping System.Drawing.Colors to System.ConsoleColors.	Based on code
	/// that was originally written by Alex Shvedov, and that was then modified by MercuryP.
	/// </summary>
	public sealed class ColorMapper
	{
		[StructLayout(LayoutKind.Sequential)]
		internal struct COORD
		{
			internal short X;

			internal short Y;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct SMALL_RECT
		{
			internal short Left;

			internal short Top;

			internal short Right;

			internal short Bottom;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct CONSOLE_SCREEN_BUFFER_INFO_EX
		{
			internal int cbSize;

			internal COORD dwSize;

			internal COORD dwCursorPosition;

			internal ushort wAttributes;

			internal SMALL_RECT srWindow;

			internal COORD dwMaximumWindowSize;

			internal ushort wPopupAttributes;

			internal bool bFullscreenSupported;


			internal COLORREF black;

			internal COLORREF darkBlue;

			internal COLORREF darkGreen;

			internal COLORREF darkCyan;

			internal COLORREF darkRed;

			internal COLORREF darkMagenta;

			internal COLORREF darkYellow;

			internal COLORREF gray;

			internal COLORREF darkGray;

			internal COLORREF blue;

			internal COLORREF green;

			internal COLORREF cyan;

			internal COLORREF red;

			internal COLORREF magenta;

			internal COLORREF yellow;

			internal COLORREF white;
		}


		internal class ColorRefInfo
		{
			[NotNull]
			private readonly Expression<Func<CONSOLE_SCREEN_BUFFER_INFO_EX, COLORREF>> _fieldExpression;
			private readonly Func<CONSOLE_SCREEN_BUFFER_INFO_EX, COLORREF> _fieldGetterFunc;
			private readonly Action<CONSOLE_SCREEN_BUFFER_INFO_EX, COLORREF> _fieldSetterFunc;


			public ConsoleColor ConsoleColor { get; }

			public string ColorName { get; }
			

			private ColorRefInfo(
				ConsoleColor consoleColor,
				[NotNull] Expression<Func<CONSOLE_SCREEN_BUFFER_INFO_EX, COLORREF>> fieldExpression)
			{
				ConsoleColor = consoleColor;

				fieldExpression.IsNotNull(nameof(fieldExpression));
				_fieldExpression = fieldExpression;

				var memberBodiedExpression = fieldExpression.GetEmbodiedMemberExpression();
				ColorName = memberBodiedExpression.Member.Name;

				_fieldGetterFunc = _fieldExpression.Compile();

				var fieldSetterExpression = _fieldExpression.ToFieldSetterExpression();
				_fieldSetterFunc = fieldSetterExpression.Compile();
			}


			internal static ColorRefInfo Define(
				ConsoleColor consoleColor,
				[NotNull] Expression<Func<CONSOLE_SCREEN_BUFFER_INFO_EX, COLORREF>> fieldExpression)
			{
				return new ColorRefInfo(
					consoleColor,
					fieldExpression);
			}


			internal COLORREF GetFieldValue(
				CONSOLE_SCREEN_BUFFER_INFO_EX @this)
			{
				return _fieldGetterFunc(@this);
			}

			internal void SetFieldValue(
				CONSOLE_SCREEN_BUFFER_INFO_EX @this,
				COLORREF value)
			{ 
				_fieldSetterFunc(@this, value);
			}
		}


		private const int STD_OUTPUT_HANDLE = -11;
		private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);


		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr GetStdHandle(int nStdHandle);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool GetConsoleScreenBufferInfoEx(
			IntPtr hConsoleOutput,
			ref CONSOLE_SCREEN_BUFFER_INFO_EX csbe);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool SetConsoleScreenBufferInfoEx(
			IntPtr hConsoleOutput,
			ref CONSOLE_SCREEN_BUFFER_INFO_EX csbe);




		private static readonly IReadOnlyCollection<ColorRefInfo> _bufferColorRefInfos = new[]
		{
			ColorRefInfo.Define(ConsoleColor.Black, t => t.black),
			ColorRefInfo.Define(ConsoleColor.DarkBlue, t => t.darkBlue),
			ColorRefInfo.Define(ConsoleColor.DarkGreen, t => t.darkGreen),
			ColorRefInfo.Define(ConsoleColor.DarkCyan, t => t.darkCyan),
			ColorRefInfo.Define(ConsoleColor.DarkRed, t => t.darkRed),
			ColorRefInfo.Define(ConsoleColor.DarkMagenta, t => t.darkMagenta),
			ColorRefInfo.Define(ConsoleColor.DarkYellow, t => t.darkYellow),
			ColorRefInfo.Define(ConsoleColor.Gray, t => t.gray),
			ColorRefInfo.Define(ConsoleColor.DarkGray, t => t.darkGray),
			ColorRefInfo.Define(ConsoleColor.Blue, t => t.blue),
			ColorRefInfo.Define(ConsoleColor.Green, t => t.green),
			ColorRefInfo.Define(ConsoleColor.Cyan, t => t.cyan),
			ColorRefInfo.Define(ConsoleColor.Red, t => t.red),
			ColorRefInfo.Define(ConsoleColor.Magenta, t => t.magenta),
			ColorRefInfo.Define(ConsoleColor.Yellow, t => t.yellow),
			ColorRefInfo.Define(ConsoleColor.White, t => t.white)
		};

		internal static IReadOnlyDictionary<ConsoleColor, ColorRefInfo> ConsoleColorToColorRefInfoMap
		{
			get => _bufferColorRefInfos.ToDictionary(
				t => t.ConsoleColor,
				t => t);
		}


		/// <summary>
		/// Gets a collection of all 16 colors in the console buffer.
		/// </summary>
		/// <returns>
		/// Returns all 16 COLORREFs in the console buffer as a dictionary keyed by the COLORREF's alias
		/// in the buffer's ColorTable.
		/// </returns>
		public Dictionary<string, COLORREF> GetBufferColors()
		{
			var colors = new Dictionary<string, COLORREF>();

			var hConsoleOutput = GetStdHandle(STD_OUTPUT_HANDLE);
			var csbe = GetBufferInfo(hConsoleOutput);

			foreach (var bufferColorRefInfo in _bufferColorRefInfos)
			{
				var colorKeyName = bufferColorRefInfo.ColorName;
				var colorRef = bufferColorRefInfo.GetFieldValue(csbe);

				colors.Add(colorKeyName, colorRef);
			}
			return colors;
		}

		/// <summary>
		///	Sets all 16 colors in the console buffer using colors supplied in a dictionary.
		/// </summary>
		/// <param name="colors">
		///	A dictionary containing a pair between a color's alias name defined in the buffer's
		///	ColorTable, and its corresponding <see cref="COLORREF"/> value.
		/// </param>
		public void SetBatchBufferColors(
			Dictionary<string, COLORREF> colors)
		{
			var hConsoleOutput = GetStdHandle(STD_OUTPUT_HANDLE);
			var csbe = GetBufferInfo(hConsoleOutput);

			foreach (var bufferColorRefInfo in _bufferColorRefInfos)
			{
				var colorKeyName = bufferColorRefInfo.ColorName;
				var colorRef = colors[colorKeyName];

				bufferColorRefInfo.SetFieldValue(csbe, colorRef);
			}
			SetBufferInfo(hConsoleOutput, csbe);
		}


		private CONSOLE_SCREEN_BUFFER_INFO_EX GetBufferInfo(
			IntPtr hConsoleOutput)
		{
			var csbe = new CONSOLE_SCREEN_BUFFER_INFO_EX();
			csbe.cbSize = Marshal.SizeOf(csbe); // 96 = 0x60

			if (hConsoleOutput == INVALID_HANDLE_VALUE)
				throw CreateException(Marshal.GetLastWin32Error());
			
			var brc = GetConsoleScreenBufferInfoEx(hConsoleOutput, ref csbe);

			if (!brc)
				throw CreateException(Marshal.GetLastWin32Error());
			
			return csbe;
		}


		/// <summary>
		/// Maps a System.Drawing.Color to a System.ConsoleColor.
		/// </summary>
		/// <param name="oldColor">
		/// The color to be replaced.
		/// </param>
		/// <param name="newColor">
		/// The color to be mapped.
		/// </param>
		/// <remarks>
		/// The default console colors used are gray (foreground) and black (background).
		/// </remarks>
		public void MapColor(
			ConsoleColor oldColor,
			Color newColor)
		{
			MapColor(
				oldColor,
				newColor.R, 
				newColor.G,
				newColor.B);
		}

		private void MapColor(
			ConsoleColor color,
			uint r,
			uint g,
			uint b)
		{
			var hConsoleOutput = GetStdHandle(STD_OUTPUT_HANDLE);
			var csbe = GetBufferInfo(hConsoleOutput);

			if (!ConsoleColorToColorRefInfoMap.TryGetValue(color, out var colorRefInfo))
				throw new KeyNotFoundException(
					$"Cannot find the key {nameof(ConsoleColor).SQuote()} in the dictionary mapping " +
					$"ConsoleColorToColorRefInfoMap.");

			var colorRef = new COLORREF(r, g, b);
			colorRefInfo.SetFieldValue(csbe, colorRef);
			
			SetBufferInfo(hConsoleOutput, csbe);
		}

		private void SetBufferInfo(
			IntPtr hConsoleOutput,
			CONSOLE_SCREEN_BUFFER_INFO_EX csbe)
		{
			csbe.srWindow.Bottom++;
			csbe.srWindow.Right++;

			var brc = SetConsoleScreenBufferInfoEx(hConsoleOutput, ref csbe);

			if (!brc)
				throw CreateException(Marshal.GetLastWin32Error());
		}

		private Exception CreateException(
			int errorCode)
		{
			const int ERROR_INVALID_HANDLE = 6;

			// Raised if the console is being run via another application, for example.
			if (errorCode == ERROR_INVALID_HANDLE)
				return new ConsoleAccessException();
			
			return new ColorMappingException(errorCode);
		}
	}
}



//switch (color)
//{
//case ConsoleColor.Black:
//csbe.black = new COLORREF(r, g, b);
//	break;
//	case ConsoleColor.DarkBlue:
//csbe.darkBlue = new COLORREF(r, g, b);
//	break;
//	case ConsoleColor.DarkGreen:
//csbe.darkGreen = new COLORREF(r, g, b);
//	break;
//	case ConsoleColor.DarkCyan:
//csbe.darkCyan = new COLORREF(r, g, b);
//	break;
//	case ConsoleColor.DarkRed:
//csbe.darkRed = new COLORREF(r, g, b);
//	break;
//	case ConsoleColor.DarkMagenta:
//csbe.darkMagenta = new COLORREF(r, g, b);
//	break;
//	case ConsoleColor.DarkYellow:
//csbe.darkYellow = new COLORREF(r, g, b);
//	break;
//	case ConsoleColor.Gray:
//csbe.gray = new COLORREF(r, g, b);
//	break;
//	case ConsoleColor.DarkGray:
//csbe.darkGray = new COLORREF(r, g, b);
//	break;
//	case ConsoleColor.Blue:
//csbe.blue = new COLORREF(r, g, b);
//	break;
//	case ConsoleColor.Green:
//csbe.green = new COLORREF(r, g, b);
//	break;
//	case ConsoleColor.Cyan:
//csbe.cyan = new COLORREF(r, g, b);
//	break;
//	case ConsoleColor.Red:
//csbe.red = new COLORREF(r, g, b);
//	break;
//	case ConsoleColor.Magenta:
//csbe.magenta = new COLORREF(r, g, b);
//	break;
//	case ConsoleColor.Yellow:
//csbe.yellow = new COLORREF(r, g, b);
//	break;
//	case ConsoleColor.White:
//csbe.white = new COLORREF(r, g, b);
//	break;
//	default:
//	throw new ArgumentOutOfRangeException(
//	nameof(color), color, null);
//}



//csbe.black = colors["black"];
//csbe.darkBlue = colors["darkBlue"];
//csbe.darkGreen = colors["darkGreen"];
//csbe.darkCyan = colors["darkCyan"];
//csbe.darkRed = colors["darkRed"];
//csbe.darkMagenta = colors["darkMagenta"];
//csbe.darkYellow = colors["darkYellow"];
//csbe.gray = colors["gray"];
//csbe.darkGray = colors["darkGray"];
//csbe.blue = colors["blue"];
//csbe.green = colors["green"];
//csbe.cyan = colors["cyan"];
//csbe.red = colors["red"];
//csbe.magenta = colors["magenta"];
//csbe.yellow = colors["yellow"];
//csbe.white = colors["white"];



//colors.Add("black", csbe.black);
//colors.Add("darkBlue", csbe.darkBlue);
//colors.Add("darkGreen", csbe.darkGreen);
//colors.Add("darkCyan", csbe.darkCyan);
//colors.Add("darkRed", csbe.darkRed);
//colors.Add("darkMagenta", csbe.darkMagenta);
//colors.Add("darkYellow", csbe.darkYellow);
//colors.Add("gray", csbe.gray);
//colors.Add("darkGray", csbe.darkGray);
//colors.Add("blue", csbe.blue);
//colors.Add("green", csbe.green);
//colors.Add("cyan", csbe.cyan);
//colors.Add("red", csbe.red);
//colors.Add("magenta", csbe.magenta);
//colors.Add("yellow", csbe.yellow);
//colors.Add("white", csbe.white);