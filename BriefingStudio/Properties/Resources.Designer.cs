﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BriefingStudio.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BriefingStudio.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to TXB BRIEFING HELP FOR DESCENT I
        ///===============================
        ///
        ///Sequence
        ///--------
        ///
        ///Sequence numbers are screen numbers, and are hardcoded.
        ///For a complete table of screen numbers, consul
        ///Appendix A.
        ///
        ///Notes
        ///-----
        ///Note that text colors are hardcoded, but will be matched
        ///against the palette of the current PCX background,
        ///allowing some variation. In order to preserve the
        ///correct colors, they should be present in the PCX palette.
        ///
        ///Any lines starting with ; are ignored.
        ///
        ///Commands
        ///--------
        ///
        ///S [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string briefingHelpD1 {
            get {
                return ResourceManager.GetString("briefingHelpD1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to TXB BRIEFING HELP FOR DESCENT II
        ///================================
        ///
        ///Sequence
        ///--------
        ///
        ///Sequence numbers correspond to level numbers.
        ///The corresponding sequence N is played as a briefing
        ///before level N starts. Sequence 0 is not used.
        ///
        ///To play a briefing after all of the levels are complete,
        ///use the sequence number N+1, where N is the number
        ///of levels.
        ///
        ///Briefings cannot be added to secret levels.
        ///
        ///Notes
        ///-----
        ///Note that text colors are hardcoded, but will be matched
        ///against the palette of the [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string briefingHelpD2 {
            get {
                return ResourceManager.GetString("briefingHelpD2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap toolStripButtonBlue_Image {
            get {
                object obj = ResourceManager.GetObject("toolStripButtonBlue_Image", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap toolStripButtonGray_Image {
            get {
                object obj = ResourceManager.GetObject("toolStripButtonGray_Image", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap toolStripButtonGreen_Image {
            get {
                object obj = ResourceManager.GetObject("toolStripButtonGreen_Image", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
    }
}
