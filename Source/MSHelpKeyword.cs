//===============================================================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : MSHelpKeyword.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 04/08/2021
// Note    : Copyright 2008-2021, Eric Woodruff, All rights reserved
//
// This file contains a class representing an HTML Help 2.x index keyword that can be added to the XML data
// island in each help topic generated by BuildAssembler.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://GitHub.com/EWSoftware/HtmlToMamlConverter.  This
// notice, the author's name, and all copyright notices must remain intact in all applications, documentation,
// and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/25/2008  EFW  Created the code
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Globalization;
using System.Net;

namespace HtmlToMamlConversion
{
    /// <summary>
    /// This represents an HTML Help 2.x index keyword that can be added to the XML data island in each help
    /// topic generated by BuildAssembler.
    /// </summary>
    [DefaultProperty("Index"), Serializable]
    public class MSHelpKeyword : IComparable<MSHelpKeyword>
    {
        #region Private data members
        //=====================================================================

        private string index;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to get or set the index name
        /// </summary>
        [Category("Index"), Description("The name of the index"), DefaultValue(null)]
        public string Index
        {
            get => index;
            set
            {
                if(value == null || value.Trim().Length == 0)
                    index = "K";
                else
                    index = value;
            }
        }

        /// <summary>
        /// This is used to get or set the index term
        /// </summary>
        [Category("Index"), Description("The value of the index term"), DefaultValue(null)]
        public string Term { get; set; }

        #endregion

        #region Constructors
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>The index name defaults to "K"</remarks>
        public MSHelpKeyword()
        {
            this.Index = null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="indexName">The index name</param>
        /// <param name="keywordTerm">The index term</param>
        public MSHelpKeyword(string indexName, string keywordTerm)
        {
            this.Index = indexName;
            this.Term = keywordTerm;
        }
        #endregion

        #region IComparable<MSHelpKeyword> Members
        /// <summary>
        /// Compares this instance to another instance and returns an indication of their relative values.
        /// </summary>
        /// <param name="other">A MSHelpKeyword object to compare</param>
        /// <returns>Returns -1 if this instance is less than the value, 0 if they are equal, or 1 if this
        /// instance is greater than the value or the value is null.</returns>
        /// <remarks>Entries are sorted by name and then value</remarks>
        public int CompareTo(MSHelpKeyword other)
        {
            if(other == null)
                return 1;

            int result = String.Compare(index, other.Index, StringComparison.Ordinal);

            if(result == 0)
                result = String.Compare(this.Term, other.Term, StringComparison.Ordinal);

            return result;
        }
        #endregion

        #region Equality, hash code, and To String
        //=====================================================================

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if(!(obj is MSHelpKeyword kw))
                return false;

            return this == kw || (this.Index == kw.Index && this.Term == kw.Term);
        }

        /// <summary>
        /// Get a hash code for this item
        /// </summary>
        /// <returns>Returns the hash code for the index name and term.</returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// Return a string representation of the item
        /// </summary>
        /// <returns>Returns the item in its XML format</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "<MSHelp:Keyword Index=\"{0}\" Term=\"{1}\" />",
                index, WebUtility.HtmlEncode(this.Term));
        }
        #endregion
    }
}
