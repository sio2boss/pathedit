/*
 * This file is part of PathEdit.
 * 
 * PathEdit is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 */

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace PathEdit
{
	/// <summary>
	/// Wrapper for ShellExecute (shell32.dll).  Makes easy work of launching a 
	/// file/folder with default application.
	/// </summary>
	public class Win32Calls
	{

		#region ShellExecute

		// Performs an operation on a specified file.
		[DllImport("shell32.dll")]
		private static extern IntPtr ShellExecute(
			IntPtr hwnd,
			[MarshalAs(UnmanagedType.LPStr)]
			String lpOperation,
			[MarshalAs(UnmanagedType.LPStr)]
			String lpFile,
			[MarshalAs(UnmanagedType.LPStr)]
			String lpParameters,
			[MarshalAs(UnmanagedType.LPStr)]
			String lpDirectory,
			Int32 nShowCmd);

		/// <summary>
		/// Fires the ShellExecute function in user32.dll
		/// </summary>
		/// <param name="path">Path/File to open</param>
		/// <returns>True if successful</returns>
		public static bool open(string path)
		{
			int iRetVal = (int)ShellExecute(
				IntPtr.Zero, "open", path, "", "", 1);

			return (iRetVal > 32) ? true : false;
		}

		#endregion

		#region SendMessageTimeout

		[DllImport("user32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		private static extern bool SendMessageTimeout( 
			IntPtr hWnd,
			int Msg,
			int wParam,
			string lParam,
			int fuFlags,
			int uTimeout,
			out int lpdwResult);

		private static int HWND_BROADCAST = 0xffff;
		private static int WM_SETTINGCHANGE = 0x001A;
		private static int SMTO_NORMAL = 0x0000;
		private static int SMTO_BLOCK = 0x0001;
		private static int SMTO_ABORTIFHUNG = 0x0002;
		private static int SMTO_NOTIMEOUTIFNOTHUNG = 0x0008;

		public static void refreshEnviroment()
		{
			int dwReturnValue = 0;
			SendMessageTimeout((IntPtr)HWND_BROADCAST,
				WM_SETTINGCHANGE,
				0,
				"Environment",
				SMTO_ABORTIFHUNG,
				5000,
				out dwReturnValue);
		}

		#endregion
	}
}
