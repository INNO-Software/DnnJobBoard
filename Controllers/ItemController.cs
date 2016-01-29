using System;
using System.Linq;
using System.Web.Mvc;
using Dnn.Modules.DnnJobBoard.Components;
using Dnn.Modules.DnnJobBoard.Models;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Entities.Users;
using DotNetNuke.Framework.JavaScriptLibraries;

namespace Dnn.Modules.DnnJobBoard.Controllers
{
    public class ItemController : DnnController
    {
        [ModuleAction(ControlKey = "Edit", TitleKey = "Add Job")]
      
        public ActionResult Index()
        {
            var items = ItemManager.Instance.GetItems(ModuleContext.ModuleId);
            return View(items);
        }
        
        public ActionResult Edit(int itemId = -1)
        {
            DotNetNuke.Framework.JavaScriptLibraries.JavaScript.RequestRegistration(CommonJs.DnnPlugins);

            var userlist = UserController.GetUsers(PortalSettings.PortalId);
            var users = from user in userlist.Cast<UserInfo>().ToList()
                        select new SelectListItem { Text = user.DisplayName, Value = user.UserID.ToString() };

            ViewBag.Users = users;

            var item = (itemId == -1)
                 ? new Item { ModuleId = ModuleContext.ModuleId }
                 : ItemManager.Instance.GetItem(itemId, ModuleContext.ModuleId);

            return View(item);
        }

        public ActionResult Details(int itemId = -1)
        {
            // register the DnnPlugins JS into the control
            DotNetNuke.Framework.JavaScriptLibraries.JavaScript.RequestRegistration(CommonJs.DnnPlugins);
            
            // get DNN users for this portal
            var userlist = UserController.GetUsers(PortalSettings.PortalId);
            // for every user in the portal populate a selectlistitem 
            var users = from user in userlist.Cast<UserInfo>().ToList()
                        select new SelectListItem { Text = user.DisplayName, Value = user.UserID.ToString() };
            // store the selectlistitem full of users in a users viewbag
            ViewBag.Users = users;

            // if the item is new then generate it, otherwise load it from the system
            var item = (itemId == -1)
                 ? new Item { ModuleId = ModuleContext.ModuleId }
                 : ItemManager.Instance.GetItem(itemId, ModuleContext.ModuleId);

            // return the item we are viewing
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(Item item)
        {
            // is this a non-existent item
            if (item.ItemId == -1 || item.ItemId == 0)
            {
                // which user added this record
                item.CreatedByUserId = User.UserID;
                // the user that modified the record is one and the same as he/she who created this record
                item.LastModifiedByUserId = User.UserID;
                // set record creation date to now
                item.CreatedOnDate = DateTime.UtcNow;
                // the modified date is the same as the creation date
                item.LastModifiedOnDate = DateTime.UtcNow;

                // submit the item for creation
                ItemManager.Instance.CreateItem(item);
            }
            else // load an existing item
            {                
                var existingItem = ItemManager.Instance.GetItem(item.ItemId, item.ModuleId);

                // the module id to allow filtering data by module instance
                existingItem.ModuleId = item.ModuleId;
                // optional in-house generated for the posting
                existingItem.PostingNumber = item.PostingNumber;
                // when was the job posted (as in, by the company, not to the database)
                existingItem.PostingDate = item.PostingDate;
                // when is the job posting closed 
                existingItem.PostingCloseDate = item.PostingCloseDate;
                // job title
                existingItem.JobTitle = item.JobTitle;
                // job summary
                existingItem.JobSummary = item.JobSummary;
                // terms of the employment such a full time vs. part time
                existingItem.EmploymentTerm = item.EmploymentTerm;
                // the geographical region where the job is located
                existingItem.Region = item.Region;
                // pay rate for the job
                existingItem.RateOfPay = item.RateOfPay;
                // benefits such as an outline of health plans or vacation terms and the like
                existingItem.Benefits = item.Benefits;
                // required qualifications for the job
                existingItem.RequiredQualifications = item.RequiredQualifications;
                // required experience for the job
                existingItem.RequiredExperience = item.RequiredExperience;
                // shift information, such as 'M-F', '4 days on 3 days off', etc.
                existingItem.ShiftInfo = item.ShiftInfo;
                // the working hours such as 'weekly rotation', 'on call', '9am-5pm;
                existingItem.WorkHours = item.WorkHours;
                // the office where the job is located
                existingItem.Office = item.Office;
                // department, if applicable, for this position
                existingItem.Department = item.Department;
                // affiliation such as a union or other form of segmentation used by the employer
                existingItem.Affiliation = item.Affiliation;
                // any additional information pertaining to the job
                existingItem.AdditionalInfo = item.AdditionalInfo;
                // number of job openings               
                existingItem.NumberOfOpenings = item.NumberOfOpenings;
                // update the last modified user id
                existingItem.LastModifiedByUserId = User.UserID;
                // update the last modified date
                existingItem.LastModifiedOnDate = DateTime.UtcNow;

                ItemManager.Instance.UpdateItem(existingItem);

                // These fields are set only on NEW records. They are listed here for reference.
                // existingItem.itemId = item.itemId;
                // existingItem.createdByUserId = User.UserID;
                // existingItem.createdOnDate = item.createdOnDate;
                // existingItem.postingDate = item.postingDate;
            }

            return RedirectToDefaultRoute();
        }

        public ActionResult Delete(int itemId)
        {
            ItemManager.Instance.DeleteItem(itemId, ModuleContext.ModuleId);
            return RedirectToDefaultRoute();
        }

    }
}
