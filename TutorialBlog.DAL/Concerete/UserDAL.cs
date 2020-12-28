using TutorialBlog.DAL.Abstract;
using TutorialBlog.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace TutorialBlog.DAL.Concerete
{
	public class UserDAL : IUserDAL
	{
		private readonly IMongoCollection<User> _users;
		
		public UserDAL(UserDatabaseSettings databaseSettings)
		{
			var dbClient = new MongoClient(databaseSettings.ConnectionString);
			var database = dbClient.GetDatabase(databaseSettings.DatabaseName);

			_users = database.GetCollection<User>(databaseSettings.CollectionName);
		}

		public User Add(User entity)
		{
			_users.InsertOne(entity);

			return entity;
		}

		public void Delete(string id)
		{
			throw new NotImplementedException();
		}

		public User Get(string id)
		{
			return _users.Find(user => user.Id == id).FirstOrDefault();
		}

		public List<User> GetAll()
		{
			throw new NotImplementedException();
		}

		public User Update(string id, User entity)
		{
			var result = _users.ReplaceOne<User>(item => item.Id == entity.Id, entity);

			if (!result.IsAcknowledged)
			{
				Console.WriteLine("[UserDAL] Updating the object has failed");
				return null;
			}

			return entity;
		}

		public User WriteToken(string id, string token)
		{
			User user = _users.Find<User>(userdb => userdb.Id == id).FirstOrDefault();
			user.Token = token;

			_users.ReplaceOne(item => item.Id == user.Id, user);
			return user;
		}

		public User Authenticate(User userInput)
		{
			User found = _users.Find<User>(user => ((userInput.Username == user.Username) && (userInput.Password == user.Password))).FirstOrDefault();
			return found;
		}
	}
}
