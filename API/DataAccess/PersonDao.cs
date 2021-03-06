using API.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace API.DataAccess {
	public class PersonDao : IPersonDao {
		private readonly DbContext _dbContext;

		public PersonDao(DbContext dbContext) {
			_dbContext = dbContext;
		}

		public List<Person> GetAllPersons() {
			List<Person>? listOfPersons = null;
			string sqlQuery =
				"SELECT PersonFirstName, PersonLastName, PersonNickname, PersonEmail, PersonBirthdate FROM Person";
			using (_dbContext.Connection) {
				listOfPersons = _dbContext.Connection.Query<Person>(sqlQuery).ToList();
			}

			return listOfPersons;
		}

		public Person GetByEmail(string personEmail) {
			Person? person = null;
			string sqlQuery =
				"SELECT PersonFirstName, PersonLastName, PersonNickname, PersonEmail, PersonBirthdate FROM Person WHERE PersonEmail = @PersonEmail";
			var parameters = new { PersonEmail = personEmail.ToUpper() };
			using (_dbContext.Connection) {
				person = _dbContext.Connection.QuerySingle<Person>(sqlQuery, parameters);
			}

			return person;
		}

		public bool UpdatePerson(Person person) {
			bool restult = false;
			string sqlQuery =
				"UPDATE Person SET PersonFirstName = @PersonFirstName, PersonLastName = @PersonLastName, PersonNickname = @PersonNickname WHERE PersonEmail = @PersonEmail";
			var param = new {
				PersonFirstName = person.PersonFirstName,
				PersonLastName = person.PersonLastName,
				PersonNickname = person.PersonNickname

			};
			using (_dbContext.Connection) {
				if (_dbContext.Connection.Execute(sqlQuery, param) == 1) {
					restult = true;
				}
			}

			return restult;
		}

		public bool DeletePerson(string personEmail) {
			bool result = false;
			string sqlQuery = "DELETE FROM Person WHERE PersonEmail = @PersonEMail";
			using (_dbContext.Connection) {
				var param = new { PersonEmail = personEmail };
				if (_dbContext.Connection.Execute(sqlQuery, param) == 1) {
					result = true;
				}
			}

			return result;
		}

		//public bool CreatePersonFromForm(Person person)
		//{
		//	bool result = false;
		//	string sqlQuery = "";
		//	using (_dbContext.Connection)
		//	{
		//		var param = new
		//		{
		//			personFirstName = person.PersonFirstName,
		//			personLastName = person.PersonLastName,
		//			personNickname = person.PersonNickname,
		//			email = person.PersonEmail
		//			//birthdate = person.PersonBirthdate
		//		};
		//		if (_dbContext.Connection.Execute(sqlQuery, param) == 1)
		//		{
		//			result = true;
		//		}

		//		return result;
		//	}

		public bool CreatePerson(Person person) {
			bool result = false;
			string sqlQuery =
				"INSERT INTO Person (PersonFirstName, PersonLastName, PersonNickname, PersonEmail, PersonBirthdate) VALUES (@PersonFirstName, @PersonLastName, @PersonNickname, @PersonEmail, @PersonBirthdate)";
			using (_dbContext.Connection) {
				var param = new {
					PersonFirstName = person.PersonFirstName,
					PersonLastName = person.PersonLastName,
					PersonNickname = person.PersonNickname,
					PersonEmail = person.PersonEmail.ToUpper(),
					PersonBirthdate = person.PersonBirthdate
				};
				if (_dbContext.Connection.Execute(sqlQuery, param) == 1) {
					result = true;
				}
			}
			return result;
		}
	}
}
