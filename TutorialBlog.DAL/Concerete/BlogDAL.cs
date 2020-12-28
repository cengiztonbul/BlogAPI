using TutorialBlog.DAL.Abstract;
using TutorialBlog.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace TutorialBlog.DAL.Concerete
{
	public class BlogDAL : IBlogDAL
	{
		private readonly IMongoCollection<Blog> _blogs;
		
		public BlogDAL(BlogDatabaseSettings databaseSettings)
		{
			var dbClient = new MongoClient(databaseSettings.ConnectionString);
			var database = dbClient.GetDatabase(databaseSettings.DatabaseName);

			_blogs = database.GetCollection<Blog>(databaseSettings.CollectionName);
		}

		public Blog Get(string id)
		{
			return _blogs.Find<Blog>(blog => blog.Id == id).FirstOrDefault();
		}

		public List<Blog> GetAll()
		{
			return _blogs.Find(book => true).ToList();
		}

		public Blog Add(Blog entity)
		{
			entity.BlogDate = DateTime.Now;
			_blogs.InsertOne(entity);

			return entity;
		}

		public void Delete(string id)
		{
			_blogs.DeleteOne(blog => blog.Id == id);
		}

		public Blog Update(string id, Blog entity)
		{
			_blogs.ReplaceOne(book => book.Id == id, entity);
			return entity;
		}
	}
}
