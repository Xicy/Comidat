﻿using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Comidat")]
[assembly: AssemblyDescription("Trace Personal")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Xicy")]
[assembly: AssemblyProduct("Comidat.Viewer.TwoD")]
[assembly: AssemblyCopyright("Copyright ©  2018")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("c60484e7-0d47-44d6-9893-5a2b3dde5d91")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.1")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: Obfuscation(Exclude = false, Feature = "strong name key:../../Comidat.snk")]
[assembly: Obfuscation(Exclude = false, Feature = "+constants")]
[assembly: Obfuscation(Exclude = false, Feature = "+anti ildasm")]
[assembly: Obfuscation(Exclude = false, Feature = "+anti debug")]
//[assembly: Obfuscation(Exclude = false, Feature = "+resources(mode=dynamic)")] //Deteced by antivirus
[assembly: Obfuscation(Exclude = false, Feature = "+ctrl flow(predicate=expression)")]
[assembly: Obfuscation(Exclude = false, Feature = "+rename(mode=reversible,password=Umut1996)")]
