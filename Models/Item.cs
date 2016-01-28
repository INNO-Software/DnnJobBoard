/*
' Copyright (c) 2015 Christoc.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;

namespace Dnn.Modules.DnnJobBoard.Models
{
    [TableName("DNN_JobBoard_Items")]
    //setup the primary key for table
    //setup the primary key for table
    [PrimaryKey("ItemId", AutoIncrement = true)]
    //configure caching using PetaPoco
    [Cacheable("JobBoard", CacheItemPriority.Default, 20)]
    //scope the objects to the ModuleId of a module on a page (or copy of a module on a page)
    [Scope("ModuleId")]
    public class Item
    {
        ///<summary>
        /// The career posting row ID 
        ///</summary>
        public int ItemId { get; set; }

        ///<summary>
        /// The module instance this data is associated to
        ///</summary>
        public int ModuleId { get; set; }

        ///<summary>
        /// Used to track a proprietary (in-house) posting id
        ///</summary>
        public string PostingNumber { get; set; }

        ///<summary>
        /// The posting's open date
        ///</summary>
        public DateTime PostingDate { get; set; }

        ///<summary>
        /// The posting's closed date, if left null it is considered "Open Until Filled"
        ///</summary>
        public DateTime PostingCloseDate { get; set; }

        ///<summary>
        /// The title of the position 
        ///</summary>
        public string JobTitle { get; set; }

        ///<summary>
        /// A summary outlining the position
        ///</summary>
        public string JobSummary { get; set; }

        ///<summary>
        /// An outline of the benefits included with the position
        ///</summary>
        public string Benefits { get; set; }

        ///<summary>
        /// The terms of the employment, such as part-time, full-time, temporary, etc.
        ///</summary>
        public string EmploymentTerm { get; set; }

        ///<summary>
        /// The qualifications required for the position
        ///</summary>
        public string RequiredQualifications { get; set; }

        ///<summary>
        /// The experience required for the position
        ///</summary>
        public string RequiredExperience { get; set; }

        ///<summary>
        /// Any additional info we might want to display
        ///</summary>
        public string AdditionalInfo { get; set; }

        ///<summary>
        /// Internal affiliations such as unions, associations, etc.
        ///</summary>
        public string Affiliation { get; set; }

        ///<summary>
        /// The region the position is associated to
        ///</summary>
        public string Region { get; set; }

        ///<summary>
        ///  A specific office building the position is tied to
        ///</summary>
        public string Office { get; set; }

        ///<summary>
        /// A department that position might be associated to 
        ///</summary>
        public string Department { get; set; }

        ///<summary>
        /// Details about the shifts expected of the position
        ///</summary>
        public string ShiftInfo { get; set; }

        ///<summary>
        /// An outline of the hours expected of the position
        ///</summary>
        public string WorkHours { get; set; }

        ///<summary>
        /// Outline of the pay rate, be it hourly, salary or otherwise
        ///</summary>
        public string RateOfPay { get; set; }

        ///<summary>
        /// A string with the proprietary (in-house) posting id
        ///</summary>
        public int NumberOfOpenings { get; set; }

        ///<summary>
        /// An integer for the user id of the user who created the object
        ///</summary>
        public int CreatedByUserId { get; set; } = -1;

        ///<summary>
        /// An integer for the user id of the user who last updated the object
        ///</summary>
        public int LastModifiedByUserId { get; set; } = -1;

        ///<summary>
        /// The date the object was created
        ///</summary>
        public DateTime CreatedOnDate { get; set; } = DateTime.UtcNow;

        ///<summary>
        /// The date the object was updated
        ///</summary>
        public DateTime LastModifiedOnDate { get; set; } = DateTime.UtcNow;

    }
}
