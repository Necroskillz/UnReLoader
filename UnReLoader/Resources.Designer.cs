﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NecroNet.UnReLoader {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NecroNet.UnReLoader.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Can not create tool window..
        /// </summary>
        internal static string CanNotCreateWindow {
            get {
                return ResourceManager.GetString("CanNotCreateWindow", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No solution is loaded..
        /// </summary>
        internal static string ErrorMessageNoSolutionLoaded {
            get {
                return ResourceManager.GetString("ErrorMessageNoSolutionLoaded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Project {0} was not found..
        /// </summary>
        internal static string ErrorMessageProjectWasNotFound {
            get {
                return ResourceManager.GetString("ErrorMessageProjectWasNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Reloading {0}.
        /// </summary>
        internal static string MessageReloadingProject {
            get {
                return ResourceManager.GetString("MessageReloadingProject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Setting {0} as startup.
        /// </summary>
        internal static string MessageSettingProjectAsStartup {
            get {
                return ResourceManager.GetString("MessageSettingProjectAsStartup", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unloading {0}.
        /// </summary>
        internal static string MessageUnloadingProject {
            get {
                return ResourceManager.GetString("MessageUnloadingProject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UnReLoader Console.
        /// </summary>
        internal static string ToolWindowTitle {
            get {
                return ResourceManager.GetString("ToolWindowTitle", resourceCulture);
            }
        }
    }
}