using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class LeaveController : Controller
    {
        private readonly HttpSessionStateBase _session;

        public LeaveController()
        {
            _session = HttpContextFactory.GetHttpContext().Session;
        }

        public IActionResult Index()
        {
            //var leaveRequests = GetLeaveRequests();
            var leaveRequests = new LeaveRequestViewModel() { LeaveRequests = GetLeaveRequests() };
            return View(leaveRequests);
        }

        [HttpPost]
        public IActionResult SubmitLeaveRequest(LeaveRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newLeaveRequest = new LeaveRequest
                {
                    EmployeeName = model.NewLeaveRequest.EmployeeName,
                    EmployeeEmail = model.NewLeaveRequest.EmployeeEmail,
                    StartDate = model.NewLeaveRequest.StartDate,
                    EndDate = model.NewLeaveRequest.EndDate
                };

                //_leaveRequests.Add(newLeaveRequest);
            }

            // Return the list of submitted leave requests
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SubmitLeaveRequest2(LeaveRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Process the submitted form data and add to the list
                var newLeaveRequest = new LeaveRequest
                {
                    EmployeeName = model.NewLeaveRequest.EmployeeName,
                    StartDate = model.NewLeaveRequest.StartDate,
                    EndDate = model.NewLeaveRequest.EndDate
                    // Add other properties as needed
                };

                //_leaveRequests.Add(newLeaveRequest);

                List<LeaveRequest> leaveRequests = HttpContext().Session["LeaveRequests"] as List<LeaveRequest> ?? new List<LeaveRequest>();

                newLeaveRequest.Add(leaveRequest);

                // Store the updated list back in the session
                Session["LeaveRequests"] = newLeaveRequest;

                return Json(new { success = true, leaveRequests = newLeaveRequest });
            }

            // If the model state is not valid, return validation errors
            var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            return Json(new { success = false, errors = errors });
        }

        private List<LeaveRequest> GetLeaveRequests()
        {
            return new List<LeaveRequest>
            {
                new LeaveRequest { Id = 1, EmployeeName = "John Doe", StartDate = DateTime.Now.Date, EndDate = DateTime.Now.Date.AddDays(2) },
                new LeaveRequest { Id = 2, EmployeeName = "Jane Smith", StartDate = DateTime.Now.Date.AddDays(5), EndDate = DateTime.Now.Date.AddDays(7) },
            };
        }
    }
}
