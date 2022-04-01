using System.Drawing;
using API.DataAccess;
using API.Managers;
using API.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace API.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class PersonsController : ControllerBase {

		// GET: api/persons
		[HttpGet]
		public ActionResult<List<Person>> GetPersons() {
			IPersonManager personManager = new PersonManager();
			List<Person> listFound = personManager.GetAllPersons();
			if (listFound == null || listFound.Count < 1) {
				return NotFound();
			}

			return Ok(listFound.ToJson());

		}

		// GET: PersonController/Details/5
		[HttpGet]
		[Route("{personEmail}")]
		public ActionResult<Person> Details(string personEmail) {
			IPersonManager personManager = new PersonManager();
			Person personFound = personManager.GetPersonByEmail(personEmail);
			if (personFound == null) {
				return NotFound();
			}

			return Ok(personFound.ToJson());
		}

		//// GET: PersonController/Create
		//public ActionResult Create() {
		//	return View();
		//}

		//// POST: PersonController/Create
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Create(IFormCollection collection) {
		//	try {
		//		return RedirectToAction(nameof(Index));
		//	} catch {
		//		return View();
		//	}
		//}

		//// GET: PersonController/Edit/5
		//public ActionResult Edit(int id) {
		//	return View();
		//}

		//// POST: PersonController/Edit/5
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Edit(int id, IFormCollection collection) {
		//	try {
		//		return RedirectToAction(nameof(Index));
		//	} catch {
		//		return View();
		//	}
		//}

		//// GET: PersonController/Delete/5
		//public ActionResult Delete(int id) {
		//	return View();
		//}

		//// POST: PersonController/Delete/5
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Delete(int id, IFormCollection collection) {
		//	try {
		//		return RedirectToAction(nameof(Index));
		//	} catch {
		//		return View();
		//	}
		//}
	}
}

