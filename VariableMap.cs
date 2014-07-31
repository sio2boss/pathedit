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
using System.Collections;
using Microsoft.Win32;

namespace PathEdit
{
	/// <summary>
	/// Summary description for VariableMap.
	/// </summary>
	public class VariableMap
	{
		public enum ENVIROMENT { SYSTEM, LOCAL };

		private ENVIROMENT type;
		private Hashtable paths = null;
		private Hashtable clean = null;
		private Hashtable remove = null;

		/// <summary>
		/// Constructor, instantiates the 
		/// </summary>
		/// <param name="type"></param>
		public VariableMap(ENVIROMENT type)
		{
			this.paths = new Hashtable();
			this.clean = new Hashtable();
			this.remove = new Hashtable();
			this.type = type;
			this.refresh();
		}

		/// <summary>
		/// Dumps origional 
		/// </summary>
		public void refresh() 
		{
			// Make prefix
			string prefix = GetPrefix();

			// Get registry key
			RegistryKey key = null;
			if ((key = GetRegistryKey()) == null) return;

			// Make sure tables are empty
			paths.Clear();
			clean.Clear();
			remove.Clear();

			// Add values to the tables
			string[] values = key.GetValueNames();
			foreach (string val in values)
			{
				paths.Add(prefix + val, key.GetValue(val).ToString());
				clean.Add(prefix + val, true);
			}
			
			// Done with RegistryKey
			key.Flush();
			key.Close();

		}

		/// <summary>
		/// Performs checks for and writes all modifications to the
		/// registry.
		/// </summary>
		public void apply()
		{
			// Open RegistryKey
			RegistryKey key = null;
			if ((key = GetRegistryKey()) == null) return;

			// Write all the changed values to the registry
			IDictionaryEnumerator en = paths.GetEnumerator();
			while (en.MoveNext())
			{
				string k = GetRegistryValue((string)en.Key);

				// Check for modification
				if (!(bool)clean[(string)en.Key])
				{
					// Write
					//System.Console.WriteLine("Writing key: " + k);
					key.SetValue(k, (string)paths[(string)en.Key]);
					clean[(string)en.Key] = true;
				}
			}

			// Remove items in list
			IDictionaryEnumerator rm = remove.GetEnumerator();
			while (rm.MoveNext())
			{
				string k = GetRegistryValue((string)rm.Key);
				key.DeleteValue(k, false);
			}

			// Done with Registry key
			key.Flush();
			key.Close();
		}

		/// <summary>
		/// Returns array of strings generated from the
		/// keys of the path map.
		/// </summary>
		/// <returns>Array of path names.</returns>
		public string[] GetKeys()
		{
			int i=0;
			string[] keys = new string[paths.Keys.Count];
			foreach(string key in paths.Keys)
			{
				keys[i] = key;
				i++;
			}
			return keys;
		}

		/// <summary>
		/// Returns array of strings split from the
		/// path string pulled from registry.
		/// </summary>
		/// <param name="key">Key to get path for.</param>
		/// <returns>Array of path entries.</returns>
		public string[] GetPath(string key)
		{
			string path = (string)paths[key];
			if (path == null) return null;
			return path.Split(';');
		}

		/// <summary>
		/// Accessor method to change path for variable.
		/// </summary>
		/// <param name="key">Variable to change.</param>
		/// <param name="entry">New path for variable.</param>
		public void SetPath(string key, string entry)
		{
			paths[key] = entry;
			clean[key] = false;
		}

		/// <summary>
		/// Added new variable, must be in location.variable_name format.
		/// ie. SYSTEM.Path.
		/// </summary>
		/// <param name="key">Key to add.</param>
		/// <returns>True if successful, false otherwise.</returns>
		public bool AddVariable(string key)
		{
			if (paths[key] != null) return false;
			paths.Add(key, "");
			clean.Add(key, false);
			return true;
		}

		/// <summary>
		/// Moves path from one variable to a new one, basically
		/// renaming the variable.
		/// </summary>
		/// <param name="old">Old variable name.</param
		/// <param name="var">New variable name.</param>>
		public void RenameVariable(string old, string var)
		{
			paths[var] = paths[old];
			clean[var] = false;
			DeleteVariable(old);
		}

		/// <summary>
		/// Removes key
		/// </summary>
		/// <param name="key"></param>
		public void DeleteVariable(string key)
		{
			// Copy to remove table
			remove.Add(key, paths[key]);

			// Remove from viewed table
			paths.Remove(key);
			clean.Remove(key);
		}

		/// <summary>
		/// Convert table format to retistry value format,
		/// ie: System.Path to Path.
		/// </summary>
		/// <param name="key">Table key to convert.</param>
		/// <returns>Formatted string, or null if bad input.</returns>
		public string GetRegistryValue(string key)
		{
			int i = key.IndexOf(".") + 1;
			if (i == 0 ) return null;
			return key.Substring(i, key.Length - i);
		}

		/// <summary>
		/// Easy access to the correct registry key for the specified, 
		/// type of enviroment variable.
		/// </summary>
		/// <returns>A RegistryKey depending on the type of ENVIRONMENT.</returns>
		private RegistryKey GetRegistryKey()
		{
			if (type == ENVIROMENT.LOCAL) 
			{
				return Registry.CurrentUser.CreateSubKey(
					@"Environment");
			} 
			else if (type == ENVIROMENT.SYSTEM)
			{
				return Registry.LocalMachine.CreateSubKey(
					@"SYSTEM\CurrentControlSet\Control\Session Manager\Environment");
			}
			return null;
		}

		/// <summary>
		/// Creates a string coresponding to the ENVIRONMENT of the class.
		/// </summary>
		/// <returns>String specifying which enviroment.</returns>
		private string GetPrefix()
		{
			if (type == ENVIROMENT.LOCAL) 
			{
				return "Local.";
			} 
			else if (type == ENVIROMENT.SYSTEM)
			{	
				return "System.";
			}
			return "";
		}

		/// <summary>
		/// Practical ToString, actually prints map data.
		/// </summary>
		/// <returns>String representing </returns>
		public override string ToString()
		{
			string s = "";
			IDictionaryEnumerator en = paths.GetEnumerator();

			while (en.MoveNext())
			{
				s += en.Key + " : " + en.Value + "\n";
			}
			return s;
		}
	}
}
